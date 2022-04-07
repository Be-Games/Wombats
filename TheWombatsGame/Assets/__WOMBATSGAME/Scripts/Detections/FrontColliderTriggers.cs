using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using SWS;
using UnityEngine;

public class FrontColliderTriggers : MonoBehaviour
{
    private GameObject currentPersonRagdoll;
    public GameObject explosionParticleEffect;


    public void testCrash()
    {
        StartCoroutine("CarTotalled");
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Boost"))                                                                        //Boost Trigger 
        {
            
            LevelManager.Instance.BoostManager();
            
            
            other.gameObject.GetComponent<BoxCollider>().enabled = false;
            other.transform.GetChild(0).gameObject.SetActive(false);
            other.transform.GetChild(1).gameObject.GetComponent<ParticleSystem>().Play();
            
            //Destroy the boost pickup
            
                        
        }
        
        if (other.gameObject.CompareTag("People"))
        {
            Debug.Log("Coll with People");
            
            //PlayerController.Instance.playerPF.speed = 0;
            
            
            LevelManager.Instance.isCrashedWithPpl = true;

            currentPersonRagdoll = other.gameObject;
            other.GetComponent<splineMove>().ChangeSpeed(0);
            Invoke("WaitAndRag",0.05f);
            
            StartCoroutine("CarTotalled");
            
        }
        
        if (other.gameObject.CompareTag("Collision"))
        {
            if (PlayerController.Instance.targetSpeed >= LevelManager.Instance.boostSpeed)
            {
                Debug.Log("Break");
                other.GetComponent<ClickOrTapToExplode>().DestroyStuff();
                            
            }
            
            if (PlayerController.Instance.targetSpeed <=LevelManager.Instance.normalSpeed)
            {
                // Debug.Log("Car Totalled");
                            
                PlayerController.Instance.playerPF.speed = 0;
                //PlayerController.Instance.playerPF.enabled = false;
                            
                            
                StartCoroutine("CarTotalled");
            
            
            }
        }
    }
    
    IEnumerator CarTotalled()
    {
        //PlayerController.Instance.playerPF.enabled = false;
       
        
        LevelManager.Instance.Vibrate();
        
        if(LevelManager.Instance.continueCounter != 5)
            UIManager.Instance.crashedPanel.SetActive(true);
        
        if(LevelManager.Instance.continueCounter == 5)
            UIManager.Instance.postAdCrashPanel.SetActive(true);

        if (!LevelManager.Instance.isCrashedWithPpl)
        {
            LevelManager.Instance.currentPlayerCarModel.transform.GetChild(0).gameObject.SetActive(false);        //player model+effects
        
            LevelManager.Instance.currentPlayerCarModel.transform.GetChild(1).gameObject.SetActive(true);        //player upside down model
        
            LevelManager.Instance.isCrashed = true;
            LevelManager.Instance.currentPlayerCarModel.transform.GetChild(2).gameObject.SetActive(true);        //explosion effect
            
            foreach (GameObject x in LevelManager.Instance.pplToDisable)                                        //DISABLE PPL WHEN CRASHED
            {
                x.SetActive(false);
            }
        }

        LevelManager.Instance.isCrashedWithPpl = false;
       
        
        
        LevelManager.Instance.carContinueChances.text = "" + (2 - LevelManager.Instance.continueCounter);
            
        LevelManager.Instance.isGameStarted = false;
        GameManager.Instance.canControlCar = false;
        PlayerController.Instance.gameControlsClass.gestureState = GameControls.GestureState.Break;
        
        yield return new WaitForSeconds(4f);

        //carRollOverAnimator.enabled = false;
        LevelManager.Instance.currentPlayerCarModel.transform.GetChild(2).gameObject.SetActive(false);
        
        

    }

    void WaitAndRag()
    {
        currentPersonRagdoll.GetComponent<Ragdoll>().DoRagDoll(true);
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
