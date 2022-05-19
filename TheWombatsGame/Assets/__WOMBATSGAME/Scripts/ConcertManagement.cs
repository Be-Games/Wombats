using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ConcertManagement : MonoBehaviour
{
    private GameManager _gameManager;
    public GameObject[] bandMemPf;             //1 = MM , 2 = dan , 3 = tord
    public Transform fP, sP, tP;
    public Transform parentF, parentS, parentT;
    
    public RuntimeAnimatorController first, second, third;

    public TextMeshProUGUI posText;

    public GameObject fullConcertPanel, RewardStuff, bottomBtns;
    public Button collectCoinsBtn,rewardCoinsBtn;
    public TextMeshProUGUI rewardedCoins;
    public TextMeshProUGUI adCoins;
    
    //exclusive for competition
    public GameObject lastLevelButtons;
    public Button EnterCompiBtn;
    private string URLforCompi;
    

    private void Start()
    {
        _gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();

        if (GameManager.Instance != null)
        { 
             if (GameManager.Instance.playerPosi == 1)
        {
            posText.text = "#1";
            
            if (GameManager.Instance.charNumber == 1 )
            {
                bandMemPf[0].GetComponent<Animator>().runtimeAnimatorController = first;                    //us
                Instantiate(bandMemPf[0], fP.transform.position,fP.transform.rotation,parentF);
            
                bandMemPf[1].GetComponent<Animator>().runtimeAnimatorController = second;
                Instantiate(bandMemPf[1], sP.transform.position,sP.transform.rotation,parentS);
        
                bandMemPf[2].GetComponent<Animator>().runtimeAnimatorController = third;
                Instantiate(bandMemPf[2], tP.transform.position,tP.transform.rotation,parentT);
            }
        
            if (GameManager.Instance.charNumber == 2)
            {
                bandMemPf[1].GetComponent<Animator>().runtimeAnimatorController = first;                    //us
                Instantiate(bandMemPf[1], fP.transform.position,fP.transform.rotation,parentF);
            
                bandMemPf[0].GetComponent<Animator>().runtimeAnimatorController = second;
                Instantiate(bandMemPf[0], sP.transform.position,sP.transform.rotation,parentS);
        
                bandMemPf[2].GetComponent<Animator>().runtimeAnimatorController = third;
                Instantiate(bandMemPf[2], tP.transform.position,tP.transform.rotation,parentT);
            }
        
            if (GameManager.Instance.charNumber == 3)
            {
                bandMemPf[2].GetComponent<Animator>().runtimeAnimatorController = first;                    //us
                Instantiate(bandMemPf[2], fP.transform.position,fP.transform.rotation,parentF);
            
                bandMemPf[0].GetComponent<Animator>().runtimeAnimatorController = second;
                Instantiate(bandMemPf[0], sP.transform.position,sP.transform.rotation,parentS);
        
                bandMemPf[1].GetComponent<Animator>().runtimeAnimatorController = third;
                Instantiate(bandMemPf[1], tP.transform.position,tP.transform.rotation,parentT);
            }
        }
        
             if (GameManager.Instance.playerPosi == 2)
        {
            posText.text = "#2";
            
            if (GameManager.Instance.charNumber == 1 )
            {
                bandMemPf[0].GetComponent<Animator>().runtimeAnimatorController = second;                //us
                Instantiate(bandMemPf[0], sP.transform.position,sP.transform.rotation,parentS);
            
                bandMemPf[1].GetComponent<Animator>().runtimeAnimatorController = first;
                Instantiate(bandMemPf[1], fP.transform.position,fP.transform.rotation,parentF);
        
                bandMemPf[2].GetComponent<Animator>().runtimeAnimatorController = third;
                Instantiate(bandMemPf[2], tP.transform.position,tP.transform.rotation,parentT);
            }
        
            if (GameManager.Instance.charNumber == 2)
            {
                bandMemPf[1].GetComponent<Animator>().runtimeAnimatorController = second;                //us
                Instantiate(bandMemPf[1], sP.transform.position,sP.transform.rotation,parentS);
            
                bandMemPf[0].GetComponent<Animator>().runtimeAnimatorController = first;
                Instantiate(bandMemPf[0], fP.transform.position,fP.transform.rotation,parentF);
        
                bandMemPf[2].GetComponent<Animator>().runtimeAnimatorController = third;
                Instantiate(bandMemPf[2], tP.transform.position,tP.transform.rotation,parentT);
            }
        
            if (GameManager.Instance.charNumber == 3)
            {
                bandMemPf[2].GetComponent<Animator>().runtimeAnimatorController = second;                //us
                Instantiate(bandMemPf[2], sP.transform.position,sP.transform.rotation,parentS);
            
                bandMemPf[0].GetComponent<Animator>().runtimeAnimatorController = first;
                Instantiate(bandMemPf[0], fP.transform.position,fP.transform.rotation,parentF);
        
                bandMemPf[1].GetComponent<Animator>().runtimeAnimatorController = third;
                Instantiate(bandMemPf[1], tP.transform.position,tP.transform.rotation,parentT);
            }
        }
        
            if (GameManager.Instance.playerPosi == 3)
        {
            posText.text = "#3";
            
            if (GameManager.Instance.charNumber == 1 )
            {
                bandMemPf[0].GetComponent<Animator>().runtimeAnimatorController = third;                //us
                Instantiate(bandMemPf[0], tP.transform.position,tP.transform.rotation,parentT);
            
                bandMemPf[1].GetComponent<Animator>().runtimeAnimatorController = first;
                Instantiate(bandMemPf[1], fP.transform.position,fP.transform.rotation,parentF);
        
                bandMemPf[2].GetComponent<Animator>().runtimeAnimatorController = second;
                Instantiate(bandMemPf[2],sP.transform.position,sP.transform.rotation,parentS);
            }
        
            if (GameManager.Instance.charNumber == 2)
            {
                bandMemPf[1].GetComponent<Animator>().runtimeAnimatorController = third;                //us
                Instantiate(bandMemPf[1], tP.transform.position,tP.transform.rotation,parentT);
            
                bandMemPf[0].GetComponent<Animator>().runtimeAnimatorController = first;
                Instantiate(bandMemPf[0], fP.transform.position,fP.transform.rotation,parentF);
        
                bandMemPf[2].GetComponent<Animator>().runtimeAnimatorController = second;
                Instantiate(bandMemPf[2], sP.transform.position,sP.transform.rotation,parentS);
            }
        
            if (GameManager.Instance.charNumber == 3)
            {
                bandMemPf[2].GetComponent<Animator>().runtimeAnimatorController = third;                //us
                Instantiate(bandMemPf[2], fP.transform.position,fP.transform.rotation,parentF);
            
                bandMemPf[0].GetComponent<Animator>().runtimeAnimatorController = first;
                Instantiate(bandMemPf[0], fP.transform.position,fP.transform.rotation,parentF);
        
                bandMemPf[1].GetComponent<Animator>().runtimeAnimatorController = second;
                Instantiate(bandMemPf[1], sP.transform.position,sP.transform.rotation,parentS);
            }
        }
        
           GameManager.Instance.interstitialAd.StartUpInterstitial();
        }

        rewardedCoins.text = GameManager.Instance.additionalCoinsToBeGivenBasedOnRank.ToString();
        adCoins.text = "+" + GameManager.Instance.timesForCoins;
        
        lastLevelButtons.SetActive(false);
        lastLevelButtons.transform.DOLocalMoveY(-5000f, 0.5f);

        fullConcertPanel.transform.DOLocalMoveY(-5000f, 0f);
        posText.transform.DOScale(Vector3.zero, 0f);
        RewardStuff.transform.DOScale(Vector3.zero, 0f);
        bottomBtns.transform.DOLocalMoveY(-5000f, 0.5f);
        
        StartCoroutine(DoTweenPanels());
        
    }

    IEnumerator DoTweenPanels()
    {
        yield return new WaitForSeconds(3f);
        fullConcertPanel.transform.DOLocalMoveY(0f, 0.5f);
        yield return new WaitForSeconds(1f);
        posText.transform.DOScale(new Vector3(1f,1f,1f), 2f).SetEase(Ease.OutElastic);
        yield return new WaitForSeconds(1f);
        RewardStuff.transform.DOScale(new Vector3(1f,1f,1f), 2f).SetEase(Ease.OutElastic);
    }
    
    public int rewardCoinsCounter;

    public void CollectCoins()
    {
        rewardCoinsCounter = int.Parse(rewardedCoins.text);
        
        collectCoinsBtn.enabled = false;
        collectCoinsBtn.transform.DOScale(new Vector3(0f,0f,0f), 1f).SetEase(Ease.OutElastic);
        
        StartCoroutine(CountRewardCoins());
        
    }

    IEnumerator CountRewardCoins()
    {
        
        yield return new WaitForSeconds(0.005f);
        rewardCoinsCounter--;
        yield return new WaitForSeconds(0.01f);
        PlayerPrefs.SetInt("MyTotalCoins", PlayerPrefs.GetInt("MyTotalCoins")+1);
        rewardedCoins.text = rewardCoinsCounter.ToString();

        if (rewardCoinsCounter > 0)
        {
            StartCoroutine(CountRewardCoins());
        }
        else
        {
            if (GameManager.Instance.isThisTheFinalLevel)
            {
                lastLevelButtons.SetActive(true);
                lastLevelButtons.transform.DOLocalMoveY(0f, 0.5f);
                URLforCompi = "https://www.toneden.io/the-wombats-5/post/the-wombats-official-game-competition";
                GameManager.Instance.isThisTheFinalLevel = false;
                
            }
            else
            {
                lastLevelButtons.SetActive(false);
                lastLevelButtons.transform.DOLocalMoveY(-5000f, 0.5f);
                bottomBtns.transform.DOLocalMoveY(0f, 0.5f);
            }
            
        }
        
    }

    public void LoadNextLevel()
    {
        /*UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.GetComponent<Button>().enabled = false;*/
        
        if (GameManager.Instance.currentLevelName == "LONDON" && GameManager.Instance.currentLI == 1)
        {
            GameManager.Instance.currentLevelName = "ROME";
            GameManager.Instance.currentLI = 2;
        }
        else if (GameManager.Instance.currentLevelName == "ROME" && GameManager.Instance.currentLI == 2)
        {
            GameManager.Instance.currentLevelName = "SYDNEY";
            GameManager.Instance.currentLI = 1;
        }
        else if (GameManager.Instance.currentLevelName == "SYDNEY" && GameManager.Instance.currentLI == 1)
        {
            GameManager.Instance.currentLevelName = "PARIS";
            GameManager.Instance.currentLI = 2;
        }
        else if (GameManager.Instance.currentLevelName == "PARIS" && GameManager.Instance.currentLI == 2)
        {
            GameManager.Instance.currentLevelName = "EGYPT";
            GameManager.Instance.currentLI = 1;
        }
        else if (GameManager.Instance.currentLevelName == "EGYPT" && GameManager.Instance.currentLI == 1)
        {
            GameManager.Instance.currentLevelName = "CARDIFF";
            GameManager.Instance.currentLI = 2;
        }
        else if (GameManager.Instance.currentLevelName == "CARDIFF" && GameManager.Instance.currentLI == 2)
        {
            GameManager.Instance.currentLevelName = "GLASGOW";
            GameManager.Instance.currentLI = 1;
        }
        else if (GameManager.Instance.currentLevelName == "GLASGOW" && GameManager.Instance.currentLI == 1)
        {
            GameManager.Instance.currentLevelName = "TOKYO";
            GameManager.Instance.currentLI = 2;
        }
        else if (GameManager.Instance.currentLevelName == "TOKYO" && GameManager.Instance.currentLI == 2)
        {
            GameManager.Instance.currentLevelName = "MILAN";
            GameManager.Instance.currentLI = 1;
        }
        else if (GameManager.Instance.currentLevelName == "MILAN" && GameManager.Instance.currentLI == 1)
        {
            GameManager.Instance.currentLevelName = "LONDON";
            GameManager.Instance.currentLI = 2;
        }
        else if (GameManager.Instance.currentLevelName == "LONDON" && GameManager.Instance.currentLI == 2)
        {
            GameManager.Instance.currentLevelName = "ROME";
            GameManager.Instance.currentLI = 1;
        }
        else if (GameManager.Instance.currentLevelName == "ROME" && GameManager.Instance.currentLI == 1)
        {
            GameManager.Instance.currentLevelName = "SYDNEY";
            GameManager.Instance.currentLI = 2;
        }
        else if (GameManager.Instance.currentLevelName == "SYDNEY" && GameManager.Instance.currentLI == 2)
        {
            GameManager.Instance.currentLevelName = "PARIS";
            GameManager.Instance.currentLI = 1;
        }
        else if (GameManager.Instance.currentLevelName == "PARIS" && GameManager.Instance.currentLI == 1)
        {
            GameManager.Instance.currentLevelName = "EGYPT";
            GameManager.Instance.currentLI = 2;
        }
        else if (GameManager.Instance.currentLevelName == "EGYPT" && GameManager.Instance.currentLI == 2)
        {
            GameManager.Instance.currentLevelName = "CARDIFF";
            GameManager.Instance.currentLI = 1;
        }
        else if (GameManager.Instance.currentLevelName == "CARDIFF" && GameManager.Instance.currentLI == 1)
        {
            GameManager.Instance.currentLevelName = "GLASGOW";
            GameManager.Instance.currentLI = 2;
        }
        else if (GameManager.Instance.currentLevelName == "GLASGOW" && GameManager.Instance.currentLI == 2)
        {
            GameManager.Instance.currentLevelName = "TOKYO";
            GameManager.Instance.currentLI =1;
        }
        else if (GameManager.Instance.currentLevelName == "TOKYO" && GameManager.Instance.currentLI == 1)
        {
            GameManager.Instance.currentLevelName = "LIVERPOOL";
            GameManager.Instance.currentLI = 1;
            
        }
        else if (GameManager.Instance.currentLevelName == "LIVERPOOL" && GameManager.Instance.currentLI == 1)
        {
            GameManager.Instance.currentLevelName = "MILAN";
            GameManager.Instance.currentLI = 2;
            
        }
        
        else if (GameManager.Instance.currentLevelName == "MILAN" && GameManager.Instance.currentLI == 2)
        {
            GameManager.Instance.currentLevelName = "LevelSelection";

        }

        GameManager.Instance.lightingMode = GameManager.Instance.currentLI;
        
        GameManager.Instance.LoadScene(GameManager.Instance.currentLevelName);
        /*GameManager.Instance.interstitialAd.interstitialplay();*/
        
    }

    public void EnterContest()
    {
        Application.OpenURL(URLforCompi);
    }

    public void RewardedCoins()
    { 
        GameManager.Instance.isForCoinsReward = true;

        rewardCoinsBtn.transform.DOScale(new Vector3(0f,0f,0f), 1f).SetEase(Ease.OutElastic);
       //GameManager.Instance.rewardedAd.IniRewardedSystem();
       GameManager.Instance.rewardedAd.rewardedForConcert();
       
    }

    public void LevelSelection()
    {
        AudioManager.Instance.musicTracks.MusicTrackAudioSource.Stop();
        UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.GetComponent<Button>().enabled = false;
        GameManager.Instance.LoadScene("LevelSelection");
    }
    
    public void ButtonClick()
    {
        GameManager.Instance.ButtonClick();
    }
    
}
