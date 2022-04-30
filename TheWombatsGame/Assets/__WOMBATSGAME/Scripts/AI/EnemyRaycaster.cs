using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRaycaster : MonoBehaviour
{
    
    public LayerMask obstacleLayer;
    public float rayCastDistance;
    RaycastHit hit;
    
    public Vector3 adjustment;

    public EnemyController enemyController;
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
    }

    private void OnDrawGizmos()
    {
        
        Gizmos.color = Color.green;
        Gizmos.DrawRay(transform.position + adjustment,transform.forward * rayCastDistance);
        
    }
}
