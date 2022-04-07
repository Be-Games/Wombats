using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using PathCreation.Examples;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PositionSystem : MonoBehaviour
{
    public PathFollower[] allPfClasses;
    public float[] distTravelled;
    public TextMeshProUGUI F, S, T;
    public string pos1,pos2,pos3;
    public float totalDistance;
    public Slider progressBarSlider;

    public float a, b, c;
    
    private void Update()
    {
        for (int i = 0; i < 3; i++)
        {
            distTravelled[i] = allPfClasses[i].distanceTravelled;
        }
        
        Array.Sort(distTravelled);


        F.text = "" + distTravelled[2];
        S.text = "" + distTravelled[1];
        T.text = "" + distTravelled[0];
        
       
        
       
        

            // LevelManager.Instance.currentPlayerCarModel.transform.GetChild(3).GetChild(1)
            //     .GetComponent<TextMeshProUGUI>().text = playerPos;
            // if (LevelManager.Instance.enemy1 != null)
            // {
            //     LevelManager.Instance.enemy1.transform.GetChild(3).GetChild(1)
            //         .GetComponent<TextMeshProUGUI>().text = enemy1Pos;
            // }
            
            
        }
    

void SetSlider(float p)
    {
        progressBarSlider.value = p;
    }
}
