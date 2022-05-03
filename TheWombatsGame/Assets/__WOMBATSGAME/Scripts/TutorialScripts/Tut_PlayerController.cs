using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Cinemachine;
using DG.Tweening;
using PathCreation.Examples;
using UnityEngine;
using UnityEngine.UI;

public class Tut_PlayerController : MonoBehaviour
{
    [Header("Player Car Stuff")]
    public PathFollower playerPF;
    public GameObject PlayercarVisual;
    public Tut_Gamecontrols gamecontrols;

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

    public bool canMoveLeft, canMoveRight, canBreak, canBoost;
    
    public GameObject fullPanel;
    public GameObject moveLeft_GO,moveRight_GO,tapBreak_GO,coinPanel,boostPanel,finalPanel_GO;
    public GameObject getready_Panel, getreadyText, goText;

    public CinemachineVirtualCamera defCMVCCam;

    public GameObject slowWinds, fastWinds, nosEffect1, nosEffect2;
    public MeshRenderer backLightMat;
    public Material whiteMat, redMat;
    public DOTweenAnimation barrierDO;

    public GameObject boostBtn_GO;
    public Image[] boostFiller;

    public GameObject allConfetti;
    
    
    
     private void Start()
    {
        variablesInitilization();
        StartCoroutine(CarInitialPush());
        
    }

    void variablesInitilization()
    {
        movementDuration = 0.2f;
        rotationDuration = 0.1f;
        
    }

    IEnumerator CarInitialPush()
    {
       
        getready_Panel.SetActive(true);
        getreadyText.SetActive(true);
        goText.SetActive(false);
        
        targetSpeed = 2f;
        PlayercarVisual.transform.localPosition = new Vector3(PlayercarVisual.transform.localPosition.x,0.02f,0f);
        yield return new WaitForSeconds(0.5f);
        canBreak = true;
        gamecontrols.gestureState = Tut_Gamecontrols.GestureState.Break;
        yield return new WaitForSeconds(3f);
        
        getreadyText.SetActive(false);
        goText.SetActive(true);
        
        yield return new WaitForSeconds(0.4f);

        canBreak = false;
        gamecontrols.gestureState = Tut_Gamecontrols.GestureState.Release;
        targetSpeed = normalSpeed;
        
        yield return new WaitForSeconds(1f);
        getready_Panel.SetActive(false);
    }

    private void Update()
    {
        if (gamecontrols.gestureState == Tut_Gamecontrols.GestureState.Break)
        {
            slowWinds.SetActive(true);
            
        }
        else
        {
            slowWinds.SetActive(false);
            
        }
        
        if (gamecontrols.gestureState == Tut_Gamecontrols.GestureState.Release)
        {
            fastWinds.SetActive(true);
        }
        else
        {
            fastWinds.SetActive(false);
        }
        
        if (gamecontrols.gestureState == Tut_Gamecontrols.GestureState.Break && canBreak)                             // Apply Breaks
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
            //LevelManager.Instance.FastWind.Stop();
                    
            //Decelerate
            // float target = 0;                                                                                
            // float delta = playerPF.speed - target;    
            // delta *= Time.deltaTime * Dec;
            //
            // if (playerPF.speed <= 1)
            // {
            //     playerPF.speed = 0;
            // }
            // else
            // {
            //     playerPF.speed -= delta;
            // }
                    
            //Other Effects
            //LevelManager.Instance._playerVehicleManager.carEffects.breakLight.SetActive(true);        //CAR LIGHTS + TIRES SMOKES
                    
            //LevelManager.Instance._playerVehicleManager.carEffects.carBreakGO.transform.GetChild(0).GetComponent<ParticleSystem>().Stop();
            //LevelManager.Instance._playerVehicleManager.carEffects.carBreakGO.transform.GetChild(1).GetComponent<ParticleSystem>().Stop();  
                    
        }

        if ((gamecontrols.gestureState == Tut_Gamecontrols.GestureState.Release))                            //Release Breaks
        {
                   
            DOTween.To(() => defCMVCCam.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset.z,                     ////damping camera effect
                    x => defCMVCCam.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset.z = x, -2.11f, 0.5f)
                .OnUpdate(() => {
              
                });


                    
            //WINDS ON
            // LevelManager.Instance.FastWind.Play();
                    
            //Accelerate
            float target = targetSpeed;
            float delta = target - playerPF.speed;
            delta *= Time.deltaTime * Acc;
            playerPF.speed += delta;
                    
            //Other Effects
            // LevelManager.Instance._playerVehicleManager.carEffects.breakLight.SetActive(false);        //CAR LIGHTS + TIRES SMOKES

            // LevelManager.Instance._playerVehicleManager.carEffects.carBreakGO.transform.GetChild(0).GetComponent<ParticleSystem>().Play();
            // LevelManager.Instance._playerVehicleManager.carEffects.carBreakGO.transform.GetChild(1).GetComponent<ParticleSystem>().Play();
                    

            //BRAKE SOUND PLAY
            //AudioManager.Instance.Play(AudioManager.Instance.sfxAll.brakeSound); 
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
                
                
                DOTween.To(() => defCMVCCam.gameObject.GetComponent<CinemachineCameraOffset>().m_Offset.x, 
                        x => defCMVCCam.gameObject.GetComponent<CinemachineCameraOffset>().m_Offset.x = x, -cameraOffsetxOffset, 0.3f)
                    .OnUpdate(() => {
                        
                    });
                
                
                currentPosition = -1;
                break;
            
            case 1:
                PlayercarVisual.transform.DOLocalMove(centreCarTransform.localPosition, movementDuration);
                
                PlayercarVisual.transform.DOLocalRotate(new Vector3(0f,-16.83f,0f), rotationDuration)
                    .OnComplete(()=> PlayercarVisual.transform.DOLocalRotate(new Vector3(0f,0f,0f), rotationDuration));
                
                DOTween.To(() => defCMVCCam.gameObject.GetComponent<CinemachineCameraOffset>().m_Offset.x, 
                        x => defCMVCCam.gameObject.GetComponent<CinemachineCameraOffset>().m_Offset.x = x, 0, 0.3f)
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
            // case 0:
            //     PlayercarVisual.transform.DOLocalMove(rightCarTransform.localPosition, movementDuration);
            //     
            //     PlayercarVisual.transform.DOLocalRotate(new Vector3(0f,16.83f,0f), rotationDuration)
            //         .OnComplete(()=> PlayercarVisual.transform.DOLocalRotate(new Vector3(0f,0f,0f), rotationDuration));
            //     
            //     DOTween.To(() => LevelManager.Instance.defCMVCCam.gameObject.GetComponent<CinemachineCameraOffset>().m_Offset.x, 
            //             x => LevelManager.Instance.defCMVCCam.gameObject.GetComponent<CinemachineCameraOffset>().m_Offset.x = x, cameraOffsetxOffset, 0.3f)
            //         .OnUpdate(() => {
            //             
            //         });
            //     
            //     currentPosition = 1;
            //     break;
            case -1:
                PlayercarVisual.transform.DOLocalMove(centreCarTransform.localPosition, movementDuration);
                
                PlayercarVisual.transform.DOLocalRotate(new Vector3(0f,16.83f,0f), rotationDuration)
                    .OnComplete(()=> PlayercarVisual.transform.DOLocalRotate(new Vector3(0f,0f,0f), rotationDuration));
                
                DOTween.To(() =>defCMVCCam.gameObject.GetComponent<CinemachineCameraOffset>().m_Offset.x, 
                        x => defCMVCCam.gameObject.GetComponent<CinemachineCameraOffset>().m_Offset.x = x, 0, 0.3f)
                    .OnUpdate(() => {
                        
                    });
                
                currentPosition = 0;
                break;
        }
        
    }
    
    public void BoostCarButton()
    {
        StartCoroutine("BoostCarSettings");
    }

    
    IEnumerator BoostCarSettings()
    {
       gamecontrols.gestureState = Tut_Gamecontrols.GestureState.Release;
       boostPanel.SetActive(false); 
       
         targetSpeed = boostSpeed;
         PlayercarVisual.GetComponent<VehicleManager>().carEffects.NOSEffectsPS.gameObject.SetActive(true);//under car effect show once

        DOTween.To(() => defCMVCCam.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset.z,                     ////damping camera effect
                x => defCMVCCam.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset.z = x, -3f, 0.5f)
            .OnUpdate(() => {
                        
            });
        
        
        nosEffect1.SetActive(true);
        nosEffect2.SetActive(true);


        boostBtn_GO.GetComponent<DOTweenAnimation>().DOPause();
        boostBtn_GO.transform.DOScale(new Vector3(0.5f,0.5f,0.5f), 0f);
        
      
        boostBtn_GO.GetComponent<Button>().enabled = false;
        
        
            //Boost Image back to original
        foreach (var abc in boostFiller)                    
        {
            DOTween.To(() => abc.fillAmount, 
                    x => abc.fillAmount = x, 0f, 6.4f)
                .OnUpdate(() => {
                        
                });
        }
        
        //WHEN BOOST IS DONE
        yield return new WaitForSeconds(3f);

        Debug.Log("DONE");
        
        targetSpeed = normalSpeed;
        PlayercarVisual.GetComponent<VehicleManager>().carEffects.NOSEffectsPS.gameObject.SetActive(false);//under car effect show once

        DOTween.To(() => defCMVCCam.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset.z,                     ////damping camera effect
                x => defCMVCCam.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset.z = x, -2f, 0.5f)
            .OnUpdate(() => {
                        
            });
        
        
        nosEffect1.SetActive(false);
        nosEffect2.SetActive(false);

        
    }

    public void LoadScene()
    {
        GameManager.Instance.LoadScene("LevelSelection");
    }

    #endregion
    
}
