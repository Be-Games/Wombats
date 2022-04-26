using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRaycaster : MonoBehaviour
{
    public float raycastDistance;

    public bool isHit;
    RaycastHit hit;
    public LayerMask CollLayerMask;
    private void Update()
    {

        isHit = Physics.Raycast(transform.position+new Vector3(0f,0.5f,0f), transform.forward, out hit, raycastDistance,CollLayerMask);
        Debug.Log(isHit);
        if (isHit)
        {
                
            CollideTrue();
        }
        else
        {
            CollideFalse();
        } 
        // if (LevelManager.Instance.isGameStarted)
        // {
        // }

       
    }

    private void OnDrawGizmos()
    {
        
        
            if (isHit)
            {
                
                Gizmos.color = Color.red;
                Gizmos.DrawRay(transform.position+new Vector3(0f,0.5f,0f),transform.forward * hit.distance);
            }
            else
            {
                
                Gizmos.color = Color.yellow;
                Gizmos.DrawRay(transform.position,transform.forward * raycastDistance);
            }
       
    }

    void CollideTrue()
    {
        EnemyController.Instance.isGoingToCollide = true;
        EnemyController.Instance.EnemyCollisionWithObstacles();
    }

    void CollideFalse()
    {
        EnemyController.Instance.isGoingToCollide = false;
    }
}
