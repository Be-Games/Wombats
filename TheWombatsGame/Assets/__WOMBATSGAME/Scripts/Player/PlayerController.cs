using System.Collections;
using DG.Tweening;
using PathCreation.Examples;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{

    [Header("Player Car Stuff")]
    public PathFollower playerPF;
    public GameObject PlayercarVisual;        //CAR MODEL GO

    [Header("Selected Car Settings")]
    public float Acc;
    public float Dec;
    public float targetSpeed;
    public float normalSpeed;
    public float boostSpeed;

    public GameObject[] wheels;

    [Header("Car Movement Variables")]
    [SerializeField]
    private Transform leftCarTransform;
    [SerializeField] 
    private Transform rightCarTransform;
    [SerializeField] 
    private Transform centreCarTransform;
    [SerializeField]
    public int currentPosition;
    public float cameraOffsetxOffset;
    public float movementDuration,rotationDuration;
    
    
    private void Start()
    {
        variablesInitilization();
        StartCoroutine(CarInitialPush());
        
        
    }

    void variablesInitilization()
    {
        movementDuration = 0.2f;
        rotationDuration = 0.1f;
        Acc = LevelManager.Instance._playerVehicleManager.carSpeedSettings.Acc;
        Dec = 6;
        normalSpeed = LevelManager.Instance._playerVehicleManager.carSpeedSettings.normalSpeed;
        boostSpeed = LevelManager.Instance._playerVehicleManager.carSpeedSettings.boostSpeed;
        PlayercarVisual = LevelManager.Instance.playerVisual;

        if (SceneManager.GetActiveScene().name == "LIVERPOOL")
        {
            normalSpeed = normalSpeed - 2;
            boostSpeed = boostSpeed - 4;
        }

        wheels = LevelManager.Instance._playerVehicleManager.bodyTrigger.wheels;

    }

    IEnumerator CarInitialPush()
    {
        targetSpeed = normalSpeed;
        
        playerPF.speed = 0.5f;
        PlayercarVisual.transform.localPosition = new Vector3(PlayercarVisual.transform.localPosition.x,0.02f,0f);
        yield return new WaitForSeconds(0.5f);
        playerPF.speed = 0;
        
        LevelManager.Instance.backLightMaterial.EnableKeyword("_EMISSION");
        LevelManager.Instance.backLightMaterial.SetColor("_EmissionColor", LevelManager.Instance.whiteBL);
        
    }

    private void Update()
    {
        
        if (LevelManager.Instance.isGameStarted)
        {
            if (GameManager.Instance.canControlCar)
            {
                if (LevelManager.Instance._gameControls.gestureState == GameControls.GestureState.Break)                                // Apply Breaks
                {
                    if (!LevelManager.Instance.isBoosting)
                    {

                        /*foreach (var wheel in wheels)
                        {
                            wheel.transform.DOLocalRotate(new Vector3(0f, 0f, 0f), 0.1f, RotateMode.FastBeyond360)
                                .SetEase(Ease.Flash).SetLoops(-1, LoopType.Incremental);
                        }*/
                        
                        LevelManager.Instance.SetCameraDampValue(LevelManager.Instance.breakValue);
                    
                        //winds off
                        LevelManager.Instance.FastWind.gameObject.SetActive(false);
                    
                    //Decelerate
                    float target = 0;                                                                                
                    float delta = playerPF.speed - target;    
                    delta *= Time.deltaTime * Dec;
                    
                    if (playerPF.speed <= 1)
                    {
                        playerPF.speed = 0;
                    }
                    else
                    {
                        playerPF.speed -= delta;
                    }
                    
                    //Other Effects
                    LevelManager.Instance._playerVehicleManager.carEffects.breakLight.SetActive(true);        //CAR LIGHTS + TIRES SMOKES
                    LevelManager.Instance.backLightMaterial.EnableKeyword("_EMISSION");
                    LevelManager.Instance.backLightMaterial.SetColor("_EmissionColor", LevelManager.Instance.redBL);
                    
                    #region CarSmokeStop
                    LevelManager.Instance._playerVehicleManager.carEffects.carBreakSmokeL.GetComponent<ParticleSystem>().Stop();
                    LevelManager.Instance._playerVehicleManager.carEffects.carBreakSmokeR.GetComponent<ParticleSystem>().Stop();
                    #endregion
                    
                    
                    
                    
                    }
                    
                    
                    
                }

                if (LevelManager.Instance._gameControls.gestureState == GameControls.GestureState.Release)                            //Release Breaks
                {
                    if (!LevelManager.Instance.isBoosting)
                    {
                        /*foreach (var wheel in wheels)
                        {
                            wheel.transform.DORotate(new Vector3(-100f, 0f, 0f), 0.1f, RotateMode.FastBeyond360)
                                .SetEase(Ease.Flash).SetLoops(-1, LoopType.Incremental);
                        }*/
                        
                       LevelManager.Instance.SetCameraDampValue(LevelManager.Instance.defaultValue);
                    
                        //WINDS ON
                        LevelManager.Instance.FastWind.gameObject.SetActive(true);
                    
                        //Accelerate
                        float target = targetSpeed;
                        float delta = target - playerPF.speed;
                        delta *= Time.deltaTime * Acc;
                        playerPF.speed += delta;
                    
                        //Other Effects
                        LevelManager.Instance._playerVehicleManager.carEffects.breakLight.SetActive(false);        //CAR LIGHTS + TIRES SMOKES
                        LevelManager.Instance.backLightMaterial.EnableKeyword("_EMISSION");
                        LevelManager.Instance.backLightMaterial.SetColor("_EmissionColor", LevelManager.Instance.whiteBL);
                        #region CarSmokePlay
                        LevelManager.Instance._playerVehicleManager.carEffects.carBreakSmokeL.GetComponent<ParticleSystem>().Play();
                        LevelManager.Instance._playerVehicleManager.carEffects.carBreakSmokeR.GetComponent<ParticleSystem>().Play();
                        #endregion

                        // LevelManager.Instance._playerVehicleManager.carEffects.carBreakGO.transform.GetChild(0).GetComponent<ParticleSystem>().Play();
                        // LevelManager.Instance._playerVehicleManager.carEffects.carBreakGO.transform.GetChild(1).GetComponent<ParticleSystem>().Play();
                    

                        //BRAKE SOUND PLAY
                        //AudioManager.Instance.Play(AudioManager.Instance.sfxAll.brakeSound); 
                    }
                    else
                    {
                        //WINDS ON
                        LevelManager.Instance.FastWind.gameObject.SetActive(false);
                    
                        //Accelerate
                        float target = targetSpeed;
                        float delta = target - playerPF.speed;
                        delta *= Time.deltaTime * Acc;
                        playerPF.speed += delta;
                        
                        //Other Effects
                        LevelManager.Instance._playerVehicleManager.carEffects.breakLight.SetActive(false);        //CAR LIGHTS + TIRES SMOKES
                        LevelManager.Instance.backLightMaterial.EnableKeyword("_EMISSION");
                        LevelManager.Instance.backLightMaterial.SetColor("_EmissionColor", Color.white);
                    }
                    
                }
                
            }

        }
        
        else if(!LevelManager.Instance.isGameStarted)
        {
            playerPF.speed = 0;
        }

    }

    
    #region PlayerGestureMovements
    
    public void MoveLeft()
    {
        //laneswitch SOUND
        if(LevelManager.Instance._audioManager!=null && LevelManager.Instance._audioManager.isSFXenabled)
            LevelManager.Instance._audioManager.Play(LevelManager.Instance._audioManager.sfxAll.laneSwitch);
        
        switch (currentPosition)
        {
            case 0:
                PlayercarVisual.transform.DOLocalMove(leftCarTransform.localPosition, movementDuration);
                
                PlayercarVisual.transform.DOLocalRotate(new Vector3(0f,-16.83f,0f), rotationDuration)
                    .OnComplete(()=> PlayercarVisual.transform.DOLocalRotate(new Vector3(0f,0f,0f), rotationDuration));
                
                
                DOTween.To(() => LevelManager.Instance.defCMVCCam.gameObject.GetComponent<CinemachineCameraOffset>().m_Offset.x, 
                        x => LevelManager.Instance.defCMVCCam.gameObject.GetComponent<CinemachineCameraOffset>().m_Offset.x = x, -cameraOffsetxOffset, 0.3f)
                    .OnUpdate(() => {
                        
                    });
                
                
                currentPosition = -1;
                break;
            
            case 1:
                PlayercarVisual.transform.DOLocalMove(centreCarTransform.localPosition, movementDuration);
                
                PlayercarVisual.transform.DOLocalRotate(new Vector3(0f,-16.83f,0f), rotationDuration)
                    .OnComplete(()=> PlayercarVisual.transform.DOLocalRotate(new Vector3(0f,0f,0f), rotationDuration));
                
                DOTween.To(() => LevelManager.Instance.defCMVCCam.gameObject.GetComponent<CinemachineCameraOffset>().m_Offset.x, 
                        x => LevelManager.Instance.defCMVCCam.gameObject.GetComponent<CinemachineCameraOffset>().m_Offset.x = x, 0, 0.3f)
                    .OnUpdate(() => {
                        
                    });
                
                currentPosition = 0;
                break;
        }
        
    }
    
    public void MoveRight()
    {
        //laneswitch SOUND
        if(LevelManager.Instance._audioManager!=null && LevelManager.Instance._audioManager.isSFXenabled)
            LevelManager.Instance._audioManager.Play(LevelManager.Instance._audioManager.sfxAll.laneSwitch);
        
        switch (currentPosition)
        {
            case 0:
                PlayercarVisual.transform.DOLocalMove(rightCarTransform.localPosition, movementDuration);
                
                PlayercarVisual.transform.DOLocalRotate(new Vector3(0f,16.83f,0f), rotationDuration)
                    .OnComplete(()=> PlayercarVisual.transform.DOLocalRotate(new Vector3(0f,0f,0f), rotationDuration));
                
                DOTween.To(() => LevelManager.Instance.defCMVCCam.gameObject.GetComponent<CinemachineCameraOffset>().m_Offset.x, 
                        x => LevelManager.Instance.defCMVCCam.gameObject.GetComponent<CinemachineCameraOffset>().m_Offset.x = x, cameraOffsetxOffset, 0.3f)
                    .OnUpdate(() => {
                        
                    });
                
                currentPosition = 1;
                break;
            case -1:
                PlayercarVisual.transform.DOLocalMove(centreCarTransform.localPosition, movementDuration);
                
                PlayercarVisual.transform.DOLocalRotate(new Vector3(0f,16.83f,0f), rotationDuration)
                    .OnComplete(()=> PlayercarVisual.transform.DOLocalRotate(new Vector3(0f,0f,0f), rotationDuration));
                
                DOTween.To(() => LevelManager.Instance.defCMVCCam.gameObject.GetComponent<CinemachineCameraOffset>().m_Offset.x, 
                        x => LevelManager.Instance.defCMVCCam.gameObject.GetComponent<CinemachineCameraOffset>().m_Offset.x = x, 0, 0.3f)
                    .OnUpdate(() => {
                        
                    });
                
                currentPosition = 0;
                break;
        }
        
    }
    
    

    #endregion
    
}
