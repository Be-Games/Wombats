using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using Coffee.UIEffects;
using DG.Tweening;
using Lofelt.NiceVibrations;
using NatSuite.Examples;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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

    
    [Header("Other Class Ref")] 
    public UIManager _uiManager;
    public LevelProgressUI _levelProgressUI;
    public AudioManager _audioManager;
    [SerializeField]private GameManager _gameManager;
    public VehicleManager _playerVehicleManager;
    public GameControls _gameControls;
    public PlayerController _playerController;
    public EnvironmentSettingsManager envManager;
    public GifRecording gifRecording;

    [Header("Camera Refs")]
    public GameObject mainCameraGO;
    public CinemachineVirtualCamera defCMVCCam;
    public GameObject flyOverCameraGO;
    public CinemachineVirtualCamera iniCMVCCam;
    public GameObject cameraRotator;
    
    [Header("All Cars Visuals")]
    public GameObject playerVisual;
    public GameObject enemyLeftVisual;
    public GameObject enemyRightVisual;

    [Header("Lap Settings")]
    [HideInInspector] public int lapCounter = 0;
    [HideInInspector] public bool isLapTriggered;
    [HideInInspector] public GameObject[] levelTimeObjects;

    [Header("Scoring Stuff")] 
    [HideInInspector] public int currentScore;
    [HideInInspector]public int levelHighScore;


    [Header("Boost Settings")] 
    [HideInInspector]public int totalBoostNumber;                                                                            //Total Times boost can be done
    public float individualBoostCounter;                                                                    //Total boost Counter ( shd be 3 )
    public Image[] boostFiller;
    [HideInInspector]public bool isBoosting;



    [Header("CountDownTimer Settings")]
    [HideInInspector] public bool isTimerStarted;
    public TextMeshProUGUI timerText;
    public GameObject[] countdownLights;

    
    [Header("Bool Values")]
    public bool isGameStarted;
    public bool isCrashed;
    public bool isCrashedWithPpl;
    public bool adStuff;
    public bool isGameEnded;
    public bool isFinalLap;
    
    [Header("Weather Effects")] 
    public ParticleSystem slowWind;
    public ParticleSystem FastWind;
    public ParticleSystem[] allEffects,allEffects2;               
    public ParticleSystem speedLinesEffect,windGrassEffect;
    
    
    [Header("Misc Stuff")]
    public float startTime;
    public Collider playerCarCollidersToToggle;
    public TextMeshProUGUI carContinueChances;
    public int continueCounter;
    public Material backLightMaterial;
    public Color redBL, whiteBL;
    
   

    [Header("Level AI Difficulty")] 
    public bool Easy;
    public float easyValue;

    
    
    
    [Header("Stuff to Manually Modify For Each Level")]
    [Space(100)]
    //rubix tower if present
    public GameObject rubixTower;                                                
    public int totalLaps;
    public List<GameObject> boostPickUps;
    public List<GameObject> pplToDisable;
    public float singleLapDistance;
    public GameObject endConfetti;
    public string cityName;
    
    [Header("Stadium Spawn Things")] 
    public GameObject stadiumPrefab;
    public GameObject[] stuffToRemove;
    public Transform stadiumTransform;

    [Header("Social Sharing Stuff")] 
    

    [Header("Obstacles spawn Stuff")] 
    public GameObject dayObstaclesPf;
    public GameObject nightObstaclesPf;
    public Transform dayParent;
    public Transform nightParent;
    private GameObject tempPrefab;
    
    
    
    public RectTransform continueButton,shareBtn;
    public List<GameObject> carHeadLights;
    
    private void Awake()
    {
        _instance = this;
        
        _gameManager = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();
        // if (_gameManager)
        // {
        //     currentPlayerCarModel = _gameManager.playerCarModels;
        //     enemy1 = _gameManager.enemyCarModels[0];
        //     enemy2 = _gameManager.enemyCarModels[1];
        // }
        //
        //
        // Instantiate(currentPlayerCarModel, CARMODELgo.transform.position, sampleCartransform.rotation, CARMODELgo.transform);
        // Instantiate(enemy1, ENEMYLEFTgo.transform.position, sampleCartransform.rotation, ENEMYLEFTgo.transform);
        // Instantiate(enemy2, ENEMYRIGHTgo.transform.position, sampleCartransform.rotation, ENEMYRIGHTgo.transform);
        //
        
        _playerVehicleManager = GameObject.FindGameObjectWithTag("Player").GetComponent<VehicleManager>();
        _audioManager = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();
    }
    
    private void Start()
    {

        InitializingVariables();
        SceneStart();
        LightingAndObstaclesChecker();
        WeatherSetup();
        
       // _audioManager.LoadIcons();
        if (_audioManager.isMusicEnabled)
        {
            _audioManager.musicTracks.mainMenuAudioSource.Stop();
            _audioManager.musicTracks.mainMenuAudioSource.gameObject.SetActive(false);
            _audioManager.i = 0;
            _audioManager.isTrackFinishedG = false;
        }
        
    }

    void InitializingVariables()
    {
        playerCarCollidersToToggle = playerVisual.GetComponent<Collider>();
        
        currentScore = 0;
        _uiManager.scoreText.text = currentScore.ToString();
        isFinalLap = false;
        _uiManager.BoostBtn.GetComponent<Button>().enabled = false;
        
        //Startup rewarded system 
        if(_gameManager != null)
            _gameManager.rewardedAd.IniRewardedSystem();
        
        _playerVehicleManager.overHeadBoostUI.timerText.gameObject.SetActive(false);
        _uiManager.resumeTimer.gameObject.SetActive(false);
    }

    void SceneStart()
    {
        startTime = Time.time;
        
        //Game Start - Flyover Camera 
        _uiManager.flyThroughCamCityName.text = cityName + " TOUR ";
        flyOverCameraGO.SetActive(true);
        mainCameraGO.SetActive(false);
        
        //Back smoke effects off for all cars
        _playerVehicleManager.carEffects.carBreakSmokeL.GetComponent<ParticleSystem>().Stop();
        _playerVehicleManager.carEffects.carBreakSmokeR.GetComponent<ParticleSystem>().Stop();
        
      //  enemyLeftVisual.GetComponent<VehicleManager>().carEffects.carBreakSmokeL.GetComponent<ParticleSystem>().Stop();
      //  enemyLeftVisual.GetComponent<VehicleManager>().carEffects.carBreakSmokeR.GetComponent<ParticleSystem>().Stop();
        
      //  enemyRightVisual.GetComponent<VehicleManager>().carEffects.carBreakSmokeL.GetComponent<ParticleSystem>().Stop();
      //  enemyRightVisual.GetComponent<VehicleManager>().carEffects.carBreakSmokeR.GetComponent<ParticleSystem>().Stop();
    }

    void LightingAndObstaclesChecker()
    {
        if (_gameManager.lightingMode == 1)
        {
            //DAY OBSTACLES
            tempPrefab =  (GameObject)Instantiate(dayObstaclesPf, dayParent);
            tempPrefab.tag = "TEMP";
        }
        
        if (_gameManager.lightingMode == 2)
        {
            //NIGHT OBSTACLES
            tempPrefab =  (GameObject)Instantiate(nightObstaclesPf, nightParent);
            tempPrefab.tag = "TEMP";
        }
    }
    
    void WeatherSetup()
    {
        
        
        slowWind.Stop();
        FastWind.Stop();
        speedLinesEffect.Stop();
        windGrassEffect.Stop();
        
        envManager.Clear();
        
        _gameManager.weatherEffect = Random.Range(0, allEffects.Length);
        if(_gameManager.weatherEffect == 0)
            envManager.Clear();
        if (_gameManager.weatherEffect == 1)
            envManager.Rain();
        if(_gameManager.weatherEffect == 2)
            envManager.Clear();
        if(_gameManager.weatherEffect == 3)
            envManager.Snow();
        
    }
    
    
    public void StartBtn()
    {
        //Game Start - Normal Camera 
        flyOverCameraGO.SetActive(false);
        mainCameraGO.SetActive(true);
        
        if(_gameManager.weatherEffect == 0)
            envManager.Clear();
        if (_gameManager.weatherEffect == 1)
            envManager.Rain();
        if(_gameManager.weatherEffect == 2)
            envManager.Clear();
        if(_gameManager.weatherEffect == 3)
            envManager.Snow();
        
        _uiManager.flyThruPanel.SetActive(false);
        _uiManager.FlyOverBlackPanelsTweening();
        
        
        iniCMVCCam.Priority = 1;
        defCMVCCam.Priority = 0;
        
         isTimerStarted = true;
         StartCoroutine("CountDownTimer");
    }

    IEnumerator CountDownTimer()
    {
        yield return new WaitForSeconds(2.1f);
        
        //COUNTDOWNTIMER SOUND
         if(_audioManager && _audioManager.isSFXenabled)
             _audioManager.Play(_audioManager.sfxAll.countDownSound);
        
        
        
        countdownLights[0].SetActive(true);
        yield return new WaitForSeconds(1f);
        countdownLights[1].SetActive(true);
        yield return new WaitForSeconds(1f);
        
       _uiManager.gameUIPanel.GetComponent<RectTransform>().DOAnchorPos(Vector2.zero, 0.7f);
        iniCMVCCam.Priority = 0;
        defCMVCCam.Priority = 1;

        if (_audioManager.isMusicEnabled)
        {
            _audioManager.musicTracks.MusicTrackAudioSource.gameObject.SetActive(true);
            _audioManager.musicTracks.MusicTrackAudioSource.Play();
        }
        
        //Back smoke effects on for all cars
        _playerVehicleManager.carEffects.carBreakSmokeL.GetComponent<ParticleSystem>().Play();
        _playerVehicleManager.carEffects.carBreakSmokeR.GetComponent<ParticleSystem>().Play();
        
       // enemyLeftVisual.GetComponent<VehicleManager>().carEffects.carBreakSmokeL.GetComponent<ParticleSystem>().Play();
       // enemyLeftVisual.GetComponent<VehicleManager>().carEffects.carBreakSmokeR.GetComponent<ParticleSystem>().Play();
        
       // enemyRightVisual.GetComponent<VehicleManager>().carEffects.carBreakSmokeL.GetComponent<ParticleSystem>().Play();
       // enemyRightVisual.GetComponent<VehicleManager>().carEffects.carBreakSmokeR.GetComponent<ParticleSystem>().Play();
            
        
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
        _gameControls.gestureState = GameControls.GestureState.Release;
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
            
            mySequence.Append( _uiManager.StatusIndicatorPanelGO.GetComponent<RectTransform>().DOAnchorPos(new Vector2(-1233, _uiManager.StatusIndicatorPanelGO.GetComponent<RectTransform>().anchoredPosition.y), 0f)
                .OnComplete(() => _uiManager.StatusIndicatorPanelGO.GetComponent<RectTransform>().DOAnchorPos(new Vector2(0,_uiManager.StatusIndicatorPanelGO.GetComponent<RectTransform>().anchoredPosition.y), 0.3f).SetEase(Ease.Flash)));
            
            
            mySequence.AppendInterval(2);

            mySequence.OnComplete(() => _uiManager.StatusIndicatorPanelGO.GetComponent<RectTransform>().DOAnchorPos(new Vector2(1233,_uiManager.StatusIndicatorPanelGO.GetComponent<RectTransform>().anchoredPosition.y), 0.3f).SetEase(Ease.Flash));
            
            #endregion
            
        }
        
        if (lapCounter < totalLaps)
        {
            
            if (_gameManager.lightingMode == 1)
            {
                Destroy(GameObject.FindGameObjectWithTag("TEMP"));
                
                //DAY OBSTACLES
                tempPrefab =  (GameObject)Instantiate(dayObstaclesPf, dayParent);
                tempPrefab.tag = "TEMP";
            }
        
            if (_gameManager.lightingMode == 2)
            {
                Destroy(GameObject.FindGameObjectWithTag("TEMP"));
                
                //NIGHT OBSTACLES
                tempPrefab =  (GameObject)Instantiate(nightObstaclesPf, nightParent);
                tempPrefab.tag = "TEMP";
            }
            
            // boostPickUps = new List<GameObject>();
            // if (GameObject.FindGameObjectWithTag("Boost").activeInHierarchy)
            // {
            //     boostPickUps.AddRange(GameObject.FindGameObjectsWithTag("Boost"));
            // }
            
        
            pplToDisable = new List<GameObject>();
            if (GameObject.FindGameObjectWithTag("People").activeInHierarchy)
            {
                pplToDisable.AddRange(GameObject.FindGameObjectsWithTag("People")); 
            }

            // for(var i = pplToDisable.Count - 1; i > -1; i--)
            // {
            //     if (pplToDisable[i] == null)
            //         pplToDisable.RemoveAt(i);
            // }
            
            
            _uiManager.StatusIndicatorPanelGO.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text =
                "LAP : " + (lapCounter+1) + "/" + totalLaps;

            if (lapCounter == totalLaps - 1)
            {
                _uiManager.StatusIndicatorPanelGO.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text =
                    "FINAL LAP";
                
                isFinalLap = true;
            }

            
            #region STATUS INDICATOR
            
            var mySequence = DOTween.Sequence();
            
            mySequence.Append( _uiManager.StatusIndicatorPanelGO.GetComponent<RectTransform>().DOAnchorPos(new Vector2(-921,545f), 0f)
                .OnComplete(() => _uiManager.StatusIndicatorPanelGO.GetComponent<RectTransform>().DOAnchorPos(new Vector2(0,545f), 0.3f).SetEase(Ease.Flash)));
            
            
            mySequence.AppendInterval(2);

            mySequence.OnComplete(() => _uiManager.StatusIndicatorPanelGO.GetComponent<RectTransform>().DOAnchorPos(new Vector2(921,545f), 0.3f).SetEase(Ease.Flash));
            
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
        if (_levelProgressUI.playerPosi == 1)
        {
            continueButton.DOScale(Vector3.zero, 0f);
            shareBtn.DOScale(Vector3.zero, 0f);
            endConfetti.SetActive(true);
            
            yield return new WaitForSeconds(0.15f);
            
            isGameEnded = true;
            _uiManager.BoostBtn.gameObject.SetActive(false);
            cameraRotator.SetActive(true);
            isGameStarted = false;
            GameManager.Instance.canControlCar = false;
            
            yield return new WaitForSeconds(1f);
            
            //ReplayKitDemo.StopRecording();                                //recording Stop
            
            yield return new WaitForSeconds(0.1f);
            
            continueButton.DOScale(Vector3.one, 0.8f).SetEase(Ease.Flash);
            shareBtn.DOScale(Vector3.one, 0.8f).SetEase(Ease.Flash);
        }

        else
        {
            isGameEnded = true;
            _uiManager.BoostBtn.gameObject.SetActive(false);
            isGameStarted = false;
            GameManager.Instance.canControlCar = false;
            _uiManager.postAdCrashPanel.SetActive(true);
        }
        
     
    }

    public void GoToStadium()
    {
        if (_levelProgressUI.playerPosi == 1)
        {
            _gameManager.LoadScene("Concert_Scn");
        }
        //ReplayKitDemo.Discard();
        else
        {
            _uiManager.postAdCrashPanel.SetActive(true);
        }
    }
    

    #region ALLABOUTBOOST
    
    public void BoostManager()
    {
        individualBoostCounter += 1;    
       _playerVehicleManager.carEffects.boostCapturedEffectPS.Play();
        
            //BOOST FILL AMOUNT image
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
        
        //ENABLE BOOST BTN
        _uiManager.BoostBtn.GetComponent<Button>().enabled = true;
        
        //SHINY EFFECT
        _uiManager.BoostBtn.transform.GetChild(0).GetChild(0).GetComponent<UIShiny>().effectPlayer.play = true;
        _uiManager.BoostBtn.transform.GetChild(0).GetChild(1).GetComponent<UIShiny>().effectPlayer.play = true;
        _uiManager.BoostBtn.GetComponent<DOTweenAnimation>().DOPlay();
        
        //DISABLE ALL THE BOOST PICKUPS
        // foreach (GameObject x in boostPickUps)                                                            //Dissable all the boost pickups
        // {
        //     if(x != null)
        //         x.SetActive(false);
        // }
        
    }
    
    
    public void BoostCarButton()
    {
        _uiManager.BoostBtn.GetComponent<DOTweenAnimation>().DOPause();
        _uiManager.BoostBtn.transform.DOScale(new Vector3(0.4f, 0.4f, 0.4f), 0f);
        
       
        
        StartCoroutine("BoostCarSettings");
    }

    
    IEnumerator BoostCarSettings()
    {
        
        CarBoostOn();

        _playerVehicleManager.overHeadBoostUI.timerText.gameObject.SetActive(true);
        _playerVehicleManager.overHeadBoostUI.timerText.text = "00:09";
        yield return new WaitForSeconds(1f);
        _playerVehicleManager.overHeadBoostUI.timerText.text = "00:08";
        yield return new WaitForSeconds(1f);
        _playerVehicleManager.overHeadBoostUI.timerText.text = "00:07";
        yield return new WaitForSeconds(1f);
        _playerVehicleManager.overHeadBoostUI.timerText.text = "00:06";
        yield return new WaitForSeconds(1f);
        _playerVehicleManager.overHeadBoostUI.timerText.text = "00:05";
        yield return new WaitForSeconds(1f);
        _playerVehicleManager.overHeadBoostUI.timerText.text = "00:04";
        yield return new WaitForSeconds(1f);
        _playerVehicleManager.overHeadBoostUI.timerText.text = "00:03";
        yield return new WaitForSeconds(1f);
        _playerVehicleManager.overHeadBoostUI.timerText.text = "00:02";
        yield return new WaitForSeconds(1f);
        _playerVehicleManager.overHeadBoostUI.timerText.text = "00:01";
        yield return new WaitForSeconds(1f);
        _playerVehicleManager.overHeadBoostUI.timerText.gameObject.SetActive(false);
        
        CarBoostOff();
        
        
        // DOTween.To(() => defCMVCCam.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset.z,                     ////damping camera effect
        //         x => defCMVCCam.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset.z = x, -3f, 0.5f)
        //     .OnUpdate(() => {
        //                 
        //     });
        
        //VIBRATE ON BOOST BTN PRESSED
        
        // if (_audioManager.isHapticEnabled)
           // playerVisual.GetComponent<HapticSource>().Play();

           // float duration = 9f; // 3 seconds you can change this to
           //
           // float totalTime = 0;
           // float myValue;
           // while(totalTime <= duration)
           // {
           //     myValue = totalTime / duration;
           //     totalTime += Time.deltaTime;
           //     var integer = (int)totalTime; /* choose how to quantize this */
           //     _playerVehicleManager.overHeadBoostUI.timerText.text = integer.ToString();
           //     /* convert integer to string and assign to text */
           //     yield return null;
           // }
           
           // _playerVehicleManager.overHeadBoostUI.boostSlider.gameObject.SetActive(true);
           // DOTween.To(() => _playerVehicleManager.overHeadBoostUI.boostSlider.value,         ////damping camera effect
           //         x => _playerVehicleManager.overHeadBoostUI.boostSlider.value = x, 0f, 8f).SetEase(Ease.Flash)
           //     .OnUpdate(() => {
           //         CarBoostOff();
           //     });
       
       
        //WHEN BOOST IS DONE
        yield return new WaitForSeconds(8f);

        
        // foreach (GameObject x in boostPickUps)                                                            //Dissable all the boost pickups
        // {
        //     if(x != null)
        //         x.SetActive(true);
        // }
        
        
        
        // DOTween.To(() => defCMVCCam.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset.z,         ////damping camera effect
        //         x => defCMVCCam.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset.z = x, -1.75f, 0.8f)
        //     .OnUpdate(() => {
        //                 
        //     });
    }


    void CarBoostOn()
    {
        //blur boost panel appear
        _uiManager.boostBlurPanel.SetActive(true);
        _uiManager.boostBlurPanel.GetComponent<Image>().DOFade(1f, 0.5f).SetEase(Ease.Flash);
        
        
       // disable shiny effect
         _uiManager.BoostBtn.transform.GetChild(0).GetChild(0).GetComponent<UIShiny>().effectPlayer.play = false;
         _uiManager.BoostBtn.transform.GetChild(0).GetChild(0).GetComponent<UIShiny>().effectFactor = 0;
         _uiManager.BoostBtn.transform.GetChild(0).GetChild(1).GetComponent<UIShiny>().effectPlayer.play = false;
         _uiManager.BoostBtn.transform.GetChild(0).GetChild(1).GetComponent<UIShiny>().effectFactor = 0;
        
        isBoosting = true;
        
        //speed change
        _playerController.targetSpeed = _playerController.boostSpeed;    
        
        //Weather Effects
        windGrassEffect.gameObject.SetActive(true);
        speedLinesEffect.gameObject.SetActive(true);
        
        //Car Effects
        _playerVehicleManager.carEffects.NOSEffectsPS.gameObject.SetActive(true);                 //NOS Particle Effect
        
        //boost btn changes
        _uiManager.BoostBtn.transform.DOScale(new Vector3(0.5f,0.5f,0.5f), 0f);
        _uiManager.BoostBtn.GetComponent<Button>().enabled = false;
        
        //Boost Image back to original
        foreach (var abc in boostFiller)                    
        {
            DOTween.To(() => abc.fillAmount, 
                    x => abc.fillAmount = x, 0f, 9f)
                .OnUpdate(() => {
                        
                });
        }
        
        //ppl disable
        foreach (GameObject x in pplToDisable)                                                                        
        {
            if(x!= null)
                x.SetActive(false);
        }
    }

    void CarBoostOff()
    {
        isBoosting = false;
        
        //blur boost panel appear
        _uiManager.boostBlurPanel.SetActive(false);
        _uiManager.boostBlurPanel.GetComponent<Image>().DOFade(0f, 0f).SetEase(Ease.Flash);
        
        //speed change
        _playerController.targetSpeed = _playerController.normalSpeed;    
        
        //Weather Effects
        windGrassEffect.gameObject.SetActive(false);
        speedLinesEffect.gameObject.SetActive(false);
        
        //Car Effects
        _playerVehicleManager.carEffects.NOSEffectsPS.gameObject.SetActive(false);                 //NOS Particle Effect
        
        //enable ppl
        foreach (GameObject x in pplToDisable)
        {
            if(x != null)
                x.SetActive(true);
        }
        
        individualBoostCounter = 0;
    }
    #endregion
    
    

    public void ResetGame()
    {
        Time.timeScale = 1;
        _gameManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ReturnToMainMenu()
    {
        Time.timeScale = 1;
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
        _gameControls.gestureState = GameControls.GestureState.Release;
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
        _uiManager.crashedPanel.SetActive(false);
        _playerVehicleManager.postCrashStuff.crashPS.gameObject.SetActive(false);
        _playerVehicleManager.postCrashStuff.up_car.SetActive(true);
        _playerVehicleManager.postCrashStuff.down_car.SetActive(false);
        
        playerCarCollidersToToggle.enabled = false;
        
       //show timer
       _uiManager.resumeTimer.gameObject.SetActive(true);
       _uiManager.resumeTimer.text = "3";
       yield return new WaitForSeconds(0.7f);
       _uiManager.resumeTimer.text = "2";
       yield return new WaitForSeconds(0.7f);
       _uiManager.resumeTimer.text = "1";
       yield return new WaitForSeconds(0.7f);
       _uiManager.resumeTimer.gameObject.SetActive(false);
       
       //RESUME WITH THE STUFF 
       
        yield return new WaitForSeconds(0.1f);
        
        if(!adStuff && continueCounter!=5)
            continueCounter++;
        
        if (continueCounter == 2)
        {
            _uiManager.rewardBtn.SetActive(true);           //ADD GET MORE LIVES BUTTON
            _uiManager.continueBtn.SetActive(false);        //CONTINUE BUTTON REMOVE
            
        }
        
        
        isGameStarted = true;
        isCrashed = false;
        
        
        _playerController.playerPF.speed = _playerVehicleManager.carSpeedSettings.normalSpeed;
        _playerController.playerPF.enabled = true;
        
        GameManager.Instance.canControlCar = true;
        _gameControls.gestureState = GameControls.GestureState.Release;

        yield return new WaitForSeconds(0.5f);
        playerCarCollidersToToggle.enabled = true;

        // foreach (GameObject x in pplToDisable)
        // {
        //     if(x != null)
        //         x.SetActive(true);
        // }
    
        
        
        // _playerController.PlayercarVisual.SetActive(false);
        //
        // yield return new WaitForSeconds(0.2f);
        //
        // _playerController.PlayercarVisual.SetActive(true);
        //
        // yield return new WaitForSeconds(0.2f);
        //
        // _playerController.PlayercarVisual.SetActive(false);
        //
        // yield return new WaitForSeconds(0.2f);
        //
        // _playerController.PlayercarVisual.SetActive(true);
        //
        // yield return new WaitForSeconds(0.2f);
        //
        // _playerController.PlayercarVisual.SetActive(false);
        //
        // yield return new WaitForSeconds(0.2f);
        //
        // _playerController.PlayercarVisual.SetActive(true);
        //
        // yield return new WaitForSeconds(0.2f);
        //
        // _playerController.PlayercarVisual.SetActive(false);
        //
        //
        // yield return new WaitForSeconds(0.2f);
        //
        // _playerController.PlayercarVisual.SetActive(true);
        //
        // yield return new WaitForSeconds(0.2f);
        //
        // _playerController.PlayercarVisual.SetActive(false);
        //
        // yield return new WaitForSeconds(0.2f);
        //
        // _playerController.PlayercarVisual.SetActive(true);
        //
        // yield return new WaitForSeconds(0.2f);
        //
        // _playerController.PlayercarVisual.SetActive(false);
        //
        // yield return new WaitForSeconds(0.2f);
        //
        // _playerController.PlayercarVisual.SetActive(true);
        
        playerCarCollidersToToggle.enabled = true;
        
        yield return new WaitForSeconds(0f);
        
    }
    

    public void ReceiveLifeBtn()
    {
        
        _uiManager.extraLifePanel.SetActive(false);
        adStuff = true;
        continueCounter = 5;
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

    public void RunRewardedAd()
    {
        _gameManager.rewardedAd.rewarded();
        Invoke("ShowRevivePanel",2f);
    }

    void ShowRevivePanel()
    {
        _uiManager.receiveLifePanel.SetActive(true);
    }
    
}
