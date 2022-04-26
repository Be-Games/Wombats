using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

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
        
        if (other.gameObject.name == "Break")
        {
            Debug.Log("Brake On");
            
            
            //slow down car to 0 
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

            //show panel
            TutPlayerController.canBreak = true;
            TutPlayerController.tapBreak_GO.SetActive(true);
        }

        if (other.gameObject.name == "CoinPanel")
        {
            TutPlayerController.coinPanel.SetActive(true);
        }
        
        if (other.gameObject.CompareTag("Coin"))
        {
            other.transform.gameObject.GetComponent<ParticleSystem>().Stop();
            other.transform.GetChild(0).GetChild(0).gameObject.GetComponent<ParticleSystem>().Play();
        }

        if (other.gameObject.name == "BoostPanel")
        {
            TutPlayerController.boostPanel.SetActive(true);
            TutPlayerController.boostBtn_GO.SetActive(true);
            
        }

        if (other.gameObject.CompareTag("Boost"))
        {
            other.transform.GetChild(0).gameObject.GetComponent<ParticleSystem>().Stop();
            other.transform.GetChild(1).gameObject.GetComponent<ParticleSystem>().Play();

            foreach (var abc in TutPlayerController.boostFiller)
            {
                DOTween.To(() => abc.fillAmount, 
                    x => abc.fillAmount = x, 1f, 0.6f);
            }
            TutPlayerController.boostBtn_GO.GetComponent<DOTweenAnimation>().DOPlay();
            
            //BREAK
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
            
            
            //ENABLE BOOST BTN
            
            TutPlayerController.boostBtn_GO.GetComponent<Button>().enabled = true;
            
            other.gameObject.SetActive(false);
        }
        
        
        if (other.gameObject.CompareTag("Collision"))
        {
            if (TutPlayerController.targetSpeed >= TutPlayerController.boostSpeed)
            {
                other.GetComponent<ClickOrTapToExplode>().DestroyStuff();
            }
        }
        
        
        if (other.gameObject.name == "FinishLine")
        {
            TutPlayerController.boostBtn_GO.SetActive(false);
            TutPlayerController.finalPanel_GO.SetActive(true);
            TutPlayerController.allConfetti.SetActive(true);
        }
        yield return new WaitForSeconds(1.2f);
        TutPlayerController.coinPanel.SetActive(false);
        
        yield return new WaitForSeconds(1.2f);
        TutPlayerController.coinPanel.SetActive(false);
        
        yield return null;
    }
    
}
