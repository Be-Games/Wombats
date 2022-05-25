using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tut_Gamecontrols : MonoBehaviour
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

    public float swipeRange;

    public Tut_PlayerController TutPC;
    
    //////////////
    private bool enableSlowSwipe = false;

    private void Start()
    {
        swipeRange = 5;
    }

    void Update()
    {

       fastSwipe();
        
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
                    if (TutPC.canMoveLeft)
                    {
                        TutPC.moveLeft_GO.SetActive(false);
                        gestureState = GestureState.Left;
                        TutPC.MoveLeft();
                       
                        
                        stopTouch = false;
                        gestureState = GestureState.Release;
                        TutPC.canMoveLeft = false;
                        TutPC.canBreak = false;
                    }
                    
                }
                else if (Distance.x > swipeRange)
                {
                    if (TutPC.canMoveRight)
                    {
                        TutPC.moveRight_GO.SetActive(false);
                        gestureState = GestureState.Right;
                        TutPC.MoveRight();
                       
                        
                        stopTouch = false;
                        gestureState = GestureState.Release;
                        TutPC.canMoveRight = false;
                        TutPC.canBreak = false;
                    }
                }
               
            }

        }

        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Stationary && TutPC.canBreak)
        {
            TutPC.tapBreak_GO.SetActive(false);
            gestureState = GestureState.Break;
            //press brake - show red light and change material to red
            TutPC.PlayercarVisual.GetComponent<VehicleManager>().carEffects.breakLight.SetActive(true);
            var materials = TutPC.PlayercarVisual.GetComponent<VehicleManager>().bodyTrigger.body
                .GetComponent<MeshRenderer>().materials;
            materials[1] = TutPC.redMat;
            //show barrier animation
            TutPC.barrierDO.DOPlay();
            
            //after animation release car
            Invoke("PostBarrierAnimation",1.2f);
            
        }
        
    }

    void PostBarrierAnimation()
    {
        gestureState = GestureState.Release;
        TutPC.canBreak = false;

    }
}
