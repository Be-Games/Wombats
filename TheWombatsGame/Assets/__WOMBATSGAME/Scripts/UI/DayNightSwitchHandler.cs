using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DayNightSwitchHandler : MonoBehaviour
{
    [SerializeField]private int switchState = 1;
    [SerializeField] private GameObject switchBtn;
    public TextMeshProUGUI dayText, NightText;
    

    public void OnSwitchButtonClicked()
    {
        switchBtn.transform.DOLocalMoveX(-switchBtn.transform.localPosition.x,0.2f);
        switchState = Math.Sign(-switchBtn.transform.localPosition.x);
        dayText.gameObject.SetActive(!dayText.gameObject.activeInHierarchy);
        NightText.gameObject.SetActive(!NightText.gameObject.activeInHierarchy);
        
        if(NightText.gameObject.activeInHierarchy)
            switchBtn.GetComponent<RawImage>().color = Color.black;
        
        if(dayText.gameObject.activeInHierarchy)
            switchBtn.GetComponent<RawImage>().color = Color.yellow;
    }
}
