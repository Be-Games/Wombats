using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using SWS;
using UnityEngine;
using Lofelt.NiceVibrations;
using UnityEngine.Analytics;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FrontColliderTriggers : MonoBehaviour
{
    private GameObject currentPersonRagdoll;

    private void Start()
    {
        LevelManager.Instance._uiManager.redCrashedPanel.SetActive(false);
    }

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
            // if (LevelManager.Instance._audioManager.isHapticEnabled)
            //     LevelManager.Instance.playerVisual.GetComponent<HapticSource>().Play();
            
            other.GetComponent<Collider>().enabled = false;
            LevelManager.Instance.isCrashedWithPpl = true;

            currentPersonRagdoll = other.gameObject;
            other.GetComponent<splineMove>().Stop();
            
            if (other.transform.localRotation.y >= 0)
            {
                other.transform.localRotation = new Quaternion(0,-0.6f,0f,0.6f);
         
            }

            Invoke("WaitAndRag",0.05f);
            StartCoroutine(CarTotalled());
            
        }
        
        if (other.gameObject.CompareTag("Collision"))
        {
            //BOOSTING
            if (LevelManager.Instance._playerController.targetSpeed >=LevelManager.Instance._playerController.boostSpeed)
            {
                other.GetComponent<ClickOrTapToExplode>().DestroyStuff();
                
                // if (LevelManager.Instance._audioManager.isHapticEnabled)
                //     LevelManager.Instance.playerVisual.GetComponent<HapticSource>().Play();
                            
            }
            
            //NOT BOOSTING
            if (LevelManager.Instance._playerController.targetSpeed <=LevelManager.Instance._playerController.normalSpeed)
            {
                
                other.gameObject.GetComponent<Collider>().enabled = false;
                StartCoroutine(CarTotalled());
                
                // if (LevelManager.Instance._audioManager.isHapticEnabled)
                 //     LevelManager.Instance.currentPlayerCarModel.GetComponent<HapticSource>().Play();        
             
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
            LevelManager.Instance.currentScore++;
            UIManager.Instance.scoreText.text = LevelManager.Instance.currentScore.ToString();
            //other.gameObject.GetComponent<BoxCollider>().enabled = false;                                            //For the triggered pickup
            other.transform.gameObject.GetComponent<ParticleSystem>().Stop();
            other.transform.GetChild(0).GetChild(0).gameObject.GetComponent<ParticleSystem>().Play();
            
            //VIBRATE ON trigger with coin
            // if (LevelManager.Instance._audioManager.isHapticEnabled)
            //     LevelManager.Instance.currentPlayerCarModel.GetComponent<HapticSource>().Play();
        }

        if (other.gameObject.CompareTag("TowerFallingTrigger"))
        {
            TowerFallAnimation();
        }
    }

    void TowerFallAnimation()
    {
        LevelManager.Instance.rubixTower.transform.DOLocalRotate(new Vector3(0f, 0f, 54.781f), 1f).SetEase(Ease.Flash);
    }

    
    IEnumerator CarTotalled()
    {
        
        //VIBRATE ON CRASH PRESSED
        // if (LevelManager.Instance._audioManager.isHapticEnabled)
        //     LevelManager.Instance.playerVisual.GetComponent<HapticSource>().Play();
        
        //Pause the music
        LevelManager.Instance._audioManager.musicTracks.MusicTrackAudioSource.Pause();

        LevelManager.Instance.isGameStarted = false;
        LevelManager.Instance.isCrashed = true;
        LevelManager.Instance._playerController.playerPF.speed = 0;

        //red crash panel appear
        LevelManager.Instance._uiManager.redCrashedPanel.SetActive(true);
        LevelManager.Instance._uiManager.redCrashedPanel.GetComponent<Image>().DOFade(1f, 0.5f).SetEase(Ease.Flash);

        if(LevelManager.Instance.continueCounter != 5)
            UIManager.Instance.crashedPanel.SetActive(true);

        if (LevelManager.Instance.continueCounter == 5)
        {
            UIManager.Instance.postAdCrashPanel.SetActive(true);
            LevelManager.Instance.isGameStarted = false;
            
            //Level Lose!
            if (GameManager.Instance.lightingMode == 1)
                Analytics.CustomEvent("LevelLose" + SceneManager.GetActiveScene().name + " DAY ");
            if (GameManager.Instance.lightingMode == 2)
                Analytics.CustomEvent("LevelLose" + SceneManager.GetActiveScene().name + " NIGHT ");
        }
        
       
        
        if (!LevelManager.Instance.isCrashedWithPpl)
        {
            LevelManager.Instance._playerVehicleManager.postCrashStuff.up_car.SetActive(false);        //player model+effects
        
            LevelManager.Instance._playerVehicleManager.postCrashStuff.down_car.SetActive(true);        //player upside down model
        
            LevelManager.Instance.isCrashed = true;
            LevelManager.Instance._playerVehicleManager.postCrashStuff.crashPS.gameObject.SetActive(true);     //explosion effect
            
        }
        
        LevelManager.Instance.isCrashedWithPpl = false;
        LevelManager.Instance.carContinueChances.text = "" + (2 - LevelManager.Instance.continueCounter); 
        GameManager.Instance.canControlCar = false;
        LevelManager.Instance._gameControls.gestureState = GameControls.GestureState.Break;
        LevelManager.Instance.FastWind.gameObject.SetActive(false);
        LevelManager.Instance.slowWind.gameObject.SetActive(false);
        

        yield return new WaitForSeconds(0.6f);
        
        LevelManager.Instance.isCrashed = false;
        
        //red crash panel appear
        
        LevelManager.Instance._uiManager.redCrashedPanel.GetComponent<Image>().DOFade(0f, 0.7f).SetEase(Ease.Flash);
        LevelManager.Instance._playerVehicleManager.postCrashStuff.crashPS.gameObject.SetActive(false);       //explosion effect
       
        yield return new WaitForSeconds(0.8f);
        LevelManager.Instance._uiManager.redCrashedPanel.SetActive(false);
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
           LevelManager.Instance.stadiumPrefab.SetActive(true);
            foreach (var x in LevelManager.Instance.stuffToRemove)
            {
                if(x!=null)
                    x.SetActive(false);
            }
            
            //Video Replay
            LevelManager.Instance.gifRecording.StartRecording();
        }
        
        if (other.gameObject.CompareTag("SecondTrigger") && LevelManager.Instance.isFinalLap)
        {
            //Video Replay
            //LevelManager.Instance.gifRecording.StopRecording();
            
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
