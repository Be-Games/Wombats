using System;
using System.Collections;
using System.Collections.Generic;
using DataManager;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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
    
    public GameObject lockImage;
    public Button raceBtn;
    private string path;

    public GameObject garageBtn, playerSelectBtn;

    public GameObject enterContestBtn;

    public GameObject nightLightsGO;
    

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    // called second
    
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        
        if(PlayerPrefs.GetInt("LevelIndex") == 0)
            PlayerPrefs.SetInt("LevelIndex",2);   //change to 2
        
       
        
        index = PlayerPrefs.GetInt("LevelIndex")-1;
        
    }

    public void LoadGarage()
    {
        UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.GetComponent<Button>().enabled = false;
        
        PlayerPrefs.SetInt("isGarage",1);
        GameManager.Instance.GetComponent<GameManager>().LoadScene("PlayerSelection");
    }

    public void LoadPlayerSelect()
    {
        UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.GetComponent<Button>().enabled = false;
        
        PlayerPrefs.SetInt("isGarage",0);
        GameManager.Instance.GetComponent<GameManager>().LoadScene("PlayerSelection");
    }
    
    private void Start()
    {
        /*garageBtn.SetActive(false);
        playerSelectBtn.SetActive(false);*/
        StartCoroutine("Index");
        enterContestBtn.SetActive(false);
        
    }

    public void menuBtn()
    {
        if (garageBtn.activeInHierarchy)
        {
            garageBtn.SetActive(false);
            playerSelectBtn.SetActive(false);
        }
        else if (!garageBtn.activeInHierarchy)
        {
            garageBtn.SetActive(true);
            playerSelectBtn.SetActive(true);
        }
    }

    private void Update()
    {
        if (GameManager.Instance.isThisTheFinalLevel)
        {
            enterContestBtn.SetActive(true);
        }

        prevBtn.SetActive(index != 0);

        nextBtn.SetActive(index != (20)-1);
        
        for (int i = 1; i <= 19; i++)
        {
            if ( PlayerPrefs.GetInt("LevelIndex") == i)
            {
                if(index >= i)
                    lockImage.SetActive(true);
                else
                {
                    lockImage.SetActive(false);
                }
            }
        }
        
        if (lockImage.activeInHierarchy)
        {
            racebtn.GetComponent<Button>().enabled = false;
        }
        else
        {
            racebtn.GetComponent<Button>().enabled = true;
        }

        


    }

    public void EnterContest()
    {
        Application.OpenURL("https://www.toneden.io/the-wombats-5/post/the-wombats-official-game-competition");
    }

    void DAYMODE()
    {
        //RenderSettings.skybox = daySkyboxmat;
        RenderSettings.ambientSkyColor = daycolor;
        RenderSettings.fog = false;
        nightLightsGO.SetActive(false);
    }

    void NIGHTMODE()
    {
        //RenderSettings.skybox = nightSkyboxmat;
        RenderSettings.ambientSkyColor = nightColor;
        RenderSettings.fog = true;
        nightLightsGO.SetActive(true);
    }

    public void nextIndex()
    {
        index++;
        if (index > 20)
            index = 20;
        //allLevelsGO[index-1].SetActive(false);
        StartCoroutine("Index");
    }

    public void prevIndex()
    {
        index--;
        if (index < 0)
            index = 0;
        //allLevelsGO[index+1].SetActive(false);
        StartCoroutine("Index");
    }

    IEnumerator Index()
    {
        yield return null;
        
        if (index == 0)                                    //tutorial
        {

            currentSceneName = "Tutorial";
            DAYMODE();
            levelDetailTxt.text = " TUTORIAL ";
            allLevelsGO[0].SetActive(true);

            for (int i = 0; i < 11; i++)
            {
              if(i != 0)
                  allLevelsGO[i].SetActive(false); 
            }
        }
        
        if (index == 1)                                    //london day
        {

            currentSceneName = "LONDON";
            lightingIndex = 1;
            
            DAYMODE();
            levelDetailTxt.text = " LONDON DAY";
            allLevelsGO[1].SetActive(true);
            
            for (int i = 0; i < 11; i++)
            {
                if(i != 1)
                    allLevelsGO[i].SetActive(false); 
            }
        }
        
        if (index == 2)                                    //ROME NIGHT
        {

            currentSceneName = "ROME";
            lightingIndex = 2;
            
            NIGHTMODE();
            levelDetailTxt.text = " ROME NIGHT";
            allLevelsGO[2].SetActive(true);
            
            for (int i = 0; i < 11; i++)
            {
                if(i != 2)
                    allLevelsGO[i].SetActive(false); 
            }
            
        }
        
        //
        
        if (index == 3)                                    //london day
        {

            currentSceneName = "SYDNEY";
            lightingIndex = 1;
            
            DAYMODE();
            levelDetailTxt.text = " SYDNEY DAY";
            allLevelsGO[3].SetActive(true);
            
            for (int i = 0; i < 11; i++)
            {
                if(i != 3)
                    allLevelsGO[i].SetActive(false); 
            }
            
        }
        
        
        //
        if (index == 4)                                    //london day
        {

            currentSceneName = "PARIS";
            lightingIndex = 2;
            
            NIGHTMODE();
            levelDetailTxt.text = " PARIS NIGHT";
            allLevelsGO[4].SetActive(true);
            
            for (int i = 0; i < 11; i++)
            {
                if(i != 4)
                    allLevelsGO[i].SetActive(false); 
            }
            
        }
        
        if (index == 5)                                    //ROME NIGHT
        {

            currentSceneName = "EGYPT";
            lightingIndex = 1;
            
            DAYMODE();
            levelDetailTxt.text = " EGYPT DAY";
            allLevelsGO[5].SetActive(true);
            
            for (int i = 0; i < 11; i++)
            {
                if(i != 5)
                    allLevelsGO[i].SetActive(false); 
            }
            
        }
        
        //
        
        if (index == 6)                                    //london day
        {

            currentSceneName = "CARDIFF";
            lightingIndex = 2;
            
            NIGHTMODE();
            levelDetailTxt.text = " CARDIFF NIGHT";
            allLevelsGO[6].SetActive(true);
            
            for (int i = 0; i < 11; i++)
            {
                if(i != 6)
                    allLevelsGO[i].SetActive(false); 
            }
            
        }
        
        if (index == 7)                                    //ROME NIGHT
        {

            currentSceneName = "GLASGOW";
            lightingIndex = 1;
            
            DAYMODE();
            levelDetailTxt.text = " GLASGOW DAY";
            allLevelsGO[7].SetActive(true);
            
            for (int i = 0; i < 11; i++)
            {
                if(i != 7)
                    allLevelsGO[i].SetActive(false); 
            }
            
        }
        
        if (index == 8)                                    //london day
        {

            currentSceneName = "TOKYO";
            lightingIndex = 2;
            
            NIGHTMODE();
            levelDetailTxt.text = " TOKYO NIGHT";
            allLevelsGO[8].SetActive(true);
            
            for (int i = 0; i < 11; i++)
            {
                if(i != 8)
                    allLevelsGO[i].SetActive(false); 
            }
            
        }
        
        if (index == 9)                                    //ROME NIGHT
        {

            currentSceneName = "MILAN";
            lightingIndex = 1;
            
            DAYMODE();
            levelDetailTxt.text = " MILAN DAY";
            allLevelsGO[9].SetActive(true);
            
            for (int i = 0; i < 11; i++)
            {
                if(i != 9)
                    allLevelsGO[i].SetActive(false); 
            }
            
        }
        
        //
        
        if (index == 10)                                    //london day
        {

            currentSceneName = "LONDON";
            lightingIndex = 2;
            
            NIGHTMODE();
            levelDetailTxt.text = " LONDON NIGHT";
            allLevelsGO[1].SetActive(true);
            
            for (int i = 0; i < 11; i++)
            {
                if(i != 1)
                    allLevelsGO[i].SetActive(false); 
            }
            
        }
        
        if (index == 11)                                    //ROME NIGHT
        {

            currentSceneName = "ROME";
            lightingIndex = 1;
            
            DAYMODE();
            levelDetailTxt.text = " ROME DAY";
            allLevelsGO[2].SetActive(true);
            
            for (int i = 0; i < 11; i++)
            {
                if(i != 2)
                    allLevelsGO[i].SetActive(false); 
            }
            
        }
        
        //
        
        if (index == 12)                                    //london day
        {

            currentSceneName = "SYDNEY";
            lightingIndex = 2;
            
            NIGHTMODE();
            levelDetailTxt.text = " SYDNEY NIGHT";
            allLevelsGO[3].SetActive(true);
            
            for (int i = 0; i < 11; i++)
            {
                if(i != 3)
                    allLevelsGO[i].SetActive(false); 
            }
            
        }
        
        
        //
        if (index == 13)                                    //london day
        {

            currentSceneName = "PARIS";
            lightingIndex = 1;
            
            DAYMODE();
            levelDetailTxt.text = " PARIS DAY";
            allLevelsGO[4].SetActive(true);
            
            for (int i = 0; i < 11; i++)
            {
                if(i != 4)
                    allLevelsGO[i].SetActive(false); 
            }
            
        }
        
        if (index == 14)                                    //ROME NIGHT
        {

            currentSceneName = "EGYPT";
            lightingIndex = 2;
            
            NIGHTMODE();
            levelDetailTxt.text = " EGYPT NIGHT";
            allLevelsGO[5].SetActive(true);
            
            for (int i = 0; i < 11; i++)
            {
                if(i != 5)
                    allLevelsGO[i].SetActive(false); 
            }
            
        }
        
        //
        
        if (index == 15)                                    //london day
        {

            currentSceneName = "CARDIFF";
            lightingIndex = 1;
            
            DAYMODE();
            levelDetailTxt.text = " CARDIFF DAY";
            allLevelsGO[6].SetActive(true);
            
            for (int i = 0; i < 11; i++)
            {
                if(i != 6)
                    allLevelsGO[i].SetActive(false); 
            }
            
        }
        
        if (index == 16)                                    //ROME NIGHT
        {

            currentSceneName = "GLASGOW";
            lightingIndex = 2;
            
            NIGHTMODE();
            levelDetailTxt.text = " GLASGOW NIGHT";
            allLevelsGO[7].SetActive(true);
            
            for (int i = 0; i < 11; i++)
            {
                if(i != 7)
                    allLevelsGO[i].SetActive(false); 
            }
            
        }
        
        if (index == 17)                                    //london day
        {

            currentSceneName = "TOKYO";
            lightingIndex = 1;
            
            DAYMODE();
            levelDetailTxt.text = " TOKYO DAY";
            allLevelsGO[8].SetActive(true);
            
            for (int i = 0; i < 11; i++)
            {
                if(i != 8)
                    allLevelsGO[i].SetActive(false); 
            }
            
        }
        
        if (index == 18)                                    //ROME NIGHT
        {

            currentSceneName = "LIVERPOOL";
            lightingIndex = 1;
            
            DAYMODE();
            levelDetailTxt.text = " LIVERPOOL DAY";
            allLevelsGO[10].SetActive(true);
            
            for (int i = 0; i < 11; i++)
            {
                if(i != 10)
                    allLevelsGO[i].SetActive(false); 
            }
            
        }
        
        if (index == 19)                                    //ROME NIGHT
        {

            currentSceneName = "MILAN";
            lightingIndex = 2;
            
            NIGHTMODE();
            levelDetailTxt.text = " MILAN NIGHT";
            allLevelsGO[9].SetActive(true);
            
            for (int i = 0; i < 11; i++)
            {
              if(i != 9)
                  allLevelsGO[i].SetActive(false); 
            }
            
        }
        
        //
    }
    
    
   
    public void RaceBtn()
    {
        if (currentSceneName != "Tutorial")
        {
            GameManager.Instance.GetComponent<GameManager>().lightingMode = lightingIndex;
            
        }
        
        GameManager.Instance.GetComponent<GameManager>().LoadScene(currentSceneName);

    }

    public void playTut(string sceneName)
    {
        GameManager.Instance.GetComponent<GameManager>().LoadScene(sceneName);
    }

    public void Vibrate()
    {
        //GameManager.Instance.VibrateOnce();
    }

    public void backToPlayerSelection()
    {
        GameManager.Instance.LoadScene("PlayerSelection");
    }
    
    public void ButtonClick()
    {
        GameManager.Instance.ButtonClick();
    }
    
}



