using System;
using System.Collections;
using System.Collections.Generic;
using PathCreation.Examples;
using UnityEngine;
using UnityEngine.UI;

public class LevelProgressUI : MonoBehaviour
{
    public Image uiFillImage;
    
    public GameObject playerScriptGO;

    public float totalLevelDistance;
    public float singleLapDistance;
    private void Start()
    {
        totalLevelDistance = LevelManager.Instance.singleLapDistance * LevelManager.Instance.totalLaps;
    }

    void UpdateProgressFill(float value)
    {
        uiFillImage.fillAmount = value;
    }

    private void Update()
    {
        float progressValue = Mathf.InverseLerp(0f,totalLevelDistance,
            playerScriptGO.GetComponent<PathFollower>().distanceTravelled);
        
        UpdateProgressFill(progressValue);
    }
}
