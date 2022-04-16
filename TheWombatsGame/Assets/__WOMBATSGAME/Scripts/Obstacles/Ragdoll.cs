using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ragdoll : MonoBehaviour
{

   public GameObject person;
   public float check;
   private void Update()
   {
      //Debug.Log(person.transform.localRotation.y);

      // if (person.transform.localRotation.y >= check)
      // {
      //    Debug.Log("Correct");
      //    
      // }
   }

   // public Collider MainCollider;
   // public Collider[] AllColliders;
   //
   // private void Awake()
   // {
   //    MainCollider = GetComponent<Collider>();
   //    AllColliders = GetComponentsInChildren<Collider>(true);
   // }
   //
   // private void Update()
   // {
   //    if (Input.GetKeyDown(KeyCode.P))
   //    {
   //       Debug.Log("Person");
   //       DoRagDoll(true);
   //    }
   // }
   //
   // public void DoRagDoll(bool isRagDoll)
   // {
   //    foreach (var col in AllColliders)
   //    {
   //       col.enabled = isRagDoll;
   //    }
   //
   //    MainCollider.enabled = !isRagDoll;
   //    GetComponent<Rigidbody>().useGravity = !isRagDoll;
   //    GetComponent<Animator>().enabled = !isRagDoll;
   // }
}
