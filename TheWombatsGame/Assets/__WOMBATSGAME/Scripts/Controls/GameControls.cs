using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameControls : MonoBehaviour
{
    
    public enum GestureState
    {
        Release,
        Break,
        Left,
        Right,
    }
    
    public GestureState gestureState;

    private Vector2 startTouchPosition;
    private Vector2 currentPosition;
    private Vector2 endTouchPosition;
    private bool stopTouch = false;

    private float swipeRange;
    
    //////////////

    private void Start()
    {
        swipeRange = 5;
    }

    void Update()
    {

        if(GameManager.Instance.canControlCar)
        {
            fastSwipe();
        }
        

        
    }

    public void fastSwipe()
    {
        
        
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            startTouchPosition = Input.GetTouch(0).position;
        }

        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved)
        {
            currentPosition = Input.GetTouch(0).position;
            Vector2 Distance = currentPosition - startTouchPosition;

            if (!stopTouch)
            {

                if (Distance.x < -swipeRange)
                {
                    gestureState = GestureState.Left;
                    LevelManager.Instance._playerController.MoveLeft();
                    stopTouch = true;
                }
                else if (Distance.x > swipeRange)
                {
                    gestureState = GestureState.Right;
                    LevelManager.Instance._playerController.MoveRight();
                    stopTouch = true;
                }
                
            }

        }

        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Stationary && !stopTouch)
        {
            gestureState = GestureState.Break;
            
            
        }
        
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)
        {
            stopTouch = false;

            endTouchPosition = Input.GetTouch(0).position;
            
            gestureState = GestureState.Release;
        }
    }

    
}


        
    
        

  
