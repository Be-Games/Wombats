using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleManager : MonoBehaviour
{
   public GameObject[] onRoadObstacles;

   public Transform[] onRoadPositions;
   public int numberOfObstacles;
   public int[] obstaclesCounter;

   private void Start()
   {
      Instantiate(onRoadObstacles[0], onRoadPositions[UnityEngine.Random.Range(0, onRoadPositions.Length)].position,
         Quaternion.identity);
   }
}
