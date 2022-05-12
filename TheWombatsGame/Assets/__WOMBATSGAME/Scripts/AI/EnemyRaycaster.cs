using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRaycaster : MonoBehaviour
{
    
    [HideInInspector]public LayerMask obstacleLayer; 
    [HideInInspector] public LayerMask slowDownLayer;
    [HideInInspector]public float rayCastDistance;
    RaycastHit hit;
    
    private Vector3 adjustment;

    public EnemyController enemyController;

    private void Start()
    {
        adjustment = new Vector3(0f,0.27f,0f);
        rayCastDistance = 2;
        obstacleLayer = LayerMask.GetMask("Obstacles");
        slowDownLayer = LayerMask.GetMask("ToSlow");
    }

    private void Update()
    {
        
        if(LevelManager.Instance.isGameStarted)
            RaycastMethod();
        
    }

    void RaycastMethod()
    {
        var ray = new Ray(transform.position + adjustment,transform.forward);
       
        if (Physics.Raycast(ray,out hit,rayCastDistance,obstacleLayer))
        {
            enemyController.EnemyCollisionWithObstacles();
        }
        
        if (Physics.Raycast(ray,out hit,rayCastDistance,slowDownLayer))
        {
            enemyController.slowDown();
        }
        else
        {
            enemyController.isSlowed = false;
        }
    }

    private void OnDrawGizmos()
    {
        
        Gizmos.color = Color.green;
        Gizmos.DrawRay(transform.position + adjustment,transform.forward * rayCastDistance);
        
    }
}
