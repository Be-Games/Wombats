using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ConcertManagement : MonoBehaviour
{
    private GameManager _gameManager;
    public GameObject[] bandMemPf;             //1 = MM , 2 = dan , 3 = tord
    public Transform fP, sP, tP;
    public Transform parentF, parentS, parentT;
    
    public RuntimeAnimatorController first, second, third;

    private void Start()
    {
        _gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();

        if (_gameManager.playerPosi == 1)
        {
            if (_gameManager.charNumber == 1 )
            {
                bandMemPf[0].GetComponent<Animator>().runtimeAnimatorController = first;                    //us
                Instantiate(bandMemPf[0], fP.transform.position,fP.transform.rotation,parentF);
            
                bandMemPf[1].GetComponent<Animator>().runtimeAnimatorController = second;
                Instantiate(bandMemPf[1], sP.transform.position,sP.transform.rotation,parentS);
        
                bandMemPf[2].GetComponent<Animator>().runtimeAnimatorController = third;
                Instantiate(bandMemPf[2], tP.transform.position,tP.transform.rotation,parentT);
            }
        
            if (_gameManager.charNumber == 2)
            {
                bandMemPf[1].GetComponent<Animator>().runtimeAnimatorController = first;                    //us
                Instantiate(bandMemPf[1], fP.transform.position,fP.transform.rotation,parentF);
            
                bandMemPf[0].GetComponent<Animator>().runtimeAnimatorController = second;
                Instantiate(bandMemPf[0], sP.transform.position,sP.transform.rotation,parentS);
        
                bandMemPf[2].GetComponent<Animator>().runtimeAnimatorController = third;
                Instantiate(bandMemPf[2], tP.transform.position,tP.transform.rotation,parentT);
            }
        
            if (_gameManager.charNumber == 3)
            {
                bandMemPf[2].GetComponent<Animator>().runtimeAnimatorController = first;                    //us
                Instantiate(bandMemPf[2], fP.transform.position,fP.transform.rotation,parentF);
            
                bandMemPf[0].GetComponent<Animator>().runtimeAnimatorController = second;
                Instantiate(bandMemPf[0], sP.transform.position,sP.transform.rotation,parentS);
        
                bandMemPf[1].GetComponent<Animator>().runtimeAnimatorController = third;
                Instantiate(bandMemPf[1], tP.transform.position,tP.transform.rotation,parentT);
            }
        }
        
        if (_gameManager.playerPosi == 2)
        {
            if (_gameManager.charNumber == 1 )
            {
                bandMemPf[0].GetComponent<Animator>().runtimeAnimatorController = second;                //us
                Instantiate(bandMemPf[0], sP.transform.position,sP.transform.rotation,parentS);
            
                bandMemPf[1].GetComponent<Animator>().runtimeAnimatorController = first;
                Instantiate(bandMemPf[1], fP.transform.position,fP.transform.rotation,parentF);
        
                bandMemPf[2].GetComponent<Animator>().runtimeAnimatorController = third;
                Instantiate(bandMemPf[2], tP.transform.position,tP.transform.rotation,parentT);
            }
        
            if (_gameManager.charNumber == 2)
            {
                bandMemPf[1].GetComponent<Animator>().runtimeAnimatorController = second;                //us
                Instantiate(bandMemPf[1], sP.transform.position,sP.transform.rotation,parentS);
            
                bandMemPf[0].GetComponent<Animator>().runtimeAnimatorController = first;
                Instantiate(bandMemPf[0], fP.transform.position,fP.transform.rotation,parentF);
        
                bandMemPf[2].GetComponent<Animator>().runtimeAnimatorController = third;
                Instantiate(bandMemPf[2], tP.transform.position,tP.transform.rotation,parentT);
            }
        
            if (_gameManager.charNumber == 3)
            {
                bandMemPf[2].GetComponent<Animator>().runtimeAnimatorController = second;                //us
                Instantiate(bandMemPf[2], sP.transform.position,sP.transform.rotation,parentS);
            
                bandMemPf[0].GetComponent<Animator>().runtimeAnimatorController = first;
                Instantiate(bandMemPf[0], fP.transform.position,fP.transform.rotation,parentF);
        
                bandMemPf[1].GetComponent<Animator>().runtimeAnimatorController = third;
                Instantiate(bandMemPf[1], tP.transform.position,tP.transform.rotation,parentT);
            }
        }
        
        if (_gameManager.playerPosi == 3)
        {
            if (_gameManager.charNumber == 1 )
            {
                bandMemPf[0].GetComponent<Animator>().runtimeAnimatorController = third;                //us
                Instantiate(bandMemPf[0], tP.transform.position,tP.transform.rotation,parentT);
            
                bandMemPf[1].GetComponent<Animator>().runtimeAnimatorController = first;
                Instantiate(bandMemPf[1], fP.transform.position,fP.transform.rotation,parentF);
        
                bandMemPf[2].GetComponent<Animator>().runtimeAnimatorController = second;
                Instantiate(bandMemPf[2],sP.transform.position,sP.transform.rotation,parentS);
            }
        
            if (_gameManager.charNumber == 2)
            {
                bandMemPf[1].GetComponent<Animator>().runtimeAnimatorController = third;                //us
                Instantiate(bandMemPf[1], tP.transform.position,tP.transform.rotation,parentT);
            
                bandMemPf[0].GetComponent<Animator>().runtimeAnimatorController = first;
                Instantiate(bandMemPf[0], fP.transform.position,fP.transform.rotation,parentF);
        
                bandMemPf[2].GetComponent<Animator>().runtimeAnimatorController = second;
                Instantiate(bandMemPf[2], sP.transform.position,sP.transform.rotation,parentS);
            }
        
            if (_gameManager.charNumber == 3)
            {
                bandMemPf[2].GetComponent<Animator>().runtimeAnimatorController = third;                //us
                Instantiate(bandMemPf[2], fP.transform.position,fP.transform.rotation,parentF);
            
                bandMemPf[0].GetComponent<Animator>().runtimeAnimatorController = first;
                Instantiate(bandMemPf[0], fP.transform.position,fP.transform.rotation,parentF);
        
                bandMemPf[1].GetComponent<Animator>().runtimeAnimatorController = second;
                Instantiate(bandMemPf[1], sP.transform.position,sP.transform.rotation,parentS);
            }
        }
        
        
        _gameManager.interstitialAd.StartUpInterstitial();
    }

    public void backtoHome(string name)
    {
        _gameManager.LoadScene(name);
    }

    public void LoadInterstitialAd()
    {
        _gameManager.interstitialAd.interstitialplay();
    }

    public void PostAdRunScene()
    {
        _gameManager.interstitialAd.InterstitialAdClosedEvent();
    }
}
