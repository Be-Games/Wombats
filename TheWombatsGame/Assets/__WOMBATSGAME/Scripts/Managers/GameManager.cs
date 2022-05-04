using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Lofelt.NiceVibrations;


public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    
    public static GameManager Instance
    {
        get
        {
            return _instance;
        }
    }

    private void Awake()
    {
        if (_instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        
        

        selectedCarModelPLAYER = 0;
        enemyCar1 = 0;
        enemyCar2 = enemyCar1 + 1;
    }

    [Header("Variables for Full Game")] 
    
    public int lightingMode = 1;
    [HideInInspector]public bool canControlCar;
    public int weatherEffect;

    [Space(20)]
        
    [Header("Stadium Scene Stuff")] 
    public int charNumber = 0;                                                    //0= MM , 1 = DH , 2 = TO 
    public int playerPosi;
    
    [Header("Car Setups for Game")] 
    public int memeberIndex;
    public int selectedCarModelPLAYER;
    public int enemyCar1;
    public int enemyCar2;
    
    [Space(20)]
    
    [SerializeField] private GameObject LoadingScreenPanel;
    [SerializeField] private Image wombatsLoadingImg;
    private float time, second;

    [Space] 
    public bool isSettingVisible;
    public GameObject settingsPanel;
    public GameObject settingsBtn;

    [Header("IronSource Scripts")]
    [Space(15)] 
    public Rewarded rewardedAd;
    public Interstitial interstitialAd;

    public GameObject[] murphPrefabs, danPrefabs, tordPrefabs;

    public int numberOfLaps;

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    // called second
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if(SceneManager.GetActiveScene().name == "PlayerSelection" ||
           SceneManager.GetActiveScene().name == "LevelSelection")
        {
            settingsPanel.SetActive(true);
            settingsBtn.SetActive(true);
        }
        else
        {
            settingsPanel.SetActive(false);
            settingsBtn.SetActive(false);
        }
    }
    
    private void Start()
    {
        if (numberOfLaps == 0)
            numberOfLaps = 2;
        
        charNumber = 1;
        
        isSettingVisible = false;
        settingsPanel.GetComponent<RectTransform>().localScale = new Vector3(1f,0f,1f);

        rewardedAd = this.gameObject.GetComponent<Rewarded>();
        interstitialAd = this.gameObject.GetComponent<Interstitial>();
    }
    

    #region LoadingScreen
    private AsyncOperation operation;
    private string nameOfScene;
    public void LoadScene(string sceneName)
    {
        nameOfScene = sceneName;
        LoadingScreenPanel.transform.GetChild(0).GetComponent<RectTransform>().DOScale(new Vector3(15f,0f,15f), 0f).SetEase(Ease.Flash);
        LoadingScreenPanel.transform.GetChild(1).GetComponent<RectTransform>().DOScale(new Vector3(15f,0f,15f), 0f).SetEase(Ease.Flash);
        wombatsLoadingImg.transform.parent.GetComponent<RectTransform>().DOScale(0, 0f).SetEase(Ease.Flash);
        
        LoadingScreenPanel.SetActive(true);
        LoadingScreenPanel.transform.GetChild(0).GetComponent<RectTransform>().DOScale(new Vector3(15f,15f,15f), 0.5f).SetEase(Ease.Flash);
        LoadingScreenPanel.transform.GetChild(1).GetComponent<RectTransform>().DOScale(new Vector3(15f, 15f, 15f), 0.5f)
            .SetEase(Ease.Flash);
        wombatsLoadingImg.transform.parent.GetComponent<RectTransform>().DOScale(0.7f, 0.5f).SetEase(Ease.Flash).OnComplete(StartStuff);
        
    }

    void StartStuff()
    {
        StartCoroutine(LoadSceneAsync(nameOfScene));
    }
    
    IEnumerator LoadSceneAsync(string sceneName)
    {
        
        operation = SceneManager.LoadSceneAsync(sceneName);
        operation.allowSceneActivation = false;
        yield return new WaitForSeconds(1f);

        while (!operation.isDone)
        {
            DOTween.To(() => wombatsLoadingImg.fillAmount, 
                    x => wombatsLoadingImg.fillAmount = x, operation.progress, 0.5f).SetEase(Ease.Flash)
                .OnUpdate(() => {
                        
                });
            //wombatsLoadingImg.fillAmount = operation.progress;
            
            if (operation.progress == 0.9f)
            {
                operation.allowSceneActivation = true;
                
                DOTween.To(() => wombatsLoadingImg.fillAmount, 
                        x => wombatsLoadingImg.fillAmount = x, 1, 0.5f).SetEase(Ease.Flash)
                    .OnUpdate(() => {
                        
                    });
                
                yield return new WaitForSeconds(0.5f);
                wombatsLoadingImg.transform.parent.GetComponent<RectTransform>().DOScale(0f, 0.5f).SetEase(Ease.Flash).OnComplete(abc);
                
            }

            yield return null;
            
        }
    }
    
    void abc()
    {
        LoadingScreenPanel.transform.GetChild(0).GetComponent<RectTransform>().DOScale(new Vector3(15f,0f,15f), 0.5f).SetEase(Ease.Flash);
        LoadingScreenPanel.transform.GetChild(1).GetComponent<RectTransform>()
            .DOScale(new Vector3(15f, 0f, 15f), 0.5f).SetEase(Ease.Flash).OnComplete(def);
       
           
    }

    void def()
    {
        LoadingScreenPanel.SetActive(false);
        DOTween.To(() => wombatsLoadingImg.fillAmount, 
                x => wombatsLoadingImg.fillAmount = x, 0, 0.5f).SetEase(Ease.Flash)
            .OnUpdate(() => {
                        
            });
    }
    
    #endregion
    
    
    public void settingsBtnDT()
    {
        if (!isSettingVisible)
        {
            settingsPanel.GetComponent<RectTransform>().DOScale(new Vector3(1f,1f,1f), 0.1f).SetEase(Ease.Flash);
            isSettingVisible = true;
        }

        else
        {
            settingsPanel.GetComponent<RectTransform>().DOScale(new Vector3(1f,0f,1f), 0.1f).SetEase(Ease.Flash);
            isSettingVisible = false;
        }
        
            
    }

    
    public void musictoggle()
    {
        AudioManager.Instance.ToggleMusic();
    }
    
    public void sfxtoggle()
    {
        AudioManager.Instance.ToggleSFX();
    }

    public void VibrateOnce()
    {
        if(AudioManager.Instance.isHapticEnabled)
            HapticPatterns.PlayConstant(1.0f, 0.0f, 0.5f);
    }
}
