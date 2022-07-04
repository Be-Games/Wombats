using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using BayatGames.SaveGameFree;
using BayatGames.SaveGameFree.Examples;
using BayatGames.SaveGameFree.Serializers;
using BayatGames.SaveGameFree.Types;
using DataManager;
using DG.Tweening;
using Lofelt.NiceVibrations;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Random = UnityEngine.Random;
using NiobiumStudios;

public class MainMenuManager : MonoBehaviour
{
    
    [Header("Main Menu Panels")] 
    public RectTransform HomeScreen;
    public Image TitleImg;
    public GameObject settingsBtn, playBtn;


    public Material homeRoadTexture;
    public float xOffset;
    private bool canOffset = false;

    public DOTweenAnimation leftCar,centreCar,rightCar;

    public Material[] leftcarMat, centreCarMat, rightCarMat;//y,b,r
    public MeshRenderer leftCarMesh, centreCarMesh, rightCarMesh;
    public ParticleSystem leftCarPS, centreCarPS, rightCarPS;
    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        
        if(PlayerPrefs.GetInt("isTutShown")==1)
            DailyRewards.instance.onClaimPrize += OnClaimPrizeDailyRewards;
    }
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        canOffset = true;
    }

    private void Awake()
    {
        CarColorSwitch();
    }
    

    void OnDisable()
    {
        DailyRewards.instance.onClaimPrize -= OnClaimPrizeDailyRewards;
    }

    // this is your integration function. Can be on Start or simply a function to be called
    public void OnClaimPrizeDailyRewards(int day)
    {
        //This returns a Reward object
        Reward myReward = DailyRewards.instance.GetReward(day);

        // And you can access any property
        print(myReward.unit);   // This is your reward Unit name
        print(myReward.reward); // This is your reward count

        var rewardsCount = PlayerPrefs.GetInt ("MyTotalCoins", 0);
        rewardsCount += myReward.reward;

        PlayerPrefs.SetInt("MyTotalCoins", PlayerPrefs.GetInt("MyTotalCoins") + rewardsCount);
        
        PlayerPrefs.Save ();
    }
    

    private void Start()
    {
        
        
        
        
        xOffset = 0;
        
        HomeScreen.GetComponent<Image>().DOFade(0f, 0f).SetEase(Ease.Flash);
        TitleImg.GetComponent<Image>().DOFade(0f, 0f).SetEase(Ease.Flash);

        settingsBtn.GetComponent<Image>().DOFade(0f, 0f).SetEase(Ease.Flash);
        //playBtn.GetComponent<TextMeshProUGUI>().DOFade(0f, 0f).SetEase(Ease.Flash);
        settingsBtn.GetComponent<Button>().enabled = false;
        //playBtn.GetComponent<Button>().enabled = false;

       
        
        StartCoroutine("HomeScreenEnable");

        
        StartCoroutine(UpdateRoadOffset());
        

    }

    void CarColorSwitch()
    {
        int randomCase = Random.Range(0, 3);

        switch (randomCase)
        {
            
            case 0:                                                                //y,b,r
                leftCarMesh.material = leftcarMat[0];
                ParticleSystem.MainModule settings = leftCarPS.transform.GetChild(0).GetComponent<ParticleSystem>().main;
                settings.startColor = new ParticleSystem.MinMaxGradient( new Color(1,0.92f,0.016f,0.4f));
                
                centreCarMesh.material = centreCarMat[1];
                ParticleSystem.MainModule settings2 = centreCarPS.transform.GetChild(0).GetComponent<ParticleSystem>().main;
                settings2.startColor = new ParticleSystem.MinMaxGradient( new Color(0,0,1,0.4f));
                
                rightCarMesh.material = rightCarMat[2];
                ParticleSystem.MainModule settings3 = rightCarPS.transform.GetChild(0).GetComponent<ParticleSystem>().main;
                settings3.startColor = new ParticleSystem.MinMaxGradient( new Color(1,0,0,0.4f));
                
                break;
            case 1:                                                                //b,r,y
                leftCarMesh.material = leftcarMat[1];
                ParticleSystem.MainModule settings4 = leftCarPS.transform.GetChild(0).GetComponent<ParticleSystem>().main;
                settings4.startColor = new ParticleSystem.MinMaxGradient( new Color(0,0,1,0.4f));
                
                centreCarMesh.material = centreCarMat[2];
                ParticleSystem.MainModule settings5 = centreCarPS.transform.GetChild(0).GetComponent<ParticleSystem>().main;
                settings5.startColor = new ParticleSystem.MinMaxGradient( new Color(1,0,0,0.4f));
                
                rightCarMesh.material = rightCarMat[0];
                ParticleSystem.MainModule settings6 = rightCarPS.transform.GetChild(0).GetComponent<ParticleSystem>().main;
                settings6.startColor = new ParticleSystem.MinMaxGradient( new Color(1,0.92f,0.016f,0.4f));
                break;
            
            case 2:                                                                //r,y,b
                leftCarMesh.material = leftcarMat[2];
                ParticleSystem.MainModule settings7 = leftCarPS.transform.GetChild(0).GetComponent<ParticleSystem>().main;
                settings7.startColor = new ParticleSystem.MinMaxGradient( new Color(1,0,0,0.4f));
                
                centreCarMesh.material = centreCarMat[0];
                ParticleSystem.MainModule settings8 = centreCarPS.transform.GetChild(0).GetComponent<ParticleSystem>().main;
                settings8.startColor = new ParticleSystem.MinMaxGradient( new Color(1,0.92f,0.016f,0.4f));
                
                rightCarMesh.material = rightCarMat[1];
                ParticleSystem.MainModule settings9 = rightCarPS.transform.GetChild(0).GetComponent<ParticleSystem>().main;
                settings9.startColor = new ParticleSystem.MinMaxGradient( new Color(0,0,1,0.4f));
                
                break;
        }
    }
    
    IEnumerator HomeScreenEnable()
    {
        yield return new WaitForSeconds(0.5f);
        HomeScreen.GetComponent<Image>().DOFade(1f, 2f).SetEase(Ease.Flash);
        TitleImg.GetComponent<Image>().DOFade(1f, 2f).SetEase(Ease.Flash);

        

        yield return new WaitForSeconds(0);
    }

    IEnumerator UpdateRoadOffset()
    {
        DOTween.To(() => xOffset, 
                x => xOffset = x, 100000, 30f).OnUpdate(RoadUpdate).OnComplete(() =>
        {
            xOffset = 0;
            StartCoroutine(UpdateRoadOffset());
        });

        yield return null;

    }

    void RoadUpdate()
    {
        if(canOffset)
            homeRoadTexture.mainTextureOffset = new Vector2(0f, xOffset);
        
        
    }

  

    void CompleteRest()
    {
        settingsBtn.GetComponent<Image>().DOFade(1f, 2f).SetEase(Ease.Flash);
       // playBtn.GetComponent<TextMeshProUGUI>().DOFade(1f, 2f).SetEase(Ease.Flash);
        settingsBtn.GetComponent<Button>().enabled = true;
        //playBtn.GetComponent<Button>().enabled = true;
    }

    public void PlayGame()
    {

        UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.GetComponent<Button>().enabled = false;
        
        leftCar.DOKill();
        leftCar.transform.DOMoveZ(20, 2f).SetEase(Ease.InOutSine).SetRelative(true).OnComplete(LoadLevel);;
        
        rightCar.DOKill();
        rightCar.transform.DOMoveZ(20, 2f).SetEase(Ease.InOutSine).SetRelative(true);
        
        centreCar.DOKill();
        centreCar.transform.DOMoveZ(20, 2f).SetEase(Ease.InOutSine).SetRelative(true);


    }

    void LoadLevel()
    {

        if (PlayerPrefs.GetInt("isTutShown") == 0)
       {
           GameManager.Instance.LoadScene("Tutorial");
           PlayerPrefs.SetInt("isTutShown",1);
       }
       
       else if (PlayerPrefs.GetInt("isTutShown") == 1)
       {
           PlayerPrefs.SetInt("isGarage",0);
           /*PlayerPrefs.SetInt("isTutShown",2);*/
           GameManager.Instance.LoadScene("LevelSelection");
           
       }
        /*else if (PlayerPrefs.GetInt("isTutShown") == 2)
        {
            GameManager.Instance.LoadScene("LevelSelection");
        }*/
      
       PlayerPrefs.Save();
    }

    public void ButtonClick()
    {
        GameManager.Instance.ButtonClick();
    }

    public void StartBtnSound()
    {
        if(AudioManager.Instance.isSFXenabled)
            AudioManager.Instance.sfxAll.carEngineStartScreen.PlayOneShot(AudioManager.Instance.sfxAll.carEngineStartScreen.clip);
    }
    
}
