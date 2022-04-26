using System.Collections;
using DG.Tweening;
using PathCreation.Examples;
using UnityEngine;

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
        Dec = LevelManager.Instance._playerVehicleManager.carSpeedSettings.Dec;
        normalSpeed = LevelManager.Instance._playerVehicleManager.carSpeedSettings.normalSpeed;
        boostSpeed = LevelManager.Instance._playerVehicleManager.carSpeedSettings.boostSpeed;
        PlayercarVisual = LevelManager.Instance.playerVisual;
        
        
    }

    IEnumerator CarInitialPush()
    {
        targetSpeed = normalSpeed;
        
        playerPF.speed = 0.5f;
        PlayercarVisual.transform.localPosition = new Vector3(PlayercarVisual.transform.localPosition.x,0.02f,0f);
        yield return new WaitForSeconds(0.5f);
        playerPF.speed = 0;
        
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
                      // DOTween.To(() => LevelManager.Instance.cmvc.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset.z,                     ////damping camera effect
                      //       x => LevelManager.Instance.cmvc.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset.z = x, -1.3f, 0.5f)
                      //   .OnUpdate(() => {
                      //   
                      //   });
                    
                    // DOTween.To(() => LevelManager.Instance.cmvc.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset.y,                     ////damping camera effect
                    //         x => LevelManager.Instance.cmvc.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset.y = x, 0.8f, 0.5f)
                    //     .OnUpdate(() => {
                    //     
                    //     });
                    
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
                    
                    // LevelManager.Instance._playerVehicleManager.carEffects.carBreakGO.transform.GetChild(0).GetComponent<ParticleSystem>().Stop();
                    // LevelManager.Instance._playerVehicleManager.carEffects.carBreakGO.transform.GetChild(1).GetComponent<ParticleSystem>().Stop();  
                    
                    }
                    
                    
                    
                }

                if (LevelManager.Instance._gameControls.gestureState == GameControls.GestureState.Release)                            //Release Breaks
                {
                    if (!LevelManager.Instance.isBoosting)
                    {
                        // DOTween.To(() => LevelManager.Instance.cmvc.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset.z,                     ////damping camera effect
                        //         x => LevelManager.Instance.cmvc.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset.z = x, -1.75f, 0.5f)
                        //     .OnUpdate(() => {
                        //
                        //     });
                    
                        // DOTween.To(() => LevelManager.Instance.cmvc.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset.y,                     ////damping camera effect
                        //         x => LevelManager.Instance.cmvc.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset.y = x, 1.2f, 0.5f)
                        //     .OnUpdate(() => {
                        //     
                        //     });
                    
                        //WINDS ON
                        LevelManager.Instance.FastWind.gameObject.SetActive(true);
                    
                        //Accelerate
                        float target = targetSpeed;
                        float delta = target - playerPF.speed;
                        delta *= Time.deltaTime * Acc;
                        playerPF.speed += delta;
                    
                        //Other Effects
                        LevelManager.Instance._playerVehicleManager.carEffects.breakLight.SetActive(false);        //CAR LIGHTS + TIRES SMOKES

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
        //COUNTDOWNTIMER SOUND
       // if(LevelManager.Instance._audioManager && LevelManager.Instance._audioManager.isSFXenabled)
           // LevelManager.Instance._audioManager.Play(LevelManager.Instance._audioManager.sfxAll.switchLaneSound);
        
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
        //COUNTDOWNTIMER SOUND
        //if(LevelManager.Instance._audioManager && LevelManager.Instance._audioManager.isSFXenabled)
           // LevelManager.Instance._audioManager.Play(LevelManager.Instance._audioManager.sfxAll.switchLaneSound);
        
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
