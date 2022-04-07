using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class SwipeControl : MonoBehaviour
{
   //  private static SwipeControl _instance;
   //
   //  public Transform leftCarPosition,rightCarPosition;
   //  public static SwipeControl Instance
   //  {
   //      get
   //      {
   //          return _instance;
   //      }
   //  }
   //
   //  private void Awake()
   //  {
   //      _instance = this;
   //  }
   //  
   //  private Vector3 fp;
   //  private Vector3 lp;
   //  public CinemachineCameraOffset cmvc;
   //  private float dragDistance;
   //
   //  public bool leftSwipee,rightSwipee,isAnimPlayed;
   //  public float smoothTime;
   //  private Vector3 Velocity = Vector3.zero;
   //  private void Start()
   //  {
   //      dragDistance = Screen.height * 15 / 100;
   //  }
   //
   //  private void Update()
   //  {
   //      
   //      if (LevelManager.Instance.isGameStarted)
   //      {
   //
   //          if (leftSwipee == true && isAnimPlayed)
   //          {
   //              PlayerController.Instance.PlayercarVisual.transform.localPosition = Vector3.SmoothDamp(
   //                  PlayerController.Instance.PlayercarVisual.transform.localPosition,
   //                  leftCarPosition.localPosition,  ref Velocity, smoothTime);
   //              Invoke("AnimPLayedFalse",0.3f);
   //              
   //          }
   //          if (leftSwipee == false && isAnimPlayed)
   //          {
   //              PlayerController.Instance.PlayercarVisual.transform.localPosition = Vector3.SmoothDamp(
   //                  PlayerController.Instance.PlayercarVisual.transform.localPosition,
   //                  rightCarPosition.localPosition,  ref Velocity, smoothTime);
   //              Invoke("AnimPLayedFalse",0.1f);
   //              
   //          }
   //          
   //          
   //          // if (Input.touchCount > 0)
   //          // {
   //          //     Touch touch = Input.GetTouch(0);
   //          //     if (touch.phase == TouchPhase.Began)
   //          //     {
   //          //         fp = touch.position;
   //          //         lp = touch.position;
   //          //         
   //          //     }
   //          //     
   //          //     else if (touch.phase == TouchPhase.Moved)
   //          //     {
   //          //         lp = touch.position;
   //          //     }
   //          //     else if (touch.phase == TouchPhase.Ended)
   //          //     {
   //          //         lp = touch.position;
   //          //         if (Mathf.Abs(lp.x - fp.x) > dragDistance || Mathf.Abs(lp.y - fp.y) > dragDistance)
   //          //         {
   //          //             if (Mathf.Abs(lp.x - fp.x) > Mathf.Abs(lp.y - fp.y))
   //          //             {
   //          //                 if ((lp.x > fp.x))
   //          //                 {
   //          //                     RightSwipe();
   //          //                 }
   //          //                 else
   //          //                 {
   //          //                     LeftSwipe();
   //          //                 }
   //          //             }
   //          //         }
   //          //         else
   //          //         {
   //          //            
   //          //             
   //          //             
   //          //             //PlayerController.Instance.mouseDown = true;
   //          //             //Debug.Log("Tap");
   //          //         }
   //          //     }
   //          // }
   //          //
   //          // if (Input.touchCount >= 1)
   //          // {
   //          //     if (Input.touches[0].phase == TouchPhase.Began)
   //          //         {
   //          //             PlayerController.Instance.mouseDown = true;
   //          //             Debug.Log("Touch Pressed");
   //          //         }
   //          //
   //          //         if (Input.touches[0].phase == TouchPhase.Ended)
   //          //         {
   //          //             PlayerController.Instance.mouseDown = false;
   //          //             Debug.Log("Touch Lifted/Released");
   //          //         }
   //          // }
   //          
   //          
   //          if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
   //          {
   //              fp = Input.GetTouch(0).position;
   //          
   //          }
   //          
   //          if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)
   //          {
   //              lp = Input.GetTouch(0).position;
   //          
   //              if (lp.x < fp.x)
   //              {
   //                  
   //                  LeftSwipe();
   //              }
   //          
   //              if (lp.x > fp.x)
   //              {
   //                  
   //                  RightSwipe();
   //              }
   //          
   //          }
   //
   //          if (Input.GetKeyDown(KeyCode.A))
   //          {
   //              LeftSwipe();
   //          }
   //          if (Input.GetKeyDown(KeyCode.D))
   //          {
   //              RightSwipe();
   //          }
   //
   //          if (leftSwipee)
   //          {
   //              float target = -0.9f;
   //              
   //              float delta = cmvc.m_Offset.x - target;
   //              delta *= Time.deltaTime * 2f;
   //
   //              cmvc.m_Offset.x -= delta;
   //          }
   //          
   //          if (!leftSwipee)
   //          {
   //              float target = 0;
   //              
   //              float delta = target - cmvc.m_Offset.x;
   //              delta *= Time.deltaTime * 2f;
   //
   //              cmvc.m_Offset.x += delta;
   //          }
   //      }
   //      
   //  }
   //
   //  public void RightSwipe()
   //  {
   //      if (LevelManager.Instance.currentPosition == -1)
   //      {
   //          LevelManager.Instance.currentPosition = 1;
   //          
   //          PlayerController.Instance.PlayercarVisual.GetComponent<Animator>().SetBool("isLeft",false);
   //          Invoke("AnimPLayedTrue",0.1f);
   //         
   //          
   //         leftSwipee = false;
   //
   //        
   //
   //      }
   //      
   //      
   //  }
   //
   //  
   //
   // public void LeftSwipe()
   //  {
   //      if (LevelManager.Instance.currentPosition == 1)
   //      {
   //          Debug.Log("Left Swipe");
   //          LevelManager.Instance.currentPosition = -1;
   //          
   //          PlayerController.Instance.PlayercarVisual.GetComponent<Animator>().SetBool("isLeft",true);
   //          Invoke("AnimPLayedTrue",0.1f);
   //
   //          
   //          leftSwipee = true;
   //
   //          
   //
   //      }
   //
   //  }
   //
   // void AnimPLayedFalse()
   // {
   //     if (isAnimPlayed)
   //     {
   //         isAnimPlayed = false;
   //         
   //     }
   //     
   // }
   //
   // void AnimPLayedTrue()
   // {
   //     if (!isAnimPlayed)
   //     {
   //         isAnimPlayed = true;
   //         
   //     }
   //    
   // }

    
}
