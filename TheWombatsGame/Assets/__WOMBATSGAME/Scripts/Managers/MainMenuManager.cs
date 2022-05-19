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
    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        canOffset = true;
    }

    private void Start()
    {
        
        
        PlayerPrefs.GetInt("isTutShown",0);
        
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

    private void Update()
    {
       
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

        leftCar.DOKill();
        leftCar.transform.DOMoveZ(26, 2f).SetEase(Ease.InOutSine).SetRelative(true).OnComplete(LoadLevel);;
        
        rightCar.DOKill();
        rightCar.transform.DOMoveZ(26, 2f).SetEase(Ease.InOutSine).SetRelative(true);
        
        centreCar.DOKill();
        centreCar.transform.DOMoveZ(26, 2f).SetEase(Ease.InOutSine).SetRelative(true);


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
