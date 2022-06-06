using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class DriftingText : MonoBehaviour {

    public GameObject canvas;
    public GameObject textPrefab;
    public float driftTime = 1f;
    public float relativeYEndPos = 1.5f;

    private TextMeshProUGUI msgTxt;
	
    void Start()
    {
        /*canvas = LevelManager.Instance._playerVehicleManager.floatingCanvas.gameObject;
        textPrefab = LevelManager.Instance._playerVehicleManager.floatingCanvas.transform.GetChild(0).gameObject;*/
    }
	
    public void MakeDriftingText (string msg, Vector2 pos) 
    {
        GameObject ptTxt = Instantiate(textPrefab,canvas.transform) as GameObject;
        msgTxt = ptTxt.GetComponent<TextMeshProUGUI>();
        ptTxt.transform.position = textPrefab.transform.position;
        msgTxt.text = msg;
		
        float endYPos = pos.y + relativeYEndPos;

        #region STATUS INDICATOR

        
        var mySequence = DOTween.Sequence();

        mySequence.Append(ptTxt.transform.DOMoveY(0, 1f).SetEase(Ease.OutQuint).SetEase(Ease.InSine));
        
        mySequence.AppendInterval(1);
        
        mySequence.Append(ptTxt.transform.DOMoveY(1f, 1f).SetEase(Ease.OutQuint).SetEase(Ease.InSine));
        mySequence.Append( msgTxt.DOFade(0f, 1f));
        
        
        Destroy(ptTxt, 3);
        
        /*mySequence.OnComplete(() => _uiManager.StatusIndicatorPanelGO.transform.DOLocalMove(new Vector3(-3266, 366f, 0f), 0f)
            .SetRelative(false).SetEase(Ease.Flash));*/
            
        #endregion
        
        /*ptTxt.transform.DOMoveY(endYPos, driftTime).SetEase(Ease.OutQuint);
        msgTxt.DOFade(0f, driftTime);
        Destroy(ptTxt, driftTime);*/
    }

}