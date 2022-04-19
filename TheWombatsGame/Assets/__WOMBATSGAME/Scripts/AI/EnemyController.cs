using System;
using System.Collections;
using System.Collections.Generic;
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
    private void Start()
    {
        if (currentEnemyNumber == -1)
        {
            
        }
        if (currentEnemyNumber == 1)
        {
            
        }
        
        StartCoroutine("IniCarPush");
    }

    IEnumerator IniCarPush()
    {
        yield return new WaitForSeconds(0.5f);
        if (currentEnemyNumber == -1)
        {
            enemyCarModelGO.transform.localPosition = new Vector3(LevelManager.Instance.enemy1.transform.localPosition.x - xOffSet,0f,0f);
            Acc = LevelManager.Instance.enemy1.GetComponent<VehicleManager>().carSpeedSettings.Acc;
            Dec = LevelManager.Instance.enemy1.GetComponent<VehicleManager>().carSpeedSettings.Dec;
            enemySpeed = LevelManager.Instance.enemy1.GetComponent<VehicleManager>().carSpeedSettings.normalSpeed;
        }
        if (currentEnemyNumber == 1)
        {
            enemyCarModelGO.transform.localPosition = new Vector3(LevelManager.Instance.enemy2.transform.localPosition.x + xOffSet,0f,0f);
            Acc = LevelManager.Instance.enemy2.GetComponent<VehicleManager>().carSpeedSettings.Acc;
            Dec = LevelManager.Instance.enemy2.GetComponent<VehicleManager>().carSpeedSettings.Dec;
            enemySpeed = LevelManager.Instance.enemy2.GetComponent<VehicleManager>().carSpeedSettings.normalSpeed;
        }
        
        yield return new WaitForSeconds(0.5f);
        enemyPF.speed = 0;
        
        
    }

    private void Update()
    {
        if (LevelManager.Instance.isGameStarted)
        {

            diffInDistance = playerPF.distanceTravelled - enemyPF.distanceTravelled;
            
            float target = enemySpeed;
            
            float delta = target - enemyPF.speed;
                        
            delta *= Time.deltaTime * Acc;
                        
            
            enemyPF.speed += delta;
            
        }
        else
        {
            enemyPF.speed = 0;
        }

        if (LevelManager.Instance.Easy)
        {
            if (Mathf.Abs(diffInDistance) > LevelManager.Instance.easyValue)
            {
                Debug.Log("inc enemy speed");
            } 
        }
        
    }

    

    public void EnemyCollisionWithObstacles()
    {
        if (enemyCurrentPos == 1)
        {
            Debug.Log("Enemy Left");
                
            enemyCarModelGO.transform.localPosition = new Vector3(enemyCarModelGO.transform.localPosition.x - 2*xOffSet,0f,0f);
                
            enemyCurrentPos = -1;
            return;
        }
            
        if (enemyCurrentPos == -1)
        {
            Debug.Log("Enemy Right");
                
            enemyCarModelGO.transform.localPosition = new Vector3(enemyCarModelGO.transform.localPosition.x + 2*xOffSet,0f,0f);
            //isGoingToCollide = false;
            enemyCurrentPos = 1;
                
        }
        
        if (isGoingToCollide)
        {
            
    
            
            
        }
    }
}
