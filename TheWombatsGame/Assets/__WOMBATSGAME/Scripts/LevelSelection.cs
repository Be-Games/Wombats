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

    public string currentSceneName;
    public int lightingIndex;

    [SerializeField] private GameObject _gameManager;

    /// <summary>
    /// ORDER OF LEVELS - 0.LONDON DAY ; 1.LONDON NIGHT
    /// </summary>
    private void Start()
    {
        _gameManager = GameObject.FindWithTag("GameManager");
        
        index = 0;
        DAYMODE();
        
        //default
        currentSceneName = "LONDON";
        lightingIndex = 1;
    }

    private void Update()
    {
        prevBtn.SetActive(index != 0);

        nextBtn.SetActive(index != (allLevelsGO.Length * 2)-1);
    }

    void DAYMODE()
    {
        RenderSettings.skybox = daySkyboxmat;
        RenderSettings.ambientSkyColor = daycolor;
    }

    void NIGHTMODE()
    {
        RenderSettings.skybox = nightSkyboxmat;
        RenderSettings.ambientSkyColor = nightColor;
    }

    public void nextIndex()
    {
        index++;
        if (index > allLevelsGO.Length * 2)
            index = allLevelsGO.Length * 2;
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
            currentSceneName = "LONDON";
            lightingIndex = 1;
            
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
            currentSceneName = "LONDON";
            lightingIndex = 2;
            
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
        
        if (index == 2)                                    //ROME day
        {
            currentSceneName = "ROME";
            lightingIndex = 1;
            
            DAYMODE();
            levelDetailTxt.text = " ROME DAY";
            allLevelsGO[1].SetActive(true);
        
            for (int i = 0; i < allLevelsGO.Length; i++)
            {
                if(i !=1)
                    allLevelsGO[i].SetActive(false);
            }
            allLevelsGO[1].transform.GetChild(0).gameObject.SetActive(true);
            allLevelsGO[1].transform.GetChild(1).gameObject.SetActive(false);
        }
        
        if (index == 3)                                    //ROME night
        {
            currentSceneName = "ROME";
            lightingIndex = 2;
            
            NIGHTMODE();
            levelDetailTxt.text = " ROME NIGHT";
            allLevelsGO[1].SetActive(true);
        
            for (int i = 0; i < allLevelsGO.Length; i++)
            {
                if(i != 1)
                    allLevelsGO[i].SetActive(false);
            }
            allLevelsGO[1].transform.GetChild(1).gameObject.SetActive(true);
            allLevelsGO[1].transform.GetChild(0).gameObject.SetActive(false);
        }
        
        if (index == 4)                                    //paaris day
        {
            currentSceneName = "PARIS";
            lightingIndex = 1;
            
            DAYMODE();
            levelDetailTxt.text = " PARIS DAY";
            allLevelsGO[2].SetActive(true);
        
            for (int i = 0; i < allLevelsGO.Length; i++)
            {
                if(i !=2)
                    allLevelsGO[i].SetActive(false);
            }
            allLevelsGO[2].transform.GetChild(0).gameObject.SetActive(true);
            allLevelsGO[2].transform.GetChild(1).gameObject.SetActive(false);
        }
        
        if (index == 5)                                    //paris night
        {
            currentSceneName = "PARIS";
            lightingIndex = 2;
            
            NIGHTMODE();
            levelDetailTxt.text = " PARIS NIGHT";
            allLevelsGO[2].SetActive(true);
        
            for (int i = 0; i < allLevelsGO.Length; i++)
            {
                if(i != 2)
                    allLevelsGO[i].SetActive(false);
            }
            allLevelsGO[2].transform.GetChild(1).gameObject.SetActive(true);
            allLevelsGO[2].transform.GetChild(0).gameObject.SetActive(false);
        }
        
        if (index == 6)                                    //SYDNEY day
        {
            currentSceneName = "SYDNEY";
            lightingIndex = 1;
            
            DAYMODE();
            levelDetailTxt.text = " SYDNEY DAY";
            allLevelsGO[3].SetActive(true);
        
            for (int i = 0; i < allLevelsGO.Length; i++)
            {
                if(i !=3)
                    allLevelsGO[i].SetActive(false);
            }
            allLevelsGO[3].transform.GetChild(0).gameObject.SetActive(true);
            allLevelsGO[3].transform.GetChild(1).gameObject.SetActive(false);
        }
        
        if (index == 7)                                    //SYDNEY night
        {
            currentSceneName = "SYDNEY";
            lightingIndex = 2;
            
            NIGHTMODE();
            levelDetailTxt.text = " SYDNEY NIGHT";
            allLevelsGO[3].SetActive(true);
        
            for (int i = 0; i < allLevelsGO.Length; i++)
            {
                if(i != 3)
                    allLevelsGO[i].SetActive(false);
            }
            allLevelsGO[3].transform.GetChild(1).gameObject.SetActive(true);
            allLevelsGO[3].transform.GetChild(0).gameObject.SetActive(false);
        }
        
        if (index == 8)                                    //EGYPT day
        {
            currentSceneName = "EGYPT";
            lightingIndex = 1;
            
            DAYMODE();
            levelDetailTxt.text = " EGYPT DAY";
            allLevelsGO[4].SetActive(true);
        
            for (int i = 0; i < allLevelsGO.Length; i++)
            {
                if(i !=4)
                    allLevelsGO[i].SetActive(false);
            }
            allLevelsGO[4].transform.GetChild(0).gameObject.SetActive(true);
            allLevelsGO[4].transform.GetChild(1).gameObject.SetActive(false);
        }
        
        if (index == 9)                                    //EGYPT night
        {
            currentSceneName = "EGYPT";
            lightingIndex = 2;
            
            NIGHTMODE();
            levelDetailTxt.text = " EGYPT NIGHT";
            allLevelsGO[4].SetActive(true);
        
            for (int i = 0; i < allLevelsGO.Length; i++)
            {
                if(i != 4)
                    allLevelsGO[i].SetActive(false);
            }
            allLevelsGO[4].transform.GetChild(1).gameObject.SetActive(true);
            allLevelsGO[4].transform.GetChild(0).gameObject.SetActive(false);
        }
        
        
    }
    
    //test car models
    public GameObject p, e1, e2;

    public void RaceBtn()
    {
        //set player and enemy cars from prefabs
        _gameManager.GetComponent<GameManager>().playerCarModels = p;
        
        _gameManager.GetComponent<GameManager>().enemyCarModels.Add(e1);
        _gameManager.GetComponent<GameManager>().enemyCarModels.Add(e2);
        
        _gameManager.GetComponent<GameManager>().lightingMode = lightingIndex;
        _gameManager.GetComponent<GameManager>().LoadScene(currentSceneName);
        
    }
}
