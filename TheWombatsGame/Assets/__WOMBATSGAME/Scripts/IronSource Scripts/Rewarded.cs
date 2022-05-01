using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rewarded : MonoBehaviour
{
    public string androidAppKey;
    public string iosAppKey;
    [HideInInspector]public string appkey;
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
        IronSourceEvents.onRewardedVideoAdClosedEvent += RewardedVideoAdClosedEvent;
    }

    public void rewarded()
    {
        IronSource.Agent.showRewardedVideo();
    }

    void RewardedVideoAdClosedEvent()
    {
        IronSource.Agent.init(appkey, IronSourceAdUnits.REWARDED_VIDEO);
        IronSource.Agent.shouldTrackNetworkState(true);
    }

    void RewardedVideoAvailabilityChangedEvent(bool available)
    {
        //Change the in-app 'Traffic Driver' state according to availability.
        bool rewardedVideoAvailability = available;
       
    }
}
