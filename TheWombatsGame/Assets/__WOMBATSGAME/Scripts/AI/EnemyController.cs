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
    
    public PathFollower enemyPF;
    public float xOffSet = -3.77f;

    public float Acc;
    public float Dec;
    public float enemySpeed;
    

    

    public bool isGoingToCollide;
    public int enemyCurrentPos = -1;

    public bool playerWithEnemy = false;
    
    private void Start()
    {
        if (currentEnemyNumber == -1)
        {
            Acc = LevelManager.Instance.enemy1.GetComponent<VehicleManager>().carSpeedSettings.Acc;
            Dec = LevelManager.Instance.enemy1.GetComponent<VehicleManager>().carSpeedSettings.Dec;
            enemySpeed = LevelManager.Instance.enemy1.GetComponent<VehicleManager>().carSpeedSettings.normalSpeed;
        }
        if (currentEnemyNumber == 1)
        {
            Acc = LevelManager.Instance.enemy2.GetComponent<VehicleManager>().carSpeedSettings.Acc;
            Dec = LevelManager.Instance.enemy2.GetComponent<VehicleManager>().carSpeedSettings.Dec;
            enemySpeed = LevelManager.Instance.enemy2.GetComponent<VehicleManager>().carSpeedSettings.normalSpeed;
        }
        
        StartCoroutine("IniCarPush");
    }

    IEnumerator IniCarPush()
    {
        if (currentEnemyNumber == -1)
        {
            LevelManager.Instance.enemy1.transform.localPosition = new Vector3(LevelManager.Instance.enemy1.transform.localPosition.x - xOffSet,0f,0f);
        }
        if (currentEnemyNumber == 1)
        {
            LevelManager.Instance.enemy2.transform.localPosition = new Vector3(LevelManager.Instance.enemy2.transform.localPosition.x + xOffSet,0f,0f);
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
        
    }

    

    // public void EnemyCollisionWithObstacles()
    // {
    //     if (enemyCurrentPos == 1)
    //     {
    //         Debug.Log("Enemy Left");
    //             
    //         enemyCarVisual.transform.localPosition = new Vector3(enemyCarVisual.transform.localPosition.x - 2*xOffSet,0f,0f);
    //             
    //         enemyCurrentPos = -1;
    //         return;
    //     }
    //         
    //     if (enemyCurrentPos == -1)
    //     {
    //         Debug.Log("Enemy Right");
    //             
    //         enemyCarVisual.transform.localPosition = new Vector3(enemyCarVisual.transform.localPosition.x + 2*xOffSet,0f,0f);
    //         //isGoingToCollide = false;
    //         enemyCurrentPos = 1;
    //             
    //     }
    //     
    //     if (isGoingToCollide)
    //     {
    //         
    //
    //         
    //         
    //     }
    // }
}
