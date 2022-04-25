using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using DG.Tweening;
using UnityEngine;

public class Tut_CarTriggers : MonoBehaviour
{
    public Tut_PlayerController TutPlayerController;

   
    
    
    
    private IEnumerator OnTriggerEnter(Collider other)
    {
        
        if (other.gameObject.name == "MoveLeft")
        {
            Debug.Log("Swipe Left Trigger On");
            
            //Decelerate
            
            DOTween.To(() => TutPlayerController.defCMVCCam.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset.z,                     ////damping camera effect
                    x => TutPlayerController.defCMVCCam.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset.z = x, -1.69f, 0.5f)
                .OnUpdate(() => {
              
                });
            
            
            while (true)
            {
                float target = 0;                                                                                
                float delta = TutPlayerController.playerPF.speed - target;    
                delta *= Time.deltaTime * TutPlayerController.Dec;
                    
                if (TutPlayerController.playerPF.speed <= 1)
                {
                    TutPlayerController.playerPF.speed = 0;
                    TutPlayerController.gamecontrols.gestureState = Tut_Gamecontrols.GestureState.Break;
                    break;
                }
                else
                {
                    TutPlayerController.playerPF.speed -= delta;
                }
            }

            TutPlayerController.canMoveLeft = true;
           TutPlayerController.moveLeft_GO.SetActive(true);
        }
        
        if (other.gameObject.name == "MoveRight")
        {
            Debug.Log("Swipe Right Trigger On");
            
            //Decelerate
            
            DOTween.To(() => TutPlayerController.defCMVCCam.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset.z,                     ////damping camera effect
                  x => TutPlayerController.defCMVCCam.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset.z = x, -1.69f, 0.5f)
              .OnUpdate(() => {
              
              });
            
            while (true)
            {
                float target = 0;                                                                                
                float delta = TutPlayerController.playerPF.speed - target;    
                delta *= Time.deltaTime * TutPlayerController.Dec;
                    
                if (TutPlayerController.playerPF.speed <= 1)
                {
                    TutPlayerController.playerPF.speed = 0;
                    TutPlayerController.gamecontrols.gestureState = Tut_Gamecontrols.GestureState.Break;
                    break;
                }
                else
                {
                    TutPlayerController.playerPF.speed -= delta;
                }
            }

            TutPlayerController.canMoveRight = true;
            TutPlayerController.moveRight_GO.SetActive(true);
        }
        
        yield return null;
    }
    
}
