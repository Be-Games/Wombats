using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateOnY : MonoBehaviour
{
    private void Update()
    {
        transform.Rotate(Vector3.up * -40f * Time.deltaTime);
    }
}
