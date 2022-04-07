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
            
            mySequence.Append( transform.DOLocalRotate(new Vector3(0f,0,-40f), 0.7f));
            
            mySequence.AppendInterval(1);
               
            mySequence.Append( transform.DOLocalRotate(new Vector3(0f,0,0f), 0.7f));

            mySequence.AppendInterval(1);
            
            mySequence.SetLoops(-1);

        }
    }
}
