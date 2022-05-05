using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using Coffee.UIEffects;
using DataManager;
using DG.Tweening;
using Lofelt.NiceVibrations;
using NatSuite.Examples;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Analytics;

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
    public GameObject[] carVisualsParents;

    [Header("Lap Settings")]
    [HideInInspector] public int lapCounter = 0;
    [HideInInspector] public bool isLapTriggered;

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
    public GameObject smokeTransitionPostCrashEffect;
    
    
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

    [Header("Camera Damping Values")] 
    public float boostOnValue;
    public float breakValue;
    public float defaultValue;
    
    

    private void Awake()
    {
        _instance = this;
        
        _gameManager = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();

        GameObject playerCarPrefab = new GameObject();
        GameObject enemyLPrefab = new GameObject();
        GameObject enemyRPrefab = new GameObject();

        if (_gameManager != null)
            totalLaps = _gameManager.numberOfLaps;
        
          if (_gameManager)
          {
              if (_gameManager.memeberIndex == 0)        //murph
              {
                  playerCarPrefab = Instantiate(_gameManager.murphPrefabs[_gameManager.selectedCarModelPLAYER-1],
                      carVisualsParents[0].transform.position, carVisualsParents[0].transform.rotation, carVisualsParents[0].transform);
                  
                  enemyLPrefab = Instantiate(_gameManager.danPrefabs[_gameManager.enemyCar1],
                      carVisualsParents[1].transform.position, carVisualsParents[1].transform.rotation, carVisualsParents[1].transform);
                  
                  enemyRPrefab = Instantiate(_gameManager.tordPrefabs[_gameManager.enemyCar2],
                      carVisualsParents[2].transform.position, carVisualsParents[2].transform.rotation, carVisualsParents[2].transform);
              }
              if (_gameManager.memeberIndex == 1)        //dan
              {
                  playerCarPrefab = Instantiate(_gameManager.danPrefabs[_gameManager.selectedCarModelPLAYER-1],
                      carVisualsParents[0].transform.position, carVisualsParents[0].transform.rotation, carVisualsParents[0].transform);
                  
                  enemyLPrefab = Instantiate(_gameManager.tordPrefabs[_gameManager.enemyCar1],
                      carVisualsParents[1].transform.position, carVisualsParents[1].transform.rotation, carVisualsParents[1].transform);
                  
                  enemyRPrefab = Instantiate(_gameManager.murphPrefabs[_gameManager.enemyCar2],
                      carVisualsParents[2].transform.position, carVisualsParents[2].transform.rotation, carVisualsParents[2].transform);
              }
              if (_gameManager.memeberIndex == 2)        //tord
              {
                  playerCarPrefab = Instantiate(_gameManager.tordPrefabs[_gameManager.selectedCarModelPLAYER-1],
                      carVisualsParents[0].transform.position, carVisualsParents[0].transform.rotation, carVisualsParents[0].transform);
                  
                  enemyLPrefab = Instantiate(_gameManager.murphPrefabs[_gameManager.enemyCar1],
                      carVisualsParents[1].transform.position, carVisualsParents[1].transform.rotation, carVisualsParents[1].transform);
                  
                  enemyRPrefab = Instantiate(_gameManager.danPrefabs[_gameManager.enemyCar2],
                      carVisualsParents[2].transform.position, carVisualsParents[2].transform.rotation, carVisualsParents[2].transform);
              }
              
          }
          
          if (_gameManager)
          {
              if(playerCarPrefab)
                  playerVisual = playerCarPrefab;

              if(enemyLPrefab)
                  enemyLeftVisual = enemyLPrefab;
              
              if(enemyRPrefab)
                  enemyRightVisual = enemyRPrefab;
              
          }
           _audioManager = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();
          
         
        
        
        _playerVehicleManager = GameObject.FindGameObjectWithTag("Player").GetComponent<VehicleManager>();
       
    }
    
    private void Start()
    {
        Ana_LevelStart();
        InitializingVariables();
        SceneStart();
        LightingAndObstaclesChecker();
        WeatherSetup();
        
        UpdateIcons();

        // _audioManager.LoadIcons();

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
        _uiManager.flyThroughCamCityName.text = SceneManager.GetActiveScene().name + " TOUR ";
        flyOverCameraGO.SetActive(true);
        mainCameraGO.SetActive(false);
        
        #region CarSmokeStop
        //Back smoke effects off for all cars
        _playerVehicleManager.carEffects.carBreakSmokeL.GetComponent<ParticleSystem>().Stop();
        _playerVehicleManager.carEffects.carBreakSmokeR.GetComponent<ParticleSystem>().Stop();
        
       enemyLeftVisual.GetComponent<VehicleManager>().carEffects.carBreakSmokeL.GetComponent<ParticleSystem>().Stop();
       enemyLeftVisual.GetComponent<VehicleManager>().carEffects.carBreakSmokeR.GetComponent<ParticleSystem>().Stop();
        
       enemyRightVisual.GetComponent<VehicleManager>().carEffects.carBreakSmokeL.GetComponent<ParticleSystem>().Stop();
       enemyRightVisual.GetComponent<VehicleManager>().carEffects.carBreakSmokeR.GetComponent<ParticleSystem>().Stop();
       #endregion
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
        
        _gameManager.weatherEffect =Random.Range(0, 4);
        if(_gameManager.weatherEffect == 0)
            envManager.Clear();
        if (_gameManager.weatherEffect == 1)
            envManager.Rain();
        if(_gameManager.weatherEffect == 2)
            envManager.Snow();
        if(_gameManager.weatherEffect == 3)
            envManager.Clear();
        
    }
    
    
    public void StartBtn()
    {
        
        
        //carBlowingEngine SOUND
        if(_audioManager!=null && _audioManager.isSFXenabled)
            _audioManager.Play(_audioManager.sfxAll.carBlowingEngine);
        
        //Game Start - Normal Camera 
        flyOverCameraGO.SetActive(false);
        mainCameraGO.SetActive(true);
        
        if(_gameManager.weatherEffect == 0)
            envManager.Clear();
        if (_gameManager.weatherEffect == 1)
            envManager.Rain();
        if(_gameManager.weatherEffect == 2)
            envManager.Snow();
        if(_gameManager.weatherEffect == 3)
            envManager.Clear();
        
        _uiManager.flyThruPanel.SetActive(false);
        _uiManager.FlyOverBlackPanelsTweening();
        
        
        iniCMVCCam.Priority = 1;
        defCMVCCam.Priority = 0;
        
         isTimerStarted = true;
         StartCoroutine("CountDownTimer");
    }

    IEnumerator CountDownTimer()
    {
        yield return new WaitForSeconds(4.5f);
        
        //COUNTDOWNTIMER SOUND
         if(_audioManager!=null && _audioManager.isSFXenabled)
             _audioManager.Play(_audioManager.sfxAll.countDownSound);
        
        
        
        countdownLights[0].SetActive(true);
        yield return new WaitForSeconds(1f);
        countdownLights[1].SetActive(true);
        yield return new WaitForSeconds(1f);
        
       _uiManager.gameUIPanel.GetComponent<RectTransform>().DOAnchorPos(Vector2.zero, 0.7f);
        iniCMVCCam.Priority = 0;
        defCMVCCam.Priority = 1;

        if (_audioManager!=null && _audioManager.isMusicEnabled)
        {
            _audioManager.musicTracks.MusicTrackAudioSource.Play();
        }
        
        #region CarSmokePlay
        //Back smoke effects off for all cars
        _playerVehicleManager.carEffects.carBreakSmokeL.GetComponent<ParticleSystem>().Play();
        _playerVehicleManager.carEffects.carBreakSmokeR.GetComponent<ParticleSystem>().Play();
        
        enemyLeftVisual.GetComponent<VehicleManager>().carEffects.carBreakSmokeL.GetComponent<ParticleSystem>().Play();
        enemyLeftVisual.GetComponent<VehicleManager>().carEffects.carBreakSmokeR.GetComponent<ParticleSystem>().Play();
        
        enemyRightVisual.GetComponent<VehicleManager>().carEffects.carBreakSmokeL.GetComponent<ParticleSystem>().Play();
        enemyRightVisual.GetComponent<VehicleManager>().carEffects.carBreakSmokeR.GetComponent<ParticleSystem>().Play();
        #endregion
            
        
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

    }
    

    private void Update()
    {

        if (isGameStarted)
        {
           
            if (lapCounter >= 1)
            {
                if (isGameStarted )
                {
                
                    float t = Time.time - startTime;
                    string minutes = ((int) t / 60).ToString();
                    string seconds = (t % 60).ToString("f2");
            
                    // levelTimeObjects[lapCounter-1].GetComponent<TextMeshProUGUI>().text = "Lap " + (lapCounter) + " : " + minutes + ":" +
                    //                                                                       seconds;
            
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
                if (GameObject.FindGameObjectsWithTag("People") != null)
                {
                    pplToDisable.AddRange(GameObject.FindGameObjectsWithTag("People")); 
                }
                
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


            StatusIndicator();
            
            lapCounter++;
            
            startTime = Time.time;
        }
        
       

       Invoke("DisableCountDownLights",2f);
        
    }

    void DisableCountDownLights()
    {
        if(countdownLights[3].activeInHierarchy)
            countdownLights[3].SetActive(false);
    }


    void StatusIndicator()
    {
        #region STATUS INDICATOR

       
        
        var mySequence = DOTween.Sequence();

        mySequence.Append(_uiManager.StatusIndicatorPanelGO.transform.DOLocalMove(new Vector3(3278, 0f, 0f), 0.4f)
            .SetRelative(true).SetEase(Ease.InSine));


        mySequence.AppendInterval(1);
        
        mySequence.Append(_uiManager.StatusIndicatorPanelGO.transform.DOLocalMove(new Vector3(6556, 0f, 0f), 0.4f)
            .SetRelative(true).SetEase(Ease.InSine));
        
        mySequence.AppendInterval(3);
        
        mySequence.OnComplete(() => _uiManager.StatusIndicatorPanelGO.transform.DOLocalMove(new Vector3(-3266, 366f, 0f), 0f)
            .SetRelative(false).SetEase(Ease.Flash));
            
        #endregion
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
        
        GameManager.Instance.VibrateOnce();
        
        StartCoroutine("BoostCarSettings");
    }

    
    IEnumerator BoostCarSettings()
    {
        
        CarBoostOn();

        _playerVehicleManager.overHeadBoostUI.timerText.gameObject.SetActive(true);
        _playerVehicleManager.overHeadBoostUI.timerText.text = "09";
        yield return new WaitForSeconds(1f);
        _playerVehicleManager.overHeadBoostUI.timerText.text = "08";
        yield return new WaitForSeconds(1f);
        _playerVehicleManager.overHeadBoostUI.timerText.text = "07";
        yield return new WaitForSeconds(1f);
        _playerVehicleManager.overHeadBoostUI.timerText.text = "06";
        yield return new WaitForSeconds(1f);
        _playerVehicleManager.overHeadBoostUI.timerText.text = "05";
        yield return new WaitForSeconds(1f);
        _playerVehicleManager.overHeadBoostUI.timerText.text = "04";
        yield return new WaitForSeconds(1f);
        _playerVehicleManager.overHeadBoostUI.timerText.text = "03";
        yield return new WaitForSeconds(1f);
        _playerVehicleManager.overHeadBoostUI.timerText.text = "02";
        yield return new WaitForSeconds(1f);
        _playerVehicleManager.overHeadBoostUI.timerText.text = "01";
        yield return new WaitForSeconds(1f);
        _playerVehicleManager.overHeadBoostUI.timerText.gameObject.SetActive(false);
        
        CarBoostOff();
        
        
        

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
       
           
        
    }


    public void SetCameraDampValue(float zOffsetValue)
    {
        //Camera Far Distance
        DOTween.To(() => defCMVCCam.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset.z,         ////damping camera effect
                x => defCMVCCam.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset.z = x,zOffsetValue, 0.8f)
            .OnUpdate(() => {
                        
            });
    }
    void CarBoostOn()
    {
        SetCameraDampValue(boostOnValue);
        
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
        _uiManager.BoostBtn.transform.DOScale(new Vector3(0.4f,0.4f,0.4f), 0f);
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
        SetCameraDampValue(defaultValue);
        
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

    private int x;
    private int y;
    IEnumerator RaceFinished()
    {
        _gameManager.playerPosi = _levelProgressUI.playerPosi;

        switch (PlayerPrefs.GetInt("LevelIndex"))
        {
            case 2:
                PlayerPrefs.SetInt("LevelIndex",3);
                break;
            case 3:
                PlayerPrefs.SetInt("LevelIndex",4);
                break;
            case 4:
                PlayerPrefs.SetInt("LevelIndex",5);
                break;
            case 5:
                PlayerPrefs.SetInt("LevelIndex",6);
                break;
            case 6:
                PlayerPrefs.SetInt("LevelIndex",7);
                break;
            case 7:
                PlayerPrefs.SetInt("LevelIndex",8);
                break;
            case 8:
                PlayerPrefs.SetInt("LevelIndex",9);
                break;
            case 9:
                PlayerPrefs.SetInt("LevelIndex",10);
                break;
            case 10:
                PlayerPrefs.SetInt("LevelIndex",11);
                break;
            case 11:
                PlayerPrefs.SetInt("LevelIndex",12);
                break;
            case 12:
                PlayerPrefs.SetInt("LevelIndex",13);
                break;
            case 13:
                PlayerPrefs.SetInt("LevelIndex",14);
                break;
            case 14:
                PlayerPrefs.SetInt("LevelIndex",15);
                break;
            case 15:
                PlayerPrefs.SetInt("LevelIndex",16);
                break;
        }

        switch (PlayerPrefs.GetInt("CarIndex"))
        {
            case 1:
                PlayerPrefs.SetInt("CarIndex", 2);
                break;
            case 2:
                PlayerPrefs.SetInt("CarIndex", 3);
                break;
            case 3:
                PlayerPrefs.SetInt("CarIndex", 4);
                break;
            case 4:
                PlayerPrefs.SetInt("CarIndex", 5);
                break;
            case 5:
                PlayerPrefs.SetInt("CarIndex", 6);
                break;
            case 6:
                PlayerPrefs.SetInt("CarIndex", 7);
                break;
            case 7:
                PlayerPrefs.SetInt("CarIndex", 8);
                break;
            case 8:
                PlayerPrefs.SetInt("CarIndex", 9);
                break;
        }
        
        PlayerPrefs.Save();
        

        PlayerPrefs.SetInt("TotalCoins", PlayerPrefs.GetInt("TotalCoins") + currentScore);
        
        PlayerPrefs.Save();
        
        if (_levelProgressUI.playerPosi == 1)
        {
            _uiManager.playerPosTXT.text = "1st";
            endConfetti.SetActive(true);
        }
        
        else if (_levelProgressUI.playerPosi == 2 || _levelProgressUI.playerPosi == 3)
        {
            if (_levelProgressUI.playerPosi == 2)
                _uiManager.playerPosTXT.text = "2nd";
            
            if (_levelProgressUI.playerPosi == 3)
                _uiManager.playerPosTXT.text = "3rd";
        }

        if (isBoosting)
        {
            _uiManager.boostBlurPanel.SetActive(false);
            windGrassEffect.gameObject.SetActive(false);
            speedLinesEffect.gameObject.SetActive(false);
            _playerVehicleManager.carEffects.NOSEffectsPS.gameObject.SetActive(false);
            _playerVehicleManager.overHeadBoostUI.timerText.gameObject.SetActive(false);
        }
        
        //Common Stuff
        _uiManager.continueButton.DOScale(Vector3.zero, 0f);
        _uiManager.shareBtn.DOScale(Vector3.zero, 0f);
        _uiManager.positionTexts.transform.DOScale(Vector3.zero, 0f);
        
        yield return new WaitForSeconds(0.15f);
        
        isGameEnded = true;
        _uiManager.BoostBtn.gameObject.SetActive(false);
        mainCameraGO.SetActive(false);
        cameraRotator.SetActive(true);
        isGameStarted = false;
        GameManager.Instance.canControlCar = false;
        
        yield return new WaitForSeconds(1f);
        
        gifRecording.StopRecording();
        
        yield return new WaitForSeconds(0.1f);
        
        //Common Stuff
        _uiManager.continueButton.DOScale(Vector3.one, 0.8f).SetEase(Ease.Flash);
        _uiManager.shareBtn.DOScale(Vector3.one, 0.8f).SetEase(Ease.Flash);
        _uiManager.positionTexts.transform.DOScale(Vector3.one, 0.8f).SetEase(Ease.Flash);

        Ana_LevelWin(_levelProgressUI.playerPosi);

        
        

    }

    public void GoToStadium()
    {
        _gameManager.LoadScene("Concert_Scn");
    }

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
        if(_audioManager!=null)
            _audioManager.musicTracks.MusicTrackAudioSource.Play();
        
        
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
        Ana_LevelContinue();
        
        smokeTransitionPostCrashEffect.SetActive(true);
        _uiManager.BoostBtn.SetActive(false);
        
        yield return new WaitForSeconds(0.3f);
        
        _uiManager.BoostBtn.SetActive(true);
        _uiManager.crashedPanel.SetActive(false);
        _playerVehicleManager.postCrashStuff.crashPS.gameObject.SetActive(false);
        _playerVehicleManager.postCrashStuff.up_car.SetActive(true);
        _playerVehicleManager.postCrashStuff.down_car.SetActive(false);
        
        playerCarCollidersToToggle.enabled = false;
        
        yield return new WaitForSeconds(0.5f);
        
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
       
       //Reset The smoke transition effect
       smokeTransitionPostCrashEffect.SetActive(false);
       
        yield return new WaitForSeconds(0.1f);
        
        #region CarSmokePlay
        //Back smoke effects off for all cars
        _playerVehicleManager.carEffects.carBreakSmokeL.GetComponent<ParticleSystem>().Play();
        _playerVehicleManager.carEffects.carBreakSmokeR.GetComponent<ParticleSystem>().Play();
        
        enemyLeftVisual.GetComponent<VehicleManager>().carEffects.carBreakSmokeL.GetComponent<ParticleSystem>().Play();
        enemyLeftVisual.GetComponent<VehicleManager>().carEffects.carBreakSmokeR.GetComponent<ParticleSystem>().Play();
        
        enemyRightVisual.GetComponent<VehicleManager>().carEffects.carBreakSmokeL.GetComponent<ParticleSystem>().Play();
        enemyRightVisual.GetComponent<VehicleManager>().carEffects.carBreakSmokeR.GetComponent<ParticleSystem>().Play();
        #endregion
        
        if(!adStuff && continueCounter!=5)
            continueCounter++;
        
        if (continueCounter == 2)
        {
            _uiManager.brokenHeart.gameObject.SetActive(true);
            _uiManager.fullHeart.gameObject.SetActive(false);
            _uiManager.rewardBtn.SetActive(true);           //ADD GET MORE LIVES BUTTON
            _uiManager.continueBtn.SetActive(false);        //CONTINUE BUTTON REMOVE
            
        }
        
        
        isGameStarted = true;
        isCrashed = false;
        
        
        _playerController.playerPF.speed = _playerVehicleManager.carSpeedSettings.normalSpeed;
        _playerController.playerPF.enabled = true;
        
        GameManager.Instance.canControlCar = true;
        _gameControls.gestureState = GameControls.GestureState.Release;

        yield return new WaitForSeconds(1f);
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
        if(_audioManager!=null)
            _audioManager.musicTracks.MusicTrackAudioSource.Play();
        _uiManager.extraLifePanel.SetActive(false);
        adStuff = true;
        continueCounter = 5;
        StartCoroutine("CarReset");
    }

    

    public void RunRewardedAd()
    {
        _gameManager.rewardedAd.rewarded();
        Ana_AdShown("rewarded");
        Invoke("ShowRevivePanel",3f);
    }

    void ShowRevivePanel()
    {
        _uiManager.receiveLifePanel.SetActive(true);
    }

    public void Vibrate()
    {
        _gameManager.VibrateOnce();
    }
    
    void Ana_LevelStart()
    {
        //Level Start!
        if (GameManager.Instance.lightingMode == 1)
            Analytics.CustomEvent("Level Start " + SceneManager.GetActiveScene().name + "-DAY ");
        if (GameManager.Instance.lightingMode == 2)
            Analytics.CustomEvent("Level Start " + SceneManager.GetActiveScene().name + "-NIGHT ");
    }
    
    void Ana_LevelWin(int myPosi)
    {
        //Level Win!
        if (GameManager.Instance.lightingMode == 1)
            Analytics.CustomEvent("Level Win " + SceneManager.GetActiveScene().name + "-DAY" + "-Position: "+ myPosi);
        if (GameManager.Instance.lightingMode == 2)
            Analytics.CustomEvent("Level Win " + SceneManager.GetActiveScene().name + "-NIGHT" + "-Position: "+ myPosi);
    }

    void Ana_LevelContinue()
    {
        //Level Continue!
        if (GameManager.Instance.lightingMode == 1)
            Analytics.CustomEvent("Level Continue after Crash " + SceneManager.GetActiveScene().name + "-DAY ");
        if (GameManager.Instance.lightingMode == 2)
            Analytics.CustomEvent("Level Continue after Crash " + SceneManager.GetActiveScene().name + "-NIGHT ");
    }
    
    public void Ana_AdShown(string typeOfAd)
    {
        //Level ShowAd!
        if (GameManager.Instance.lightingMode == 1)
            Analytics.CustomEvent("Level Ad Shown " + SceneManager.GetActiveScene().name + "-DAY-" + typeOfAd);
        if (GameManager.Instance.lightingMode == 2)
            Analytics.CustomEvent("Level Ad Shown " + SceneManager.GetActiveScene().name + "-NIGHT-" + typeOfAd);
    }
    
    
    public void ToggleSFX()
    {
        if (_audioManager.isSFXenabled)
        {
            _audioManager.isSFXenabled = false;
            _uiManager.sfxoff.gameObject.SetActive(true);
            _uiManager.sfxon.gameObject.SetActive(false);
        }
            
        
        else if (!_audioManager.isSFXenabled)
        {
            _audioManager.isSFXenabled = true;
            _uiManager.sfxoff.gameObject.SetActive(false);
            _uiManager.sfxon.gameObject.SetActive(true);
            
        }
           
    }
    
    public void ToggleMusic()
    {
        if (_audioManager.isMusicEnabled)
        {
            _audioManager.isMusicEnabled = false;
            _uiManager.musicoff.gameObject.SetActive(true);
            _uiManager.musicon.gameObject.SetActive(false);
            _audioManager.musicTracks.MusicTrackAudioSource.mute = true;
        }
            
        
        else if (!_audioManager.isMusicEnabled)
        {
            _audioManager.isMusicEnabled = true;
            _uiManager.musicoff.gameObject.SetActive(false);
            _uiManager.musicon.gameObject.SetActive(true);
            _audioManager.musicTracks.MusicTrackAudioSource.mute = false;
            
        }
           
    }
    
    public void ToggleHaptic()
    {
        if (_audioManager.isHapticEnabled)
        {
            _audioManager.isHapticEnabled = false;
            _uiManager.hapticoff.gameObject.SetActive(true);
            _uiManager.hapticon.gameObject.SetActive(false);
        }
            
        
        else if (!_audioManager.isHapticEnabled)
        {
            _audioManager.isHapticEnabled = true;
            _uiManager.hapticoff.gameObject.SetActive(false);
            _uiManager.hapticon.gameObject.SetActive(true);
            
        }
           
    }

    public void UpdateIcons()
    {
        if (_audioManager.isSFXenabled)
        {
            _uiManager.sfxoff.gameObject.SetActive(false);
            _uiManager.sfxon.gameObject.SetActive(true);
        }
            
        
        else if (!_audioManager.isSFXenabled)
        {
            _uiManager.sfxoff.gameObject.SetActive(true);
            _uiManager.sfxon.gameObject.SetActive(false);
            
        }
        
        if (_audioManager.isMusicEnabled)
        {
            _uiManager.musicoff.gameObject.SetActive(false);
            _uiManager.musicon.gameObject.SetActive(true);
        }
            
        
        else if (!_audioManager.isMusicEnabled)
        {
            _uiManager.musicoff.gameObject.SetActive(true);
            _uiManager.musicon.gameObject.SetActive(false);
            
        }
        
        if (_audioManager.isHapticEnabled)
        {
            _uiManager.hapticoff.gameObject.SetActive(false);
            _uiManager.hapticon.gameObject.SetActive(true);
        }
            
        
        else if (!_audioManager.isHapticEnabled)
        {
            _audioManager.isHapticEnabled = true;
            _uiManager.hapticoff.gameObject.SetActive(true);
            _uiManager.hapticon.gameObject.SetActive(false);
            
        }

    }
    
}
