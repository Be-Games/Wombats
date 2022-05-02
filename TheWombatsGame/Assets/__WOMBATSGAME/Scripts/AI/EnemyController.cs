using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using PathCreation.Examples;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private static EnemyController _instance;

    public static EnemyController Instance
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

    public int currentEnemyNumber;
    
    
    public float xOffSet = -3.77f;

    public float Acc;
    public float Dec;
    public float enemySpeed;

    public GameObject enemyCarModelGO;
    

    public bool isGoingToCollide;
    public int enemyCurrentPos = -1;

    public bool playerWithEnemy = false;

    public PathFollower playerPF;
    public PathFollower enemyPF;
    public float diffInDistance;

    public float movementDuration,rotationDuration;

    public Transform leftCarTransform, centreCarTransform, rightCarTransform;

    
    private void Start()
    {
        StartCoroutine("IniCarPush");
    }

    IEnumerator IniCarPush()
    {
        yield return new WaitForSeconds(0.5f);
        if (currentEnemyNumber == -1)
        {
            enemyCarModelGO.transform.localPosition = new Vector3(enemyCarModelGO.transform.localPosition.x -  xOffSet,0f,0f);
            Acc = LevelManager.Instance.enemyLeftVisual.GetComponent<VehicleManager>().carSpeedSettings.Acc;
            Dec = LevelManager.Instance.enemyLeftVisual.GetComponent<VehicleManager>().carSpeedSettings.Dec;
            enemySpeed = LevelManager.Instance._playerVehicleManager.carSpeedSettings.normalSpeed+0.2f;
        }
        if (currentEnemyNumber == 1)
        {
            enemyCarModelGO.transform.localPosition = new Vector3(enemyCarModelGO.transform.localPosition.x + xOffSet,0f,0f);
            Acc = LevelManager.Instance.enemyRightVisual.GetComponent<VehicleManager>().carSpeedSettings.Acc;
            Dec = LevelManager.Instance.enemyRightVisual.GetComponent<VehicleManager>().carSpeedSettings.Dec;
            enemySpeed = LevelManager.Instance._playerVehicleManager.carSpeedSettings.normalSpeed-0.2f;
        }
        
        yield return new WaitForSeconds(0.5f);
        enemyPF.speed = 0;
        
        
    }

    private void Update()
    {
        if (LevelManager.Instance.isGameStarted)
        {
            
            float target = enemySpeed;
            
            float delta = target - enemyPF.speed;
                        
            delta *= Time.deltaTime * Acc;
                        
            
            enemyPF.speed += delta;
            
        }
        else
        {
            enemyPF.speed = 0;
        }

        // if (LevelManager.Instance.Easy)
        // {
        //     if (Mathf.Abs(diffInDistance) > LevelManager.Instance.easyValue)
        //     {
        //         Debug.Log("inc enemy speed");
        //     } 
        // }
        
        
       
    }

    

    public void EnemyCollisionWithObstacles()
    {
        switch (enemyCurrentPos)
            {
               case 1:
                    enemyCarModelGO.transform.DOLocalMove(centreCarTransform.localPosition, movementDuration);
                
                    enemyCarModelGO.transform.DOLocalRotate(new Vector3(0f,-16.83f,0f), rotationDuration)
                        .OnComplete(()=> enemyCarModelGO.transform.DOLocalRotate(new Vector3(0f,0f,0f), rotationDuration));
                
                    enemyCurrentPos = 0;
                    break;
               
               case 0:
                   enemyCarModelGO.transform.DOLocalMove(leftCarTransform.localPosition, movementDuration);
                
                   enemyCarModelGO.transform.DOLocalRotate(new Vector3(0f,-16.83f,0f), rotationDuration)
                       .OnComplete(()=> enemyCarModelGO.transform.DOLocalRotate(new Vector3(0f,0f,0f), rotationDuration));
                
                
                   enemyCurrentPos = -1;
                   break;
                
                case -1:
                    enemyCarModelGO.transform.DOLocalMove(rightCarTransform.localPosition, movementDuration);
                
                    enemyCarModelGO.transform.DOLocalRotate(new Vector3(0f,-16.83f,0f), rotationDuration)
                        .OnComplete(()=> enemyCarModelGO.transform.DOLocalRotate(new Vector3(0f,0f,0f), rotationDuration));
                
                    enemyCurrentPos = 0;
                    break;
            }
    }

    
}
