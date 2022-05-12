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
                x => xOffset = x, 100000, 30f).OnComplete(() =>
        {
            xOffset = 0;
            StartCoroutine(UpdateRoadOffset());
        });

        yield return null;

    }

    private void Update()
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
        if (PlayerPrefs.GetInt("isTutShown") == 0)
        {
            GameManager.Instance.LoadScene("Tutorial");
            PlayerPrefs.SetInt("isTutShown",1);
        }
        
        else if (PlayerPrefs.GetInt("isTutShown") == 1)
        {
            PlayerPrefs.SetInt("isGarage",0);
            GameManager.Instance.LoadScene("PlayerSelection");
        }
       
        PlayerPrefs.Save();
    }
    
}
