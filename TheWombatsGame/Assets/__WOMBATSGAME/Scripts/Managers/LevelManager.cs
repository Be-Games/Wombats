using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
//using Coffee.UIEffects;
using DG.Tweening;
using PathCreation.Examples;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Random = System.Random;

public class LevelManager : MonoBehaviour
{
    private static LevelManager _instance;

    public static LevelManager Instance
    {
        get
        {
            return _instance;
        }
    }

    private void Awake()
    {
        _instance = this;
    }
    

    [Header("Camera Refs")]
    public CinemachineVirtualCamera threePlayerCMVC;
    public CinemachineCameraOffset cmCameraOffset;
    
    [Header("Player Car ")]
    public GameObject currentPlayerCarModel;
    
    [Header("Lap Settings")]
    public int lapCounter = 0;
    public TextMeshProUGUI lapText;
    public int totalLaps;
    public bool isLapTriggered;
    public GameObject[] levelTimeObjects;

    [Header("EnemyCar")] 
    public GameObject enemy1;
    public GameObject enemy2;

    [Header("Boost Settings")] 
    public int totalBoostNumber;                                                                            //Total Times boost can be done
    public float individualBoostCounter;                                                                    //Total boost Counter ( shd be 3 )
    public Image[] boostFiller;
    public bool isBoosting;
    public Button boostBtn;
    public GameObject[] boostPickUps;
    public GameObject envToBlur;
    public FocusSwitcher focus;
    
    
    [Header("Level Speeds")]
  [Range(10,15)]  public float boostSpeed;
    [Range(6,20)] public float normalSpeed;
    public float singleLapDistance;
    

    [Header("CountDownTimer Settings")]
    public bool isTimerStarted;
    public TextMeshProUGUI timerText;
    public GameObject[] countdownLights;

    [Header("Other Class Ref")] 
    public UIManager UiManager;
    
    [Header("Bool Values")]
    public bool isGameStarted;
    public bool isCrashed;
    public bool isCrashedWithPpl;
    public bool adStuff;
    public bool isGameEnded;
    
    [Header("Car Effects")]
    public GameObject NOSeffect;
    
    public GameObject gameOverPanel;
    public GameObject gameEndPanel;
    public GameObject popUpPanel;

    [Header("Weather Effects")] 
    public GameObject slowWind;
    public GameObject FastWind;

    [Header("DoTween Sequences")] 
    private Sequence boostBtnSeq;
    
    
    [Header("Misc Stuff")]
    public float startTime;
    public AudioSource gameMusic;
    public GameObject endConfetti;
    public GameObject[] pplToDisable;
    public GameObject[] objPlayerCarToReset;
    public GameObject[] playerCarCollidersToToggle;

    public TextMeshProUGUI carContinueChances;
    public int continueCounter;
    public CinemachineVirtualCamera originalCM, finishCM;
    
    
    //Race Finish Stuff
    public GameObject cameraRotator;
    public GameObject blackScreenFadingPanel;
    
    

    public CinemachineVirtualCamera cmvc;
    //other script references
    
    private void Start()
    {
        lapText.text = (lapCounter+1) + "/" + totalLaps;
        
        UiManager.BoostBtn.transform.localScale = Vector3.zero;
        UiManager.BoostBtn.transform.GetComponent<Button>().enabled = false;
        
        //OverHeadUIs
        //currentPlayerCarModel.transform.GetChild(3).localScale = Vector3.zero;
        
        
       startTime = Time.time;
       
       
       
    }
    
    
    public void StartBtn()
    {
        isTimerStarted = true;
        StartCoroutine("CountDownTimer");
    }

    IEnumerator CountDownTimer()
    {
        countdownLights[0].SetActive(true);
        yield return new WaitForSeconds(1f);
        countdownLights[1].SetActive(true);
        yield return new WaitForSeconds(1f);
        countdownLights[2].SetActive(true);
        yield return new WaitForSeconds(1f);
        gameMusic.enabled = true;
        countdownLights[0].SetActive(false);
        countdownLights[1].SetActive(false);
        countdownLights[2].SetActive(false);
        countdownLights[3].SetActive(true);
        yield return new WaitForSeconds(0.2f);
        isTimerStarted = false;
        //timerText.gameObject.SetActive(false);
        isGameStarted = true;

        RaceStarted();
       

    }

    void RaceStarted()
    {
        GameManager.Instance.canControlCar = true;                        // Car Gestures Enabled
        UiManager.BoostBtn.transform.DOScale(new Vector3(0.6f,0.6f,0.6f), 1f).SetEase(Ease.OutBounce);
        
        currentPlayerCarModel.transform.GetChild(3).DOScale(new Vector3(1f,1f,1f), 1f).SetEase(Ease.OutBounce);
        
    }

    public void Vibrate()
    {
        Handheld.Vibrate();
    }

    private void Update()
    {

        if (isGameStarted)
        {
           
            if (lapCounter >= 1)
            {
                if (levelTimeObjects[lapCounter-1].activeInHierarchy && isGameStarted )
                {
                
                    float t = Time.time - startTime;
                    string minutes = ((int) t / 60).ToString();
                    string seconds = (t % 60).ToString("f2");
            
                    levelTimeObjects[lapCounter-1].GetComponent<TextMeshProUGUI>().text = "Lap " + (lapCounter) + " : " + minutes + ":" +
                                                                                          seconds;
            
                }
            }

        }

    }
    
    

    public void LapManager()
    {
        
        
        

        if ((lapCounter) == totalLaps)
        {
            StartCoroutine("RaceFinished");
        }
       
        if (lapCounter == totalLaps - 1)
        {

            #region FINAL LAP STATUS INDICATOR
            UiManager.StatusIndicatorPanelGO.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text =
                "FINAL LAP";
            
            var mySequence = DOTween.Sequence();
            
            mySequence.Append( UiManager.StatusIndicatorPanelGO.transform.DOScaleX(1.5f, 0.4f)
                .OnComplete(() => UiManager.StatusIndicatorPanelGO.transform.DOScaleX(1f, 0.1f)));
            
            mySequence.AppendInterval(2);

            mySequence.OnComplete(() => UiManager.StatusIndicatorPanelGO.transform.DOScaleX(0f, 0.4f));
            
            #endregion
            
        }
        
        if (lapCounter < totalLaps)
        {
            lapText.text = (lapCounter+1) + "/" + totalLaps;
            lapCounter++;
            levelTimeObjects[lapCounter-1].SetActive(true);
            startTime = Time.time;
        }

       Invoke("DisableCountDownLights",2f);
        
    }

    void DisableCountDownLights()
    {
        if(countdownLights[3].activeInHierarchy)
            countdownLights[3].SetActive(false);
    }
    
    IEnumerator RaceFinished()
    {
        
        yield return new WaitForSeconds(0f);
        isGameEnded = true;
        UiManager.BoostBtn.gameObject.SetActive(false);
        // float alpha = blackScreenFadingPanel.GetComponent<Image>().color.a;
        // DOTween.To(() => alpha, 
        //         x => alpha = x, 255, 2f)
        //     .OnUpdate(() => {
        //                 
        //     });

        blackScreenFadingPanel.transform.GetChild(0).DOScale(new Vector3(5, 5, 1), 1f).SetEase(Ease.OutBounce);
        
        isGameStarted = false;
        GameManager.Instance.canControlCar = false;
        
        endConfetti.SetActive(true);
        
        
        
        yield return new WaitForSeconds(0.1f);
        cameraRotator.SetActive(true);
        //LevelManager.Instance.gameEndPanel.SetActive(true);
    }

    public void GoToStadium()
    {
        SceneManager.LoadScene("StadiumWin");
    }

    public void BoostManager()
    {
        individualBoostCounter += 1;
        
        //BOOST FILL AMOUNT
        switch (individualBoostCounter)
        {
            case 0:
                boostFiller[0].fillAmount = 0;
                boostFiller[1].fillAmount = 0;
                break;
            
            case 1:
                foreach (var abc in boostFiller)
                {
                    DOTween.To(() => abc.fillAmount, 
                            x => abc.fillAmount = x, 0.332f, 0.3f)
                        .OnUpdate(() => {
                        
                        });
                }

                break;
            
            case 2:
                foreach (var abc in boostFiller)
                {
                    DOTween.To(() => abc.fillAmount, 
                            x => abc.fillAmount = x, 0.699f, 0.3f)
                        .OnUpdate(() => {
                        
                        });
                }

                break;
            
            case 3:
                foreach (var abc in boostFiller)
                {
                    DOTween.To(() => abc.fillAmount, 
                            x => abc.fillAmount = x, 1f, 0.3f)
                        .OnComplete(BoostActivated);
                }
                
                break;
            
        }
      
    }
    

    void BoostActivated()
    {
        individualBoostCounter = 0;
        
        
        UiManager.BoostBtn.GetComponent<Button>().enabled = true;
        
        //DISABLE ALL THE BOOST PICKUPS
        foreach (GameObject x in boostPickUps)                                                            //Dissable all the boost pickups
        {
            x.SetActive(false);
        }

        
        
            // boostBtnSeq = DOTween.Sequence();
            //
            // boostBtnSeq.Append(  UiManager.BoostBtn.transform.DOScale(new Vector3(0.8f,0.8f,0.8f), 0.6f).SetEase(Ease.OutBounce));
            //
            // boostBtnSeq.AppendInterval(0.2f);
            //    
            // boostBtnSeq.Append( UiManager.BoostBtn.transform.DOScale(new Vector3(0.6f,0.6f,0.6f), 0.6f)
            //     .SetEase(Ease.OutBounce));
            //
            // boostBtnSeq.AppendInterval(0.2f);
            //
            // boostBtnSeq.SetLoops(loopNo);
            


                
        UiManager.StatusIndicatorPanelGO.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "BOOST ACTIVATED";
        
        //UiManager.BoostBtn.transform.GetChild(0).GetChild(0).GetComponent<UIShiny>().effectPlayer.play = true;
        //UiManager.BoostBtn.transform.GetChild(0).GetChild(1).GetComponent<UIShiny>().effectPlayer.play = true;
        
        var mySequence = DOTween.Sequence();
            
        mySequence.Append( UiManager.StatusIndicatorPanelGO.transform.DOScaleX(1.5f, 0.4f)
            .OnComplete(() => UiManager.StatusIndicatorPanelGO.transform.DOScaleX(1f, 0.1f)));
            
        mySequence.AppendInterval(1);

        mySequence.OnComplete(() => UiManager.StatusIndicatorPanelGO.transform.DOScaleX(0f, 0.4f));
            
      
    }

    
    
    public void BoostCarButton()
    {
        StartCoroutine("BoostCarSettings");
    }

    
    IEnumerator BoostCarSettings()
    {
        isBoosting = true;
        
       // UiManager.BoostBtn.transform.GetChild(0).GetChild(0).GetComponent<UIShiny>().effectPlayer.play = false;
       // UiManager.BoostBtn.transform.GetChild(0).GetChild(1).GetComponent<UIShiny>().effectPlayer.play = false;
        
        
        
        NOSeffect.SetActive(true);                                                                    //NOS Particle Effect
        PlayerController.Instance.targetSpeed = boostSpeed;                                            //Set speed to boost speed
        UiManager.BoostBtn.GetComponent<Button>().enabled = false;
        
        focus.SetFocused(envToBlur);                                                                    //blur effects
        
        DOTween.To(() => cmvc.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset.z,         ////damping camera effect
                x => cmvc.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset.z = x, -3f, 0.3f)
            .OnUpdate(() => {
                        
            });
       
        

        foreach (GameObject x in pplToDisable)                                                            //Dissable all the people
        {
            x.SetActive(false);
        }
        
       
            
        foreach (var abc in boostFiller)                    
        {
            DOTween.To(() => abc.fillAmount, 
                    x => abc.fillAmount = x, 0f, 6.4f)
                .OnUpdate(() => {
                        
                });
        }
        
        
        yield return new WaitForSeconds(5f);

        foreach (GameObject x in boostPickUps)                                                            //Dissable all the boost pickups
        {
            x.SetActive(true);
        }
        isBoosting = false;
        
        foreach (GameObject x in pplToDisable)
        {
            x.SetActive(true);
        }
        //DISABLE ALL THE BOOST PICKUPS
        foreach (GameObject x in boostPickUps)                                                            //Dissable all the boost pickups
        {
            x.SetActive(true);
        }
            
        
        NOSeffect.SetActive(false);       
        PlayerController.Instance.targetSpeed = normalSpeed;
        
        //unblur
        focus.SetFocused(null);
        
        DOTween.To(() => cmvc.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset.z,         ////damping camera effect
                x => cmvc.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset.z = x, -2.24f, 0.8f)
            .OnUpdate(() => {
                        
            });
    }

    public void ResetGame()
    {
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().name);
    }

    public void ReturnToMainMenu()
    {
        SceneManager.LoadSceneAsync(0);
    }

    public void PauseMenu()
    {
        isGameStarted = false;
        GameManager.Instance.canControlCar = false;
    }

    public void ResumeGame()
    {
        if (!isGameEnded)
        {
            isGameStarted = true;
            Invoke("CarControlsOn",0f);
        }
        
    }

    void CarControlsOn()
    {
        GameManager.Instance.canControlCar = true;
        PlayerController.Instance.gameControlsClass.gestureState = GameControls.GestureState.Release;
    }

    public void SetContinueCounterForPostAd()
    {
        continueCounter = 5;
    }

    public void ResetCar()
    {
        // PickUpTrigger.Instance.HideHuman();
        if (continueCounter < 2)
        {
            StartCoroutine("CarReset");
        }

        if (LevelManager.Instance.continueCounter == 5)
        {
            StartCoroutine("CarReset");
            
        }
            
        
    }

    IEnumerator CarReset()
    {
        
        if(!adStuff && continueCounter!=5)
            continueCounter++;
        
        if (continueCounter == 2)
        {
            UiManager.crashedPanel.transform.GetChild(0).GetChild(2).gameObject.SetActive(true);           //ADD GET MORE LIVES BUTTON
            UiManager.crashedPanel.transform.GetChild(0).GetChild(0).gameObject.SetActive(false);         //CONTINUE BUTTON REMOVE
            
        }
        

        
        
        isGameStarted = true;
        
        isCrashed = false;
        PlayerController.Instance.gameControlsClass.gestureState = GameControls.GestureState.Release;
        UiManager.crashedPanel.SetActive(false);
        
        PlayerController.Instance.playerPF.speed = normalSpeed;
        PlayerController.Instance.playerPF.enabled = true;
        
        
        objPlayerCarToReset[0].SetActive(false); //smoke effect
        objPlayerCarToReset[1].SetActive(true); //original car
        objPlayerCarToReset[2].SetActive(false); // toppled over car
        
        foreach (GameObject x in pplToDisable)
        {
            x.SetActive(true);
        }
    
        foreach (GameObject child in playerCarCollidersToToggle)
        {
            if (child.GetComponent<Collider>())
            {
                child.GetComponent<Collider>().enabled = false;
            }
            
        }
        
        PlayerController.Instance.PlayercarVisual.SetActive(false);
        
    
        yield return new WaitForSeconds(0.1f);
        
        PlayerController.Instance.PlayercarVisual.SetActive(true);
        
        yield return new WaitForSeconds(0.1f);
        
        PlayerController.Instance.PlayercarVisual.SetActive(false);
        
        yield return new WaitForSeconds(0.1f);
        
        PlayerController.Instance.PlayercarVisual.SetActive(true);
        
        yield return new WaitForSeconds(0.1f);
        
        PlayerController.Instance.PlayercarVisual.SetActive(false);
        
        yield return new WaitForSeconds(0.1f);
        
        PlayerController.Instance.PlayercarVisual.SetActive(true);
        
        yield return new WaitForSeconds(0.1f);
        
        PlayerController.Instance.PlayercarVisual.SetActive(false);
        
    
        yield return new WaitForSeconds(0.1f);
        
        PlayerController.Instance.PlayercarVisual.SetActive(true);
        
        yield return new WaitForSeconds(0.1f);
        
        PlayerController.Instance.PlayercarVisual.SetActive(false);
        
        yield return new WaitForSeconds(0.1f);
        
        PlayerController.Instance.PlayercarVisual.SetActive(true);
        
        yield return new WaitForSeconds(0.1f);
        
        PlayerController.Instance.PlayercarVisual.SetActive(false);
        
        yield return new WaitForSeconds(0.1f);
        
        PlayerController.Instance.PlayercarVisual.SetActive(true);
        
        foreach (GameObject child in playerCarCollidersToToggle)
        {
            if (child.GetComponent<Collider>())
            {
                child.GetComponent<Collider>().enabled = true;
            }
            
        }
        
        yield return new WaitForSeconds(0.1f);
        GameManager.Instance.canControlCar = true;
    }

    

   

    public void OnAdClosed()
    {
        UiManager.crashedPanel.SetActive(false);
        UiManager.extraLifePanel.SetActive(true);
        
    }

    public void ReceiveLifeBtn()
    {
        UiManager.extraLifePanel.SetActive(false);
        adStuff = true;
        StartCoroutine("CarReset");
    }

    
    
    
}
