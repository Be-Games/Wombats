using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    
    
    [Header("Level Selection Screen")]
    public GameObject loadingScreenObj;
    public Slider slider;
    AsyncOperation async;
    
    
    public int index = 0;
    public GameObject nextBtn;
    public GameObject prevBtn;

    public GameObject[] cityParent;
    public GameObject[] cityChildren;

    public DayNightSwitchHandler DNClass;
    public Toggle dayNightToggle;

    private void Start()
    {
        DNClass = this.gameObject.GetComponent<DayNightSwitchHandler>();
    }

    //Home Screen Stuff
    public void PlayBtn(string levelSelectionName)
    {
        SceneManager.LoadSceneAsync(levelSelectionName);
    }
    
    //Level Screen Stuff
    void Update()
    {
        
        
        if (index >= cityParent.Length)
        {
            index = cityParent.Length ; 
            
        }

        if (cityParent[cityParent.Length-1].activeInHierarchy)
        {
            nextBtn.SetActive(false);
        }
        else
        {
            nextBtn.SetActive(true);
        }
        
        if (cityParent[0].activeInHierarchy)
        {
            prevBtn.SetActive(false);
        }
        else
        {
            prevBtn.SetActive(true);
        }
          

        if(index < 0)
            index = 0 ;
        


        if(index == 0)
        {
            cityParent[0].gameObject.SetActive(true);
        }

        if (dayNightToggle.isOn)
        {
            cityParent[index].transform.GetChild(0).gameObject.SetActive(true);                // DAY MODE IS ON 
            cityParent[index].transform.GetChild(1).gameObject.SetActive(false);
            GameManager.Instance.lightingMode = 1;
        }
            
            if (!dayNightToggle.isOn)
            {
                cityParent[index].transform.GetChild(1).gameObject.SetActive(true);                // NIGHT MODE IS ON
                cityParent[index].transform.GetChild(0).gameObject.SetActive(false);
                GameManager.Instance.lightingMode = 2;
            }
            
    }

    public void Next()
    {
        index += 1;
        
    
        for(int i = 0 ; i < cityParent.Length; i++)
        {
            cityParent[i].gameObject.SetActive(false);
            cityParent[index].gameObject.SetActive(true);
        }
        
    }
    
    public void Previous()
    {
        index -= 1;
    
        for(int i = 0 ; i < cityParent.Length; i++)
        {
            cityParent[i].gameObject.SetActive(false);
            cityParent[index].gameObject.SetActive(true);
        }
        
    }
    
    public void GameLevels(string sceneName)
        {
            StartCoroutine(LoadingScreen(sceneName));
        }
        
       
    
    IEnumerator LoadingScreen(string name)
        {
            loadingScreenObj.SetActive(true);
            async = SceneManager.LoadSceneAsync(name);
            async.allowSceneActivation = false;
    
            while (async.isDone == false)
            {
                slider.value = async.progress;
                if (async.progress == 0.9f)
                {
                    slider.value = 1f;
                    async.allowSceneActivation = true;
                }
                yield return null;
            
            }
        }    
}
