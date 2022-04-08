using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class DoTweenAnimations : MonoBehaviour
{
    public GameObject rotatingGO;

    private void Start()
    {
        rotatingGO.transform.DOLocalRotate(Vector3.right, 0.3f).SetLoops(-1);
    }
}
