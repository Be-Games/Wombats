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
    public GameObject explosionParticleEffect;


    
    private void OnTriggerEnter(Collider other)
    {
        #region BoostTrigger
        if (other.gameObject.CompareTag("Boost"))                                                                        //Boost Trigger 
        {
            
            LevelManager.Instance.BoostManager();
            
            
            other.gameObject.GetComponent<BoxCollider>().enabled = false;                                            //For the triggered pickup
            other.transform.GetChild(0).gameObject.SetActive(false);
            other.transform.GetChild(1).gameObject.GetComponent<ParticleSystem>().Play();
            
            LevelManager.Instance.playerVehicleManager.carEffects.boostCapturedEffectPS.Play();                                  //under car effect show once
            
            
        }
        
        #endregion
        if (other.gameObject.CompareTag("People"))
        {
            //VIBRATE ON trigger with person
            if (LevelManager.Instance._audioManager.isHapticEnabled)
                LevelManager.Instance.currentPlayerCarModel.GetComponent<HapticSource>().Play();
            
            Debug.Log("Coll with People");
            other.GetComponent<Collider>().enabled = false;
            
            
            PlayerController.Instance.playerPF.speed = 0;

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
            if (PlayerController.Instance.targetSpeed >=PlayerController.Instance.boostSpeed)
            {
                Debug.Log("Break");
                other.GetComponent<ClickOrTapToExplode>().DestroyStuff();
                            
            }
            
            if (PlayerController.Instance.targetSpeed <=PlayerController.Instance.normalSpeed)
            {
                 Debug.Log("Car Totalled");
                 //VIBRATE ON trigger with coin
                 if (LevelManager.Instance._audioManager.isHapticEnabled)
                     LevelManager.Instance.currentPlayerCarModel.GetComponent<HapticSource>().Play();        
                 
                 
                PlayerController.Instance.playerPF.speed = 0;
                //PlayerController.Instance.playerPF.enabled = false;

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
            LevelManager.Instance.currentScore++;
            UIManager.Instance.scoreText.text = LevelManager.Instance.currentScore.ToString();
            other.gameObject.GetComponent<BoxCollider>().enabled = false;                                            //For the triggered pickup
            other.transform.gameObject.GetComponent<ParticleSystem>().Stop();
            other.transform.GetChild(0).GetChild(0).gameObject.GetComponent<ParticleSystem>().Play();
            
            //VIBRATE ON trigger with coin
            if (LevelManager.Instance._audioManager.isHapticEnabled)
                LevelManager.Instance.currentPlayerCarModel.GetComponent<HapticSource>().Play();
        }
    }

    
    IEnumerator CarTotalled()
    {
        //PlayerController.Instance.playerPF.enabled = false;
       
        //VIBRATE ON CRASH PRESSED
        // if(GameManager.Instance.isHapticEnabled)
        //    PlayerController.Instance.gameObject.GetComponent<HapticSource>().Play();
        
        
        if(LevelManager.Instance.continueCounter != 5)
            UIManager.Instance.crashedPanel.SetActive(true);
        
        if(LevelManager.Instance.continueCounter == 5)
            UIManager.Instance.postAdCrashPanel.SetActive(true);

        if (!LevelManager.Instance.isCrashedWithPpl)
        {
            LevelManager.Instance.playerVehicleManager.postCrashStuff.up_car.SetActive(false);        //player model+effects
        
            LevelManager.Instance.playerVehicleManager.postCrashStuff.down_car.SetActive(true);        //player upside down model
        
            LevelManager.Instance.isCrashed = true;
            LevelManager.Instance.playerVehicleManager.postCrashStuff.crashPS.Play();       //explosion effect
            
            foreach (GameObject x in LevelManager.Instance.pplToDisable)                                        //DISABLE PPL WHEN CRASHED
            {
                x.SetActive(false);
            }
        }

        LevelManager.Instance.isCrashedWithPpl = false;
       
        
        
        LevelManager.Instance.carContinueChances.text = "" + (2 - LevelManager.Instance.continueCounter);
            
       // LevelManager.Instance.isGameStarted = false;
        GameManager.Instance.canControlCar = false;
        PlayerController.Instance.gameControlsClass.gestureState = GameControls.GestureState.Break;
        LevelManager.Instance.FastWind.SetActive(false);
        LevelManager.Instance.slowWind.SetActive(false);
        
        yield return new WaitForSeconds(4f);

        //carRollOverAnimator.enabled = false;
        LevelManager.Instance.playerVehicleManager.postCrashStuff.crashPS.Stop();       //explosion effect
        
        

    }

    void WaitAndRag()
    {
        currentPersonRagdoll.GetComponent<Animator>().SetBool("isDead",true);
    }

    void ResetHuman()
    {
        if (currentPersonRagdoll != null)
        {
            currentPersonRagdoll.GetComponent<Collider>().enabled = true;
            currentPersonRagdoll.GetComponent<splineMove>().StartMove();
        }
        
        
    }
    
    public void HideHuman()
    {
        if (currentPersonRagdoll != null)
        {
            currentPersonRagdoll.SetActive(false);
        }
        
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("FinishLine") && !LevelManager.Instance.isLapTriggered)
        {
            StartCoroutine("LapSystemDelay");
           
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
