using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotator : MonoBehaviour
{
    public float speed;
    private void Update()
    {
        transform.Rotate(0,speed * Time.deltaTime,0);
    }
}
