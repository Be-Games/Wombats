using System;
using System.Collections;
using System.Collections.Generic;
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
   [SerializeField] private GameObject garage_Panel;
   
   [SerializeField] private TextMeshProUGUI playerGarageName;
   [SerializeField] private GameObject[] playerCars;                    // 0 - m , 1 - d , 3 - t
   [SerializeField] Image[] mainCarImage;
   
   [SerializeField] private Image[] allMurphCars,allDanCars,allToddCars;
   
    private void Start()
    {
        //SET DEFAULT GARAGE PANEL OFF AND ALPHA TO 
        garage_Panel.GetComponent<CanvasGroup>().alpha = 0;
        garage_Panel.SetActive(false);
        
        //REFERENCES
        _gameManager = GameObject.FindWithTag("GameManager");
        index = 0;
        

    }

    

    void Update()
    {
        if (index == 0)
        {
            currrentMemberName.text = "Matthew Murphy" + "\n" + "MURPH";
            prevBtn.SetActive(false);
            nextBtn.SetActive(true);

            sceneCamera.transform.DOMove(cameraPositions[index].transform.position, 0.5f).SetEase(Ease.Flash);
        }

        if (index == 1)
        {
            currrentMemberName.text = "Dan Haggis" + "\n" + "DAN";
            prevBtn.SetActive(true);
            nextBtn.SetActive(true);
            sceneCamera.transform.DOMove(cameraPositions[index].transform.position, 0.5f).SetEase(Ease.Flash);
        }

        if (index == 2)
        {
            currrentMemberName.text = "Tord Øverland Knudsen" + "\n" + "TORD";
            prevBtn.SetActive(true);
            nextBtn.SetActive(false);
            sceneCamera.transform.DOMove(cameraPositions[index].transform.position, 0.5f).SetEase(Ease.Flash);
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

        _gameManager.GetComponent<GameManager>().charNumber = index+1;
        _gameManager.GetComponent<GameManager>().LoadScene("LevelSelection");
    }
    
    //---------------------------------------------------------------------------------------

    public void GarageBtn()
    {
        garage_Panel.SetActive(true);
        garage_Panel.GetComponent<CanvasGroup>().DOFade(1f, 0.3f).SetEase(Ease.Flash);
        
        InitialisePanel();

    }
    
    void InitialisePanel()
    {
        if (index == 0)
            playerGarageName.text = "MURPH'S GARAGE";
        if (index == 1)
            playerGarageName.text = "DAN'S GARAGE";
        if (index == 2)
            playerGarageName.text = "TORD'S GARAGE";
        
        for (int i = 0; i < playerCars.Length; i++)
        {
            if(i != index)
                playerCars[i].SetActive(false);
            else 
                playerCars[index].SetActive(true);
        }

        
        
        mainCarImage[0].sprite = allMurphCars[PlayerPrefs.GetInt("MurphKey",0)].sprite;
        mainCarImage[1].sprite = allDanCars[PlayerPrefs.GetInt("DanKey",0)].sprite;
        mainCarImage[2].sprite = allToddCars[PlayerPrefs.GetInt("TordKey",0)].sprite;
       
    }

    public void BackToPlayerPanel()
    {
        garage_Panel.GetComponent<CanvasGroup>().DOFade(0f, 0.3f).SetEase(Ease.Flash).OnComplete(ReturnToPlayerScreen_OC);
        
    }

    void ReturnToPlayerScreen_OC()
    {
        garage_Panel.SetActive(false);
    }

    public void carSelectedM()
    {
         PlayerPrefs.SetInt("MurphKey", int.Parse(UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.transform.GetChild(0).name));
         PlayerPrefs.Save();

         mainCarImage[0].sprite = allMurphCars[PlayerPrefs.GetInt("MurphKey",0)].sprite;

    }
    public void carSelectedD()
    {
        PlayerPrefs.SetInt("DanKey", int.Parse(UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.transform.GetChild(0).name));
        PlayerPrefs.Save();

        mainCarImage[1].sprite = allDanCars[PlayerPrefs.GetInt("DanKey",0)].sprite;

    }
    
    public void carSelectedT()
    {
        PlayerPrefs.SetInt("TordKey", int.Parse(UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.transform.GetChild(0).name));
        PlayerPrefs.Save();

        mainCarImage[2].sprite = allToddCars[PlayerPrefs.GetInt("TordKey",0)].sprite;

    }
    
}
