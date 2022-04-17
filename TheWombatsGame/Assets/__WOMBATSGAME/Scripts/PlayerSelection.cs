using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerSelection : MonoBehaviour
{
    public Camera sceneCamera;
    public Transform[] cameraPositions;

    public GameObject nextBtn, prevBtn, continueBtn;

    public TextMeshProUGUI currrentMemberName;

    public int index;

   [SerializeField] private GameObject _gameManager;

    private void Start()
    {
        _gameManager = GameObject.FindWithTag("GameManager");
        index = 0;

    }

    void Update()
    {
        if (index == 0)
        {
            currrentMemberName.text = "Matthew Murphy";
            prevBtn.SetActive(false);
            nextBtn.SetActive(true);

            sceneCamera.transform.DOMove(cameraPositions[index].transform.position, 0.5f).SetEase(Ease.Flash);
        }

        if (index == 1)
        {
            currrentMemberName.text = "Dan Haggis";
            prevBtn.SetActive(true);
            nextBtn.SetActive(true);
            sceneCamera.transform.DOMove(cameraPositions[index].transform.position, 0.5f).SetEase(Ease.Flash);
        }

        if (index == 2)
        {
            currrentMemberName.text = "Tord Øverland Knudsen";
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

        _gameManager.GetComponent<GameManager>().charNumber = index;
        _gameManager.GetComponent<GameManager>().LoadScene("LevelSelection");
    }
    
}
