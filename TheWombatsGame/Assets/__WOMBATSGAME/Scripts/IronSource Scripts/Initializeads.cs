using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Initializeads : MonoBehaviour
{

    public string androidAppKey;
    public string iosAppKey;
    [HideInInspector]public string appkey;

    // Start is called before the first frame update

    private void Awake()
    {

    }

    void Start()
    {
        //Loadbanner();
    }

    public void iniBannerStuff()
    {
#if UNITY_ANDROID
        appkey = "148013819";
#endif
        
#if UNITY_IOS
            appkey ="1480051f9";
#endif
        
        IronSource.Agent.init(appkey);
    }

    void OnApplicationPause(bool isPaused)
    {
        IronSource.Agent.onApplicationPause(isPaused);
    }

    public void Loadbanner()
    {
        IronSource.Agent.loadBanner(IronSourceBannerSize.BANNER, IronSourceBannerPosition.BOTTOM);
    }
}
