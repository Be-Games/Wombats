using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartUpScreen : MonoBehaviour
{
   private float time, second;
   public Image fillImage;

   private void Start()
   {
      second = 4;
      Invoke("LoadHome",4f);
   }

   private void Update()
   {
      if (time < 4f)
      {
         time += Time.deltaTime;
         fillImage.fillAmount = time / second;
      }
   }

   public void LoadHome()
   {
      SceneManager.LoadScene("HomeScreen");
   }
}
