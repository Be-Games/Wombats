using System;
using System.Collections;
using System.Collections.Generic;
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



    private void Start()
    {
        HomeScreen.GetComponent<Image>().DOFade(0f, 0f).SetEase(Ease.Flash);
        TitleImg.GetComponent<Image>().DOFade(0f, 0f).SetEase(Ease.Flash);

        settingsBtn.GetComponent<Image>().DOFade(0f, 0f).SetEase(Ease.Flash);
        playBtn.GetComponent<Text>().DOFade(0f, 0f).SetEase(Ease.Flash);
        settingsBtn.GetComponent<Button>().enabled = false;
        playBtn.GetComponent<Button>().enabled = false;

        
        
        StartCoroutine("HomeScreenEnable");
        
    }

    IEnumerator HomeScreenEnable()
    {
        yield return new WaitForSeconds(0.5f);
        HomeScreen.GetComponent<Image>().DOFade(1f, 2f).SetEase(Ease.Flash);
        TitleImg.GetComponent<Image>().DOFade(1f, 2f).SetEase(Ease.Flash).OnComplete(CompleteRest);

        

        yield return new WaitForSeconds(0);
    }

    void CompleteRest()
    {
        settingsBtn.GetComponent<Image>().DOFade(1f, 2f).SetEase(Ease.Flash);
        playBtn.GetComponent<Text>().DOFade(1f, 2f).SetEase(Ease.Flash);
        settingsBtn.GetComponent<Button>().enabled = true;
        playBtn.GetComponent<Button>().enabled = true;
    }
    


}
