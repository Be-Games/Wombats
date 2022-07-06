using System;
using System.Collections;
using System.Collections.Generic;
using DataManager;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
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

    //public GameObject enterContestBtn;

    public GameObject nightLightsGO;

    //public GameObject jublieeSpecial;

    //public GameObject crownPanel;

    public GameObject[] wombatPlayer;
    public GameObject[] murphCars, danCars, tordCars;
    public GameObject murphPO, danPO, tordPO;

    public Transform[] levelImages;
    public GameObject[] musicImgs;
    public TextMeshProUGUI[] levelNameText, musicNameText;
    public Image bgImg;
    public Color[] bgImageColors;

    public Color selectedColorBtn, normalColorBtn;

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    // called second
    
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        
        /*if(PlayerPrefs.GetInt("LevelIndex") == 0)
            PlayerPrefs.SetInt("LevelIndex",2); */  //change to 2
        
       
        
        index = PlayerPrefs.GetInt("LevelIndex");

    }

    private void Awake()
    {
        foreach (var mem in wombatPlayer)
        {
            mem.SetActive(false);
        }

        foreach (var VARIABLE in murphCars)
        {
            VARIABLE.SetActive(false);
        }

        foreach (var VARIABLE in tordCars)
        {
            VARIABLE.SetActive(false);
        }

        foreach (var VARIABLE in danCars)
        {
            VARIABLE.SetActive(false);
        }

        foreach (var VARIABLE in musicImgs)
        {
            VARIABLE.SetActive(false);
        }
        
        murphPO.SetActive(false);
        danPO.SetActive(false);
        tordPO.SetActive(false);
        
        foreach (var lvlImg in levelImages)
        {
            lvlImg.transform.DOScaleX(0, 0);
        }
        foreach (var a in levelNameText)
        {
            a.transform.DOScaleX(0, 0);
        }
        foreach (var b in musicNameText)
        {
            b.transform.DOScaleX(0, 0);
        }

        foreach (var x in levelImages)
        {
            x.GetComponent<Button>().interactable = false;
            x.GetComponent<Image>().color = normalColorBtn;
        }
        
        StartCoroutine(SetPlayerAndCar());
        StartCoroutine(OpeningAnimation());
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
        StartCoroutine(MyUpdate());
    }

    private int i = 0;

    IEnumerator SetPlayerAndCar()
    {
        yield return new WaitForSeconds(0.1f);
        wombatPlayer[GameManager.Instance.memeberIndex].SetActive(true);

        if (wombatPlayer[0].activeInHierarchy)
        {
            bgImg.color = bgImageColors[0];
            murphPO.SetActive(true);
            murphCars[GameManager.Instance.selectedCarModelPLAYER-1].SetActive(true);
        }


        if (wombatPlayer[1].activeInHierarchy)
        {
            bgImg.color = bgImageColors[1];
            danPO.SetActive(true);
            danCars[GameManager.Instance.selectedCarModelPLAYER-1].SetActive(true);
        }


        if (wombatPlayer[2].activeInHierarchy)
        {
            bgImg.color = bgImageColors[2];
            tordPO.SetActive(true);
            tordCars[GameManager.Instance.selectedCarModelPLAYER-1].SetActive(true);
        }

    }
    
    IEnumerator OpeningAnimation()
    {
        yield return new WaitForSeconds(0.03f);
        levelImages[i].transform.DOScaleX(1.1f, 0.1f).OnComplete(() =>
        {

            levelNameText[i].transform.DOScaleX(1f, 0.1f).OnComplete(() =>
            {
                musicImgs[i].SetActive(true);
                musicNameText[i].transform.DOScaleX(1f, 0.1f).OnComplete(() =>
                {
                    i++;
                    if (i < 19)
                        StartCoroutine(OpeningAnimation());
                });
                

            });

        });

    }
    
    
    IEnumerator MyUpdate()
    {

        
        /*//JUBLIEE PANEL
        /*if (index == 1 || index == 10)
        {
            jublieeSpecial.SetActive(true);
        }
        else
        {
            jublieeSpecial.SetActive(false);
        }#1#
        
        /#1#/CROWN PANEL
        if (index == 1 || index == 2 || index == 3 || index == 4)
        {
            crownPanel.SetActive(true);
        }
        else
        {
            crownPanel.SetActive(false);
        }#1#
        
        
        /*if (GameManager.Instance.isThisTheFinalLevel)
        {
            enterContestBtn.SetActive(true);
        }#1#

        prevBtn.SetActive(index != 0);

        nextBtn.SetActive(index != (20)-1);*/
        
        /*for (int i = 1; i <= 19; i++)
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
        }*/

        for (int j = 0; j <= index; j++)
        {
            levelImages[j].GetComponent<Button>().interactable = true;
            if (j == index)
            {
                levelImages[j].GetComponent<Image>().color = Color.Lerp(normalColorBtn, selectedColorBtn,
                    Mathf.PingPong(Time.time * 2f, 2));
            }
               
        }
        


        yield return null;
        StartCoroutine(MyUpdate());


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

    /*IEnumerator Index()
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
    }*/
    
    
   
    /*public void RaceBtn()
    {
        if (currentSceneName != "Tutorial")
        {
            GameManager.Instance.GetComponent<GameManager>().lightingMode = lightingIndex;
            
        }
        
        GameManager.Instance.GetComponent<GameManager>().LoadScene(currentSceneName);

    }*/
    

    
    
    public void ButtonClick()
    {
        GameManager.Instance.ButtonClick();
    }

   

    /*/*public void CrownPanelToggle()
    {
        StartCoroutine(testmusic());
        
        if (!isPanel)
        {
            crownPanel.transform.DOScale(1.2f, 0.5f).SetEase(Ease.OutBounce);
            isPanel = true;
        }
        
        else if (isPanel)
        {
            crownPanel.transform.DOScale(0f, 0.5f).SetEase(Ease.OutBounce);
            isPanel = false;
        }
    }#1#

    IEnumerator testmusic()
    {
        
        UnityWebRequest www = UnityWebRequestMultimedia.GetAudioClip("https://file-examples.com/wp-content/uploads/2017/11/file_example_OOG_1MG.ogg", AudioType.OGGVORBIS);
        if (www.result == UnityWebRequest.Result.ConnectionError)
        {
            Debug.Log(www.error);
        }
        else
        {
            Debug.Log("Audio Loaded");
            AudioClip myClip = DownloadHandlerAudioClip.GetContent(www);
            AudioManager.Instance.musicTracks.MusicTrackAudioSource.clip = myClip;
        }

        yield return null;
    }
    
    public void DownloadSound(string url)
    {
        StartCoroutine(SoundRequest(url, (UnityWebRequest req) =>
        {
            if (req.isNetworkError || req.isHttpError)
            {
                Debug.Log($"{req.error}: {req.downloadHandler.text}");
            }
            else
            {
                // Get the sound out using a helper class
                AudioClip clip = DownloadHandlerAudioClip.GetContent(req);
                // Load the clip into our audio source and play
                AudioManager.Instance.musicTracks.MusicTrackAudioSource.Stop();
                AudioManager.Instance.musicTracks.MusicTrackAudioSource.clip = clip;
                AudioManager.Instance.musicTracks.MusicTrackAudioSource.Play();
                Debug.Log("GT PLaying ");
            }
        }));
    }

    IEnumerator SoundRequest(string url, Action<UnityWebRequest> callback)
    {
        // Note, we try to download an OGGVORBIS (ogg) file because Windows doesn't support
        // MPEG readily. If you're on a mac, you can try MPEG (mp3)
        using (UnityWebRequest req = UnityWebRequestMultimedia.GetAudioClip
            ("https://cf-media.sndcdn.com/when46Cx8MCO.128.mp3?Policy=eyJTdGF0ZW1lbnQiOlt7IlJlc291cmNlIjoiKjovL2NmLW1lZGlhLnNuZGNkbi5jb20vd2hlbjQ2Q3g4TUNPLjEyOC5tcDMqIiwiQ29uZGl0aW9uIjp7IkRhdGVMZXNzVGhhbiI6eyJBV1M6RXBvY2hUaW1lIjoxNjU0MTUyMTUxfX19XX0_&Signature=akuMtQDeyGqduG760EfCAOIY-zrK0a17yOArA-CupkvQVcX1~B7s-u-42k6b6qzCkzrKTIo-tdZIz31aIOPmo~FOtTKDaDRau~BP5kZ52YlRmVylqcGZRGIDCjXQvGYFsTmolHB6cQ4Ku5BUqfZSup3eMcPN3Xafohlxylefkj~rHyfddrnqj~RD8k8NK1lUKj5RIlvB~mBhSu2RKODa6PCynka2IO3394wLA6Un-yXpB6OPJwYw1Yo6eEk1Po1l9ob1gzk2kWOvGN~vO7ud50tXBDPTHQ-FS-Is~OO2Zua6uWDEBkNRG2BnK0WkFMOm3nJ2YKUki3-BstV7JT2kNw__&Key-Pair-Id=APKAI6TU7MMXM5DG6EPQ", AudioType.MPEG))
        {
            yield return req.SendWebRequest();
            callback(req);
        }
    }*/

    public void LevelSelectedBtn()
    {
        UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.GetComponent<Button>().enabled = false;
        
        switch (UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.name)
        {
            case "LD":

                lightingIndex = 1;
                currentSceneName = "LONDON";
                break;
            case "RN":

                lightingIndex = 2;
                currentSceneName = "ROME";
                break;
            case "SD":

                lightingIndex = 1;
                currentSceneName = "SYDNEY";
                break;
            case "PN":

                lightingIndex = 2;
                currentSceneName = "PARIS";
                break;
            case "ED":

                lightingIndex = 1;
                currentSceneName = "EGYPT";
                break;
            case "CN":

                lightingIndex = 2;
                currentSceneName = "CARDIFF";
                break;
            case "GD":

                lightingIndex = 1;
                currentSceneName = "GLASGOW";
                break;
            case "TN":

                lightingIndex = 2;
                currentSceneName = "TOKYO";
                break;
            case "MD":

                lightingIndex = 1;
                currentSceneName = "MILAN";
                break;
            case "LN":

                lightingIndex = 2;
                currentSceneName = "LONDON";
                break;
            
            case "RD":

                lightingIndex = 1;
                currentSceneName = "ROME";
                break;
            case "SN":

                lightingIndex = 2;
                currentSceneName = "SYDNEY";
                break;
            case "PD":

                lightingIndex = 1;
                currentSceneName = "PARIS";
                break;
            case "EN":

                lightingIndex = 2;
                currentSceneName = "EGYPT";
                break;
            case "CD":

                lightingIndex = 1;
                currentSceneName = "CARDIFF";
                break;
            case "GN":

                lightingIndex = 2;
                currentSceneName = "GLASGOW";
                break;
            case "TD":

                lightingIndex = 1;
                currentSceneName = "TOKYO";
                break;
            case "LID":

                lightingIndex = 1;
                currentSceneName = "LIVERPOOL";
                break;
            case "MN":

                lightingIndex = 2;
                currentSceneName = "MILAN";
                break;
                
        }
        
        GameManager.Instance.GetComponent<GameManager>().lightingMode = lightingIndex;
        GameManager.Instance.GetComponent<GameManager>().LoadScene(currentSceneName);
    }
    
}





