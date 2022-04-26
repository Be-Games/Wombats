using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CustomisationSettings : MonoBehaviour
{
    [SerializeField]private int switchState = 1;
    
    [Header("Haptic")]
    [SerializeField] private GameObject switchBtnH;
    public Button onTextH, offTextH;
    
    [Header("SFX")]
    [SerializeField] private GameObject switchBtnS;
    public Button onTextS, offTextS;
    
    [Header("Music")]
    [SerializeField] private GameObject switchBtnM;
    public Button onTextM, offTextM;
    

    public void HapticSwitch()
    {
        switchBtnH.transform.DOLocalMoveX(-switchBtnH.transform.localPosition.x,0.2f);
        switchState = Math.Sign(-switchBtnH.transform.localPosition.x);
        onTextH.gameObject.SetActive(!onTextH.gameObject.activeInHierarchy);
        offTextH.gameObject.SetActive(!offTextH.gameObject.activeInHierarchy);
        
        //GameManager.Instance.isHapticEnabled = (!GameManager.Instance.isHapticEnabled);
    }
    
    public void SFXSwitch()
    {
        switchBtnS.transform.DOLocalMoveX(-switchBtnS.transform.localPosition.x,0.2f);
        switchState = Math.Sign(-switchBtnS.transform.localPosition.x);
        onTextS.gameObject.SetActive(!onTextS.gameObject.activeInHierarchy);
        offTextS.gameObject.SetActive(!offTextS.gameObject.activeInHierarchy);
        
        //GameManager.Instance.isSFXenabled = (!GameManager.Instance.isSFXenabled);
    }
    
    public void MusicSwitch()
    {
        switchBtnM.transform.DOLocalMoveX(-switchBtnM.transform.localPosition.x,0.2f);
        switchState = Math.Sign(-switchBtnM.transform.localPosition.x);
        onTextM.gameObject.SetActive(!onTextM.gameObject.activeInHierarchy);
        offTextM.gameObject.SetActive(!offTextM.gameObject.activeInHierarchy);
        
        //GameManager.Instance.isMusicEnabled = (!GameManager.Instance.isMusicEnabled);
    }
    
}
