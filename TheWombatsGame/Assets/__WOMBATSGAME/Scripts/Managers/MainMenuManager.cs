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


    public Material homeRoadTexture;
    public float xOffset;

    private void Awake()
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

    IEnumerator HomeScreenEnable()
    {
        yield return new WaitForSeconds(0.5f);
        HomeScreen.GetComponent<Image>().DOFade(1f, 2f).SetEase(Ease.Flash);
        TitleImg.GetComponent<Image>().DOFade(1f, 2f).SetEase(Ease.Flash).OnComplete(CompleteRest);

        

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
        GameManager.Instance.LoadScene("PlayerSelection");
    }
    
    


}
