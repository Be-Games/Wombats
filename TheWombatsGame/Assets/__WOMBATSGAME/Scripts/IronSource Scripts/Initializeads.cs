using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Initializeads : MonoBehaviour
{

    public string androidAppKey;
    public string iosAppKey;
    public string appkey;

    // Start is called before the first frame update

    private void Awake()
    {
#if UNITY_ANDROID
        appkey = androidAppKey;
#endif
        
#if UNITY_IOS
            appkey = iosAppKey;
#endif
        
        IronSource.Agent.init(appkey);
    }

    void Start()
    {
        Loadbanner();
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
