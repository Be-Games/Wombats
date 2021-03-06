using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRaycaster : MonoBehaviour
{
    
    [HideInInspector]public LayerMask obstacleLayer; 
    [HideInInspector] public LayerMask slowDownLayer;
    [HideInInspector]public LayerMask crashLayer;
    [HideInInspector]public float rayCastDistance;
    RaycastHit hit;
    
    private Vector3 adjustment;

    public EnemyController enemyController;

    private void Start()
    {
        adjustment = new Vector3(0f,0.32f,0f);
        rayCastDistance = 2;
        obstacleLayer = LayerMask.GetMask("Obstacles");
        slowDownLayer = LayerMask.GetMask("ToSlow");
        crashLayer = LayerMask.GetMask("Crashed");

        StartCoroutine(MyUpdate());
    }

    IEnumerator MyUpdate()
    {
        
        if(LevelManager.Instance.isGameStarted)
            RaycastMethod();

        yield return null;
        StartCoroutine(MyUpdate());
    }

    void RaycastMethod()
    {
        var ray = new Ray(transform.position + adjustment,transform.forward);
       
        if (Physics.Raycast(ray,out hit,rayCastDistance,obstacleLayer))
        {
            enemyController.EnemyCollisionWithObstacles();
        }
        
        /*if (Physics.Raycast(ray,out hit,0.2f,crashLayer))
        {
            enemyController.EnemyCollisionWithObstacles2();
        }*/
        
        
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
