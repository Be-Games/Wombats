using System;
using System.Collections;
using System.Collections.Generic;
using SWS;
using UnityEngine;

public class PickUpTrigger : MonoBehaviour
{
    private static PickUpTrigger _instance;

    public static PickUpTrigger Instance
    {
        get
        {
            return _instance;
        }
    }

    private void Awake()
    {
        _instance = this;
    }
    
    public ClickOrTapToExplode destroyScript;
    public GameObject explosionParticleEffect;
    public Animator carRollOverAnimator;
    public GameObject vehicleDestroyed;

    public bool isEnemy;

    
    
    

    
    private void OnTriggerEnter(Collider other)
    {
        if (!isEnemy)
            
        {
                    
            
                   
            
                   
                    
                    

                    if (other.gameObject.CompareTag("Enemy"))
                    {
                        
                        
                        
                        // if (SwipeControl.Instance.leftSwipee)
                        // {
                        //     Debug.Log("Back to right");
                        //     LevelManager.Instance.currentPosition = 1;
                        //     PlayerController.Instance.PlayercarVisual.transform.localPosition = new Vector3(
                        //         PlayerController.Instance.PlayercarVisual.transform.localPosition.x +
                        //         2 * PlayerController.Instance.xOffSet, 0f, 0f);
                        //
                        //
                        //     SwipeControl.Instance.leftSwipee = false;
                        // }
                        //
                        // if (SwipeControl.Instance.rightSwipee)
                        // {
                        //     Debug.Log("Back to Left");
                        //     LevelManager.Instance.currentPosition = -1;
                        //     PlayerController.Instance.PlayercarVisual.transform.localPosition = new Vector3(PlayerController.Instance.PlayercarVisual.transform.localPosition.x - 2*PlayerController.Instance.xOffSet,0f,0f);
                        //
                        //     SwipeControl.Instance.rightSwipee = true;
                        // }
                    }
        }
       

        
    }

    

    

    
    
}
