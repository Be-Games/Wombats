using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Interstitial : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Invoked when the interstitial ad closed and the user goes back to the application screen.
    public void InterstitialAdClosedEvent()
    {
        GameManager.Instance.LoadScene(GameManager.Instance.currentLevelName);
    }

    public void StartUpInterstitial()
    {
        IronSource.Agent.loadInterstitial();
        IronSourceEvents.onInterstitialAdClosedEvent += InterstitialAdClosedEvent;
       
    }

    public void interstitialplay() 
    {
        /*if (IronSource.Agent.isInterstitialReady())
        {
            Debug.Log("Works");
            IronSource.Agent.showInterstitial();
        }
        else
        {
            Debug.Log("Doesnt Work");
            GameManager.Instance.LoadScene(GameManager.Instance.currentLevelName);
        }*/
        
        IronSource.Agent.showInterstitial();
       
    }
}
