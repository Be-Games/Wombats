using System;
using System.Collections;
using System.Collections.Generic;
using DataManager;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;



public class PlayerSelection : MonoBehaviour
{
    public Camera sceneCamera;
    public Transform[] cameraPositions;

    public GameObject nextBtn, prevBtn, continueBtn;

    public TextMeshProUGUI currrentMemberName;

    public int index;
    
   
   [Space(50)]
   //Car Selection Variables
   [SerializeField] private GameObject playerSelection_Panel;
   public GameObject playerObjects;
   [SerializeField] private GameObject garage_Panel;
   public GameObject carModel;
   
   [SerializeField] private TextMeshProUGUI playerGarageName;

   [SerializeField] private GameObject[] allMurphCars,allDanCars,allToddCars;
   [SerializeField] private GameObject[] parentM, parentD, parentT;
   
   public TextMeshProUGUI carCounterText;
   public GameObject carModelParent;



   public Button prev, next;

   public Button garageBtn;

   public GameObject lockImage;
   public Button backButton;
   private string path;

   public TextMeshProUGUI currentTotalCoins;

   public GameObject[] currentMemebers;
   public RuntimeAnimatorController selectedController;


   public Button buyBtn, unlockwithAdBbtn;
   public TextMeshProUGUI carPrice;

   public GameObject[] unlockCelebration;
   void OnEnable()
   {
       SceneManager.sceneLoaded += OnSceneLoaded;
   }
   void OnSceneLoaded(Scene scene, LoadSceneMode mode)
   {
       //REFERENCES

      GameManager.Instance.carIndex = 1;
       currentTotalCoins.text = PlayerPrefs.GetInt("TotalCoins").ToString();
       
   }
   private static PlayerSelection _instance;
    
   public static PlayerSelection Instance
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

   private void Start()
   {
       
       index = GameManager.Instance.charNumber-1;
       
       GameManager.Instance.rewardedAd.IniRewardedSystem();
       
       PlayerPrefs.SetInt("Car" + 1, 1);

       if (PlayerPrefs.GetInt("isGarage") == 0)
       {
           playerSelection_Panel.SetActive(true);
           garage_Panel.SetActive(false);
       }
       else
       {
           playerSelection_Panel.SetActive(false);
           garage_Panel.SetActive(true);
       }
        
       InitialisePanel();

        
       for (int i = 0; i < 9; i++)
       {
           if (i == GameManager.Instance.carIndex - 1)
           {
               allMurphCars[i].gameObject.SetActive(true);
           }
           else
           {
               allMurphCars[i].gameObject.SetActive(false); 
           }
           
       }

       for (int i = 0; i < 9; i++)
       {
           if (i == GameManager.Instance.carIndex - 1)
           {
               allDanCars[i].gameObject.SetActive(true);
           }
           else
           {
               allDanCars[i].gameObject.SetActive(false);
           }
       }
       
       for (int i = 0; i < 9; i++)
       {
           if (i == GameManager.Instance.carIndex - 1)
           {
               allToddCars[i].gameObject.SetActive(true);
           }
           else
           {
               allToddCars[i].gameObject.SetActive(false); 
           }
           
       }
       
       StartCoroutine(MyUpdate());

       
       carDetailsPanel.transform.DOScale(0f, 0.5f).SetEase(Ease.Linear);

   }

   private int temp;
    IEnumerator MyUpdate()
    {
        if (index == 0)
        {
            currrentMemberName.text = "Matthew Murphy" + "\n" + "'MURPH'";
            prevBtn.SetActive(false);
            garageBtn.image.color = Color.red;
            nextBtn.SetActive(true);

            sceneCamera.transform.DOMove(cameraPositions[index].transform.position, 0.5f).SetEase(Ease.Flash);
        }

        if (index == 1)
        {
            currrentMemberName.text = "Dan Haggis" + "\n" + "'DAN'";
            prevBtn.SetActive(true);
            garageBtn.image.color = Color.yellow;
            nextBtn.SetActive(true);
            sceneCamera.transform.DOMove(cameraPositions[index].transform.position, 0.5f).SetEase(Ease.Flash);
        }

        if (index == 2)
        {
            currrentMemberName.text = "Tord Øverland Knudsen" + "\n" + "'TORD'";
            prevBtn.SetActive(true);
            garageBtn.image.color = Color.blue;
            nextBtn.SetActive(false);
            sceneCamera.transform.DOMove(cameraPositions[index].transform.position, 0.5f).SetEase(Ease.Flash);
        }
        

        if (garage_Panel.activeInHierarchy)
        {
            if (GameManager.Instance.carIndex <= 1)
            {
                GameManager.Instance.carIndex = 1;
                prev.gameObject.SetActive(false);
            }
            else
            {
                prev.gameObject.SetActive(true);
            }
            if (GameManager.Instance.carIndex == 9)
            {
                GameManager.Instance.carIndex = 9;
                next.gameObject.SetActive(false);
            }
            else
            {
                next.gameObject.SetActive(true);
            }
                
        }

        
        if (PlayerPrefs.GetInt("Car" + GameManager.Instance.carIndex) == 0)
        {
            lockImage.SetActive(true);
        }
        else if (PlayerPrefs.GetInt("Car" + GameManager.Instance.carIndex) == 1)
        {
            lockImage.SetActive(false);
        }
        
        
        if (lockImage.activeInHierarchy)
        {
            buyBtn.gameObject.SetActive(true);
            unlockwithAdBbtn.gameObject.SetActive(true);
            backButton.enabled = false;
        }
        else
        {
            buyBtn.gameObject.SetActive(false);
            unlockwithAdBbtn.gameObject.SetActive(false);
            backButton.enabled = true;
        }


        //CAR PRICES SET
       if(index == 0)
           allMurphCars[GameManager.Instance.carIndex-1].SetActive(true);
       if(index == 1)
           allDanCars[GameManager.Instance.carIndex-1].SetActive(true);
       if(index == 2)
           allToddCars[GameManager.Instance.carIndex-1].SetActive(true);
            
        if (GameManager.Instance.carIndex == 2)
            carPrice.text = 200.ToString();
        if (GameManager.Instance.carIndex == 3)
            carPrice.text = 350.ToString();
        if (GameManager.Instance.carIndex == 4)
            carPrice.text = 500.ToString();
        if (GameManager.Instance.carIndex == 5)
            carPrice.text = 650.ToString();
        if (GameManager.Instance.carIndex == 6)
            carPrice.text = 800.ToString();
        if (GameManager.Instance.carIndex == 7)
            carPrice.text = 950.ToString();
        if (GameManager.Instance.carIndex == 8)
            carPrice.text = 1100.ToString();
        if (GameManager.Instance.carIndex == 9)
            carPrice.text = 1200.ToString();
        
        yield return null;
        StartCoroutine(MyUpdate());
    }

    public void buyCar()
    {
        if (int.Parse(carPrice.text) > PlayerPrefs.GetInt("MyTotalCoins"))
        {
            Debug.Log("Cant Buy");
        }
        else if(int.Parse(carPrice.text) <= PlayerPrefs.GetInt("MyTotalCoins"))
        {
            PlayerPrefs.SetInt("MyTotalCoins",PlayerPrefs.GetInt("MyTotalCoins") - int.Parse(carPrice.text));
            
            PlayerPrefs.SetInt("Car" + GameManager.Instance.carIndex, 1);

            foreach (Transform child in unlockCelebration[index].transform)
            {
                child.GetComponent<ParticleSystem>().Play();
            } 
            
            if(AudioManager.Instance.isSFXenabled)
                AudioManager.Instance.sfxAll.unlockCar.PlayOneShot(AudioManager.Instance.sfxAll.unlockCar.clip);
            
            lockImage.SetActive(false);
        }

       
    }

    public bool adForCar = false;

    public void unlockCar()
    {
        GameManager.Instance.rewardedAd.rewardedForConcert();
        adForCar = true;
    }

    public void postCarAd()
    {
        PlayerPrefs.SetInt("Car" + GameManager.Instance.carIndex, 1);
        foreach (Transform child in unlockCelebration[index].transform)
        {
            child.GetComponent<ParticleSystem>().Play();
        } 
        PlayerSelection.Instance.lockImage.SetActive(false);
        if(AudioManager.Instance.isSFXenabled)
            AudioManager.Instance.sfxAll.unlockCar.PlayOneShot(AudioManager.Instance.sfxAll.unlockCar.clip);
    }

    public void RewardAdAndUnlockCar()
    {
        GameManager.Instance.isForCarInterstitial = true;
        GameManager.Instance.interstitialAd.interstitialplay();
    }
    

    public void Next()
    {
        index += 1;
    }
    
    public void Previous()
    {
        index -= 1;
    }

    public void ContinueBtn()
    {
        UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.GetComponent<Button>().enabled = false;
        
        //set selected player animation to blowing kiss
        currentMemebers[index].GetComponent<Animator>().runtimeAnimatorController = selectedController;
        prevBtn.GetComponent<Button>().enabled = false;
        nextBtn.GetComponent<Button>().enabled = false;
        continueBtn.GetComponent<Button>().enabled = false;
        Invoke("RunScene",1.5f);
        //wait few secs

        
    }

    void RunScene()
    {
        GameManager.Instance.GetComponent<GameManager>().charNumber = index + 1;
        GameManager.Instance.GetComponent<GameManager>().memeberIndex = index;
        GameManager.Instance.GetComponent<GameManager>().selectedCarModelPLAYER = GameManager.Instance.carIndex;

        GameManager.Instance.GetComponent<GameManager>().enemyCar1 = GameManager.Instance.carIndex + 2;
        GameManager.Instance.GetComponent<GameManager>().enemyCar2 = GameManager.Instance.carIndex + 1;

        if ((GameManager.Instance.carIndex + 1) > 9)
        {
            GameManager.Instance.GetComponent<GameManager>().enemyCar1 = 0;
            GameManager.Instance.GetComponent<GameManager>().enemyCar2 = 0;
        }

        GameManager.Instance.GetComponent<GameManager>().LoadScene("LevelSelection");
    }
    
    //---------------------------------------------------------------------------------------
    
    public void GarageBtn()
    {
        playerSelection_Panel.SetActive(false);

        garage_Panel.SetActive(true);

        InitialisePanel();

    }
    
    void InitialisePanel()
    {
        if (index == 0)
        {
            playerGarageName.text = "MURPH'S GARAGE";
            
            foreach (var x in parentD)
            {
                x.SetActive(false);
            }
            foreach (var x in parentM)
            {
                x.SetActive(true);
            }
            foreach (var x in parentT)
            {
                x.SetActive(false);
            }
        }

        if (index == 1)
        {
            playerGarageName.text = "DAN'S GARAGE";
            
            foreach (var x in parentD)
            {
                x.SetActive(true);
            }
            foreach (var x in parentM)
            {
                x.SetActive(false);
            }
            foreach (var x in parentT)
            {
                x.SetActive(false);
            }
        }

        if (index == 2)
        {
            playerGarageName.text = "TORD'S GARAGE";
            
            foreach (var x in parentD)
            {
                x.SetActive(false);
            }
            foreach (var x in parentM)
            {
                x.SetActive(false);
            }
            foreach (var x in parentT)
            {
                x.SetActive(true);
            }
        }
        
    }

    public void BackToPlayerPanel()
    {
        //garage_Panel.GetComponent<CanvasGroup>().DOFade(0f, 0.3f).SetEase(Ease.Flash).OnComplete(ReturnToPlayerScreen_OC);
        garage_Panel.SetActive(false);
        playerSelection_Panel.SetActive(true);

       
    }
    

    public void carSelectedM()
    {
         PlayerPrefs.SetInt("MurphKey", int.Parse(UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.transform.GetChild(0).name));
         PlayerPrefs.Save();

         // mainCarImage[0].sprite = allMurphCars[PlayerPrefs.GetInt("MurphKey",0)].sprite;

    }
    public void carSelectedD()
    {
        PlayerPrefs.SetInt("DanKey", int.Parse(UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.transform.GetChild(0).name));
        PlayerPrefs.Save();

        // mainCarImage[1].sprite = allDanCars[PlayerPrefs.GetInt("DanKey",0)].sprite;

    }
    
    public void carSelectedT()
    {
        PlayerPrefs.SetInt("TordKey", int.Parse(UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.transform.GetChild(0).name));
        PlayerPrefs.Save();

        // mainCarImage[2].sprite = allToddCars[PlayerPrefs.GetInt("TordKey",0)].sprite;

    }

    private void Update()
    {
        switch (GameManager.Instance.carIndex)
        {
            case 1:
                DOTween.To(() => accImage.fillAmount, 
                        x => accImage.fillAmount = x, 0.1f, 0.2f)
                    .OnComplete(() => {
                        accValue.text = "25";
                    });
                
                

                DOTween.To(() => speedImage.fillAmount, 
                        x => speedImage.fillAmount = x, 0.13f, 0.2f)
                    .OnComplete(() => {
                        speedValue.text = "35";
                    });

                break;
            case 2:
                DOTween.To(() => accImage.fillAmount, 
                        x => accImage.fillAmount = x, 0.15f, 0.2f)
                    .OnComplete(() => {
                        accValue.text = "35";
                    });
                
                

                DOTween.To(() => speedImage.fillAmount, 
                        x => speedImage.fillAmount = x, 0.18f, 0.2f)
                    .OnComplete(() => {
                        speedValue.text = "45";
                    });

                break;
            case 3:
                DOTween.To(() => accImage.fillAmount, 
                        x => accImage.fillAmount = x, 0.23f, 0.2f)
                    .OnComplete(() => {
                        accValue.text = "45";
                    });
                
                

                DOTween.To(() => speedImage.fillAmount, 
                        x => speedImage.fillAmount = x, 0.26f, 0.2f)
                    .OnComplete(() => {
                        speedValue.text = "55";
                    });

                break;
            case 4:
                DOTween.To(() => accImage.fillAmount, 
                        x => accImage.fillAmount = x, 0.3f, 0.2f)
                    .OnComplete(() => {
                        accValue.text = "55";
                    });
                
                

                DOTween.To(() => speedImage.fillAmount, 
                        x => speedImage.fillAmount = x, 0.34f, 0.2f)
                    .OnComplete(() => {
                        speedValue.text = "65";
                    });

                break;
            case 5:
                DOTween.To(() => accImage.fillAmount, 
                        x => accImage.fillAmount = x, 0.4f, 0.2f)
                    .OnComplete(() => {
                        accValue.text = "65";
                    });
                
                

                DOTween.To(() => speedImage.fillAmount, 
                        x => speedImage.fillAmount = x, 0.42f, 0.2f)
                    .OnComplete(() => {
                        speedValue.text = "75";
                    });

                break;
            case 6:
                DOTween.To(() => accImage.fillAmount, 
                        x => accImage.fillAmount = x, 0.48f, 0.2f)
                    .OnComplete(() => {
                        accValue.text = "75";
                    });
                
                

                DOTween.To(() => speedImage.fillAmount, 
                        x => speedImage.fillAmount = x, 0.52f, 0.2f)
                    .OnComplete(() => {
                        speedValue.text = "85";
                    });

                break;
            case 7:
                DOTween.To(() => accImage.fillAmount, 
                        x => accImage.fillAmount = x, 0.57f, 0.2f)
                    .OnComplete(() => {
                        accValue.text = "85";
                    });
                
                

                DOTween.To(() => speedImage.fillAmount, 
                        x => speedImage.fillAmount = x, 0.6f, 0.2f)
                    .OnComplete(() => {
                        speedValue.text = "95";
                    });

                break;
            case 8:
                DOTween.To(() => accImage.fillAmount, 
                        x => accImage.fillAmount = x, 0.62f, 0.2f)
                    .OnComplete(() => {
                        accValue.text = "95";
                    });
                
                

                DOTween.To(() => speedImage.fillAmount, 
                        x => speedImage.fillAmount = x, 0.63f, 0.2f)
                    .OnComplete(() => {
                        speedValue.text = "105";
                    });

                break;
            case 9:
                DOTween.To(() => accImage.fillAmount, 
                        x => accImage.fillAmount = x, 0.7f, 0.2f)
                    .OnComplete(() => {
                        accValue.text = "105";
                    });
                
                

                DOTween.To(() => speedImage.fillAmount, 
                        x => speedImage.fillAmount = x, 0.7f, 0.2f)
                    .OnComplete(() => {
                        speedValue.text = "115";
                    });

                break;
            
        }
        
    }


    //CAR SELECTION MANAGEMENT

    public void NextCarSelectionBtn()
    {
        prev.enabled = false;
        next.enabled = false;
        
        GameManager.Instance.carIndex++;

        DOTween.To(() => temp, 
                x => temp = x, temp-341, 0.1f).SetEase(Ease.Flash)
            .OnComplete(() => {
                prev.enabled = true;
                next.enabled = true; 
            });

        carCounterText.text = GameManager.Instance.carIndex + " / 9";
        
        for (int i = 0; i < 9; i++)
        {
            if (i == GameManager.Instance.carIndex - 1)
            {
                allMurphCars[i].gameObject.SetActive(true);
            }
            else
            {
                allMurphCars[i].gameObject.SetActive(false); 
            }
           
        }
        
        for (int i = 0; i < 9; i++)
        {
            if (i == GameManager.Instance.carIndex - 1)
            {
                allDanCars[i].gameObject.SetActive(true);
            }
            else
            {
                allDanCars[i].gameObject.SetActive(false); 
            }
           
        }
        
        for (int i = 0; i < 9; i++)
        {
            if (i == GameManager.Instance.carIndex - 1)
            {
                allToddCars[i].gameObject.SetActive(true);
            }
            else
            {
                allToddCars[i].gameObject.SetActive(false); 
            }
           
        }
    }

    public void PrevCarSelectionBtn()
    {
        prev.enabled = false;
        next.enabled = false;
        
        GameManager.Instance.carIndex--;

        DOTween.To(() => temp, 
                x => temp = x, temp+341, 0.1f).SetEase(Ease.Flash)
            .OnComplete(() => {
                prev.enabled = true;
                next.enabled = true; 
            });
        
        carCounterText.text = GameManager.Instance.carIndex + " / 9";
        
        for (int i = 0; i < 9; i++)
        {
            if (i == GameManager.Instance.carIndex - 1)
            {
                allMurphCars[i].gameObject.SetActive(true);
            }
            else
            {
                allMurphCars[i].gameObject.SetActive(false); 
            }
           
        }
        
        for (int i = 0; i < 9; i++)
        {
            if (i == GameManager.Instance.carIndex - 1)
            {
                allDanCars[i].gameObject.SetActive(true);
            }
            else
            {
                allDanCars[i].gameObject.SetActive(false); 
            }
           
        }
        
        for (int i = 0; i < 9; i++)
        {
            if (i == GameManager.Instance.carIndex - 1)
            {
                allToddCars[i].gameObject.SetActive(true);
            }
            else
            {
                allToddCars[i].gameObject.SetActive(false); 
            }
           
        }
    }

    public void Vibrate()
    {
        //GameManager.Instance.VibrateOnce();
    }
    
    public void ButtonClick()
    {
        GameManager.Instance.ButtonClick();
    }

    public GameObject carDetailsPanel;
    public Image accImage, speedImage;
    public TextMeshProUGUI accValue, speedValue;

    public void CarDetailsBtn()
    {
        if(carDetailsPanel.transform.localScale.x == 0f)
            carDetailsPanel.transform.DOScale(1.2f, 0.5f).SetEase(Ease.Linear);
        else
        {
            carDetailsPanel.transform.DOScale(0f, 0.5f).SetEase(Ease.Linear);
        }
    }
    
}




