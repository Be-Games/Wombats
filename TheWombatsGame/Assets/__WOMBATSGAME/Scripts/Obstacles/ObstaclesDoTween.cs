using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class ObstaclesDoTween : MonoBehaviour
{
    private void Start()
    {
        if (this.gameObject.name == "Barrier")
        {
            var mySequence = DOTween.Sequence();
            
            mySequence.Append( transform.DOLocalRotate(new Vector3(0f,0,-40f), 0.7f).SetEase(Ease.InOutSine));
            
            mySequence.AppendInterval(0.7f);
               
            mySequence.Append( transform.DOLocalRotate(new Vector3(0f,0,0f), 0.7f).SetEase(Ease.InOutSine));

            mySequence.AppendInterval(0.7f);
            
            mySequence.SetLoops(-1);

        }
        
        if (this.gameObject.name == "CRANE")
        {
            var mySequence = DOTween.Sequence();
            
            mySequence.Append( transform.DOLocalRotate(new Vector3(-43.29f,0,0f), 0.7f)).SetEase(Ease.Flash);
            
            mySequence.AppendInterval(0.3f);
               
            mySequence.Append( transform.DOLocalRotate(new Vector3(19.45f,0,0f), 0.7f).SetEase(Ease.Flash));

            mySequence.AppendInterval(0.3f);
            
            mySequence.SetLoops(-1);

        }
    }
}
