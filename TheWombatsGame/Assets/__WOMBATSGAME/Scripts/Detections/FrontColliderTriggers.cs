using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using SWS;
using UnityEngine;
using Lofelt.NiceVibrations;

public class FrontColliderTriggers : MonoBehaviour
{
    private GameObject currentPersonRagdoll;
    
    private void OnTriggerEnter(Collider other)
    {
        #region BoostTrigger
        if (other.gameObject.CompareTag("Boost"))                                                                        //Boost Trigger 
        {

            if (LevelManager.Instance.individualBoostCounter < 3)
            {
                
                LevelManager.Instance.BoostManager();
            
                other.gameObject.GetComponent<BoxCollider>().enabled = false;                                            //For the triggered pickup
                other.transform.GetChild(0).gameObject.SetActive(false);
                other.transform.GetChild(1).gameObject.GetComponent<ParticleSystem>().Play();
            
                // LevelManager.Instance._playerVehicleManager.carEffects.boostCapturedEffectPS.Play();                                  //under car effect show once


            }
            
        }
        
        #endregion
        if (other.gameObject.CompareTag("People"))
        {
            //VIBRATE ON trigger with person
            if (LevelManager.Instance._audioManager.isHapticEnabled)
                LevelManager.Instance.playerVisual.GetComponent<HapticSource>().Play();
            
            Debug.Log("Coll with People");
            other.GetComponent<Collider>().enabled = false;
            
            
            LevelManager.Instance._playerController.playerPF.speed = 0;

            LevelManager.Instance.isCrashedWithPpl = true;

            currentPersonRagdoll = other.gameObject;
            other.GetComponent<splineMove>().Stop();
            
            if (other.transform.localRotation.y >= 0)
            {
                other.transform.localRotation = new Quaternion(0,-0.6f,0f,0.6f);
         
            }

            Invoke("WaitAndRag",0.05f);
            
            StartCoroutine("CarTotalled");
            
        }
        
        if (other.gameObject.CompareTag("Collision"))
        {
            if (LevelManager.Instance._playerController.targetSpeed >=LevelManager.Instance._playerController.boostSpeed)
            {
                Debug.Log("Break");
                other.GetComponent<ClickOrTapToExplode>().DestroyStuff();
                
                // if (LevelManager.Instance._audioManager.isHapticEnabled)
                //     LevelManager.Instance.playerVisual.GetComponent<HapticSource>().Play();
                            
            }
            
            if (LevelManager.Instance._playerController.targetSpeed <=LevelManager.Instance._playerController.normalSpeed)
            {
                //VIBRATE ON trigger with coin
                 // if (LevelManager.Instance._audioManager.isHapticEnabled)
                 //     LevelManager.Instance.currentPlayerCarModel.GetComponent<HapticSource>().Play();        
                 
                 
                LevelManager.Instance._playerController.playerPF.speed = 0;
                //LevelManager.Instance._playerController.playerPF.enabled = false;

                other.gameObject.GetComponent<Collider>().enabled = false;
                            
                StartCoroutine("CarTotalled");
            
            
            }
        }

        if (other.gameObject.CompareTag("AlmostEnd"))
        {
            if (LevelManager.Instance.lapCounter == LevelManager.Instance.totalLaps)
            {
                Debug.Log("End Reached");
            }
        }

        if (other.gameObject.CompareTag("Coin"))
        {
            Debug.Log("Coin ");
            
            LevelManager.Instance.currentScore++;
            UIManager.Instance.scoreText.text = LevelManager.Instance.currentScore.ToString();
            //other.gameObject.GetComponent<BoxCollider>().enabled = false;                                            //For the triggered pickup
            other.transform.gameObject.GetComponent<ParticleSystem>().Stop();
            other.transform.GetChild(0).GetChild(0).gameObject.GetComponent<ParticleSystem>().Play();
            
            //VIBRATE ON trigger with coin
            // if (LevelManager.Instance._audioManager.isHapticEnabled)
            //     LevelManager.Instance.currentPlayerCarModel.GetComponent<HapticSource>().Play();
        }
    }

    
    IEnumerator CarTotalled()
    {
        //LevelManager.Instance._playerController.playerPF.enabled = false;
       
        //VIBRATE ON CRASH PRESSED
        // if (LevelManager.Instance._audioManager.isHapticEnabled)
        //     LevelManager.Instance.playerVisual.GetComponent<HapticSource>().Play();
        
        
        if(LevelManager.Instance.continueCounter != 5)
            UIManager.Instance.crashedPanel.SetActive(true);

        if (LevelManager.Instance.continueCounter == 5)
        {
            UIManager.Instance.postAdCrashPanel.SetActive(true);
            LevelManager.Instance.isGameStarted = false;
        }
            

        if (!LevelManager.Instance.isCrashedWithPpl)
        {
            LevelManager.Instance._playerVehicleManager.postCrashStuff.up_car.SetActive(false);        //player model+effects
        
            LevelManager.Instance._playerVehicleManager.postCrashStuff.down_car.SetActive(true);        //player upside down model
        
            LevelManager.Instance.isCrashed = true;
            LevelManager.Instance._playerVehicleManager.postCrashStuff.crashPS.gameObject.SetActive(true);     //explosion effect
            
            // foreach (GameObject x in LevelManager.Instance.pplToDisable)                                        //DISABLE PPL WHEN CRASHED
            // {
            //     if(x != null)
            //         x.SetActive(false);
            // }
        }

        LevelManager.Instance.isCrashedWithPpl = false;
       
        
        
        LevelManager.Instance.carContinueChances.text = "" + (2 - LevelManager.Instance.continueCounter);
            
       // LevelManager.Instance.isGameStarted = false;
        GameManager.Instance.canControlCar = false;
        LevelManager.Instance._gameControls.gestureState = GameControls.GestureState.Break;
        LevelManager.Instance.FastWind.gameObject.SetActive(false);
        LevelManager.Instance.slowWind.gameObject.SetActive(false);
        
        yield return new WaitForSeconds(4f);

        //carRollOverAnimator.enabled = false;
        LevelManager.Instance._playerVehicleManager.postCrashStuff.crashPS.gameObject.SetActive(false);       //explosion effect
        
        

    }

    void WaitAndRag()
    {
        currentPersonRagdoll.GetComponent<Animator>().SetBool("isDead",true);
    }
    
    

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("FinishLine") && !LevelManager.Instance.isLapTriggered)
        {
            StartCoroutine("LapSystemDelay");
           
        }

        if (other.gameObject.CompareTag("FirstTrigger") && LevelManager.Instance.isFinalLap)
        {
            Instantiate(LevelManager.Instance.stadiumPrefab, LevelManager.Instance.stadiumTransform.position,
                LevelManager.Instance.stadiumTransform.rotation);
            foreach (var x in LevelManager.Instance.stuffToRemove)
            {
                x.SetActive(false);
            }
            
            //Video Replay
            //LevelManager.Instance.ReplayKitDemo.Initialise();
        }
        
        if (other.gameObject.CompareTag("SecondTrigger") && LevelManager.Instance.isFinalLap)
        {
            //Record Video
            //LevelManager.Instance.ReplayKitDemo.SetMicrophoneStatus();
           // LevelManager.Instance.ReplayKitDemo.PrepareRecording();
           // LevelManager.Instance.ReplayKitDemo.StartRecording();
            
        }
    }

    IEnumerator LapSystemDelay()
    {
        LevelManager.Instance.isLapTriggered = true;
        LevelManager.Instance.LapManager();
        yield return new WaitForSeconds(2f);
        LevelManager.Instance.isLapTriggered = false;
    }
    
    
}
