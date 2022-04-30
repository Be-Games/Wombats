using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using PathCreation.Examples;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelProgressUI : MonoBehaviour
{
    private RectTransform[] uFillImage = new RectTransform[3];                         // 0 - player , 1 - enemy1 , 2 - enemy2

    public PathFollower[] chars;            // 0 - player , 1 - enemy1 , 2 - enemy2

    public float totalLevelDistance;                    //current level dist * no. of laps

    public int playerPosi = 1;
    
    public int enemyPosiL,enemyPosiR;
    public TextMeshProUGUI enemyPosTXTL,enemyPosTXTR;
    
    private void Start()
    {
        uFillImage[0] = LevelManager.Instance._uiManager.playerProgressLine.GetComponent<RectTransform>();
        uFillImage[1] = LevelManager.Instance._uiManager.enemyLProgressLine.GetComponent<RectTransform>();
        uFillImage[2] = LevelManager.Instance._uiManager.enemyRightProgressLine.GetComponent<RectTransform>();

        LevelManager.Instance._uiManager.wombatLogoPlayer.GetComponent<Image>().color =
            LevelManager.Instance._playerVehicleManager.universalCarColor;
        LevelManager.Instance._uiManager.wombatLogoPlayer.transform.GetChild(0).GetComponent<RawImage>().color = 
            LevelManager.Instance._playerVehicleManager.universalCarColor;
        
        LevelManager.Instance._uiManager.wombatLogoenemyLeft.GetComponent<Image>().color =
            LevelManager.Instance.enemyLeftVisual.GetComponent<VehicleManager>().universalCarColor;
        LevelManager.Instance._uiManager.wombatLogoenemyLeft.transform.GetChild(0).GetComponent<RawImage>().color = 
            LevelManager.Instance.enemyLeftVisual.GetComponent<VehicleManager>().universalCarColor;
        
        LevelManager.Instance._uiManager.wombatLogoenemyRight.GetComponent<Image>().color =
            LevelManager.Instance.enemyRightVisual.GetComponent<VehicleManager>().universalCarColor;
        LevelManager.Instance._uiManager.wombatLogoenemyRight.transform.GetChild(0).GetComponent<RawImage>().color = 
            LevelManager.Instance.enemyRightVisual.GetComponent<VehicleManager>().universalCarColor;
        
        totalLevelDistance = LevelManager.Instance.singleLapDistance * LevelManager.Instance.totalLaps;
        
        playerPosi = 1;
    }
    

    

    private void Update()
    {
    
        //PLAYER POSITION CALCULATION
        UIManager.Instance.playerPosition.text = playerPosi.ToString();
        
        if (chars[0].distanceTravelled > chars[1].distanceTravelled &&
            chars[0].distanceTravelled > chars[2].distanceTravelled)
        {
            playerPosi = 1;
        }
        
        if (chars[0].distanceTravelled < chars[1].distanceTravelled &&
            chars[0].distanceTravelled < chars[2].distanceTravelled)
        {
            playerPosi = 3;
        }
        
        if ((chars[0].distanceTravelled > chars[1].distanceTravelled &&
            chars[0].distanceTravelled < chars[2].distanceTravelled) || (chars[0].distanceTravelled < chars[1].distanceTravelled &&
                                                                         chars[0].distanceTravelled > chars[2].distanceTravelled))
        {
            playerPosi = 2;
        }
        
        
        
        float progressValue0 = Mathf.InverseLerp(0f,totalLevelDistance,
            chars[0].distanceTravelled);
        
        float progressValue1 = Mathf.InverseLerp(0f,totalLevelDistance,
            chars[1].distanceTravelled);
        
        float progressValue2 = Mathf.InverseLerp(0f,totalLevelDistance,
            chars[2].distanceTravelled);
        
        UpdateProgressFill(progressValue0,uFillImage[0]);
        UpdateProgressFill(progressValue1,uFillImage[1]);
        UpdateProgressFill(progressValue2,uFillImage[2]);
        
    }
    
    void UpdateProgressFill(float value,RectTransform myTransform)
    {
        myTransform.GetComponent<Slider>().value = value;
        
    }
    
}
