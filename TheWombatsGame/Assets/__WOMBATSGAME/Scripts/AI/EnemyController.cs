using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using PathCreation.Examples;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    private float movementDuration,rotationDuration;

    public Transform leftCarTransform, centreCarTransform, rightCarTransform;

    public bool isSlowed;
    
    public GameObject[] wheels;
    private void Start()
    {
        movementDuration = 0.15f;
        rotationDuration = 0.1f;
        StartCoroutine("IniCarPush");
        
        
    }

    IEnumerator IniCarPush()
    {
        foreach (var wheel in wheels)
        {
            if(wheel != null)
                wheel.GetComponent<DOTweenAnimation>().DOPlay();
        }
        
        if (currentEnemyNumber == -1)
        {
            enemyCarModelGO.transform.localPosition = new Vector3(enemyCarModelGO.transform.localPosition.x -  xOffSet,0f,0f);
            Acc = LevelManager.Instance.enemyLeftVisual.GetComponent<VehicleManager>().carSpeedSettings.Acc;
            Dec = LevelManager.Instance.enemyLeftVisual.GetComponent<VehicleManager>().carSpeedSettings.Dec;
            enemySpeed = LevelManager.Instance._playerVehicleManager.carSpeedSettings.normalSpeed+UnityEngine.Random.Range(-0.1f,0.7f);
            
            wheels = LevelManager.Instance.enemyLeftVisual.GetComponent<VehicleManager>().bodyTrigger.wheels;
        }
        if (currentEnemyNumber == 1)
        {
            enemyCarModelGO.transform.localPosition = new Vector3(enemyCarModelGO.transform.localPosition.x + xOffSet,0f,0f);
            Acc = LevelManager.Instance.enemyRightVisual.GetComponent<VehicleManager>().carSpeedSettings.Acc;
            Dec = LevelManager.Instance.enemyRightVisual.GetComponent<VehicleManager>().carSpeedSettings.Dec;
            enemySpeed = LevelManager.Instance._playerVehicleManager.carSpeedSettings.normalSpeed+UnityEngine.Random.Range(-0.1f,0.7f);
            
            wheels = LevelManager.Instance.enemyRightVisual.GetComponent<VehicleManager>().bodyTrigger.wheels;
        }
        
        if (SceneManager.GetActiveScene().name == "LIVERPOOL")
        {
            enemySpeed = enemySpeed - 2;
        }
        
        yield return new WaitForSeconds(0.1f);
        enemyPF.speed = 0;
        
        
        
    }

    private void Update()
    {
        if (LevelManager.Instance.isGameStarted && !isSlowed)
        {
            /*if (enemyPF.distanceTravelled >= (LevelManager.Instance.singleLapDistance * 2))
            {
                enemyPF.speed = 0;
            }
            else
            {
                
            }*/
            
            foreach (var wheel in wheels)
            {
                if(wheel != null)
                    wheel.GetComponent<DOTweenAnimation>().DOPlay();
            }
            
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
                   switch (UnityEngine.Random.Range(0,2))
                   {
                       case 0:
                           enemyCarModelGO.transform.DOLocalMove(leftCarTransform.localPosition, movementDuration);
                
                           enemyCarModelGO.transform.DOLocalRotate(new Vector3(0f,-16.83f,0f), rotationDuration)
                               .OnComplete(()=> enemyCarModelGO.transform.DOLocalRotate(new Vector3(0f,0f,0f), rotationDuration));
                   
                           enemyCurrentPos = -1;
                           break;
                       case 1:
                           enemyCarModelGO.transform.DOLocalMove(rightCarTransform.localPosition, movementDuration);
                
                           enemyCarModelGO.transform.DOLocalRotate(new Vector3(0f,16.83f,0f), rotationDuration)
                               .OnComplete(()=> enemyCarModelGO.transform.DOLocalRotate(new Vector3(0f,0f,0f), rotationDuration));
                   
                           enemyCurrentPos = 1;
                           break;
                   }

                   break;
                  
                
                case -1:
                    enemyCarModelGO.transform.DOLocalMove(centreCarTransform.localPosition, movementDuration);
                
                    enemyCarModelGO.transform.DOLocalRotate(new Vector3(0f,16.83f,0f), rotationDuration)
                        .OnComplete(()=> enemyCarModelGO.transform.DOLocalRotate(new Vector3(0f,0f,0f), rotationDuration));
                
                    enemyCurrentPos = 0;
                    break;
            }
    }
    
     public void EnemyCollisionWithObstacles2()
    {
        if (currentEnemyNumber == -1)
        {
            LevelManager.Instance.enemyLeftVisual.GetComponent<VehicleManager>().postCrashStuff.up_car
                .SetActive(false); 
            LevelManager.Instance.enemyLeftVisual.GetComponent<VehicleManager>().postCrashStuff.down_car
                .SetActive(true); 
                 
        }
        if (currentEnemyNumber == 1)
        {
            LevelManager.Instance.enemyRightVisual.GetComponent<VehicleManager>().postCrashStuff.up_car
                .SetActive(false); 
            LevelManager.Instance.enemyRightVisual.GetComponent<VehicleManager>().postCrashStuff.down_car
                .SetActive(true); 
                   
        }
               
               

        isSlowed = true;
        enemyPF.speed = 0;
    }

    public void slowDown()
    {
        foreach (var wheel in wheels)
        {
            if(wheel != null)
                wheel.GetComponent<DOTweenAnimation>().DOPause();
        }
        
        isSlowed = true;
        enemyPF.speed = 0;
        /*StartCoroutine(SpeedToZero());*/
    }

    IEnumerator SpeedToZero()
    {
        yield return null;
        while (true)
        {
            
        }
    }

    
}
