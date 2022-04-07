using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camera_upright : MonoBehaviour
{


float lockPos = 0;

void Update()
{
     transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, lockPos);
}


//void Update() {
   //transform.rotation = Quaternion.identity;}
 
}