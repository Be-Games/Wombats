using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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
    public bool canControlCar;


    [Header("Stadium Scene Stuff")] 
    public int charNumber = 0;                                                    //0= MM , 1 = DH , 2 = TO 
    //public int podiumPos = 1;

    [Header("Car Setups for Game")] 
    public GameObject playerCarModels;
    public List<GameObject> enemyCarModels;
    // public GameObject[] playerCarModels;
    // public GameObject[] enemyCarModels;
    public int selectedCarModelPLAYER;
    public int enemyCar1;
    public int enemyCar2;
    
    [Space]
    
    [SerializeField] private GameObject LoadingScreenPanel;
    [SerializeField] private Image wombatsLoadingImg;
    private float time, second;

    [Space] 
    public bool isSettingVisible;
    public GameObject settingsPanel;
    public GameObject settingsBtn;


    private void Start()
    {
        //INITIALISE LIST
        enemyCarModels = new List<GameObject>();
        
        charNumber = 0;
        
        isSettingVisible = false;
        settingsPanel.GetComponent<RectTransform>().localScale = new Vector3(1f,0f,1f);
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
            wombatsLoadingImg.fillAmount = operation.progress;
            
            if (operation.progress == 0.9f)
            {
                operation.allowSceneActivation = true;
                
                wombatsLoadingImg.fillAmount = 1f;
                
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

    

    private void Update()
    {
        if(SceneManager.GetActiveScene().name == "HomeScreen" ||
            SceneManager.GetActiveScene().name == "PlayerSelection" ||
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
}
