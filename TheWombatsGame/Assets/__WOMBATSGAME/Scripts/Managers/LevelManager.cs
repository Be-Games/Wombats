using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using Coffee.UIEffects;
using DG.Tweening;
using Lofelt.NiceVibrations;
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
    public GameObject mainCameraGO;
    public CinemachineVirtualCamera defCMVCCam;
    public CinemachineCameraOffset cmCameraOffset;
    public GameObject flyOverCameraGO;
    public CinemachineVirtualCamera iniCMVCCam;
    
    [Header("All Cars ")]
    public GameObject currentPlayerCarModel;
    public GameObject enemy1;
    public GameObject enemy2;
    public GameObject CARMODELgo, ENEMYLEFTgo, ENEMYRIGHTgo;
    
    [Header("Lap Settings")]
    public int lapCounter = 0;
    //public TextMeshProUGUI lapText;
    
    public bool isLapTriggered;
    public GameObject[] levelTimeObjects;

    [Header("Scoring Stuff")] 
    public int currentScore;
    public int levelHighScore;


    [Header("Boost Settings")] 
    public int totalBoostNumber;                                                                            //Total Times boost can be done
    public float individualBoostCounter;                                                                    //Total boost Counter ( shd be 3 )
    public Image[] boostFiller;
    public bool isBoosting;
    public Button boostBtn;
    
    public GameObject envToNotBlur;
    public FocusSwitcher focus;
    public GameObject speedLinesEffect;
    

    [Header("CountDownTimer Settings")]
    public bool isTimerStarted;
    public TextMeshProUGUI timerText;
    public GameObject[] countdownLights;

    [Header("Other Class Ref")] 
    public UIManager UiManager;
    public LevelProgressUI LevelProgressUi;
    public AudioManager _audioManager;
    [SerializeField]private GameManager _gameManager;
    public VehicleManager playerVehicleManager;
    
    [Header("Bool Values")]
    public bool isGameStarted;
    public bool isCrashed;
    public bool isCrashedWithPpl;
    public bool adStuff;
    public bool isGameEnded;
    
    [Header("Weather Effects")] 
    public GameObject slowWind;
    public GameObject FastWind;

    [Header("DoTween Sequences")] 
    private Sequence boostBtnSeq;

    [Header("GameUI DOTween Coordinates")] 
    [SerializeField] private float initial;
    [SerializeField] private float final;
    
    [Header("Misc Stuff")]
    public float startTime;
    public GameObject[] playerCarCollidersToToggle;
    public TextMeshProUGUI carContinueChances;
    public int continueCounter;
    //Race Finish Stuff
    public GameObject cameraRotator;
    public RectTransform continueButton,exitButton;
    public CinemachineVirtualCamera cmvc;

    [Header("Level AI Difficulty")] 
    public bool Easy;
    public float easyValue;

    [Header("Stuff to Manually Modify For Each Level")]
    public int totalLaps;
    public List<GameObject> boostPickUps;
    public List<GameObject> pplToDisable;
    public float singleLapDistance;
    public GameObject endConfetti;
    public string cityName;

    public Transform sampleCartransform;
    public List<GameObject> carHeadLights;
    
    private void Start()
    {
        UiManager.flyThroughCamCityName.text = cityName + " TOUR ";

        _gameManager = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();
        playerVehicleManager = GameObject.FindGameObjectWithTag("Player").GetComponent<VehicleManager>();
        _audioManager = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();
        // if (_gameManager)
        // {
        //     currentPlayerCarModel = _gameManager.playerCarModels;
        //     enemy1 = _gameManager.enemyCarModels[0];
        //     enemy2 = _gameManager.enemyCarModels[1];
        // }
        

        // Instantiate(currentPlayerCarModel, CARMODELgo.transform.position, sampleCartransform.rotation, CARMODELgo.transform);
        // Instantiate(enemy1, ENEMYLEFTgo.transform.position, sampleCartransform.rotation, ENEMYLEFTgo.transform);
        // Instantiate(enemy2, ENEMYRIGHTgo.transform.position, sampleCartransform.rotation, ENEMYRIGHTgo.transform);
        
        boostPickUps = new List<GameObject>();
        boostPickUps.AddRange(GameObject.FindGameObjectsWithTag("Boost"));
        
        pplToDisable = new List<GameObject>();
        pplToDisable.AddRange(GameObject.FindGameObjectsWithTag("People"));
        
        envToNotBlur = currentPlayerCarModel.transform.GetChild(0).GetChild(0).gameObject;
        

        for (int i = 0; i < 4; i++)
        {
            playerCarCollidersToToggle[i] = currentPlayerCarModel.GetComponent<VehicleManager>().carWheels.wheels[i];
        }

        playerCarCollidersToToggle[4] = currentPlayerCarModel.GetComponent<VehicleManager>().bodyTrigger.body;
        playerCarCollidersToToggle[5] = currentPlayerCarModel.GetComponent<VehicleManager>().bodyTrigger.trigger;
        
        
        //Game Start - Flyover Camera 
        flyOverCameraGO.SetActive(true);
        mainCameraGO.SetActive(false);


        //Initilization
        //lapText.text = (lapCounter+1) + "/" + totalLaps;
        startTime = Time.time;
        UiManager.BoostBtn.GetComponent<Button>().enabled = false;
        //OverHeadUIs
        //currentPlayerCarModel.transform.GetChild(3).localScale = Vector3.zero;   

        currentScore = 0;
        UiManager.scoreText.text = currentScore.ToString();
        
        carHeadLights = new List<GameObject>();
        carHeadLights.AddRange(GameObject.FindGameObjectsWithTag("Headlight"));
        if (_gameManager.lightingMode == 2)
        {
            
            //Night
            foreach (var hl in carHeadLights)
            {
                hl.SetActive(true);
            }
        }

        else
        {
            //Day
            foreach (var hl in carHeadLights)
            {
                hl.SetActive(false);
            }
        }
    }
    
    
    public void StartBtn()
    {
        //Game Start - Normal Camera 
        flyOverCameraGO.SetActive(false);
        mainCameraGO.SetActive(true);
        
        iniCMVCCam.Priority = 1;
        defCMVCCam.Priority = 0;
        
         isTimerStarted = true;
         StartCoroutine("CountDownTimer");
    }

    IEnumerator CountDownTimer()
    {
        yield return new WaitForSeconds(1.3f);
        
        //COUNTDOWNTIMER SOUND
        //AudioManager.Play(AudioManager.Instance.sfxAll.countDownSound);
        
        countdownLights[0].SetActive(true);
        yield return new WaitForSeconds(1f);
        countdownLights[1].SetActive(true);
        yield return new WaitForSeconds(1f);
        
        UIManager.Instance.gameUIPanel.GetComponent<RectTransform>().DOAnchorPos(Vector2.zero,0.7f);
        iniCMVCCam.Priority = 0;
        defCMVCCam.Priority = 1;
        
        //AudioManager.Instance.Play(AudioManager.Instance.musicTracks.MusicTrackAudioSource);
        
        countdownLights[2].SetActive(true);
        yield return new WaitForSeconds(1f);
        
        
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
        PlayerController.Instance.gameControlsClass.gestureState = GameControls.GestureState.Release;
        // UiManager.BoostBtn.transform.DOScale(new Vector3(0.6f,0.6f,0.6f), 1f).SetEase(Ease.OutBounce);
        //
        // currentPlayerCarModel.transform.GetChild(3).DOScale(new Vector3(1f,1f,1f), 1f).SetEase(Ease.OutBounce);

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
            
            
            #region STATUS INDICATOR
            
            var mySequence = DOTween.Sequence();
            
            mySequence.Append( UiManager.StatusIndicatorPanelGO.GetComponent<RectTransform>().DOAnchorPos(new Vector2(-921,545f), 0f)
                .OnComplete(() => UiManager.StatusIndicatorPanelGO.GetComponent<RectTransform>().DOAnchorPos(new Vector2(0,545f), 0.3f).SetEase(Ease.Flash)));
            
            
            mySequence.AppendInterval(2);

            mySequence.OnComplete(() => UiManager.StatusIndicatorPanelGO.GetComponent<RectTransform>().DOAnchorPos(new Vector2(921,545f), 0.3f).SetEase(Ease.Flash));
            
            #endregion
            
        }
        
        if (lapCounter < totalLaps)
        {
            UiManager.StatusIndicatorPanelGO.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text =
                "LAP : " + (lapCounter+1) + "/" + totalLaps;

            if (lapCounter == totalLaps - 1)
            {
                UiManager.StatusIndicatorPanelGO.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text =
                    "FINAL LAP";
            }
            #region STATUS INDICATOR
            
            var mySequence = DOTween.Sequence();
            
            mySequence.Append( UiManager.StatusIndicatorPanelGO.GetComponent<RectTransform>().DOAnchorPos(new Vector2(-921,545f), 0f)
                .OnComplete(() => UiManager.StatusIndicatorPanelGO.GetComponent<RectTransform>().DOAnchorPos(new Vector2(0,545f), 0.3f).SetEase(Ease.Flash)));
            
            
            mySequence.AppendInterval(2);

            mySequence.OnComplete(() => UiManager.StatusIndicatorPanelGO.GetComponent<RectTransform>().DOAnchorPos(new Vector2(921,545f), 0.3f).SetEase(Ease.Flash));
            
            #endregion
            
            //lapText.text = (lapCounter+1) + "/" + totalLaps;
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
        if (LevelProgressUi.playerPosi == 1)
        {
            endConfetti.SetActive(true);
            yield return new WaitForSeconds(0.1f);
            continueButton.DOScale(Vector3.one, 0.5f);
            
        }

        else
        {
            exitButton.DOScale(Vector3.one, 0.5f);
        }
        

        yield return new WaitForSeconds(0f);
        isGameEnded = true;
        UiManager.BoostBtn.gameObject.SetActive(false);
        cameraRotator.SetActive(true);

        //blackScreenFadingPanel.transform.GetChild(0).DOScale(new Vector3(5, 5, 1), 1f).SetEase(Ease.OutBounce);
        
        isGameStarted = false;
        GameManager.Instance.canControlCar = false;
        
    }

    public void GoToStadium()
    {
        if (LevelProgressUi.playerPosi == 1)
        {
            _gameManager.LoadScene("Concert_Scn");
        }
        
    }

    #region ALLABOUTBOOST
    
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
        
        //ENABLE BOOST BTN
        UiManager.BoostBtn.GetComponent<Button>().enabled = true;
        
        //DISABLE ALL THE BOOST PICKUPS
        foreach (GameObject x in boostPickUps)                                                            //Dissable all the boost pickups
        {
            if(x != null)
                x.SetActive(false);
        }
        
        
        //SHINY EFFECT
        UiManager.BoostBtn.transform.GetChild(0).GetChild(0).GetComponent<UIShiny>().effectPlayer.play = true;
        UiManager.BoostBtn.transform.GetChild(0).GetChild(1).GetComponent<UIShiny>().effectPlayer.play = true;
        UiManager.BoostBtn.GetComponent<DOTweenAnimation>().DOPlay();
        
        
    }
    
    
    public void BoostCarButton()
    {
        StartCoroutine("BoostCarSettings");
    }

    
    IEnumerator BoostCarSettings()
    {
        isBoosting = true;

        //VIBRATE ON BOOST BTN PRESSED
        if (_audioManager.isHapticEnabled)
            currentPlayerCarModel.GetComponent<HapticSource>().Play();

         speedLinesEffect.SetActive(true);
        
        //disable shiny effect
       UiManager.BoostBtn.transform.GetChild(0).GetChild(0).GetComponent<UIShiny>().effectPlayer.play = false;
       UiManager.BoostBtn.transform.GetChild(0).GetChild(0).GetComponent<UIShiny>().effectFactor = 0;
       UiManager.BoostBtn.transform.GetChild(0).GetChild(1).GetComponent<UIShiny>().effectPlayer.play = false;
       UiManager.BoostBtn.transform.GetChild(0).GetChild(1).GetComponent<UIShiny>().effectFactor = 0;

       UiManager.BoostBtn.GetComponent<DOTweenAnimation>().DOPause();
       UiManager.BoostBtn.transform.DOScale(new Vector3(0.5f,0.5f,0.5f), 0f);
        
       LevelManager.Instance.playerVehicleManager.carEffects.NOSEffectsPS.Play();                  //NOS Particle Effect
        PlayerController.Instance.targetSpeed = PlayerController.Instance.boostSpeed;                                       //Set speed to boost speed
        UiManager.BoostBtn.GetComponent<Button>().enabled = false;
        
        //focus.SetFocused(currentPlayerCarModel);                                                                                        //blur effects
        playerVehicleManager.carEffects.boostActivatedEffect.Play();                                                    //Shield effect
        
        DOTween.To(() => cmvc.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset.z,                     ////damping camera effect
                x => cmvc.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset.z = x, -3f, 0.3f)
            .OnUpdate(() => {
                        
            });
       
        

        foreach (GameObject x in pplToDisable)                                                                            //Disable all the people
        {
            if(x!= null)
                x.SetActive(false);
        }
        
       
            //Boost Image back to original
        foreach (var abc in boostFiller)                    
        {
            DOTween.To(() => abc.fillAmount, 
                    x => abc.fillAmount = x, 0f, 6.4f)
                .OnUpdate(() => {
                        
                });
        }
        
        //WHEN BOOST IS DONE
        yield return new WaitForSeconds(5f);

        
        
        speedLinesEffect.SetActive(false);
        
        foreach (GameObject x in boostPickUps)                                                            //Dissable all the boost pickups
        {
            if(x != null)
                x.SetActive(true);
        }
        
        isBoosting = false;
        
        foreach (GameObject x in pplToDisable)
        {
            if(x != null)
                x.SetActive(true);
        }

        LevelManager.Instance.playerVehicleManager.carEffects.NOSEffectsPS.Stop();                    //NOS Particle Effect
        PlayerController.Instance.targetSpeed = PlayerController.Instance.targetSpeed;                                                                //normal speed
        
        
        //focus.SetFocused(null);                                                                                            //unblur
        playerVehicleManager.carEffects.boostActivatedEffect.Stop();
        
        
        DOTween.To(() => cmvc.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset.z,         ////damping camera effect
                x => cmvc.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset.z = x, -2.24f, 0.8f)
            .OnUpdate(() => {
                        
            });
    }
    
    #endregion
    
    

    public void ResetGame()
    {
        _gameManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ReturnToMainMenu()
    {
        _gameManager.LoadScene("HomeScreen");
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
        UiManager.crashedPanel.SetActive(false);
        yield return new WaitForSeconds(0.1f);
        
        if(!adStuff && continueCounter!=5)
            continueCounter++;
        
        if (continueCounter == 2)
        {
            UiManager.crashedPanel.transform.GetChild(0).GetChild(2).gameObject.SetActive(true);           //ADD GET MORE LIVES BUTTON
            UiManager.crashedPanel.transform.GetChild(0).GetChild(0).gameObject.SetActive(false);         //CONTINUE BUTTON REMOVE
            
        }
        
        
        isGameStarted = true;
        isCrashed = false;
        
        
        

        PlayerController.Instance.playerPF.speed = playerVehicleManager.carSpeedSettings.normalSpeed;
        PlayerController.Instance.playerPF.enabled = true;
        
        GameManager.Instance.canControlCar = true;
        PlayerController.Instance.gameControlsClass.gestureState = GameControls.GestureState.Release;

        playerVehicleManager.postCrashStuff.crashPS.Stop();
        playerVehicleManager.postCrashStuff.up_car.SetActive(true);
        playerVehicleManager.postCrashStuff.down_car.SetActive(false);

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
        
        yield return new WaitForSeconds(0f);
        
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

    public void ToggleSFX()
    {
        _audioManager.ToggleSFX();
    }

    public void ToggleMusic()
    {
        _audioManager.ToggleMusic();
    }
    
    
}
