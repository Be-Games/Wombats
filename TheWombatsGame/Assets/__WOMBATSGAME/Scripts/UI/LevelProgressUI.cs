using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using PathCreation.Examples;
using UnityEngine;
using UnityEngine.UI;

public class LevelProgressUI : MonoBehaviour
{
    public RectTransform[] uFillImage;                         // 0 - player , 1 - enemy1 , 2 - enemy2

    public PathFollower[] chars;            // 0 - player , 1 - enemy1 , 2 - enemy2

    public float totalLevelDistance;                    //current level dist * no. of laps

    public int playerPosi = 1;
    
    private void Start()
    {

        totalLevelDistance = LevelManager.Instance.singleLapDistance * LevelManager.Instance.totalLaps;
        playerPosi = 1;
    }
    

    

    private void Update()
    {

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
        
        //PLAYER
        float progressValue0 = Mathf.InverseLerp(0f,totalLevelDistance,
            chars[0].distanceTravelled);
        
        UpdateProgressFill0(progressValue0);
        
        
        
    }
    
    void UpdateProgressFill0(float value)
    {
        uFillImage[0].GetComponent<Image>().fillAmount = value;
        
    }
    
}
