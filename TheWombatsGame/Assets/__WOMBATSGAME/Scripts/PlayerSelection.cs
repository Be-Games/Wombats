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

   public int carIndex = 0;
   public int carIndexPrice;

   public Button prev, next;

   public Button garageBtn;

   public GameObject lockImage;
   public Button backButton;
   private string path;

   public TextMeshProUGUI currentTotalCoins;


   private void Start()
    {
        if(PlayerPrefs.GetInt("CarIndex") == 0)
            PlayerPrefs.SetInt("CarIndex",1);

        currentTotalCoins.text = PlayerPrefs.GetInt("TotalCoins").ToString();
        
        //SET DEFAULT GARAGE PANEL OFF AND ALPHA TO 
        //garage_Panel.GetComponent<CanvasGroup>().alpha = 0;
        playerSelection_Panel.SetActive(true);
        garage_Panel.SetActive(false);
        
        //REFERENCES
        _gameManager = GameObject.FindWithTag("GameManager");
        index = 0;

        carIndex = 1;
        
        for (int i = 0; i < 9; i++)
        {
            if (i == carIndex - 1)
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
            if (i == carIndex - 1)
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
            if (i == carIndex - 1)
            {
                allToddCars[i].gameObject.SetActive(true);
            }
            else
            {
                allToddCars[i].gameObject.SetActive(false); 
            }
           
        }

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
            x.SetActive(false);
        }
       

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
            if (carIndex <= 1)
            {
                carIndex = 1;
                prev.gameObject.SetActive(false);
            }
            else
            {
                prev.gameObject.SetActive(true);
            }
            if (carIndex == 9)
            {
                carIndex = 9;
                next.gameObject.SetActive(false);
            }
            else
            {
                next.gameObject.SetActive(true);
            }
                
        }
        
        for (int i = 1; i <= 9; i++)
        {
            if (PlayerPrefs.GetInt("CarIndex") == i)
            {
               
                if(carIndex > i)
                    lockImage.SetActive(true);
                else
                {
                    lockImage.SetActive(false);
                }
            }
        }
        
        if (lockImage.activeInHierarchy)
        {
            backButton.enabled = false;
        }
        else
        {
            backButton.enabled = true;
        }
       
        
    }

    public void UnlockBtn()
    {
        if (PlayerPrefs.GetInt("TotalCoins") >= carIndexPrice)
        {
            
        }
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
        //wait few secs

        _gameManager.GetComponent<GameManager>().charNumber = index + 1;
        _gameManager.GetComponent<GameManager>().memeberIndex = index;
        _gameManager.GetComponent<GameManager>().selectedCarModelPLAYER = carIndex;

        _gameManager.GetComponent<GameManager>().enemyCar1 = carIndex + 1;
        _gameManager.GetComponent<GameManager>().enemyCar2 = carIndex + 1;

        if ((carIndex + 1) > 9)
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

    void ReturnToPlayerScreen_OC()
    {
        garage_Panel.SetActive(false);
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
        
        carIndex++;

        DOTween.To(() => temp, 
                x => temp = x, temp-341, 0.1f).SetEase(Ease.Flash)
            .OnComplete(() => {
                prev.enabled = true;
                next.enabled = true; 
            });

        carCounterText.text = carIndex + " / 9";
        
        for (int i = 0; i < 9; i++)
        {
            if (i == carIndex - 1)
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
            if (i == carIndex - 1)
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
            if (i == carIndex - 1)
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
        
        carIndex--;

        DOTween.To(() => temp, 
                x => temp = x, temp+341, 0.1f).SetEase(Ease.Flash)
            .OnComplete(() => {
                prev.enabled = true;
                next.enabled = true; 
            });
        
        carCounterText.text = carIndex + " / 9";
        
        for (int i = 0; i < 9; i++)
        {
            if (i == carIndex - 1)
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
            if (i == carIndex - 1)
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
            if (i == carIndex - 1)
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




