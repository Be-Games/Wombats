using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Rewarded : MonoBehaviour
{
    public string androidAppKey;
    public string iosAppKey;
    [HideInInspector]public string appkey;

    public GameObject adNotAvailableBox1,adNotAvailableBox2;
    // Start is called before the first frame update
    void Start()
    {
       
    }

    public void IniRewardedSystem()
    {
#if UNITY_ANDROID
        appkey = "148013819";
#endif
        
#if UNITY_IOS
            appkey = "1480051f9";
#endif
        
        IronSource.Agent.shouldTrackNetworkState(true);
        IronSourceEvents.onRewardedVideoAvailabilityChangedEvent += RewardedVideoAvailabilityChangedEvent;
        
        if (SceneManager.GetActiveScene().name =="PlayerSelection")
        {
            //Debug.Log("This");
            IronSourceEvents.onRewardedVideoAdClosedEvent += RewardedVideoAdClosedEvent2;
        }
        
        IronSourceEvents.onRewardedVideoAdClosedEvent += RewardedVideoAdClosedEvent;
    }

    public void rewarded()
    {
        
        if (IronSource.Agent.isRewardedVideoAvailable())
        {
            Debug.Log("Works");
            IronSource.Agent.showRewardedVideo();
            LevelManager.Instance.ShowRevivePanel();
            LevelManager.Instance.Ana_AdShown("rewarded");
        }
        else
        {
            Debug.Log("Doesnt Work");
            // TODO: Show dialog prompt
            if(adNotAvailableBox1!= null)
                adNotAvailableBox1.SetActive(true);
            //LevelManager.Instance.ShowRevivePanel();
        }


    }
    
    public void rewardedForConcert()
    {

        
        if (IronSource.Agent.isRewardedVideoAvailable())
        {
            Debug.Log("Works");
            IronSource.Agent.showRewardedVideo();
            //LevelManager.Instance.Ana_AdShown("rewarded");
        }
        else
        {
            Debug.Log("Doesnt Work");
            // TODO: Show dialog prompt
            if(adNotAvailableBox2!= null)
                adNotAvailableBox2.SetActive(true);
            //PlayerPrefs.SetInt("MyTotalCoins", PlayerPrefs.GetInt("MyTotalCoins") + GameManager.Instance.timesForCoins);
        }


    }

    public void RewardedVideoAdClosedEvent()
    {
        /*IronSource.Agent.init(appkey, IronSourceAdUnits.REWARDED_VIDEO);
        IronSource.Agent.shouldTrackNetworkState(true);*/

        if (GameManager.Instance.isForCoinsReward)
        {
            Debug.Log("For Concert");
            PlayerPrefs.SetInt("MyTotalCoins", PlayerPrefs.GetInt("MyTotalCoins") + GameManager.Instance.timesForCoins);
            GameManager.Instance.isForCoinsReward = false;
        }
        /*else
        {
            Debug.Log("For Revive");
            LevelManager.Instance.ShowRevivePanel();
        }*/

        
        
    }

    void RewardedVideoAdClosedEvent2()
    {
       PlayerSelection.Instance.postCarAd();
    }

    void RewardedVideoAvailabilityChangedEvent(bool available)
    {
        //Change the in-app 'Traffic Driver' state according to availability.
        bool rewardedVideoAvailability = available;
       
    }
}
