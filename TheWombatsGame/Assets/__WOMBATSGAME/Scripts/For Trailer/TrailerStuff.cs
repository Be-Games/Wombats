using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrailerStuff : MonoBehaviour
{
   public GameObject[] allCarModels;
   public int i = 0;

   private void Start()
   {
      allCarModels[i].SetActive(true);
   }

   private void Update()
   {
      
      
      if (Input.GetKeyDown(KeyCode.K))
      {
         allCarModels[i].SetActive(false);
         i++;
         allCarModels[i].SetActive(true);
         
         
      }
   }
}
