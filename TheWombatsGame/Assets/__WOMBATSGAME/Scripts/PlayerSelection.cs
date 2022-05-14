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

   [SerializeField] private GameObject _gameManager;
   
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
   public HorizontalLayoutGroup hrzLG;

   
   
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

   private void Start()
   {
       _gameManager = GameObject.FindWithTag("GameManager");
       
       index = _gameManager.GetComponent<GameManager>().charNumber-1;
       
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

       /*foreach (var x in parentD)
       {
           x.SetActive(false);
       }
       foreach (var x in parentM)
       {
           x.SetActive(false);
       }
       foreach (var x in parentT)
       {
           x.SetActive(false);
       }*/

       
   }

   private int temp;
    void Update()
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

        hrzLG.padding.left = temp;
        LayoutRebuilder.MarkLayoutForRebuild(hrzLG.GetComponent<RectTransform>());

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
            
            lockImage.SetActive(false);
        }
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
        _gameManager.GetComponent<GameManager>().charNumber = index + 1;
        _gameManager.GetComponent<GameManager>().memeberIndex = index;
        _gameManager.GetComponent<GameManager>().selectedCarModelPLAYER = GameManager.Instance.carIndex;

        _gameManager.GetComponent<GameManager>().enemyCar1 = GameManager.Instance.carIndex + 1;
        _gameManager.GetComponent<GameManager>().enemyCar2 = GameManager.Instance.carIndex + 1;

        if ((GameManager.Instance.carIndex + 1) > 9)
        {
            _gameManager.GetComponent<GameManager>().enemyCar1 = 0;
            _gameManager.GetComponent<GameManager>().enemyCar2 = 0;
        }

        _gameManager.GetComponent<GameManager>().LoadScene("LevelSelection");
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
    
}




