using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LevelSelection : MonoBehaviour
{
    public GameObject nextBtn, prevBtn, racebtn;
    public TextMeshProUGUI levelDetailTxt;

    [Header("All Levels")]
    public GameObject[] allLevelsGO;

    public int index;

    public Material daySkyboxmat, nightSkyboxmat;
    public Color daycolor, nightColor;

    /// <summary>
    /// ORDER OF LEVELS - 0.LONDON DAY ; 1.LONDON NIGHT
    /// </summary>
    private void Start()
    {
        index = 0;
        DAYMODE();
    }

    void DAYMODE()
    {
        RenderSettings.skybox = daySkyboxmat;
        RenderSettings.fogColor = daycolor;
    }

    void NIGHTMODE()
    {
        RenderSettings.skybox = nightSkyboxmat;
        RenderSettings.fogColor = nightColor;
    }

    public void nextIndex()
    {
        index++;
        StartCoroutine("Index");
    }

    public void prevIndex()
    {
        index--;
        if (index < 0)
            index = 0;
        StartCoroutine("Index");
    }

    IEnumerator Index()
    {
        yield return  new WaitForSeconds(0f);
        if (index == 0)                                    //london day
        {
            DAYMODE();
            levelDetailTxt.text = " LONDON DAY";
            allLevelsGO[0].SetActive(true);
        
            for (int i = 0; i < allLevelsGO.Length; i++)
            {
                if(i !=0)
                    allLevelsGO[i].SetActive(false);
            }
            allLevelsGO[0].transform.GetChild(0).gameObject.SetActive(true);
            allLevelsGO[0].transform.GetChild(1).gameObject.SetActive(false);
        }
        
        if (index == 1)                                    //london night
        {
            NIGHTMODE();
            levelDetailTxt.text = " LONDON NIGHT";
            allLevelsGO[0].SetActive(true);
        
            for (int i = 0; i < allLevelsGO.Length; i++)
            {
                if(i != 0)
                    allLevelsGO[i].SetActive(false);
            }
            allLevelsGO[0].transform.GetChild(1).gameObject.SetActive(true);
            allLevelsGO[0].transform.GetChild(0).gameObject.SetActive(false);
        }
        
        
    }
}
