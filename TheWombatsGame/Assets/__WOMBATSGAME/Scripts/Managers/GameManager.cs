using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
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
    }

    [Header("Variables for Full Game")] 
    
    public int lightingMode = 1;
    public bool canControlCar;


    [Header("Stadium Scene Stuff")] 
    public int charNumber = 1;                                                    //1= MM , 2 = DH , 3 = TO 
    public int podiumPos = 1;

    [SerializeField] private GameObject LoadingScreenPanel;
    [SerializeField] private Image wombatsLoadingImg;
    private float time, second;

    public bool isHapticEnabled,isSFXenabled,isMusicEnabled;


    private void Start()
    {
        isHapticEnabled = true;
        isSFXenabled = true;
        isMusicEnabled = true;
    }

    public void setCharacter(int charNo)
    {
        charNumber = charNo;
    }

    #region LoadingScreen

    public void LoadScene(string sceneName)
    {
        
        StartCoroutine(LoadSceneAsync(sceneName));
    }
    
    IEnumerator LoadSceneAsync(string sceneName)
    {
        LoadingScreenPanel.SetActive(true);
        
        yield return new WaitForSeconds(1f);
        
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);
        operation.allowSceneActivation = false;
        
        while (!operation.isDone)
        {
            wombatsLoadingImg.fillAmount = operation.progress;
            
            if (operation.progress == 0.9f)
            {
                wombatsLoadingImg.fillAmount = 1f;
                operation.allowSceneActivation = true;
            }
            yield return null;
        }
        if(operation.isDone)
            LoadingScreenPanel.SetActive(false);
        
        
    }
    
    #endregion
    

    
}
