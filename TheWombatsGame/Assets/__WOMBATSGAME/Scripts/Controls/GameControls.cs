using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameControls : MonoBehaviour
{
    private Touch touch;

    private Vector2 beginTouchPosition, endTouchPosition;

    public bool tapDown;
    
    public enum GestureState
    {
        Release,
        Break,
        Left,
        Right,
    }

    public GestureState gestureState;
    [SerializeField]private GameManager _gameManager;

    private void Start()
    {
        _gameManager = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();
    }

    private void Update()
    {
        if (_gameManager.canControlCar)
        {
            if (Input.touchCount > 0)
            {
                touch = Input.GetTouch(0);
                    
                switch (touch.phase)
                {
                    case TouchPhase.Stationary:
                        tapDown = true;
                        
                        gestureState = GestureState.Break;
                        break;
                            
                            
                    case TouchPhase.Began:
                        beginTouchPosition = touch.position;
                        break;
                                
                    case TouchPhase.Ended:
                        if (tapDown)
                        {
                            tapDown = false;
                            
                            gestureState = GestureState.Release;
                        }
                        else
                        {
                                    
                            endTouchPosition = touch.position;
                                
                            if (beginTouchPosition != endTouchPosition)
                            {
                                if (beginTouchPosition.x > endTouchPosition.x)
                                {
                                    //Debug.Log("Left Swipe");
                                    gestureState = GestureState.Left;
                                    PlayerController.Instance.MoveLeft();
                                }
                                if (beginTouchPosition.x < endTouchPosition.x)
                                {
                                    //Debug.Log("Right Swipe");
                                    gestureState = GestureState.Right;
                                    PlayerController.Instance.MoveRight();
                                }
                            }
                        }
                                
                                
                        break;
                               
                }
                        
                        
            }
        }
        
        
        

        
}
}    
