using UnityEngine;
using System.Collections;

public class Spin : MonoBehaviour
{
	public Vector3 axis = new Vector3(0, 1, 0);
	public float speed = 1;

	// Update is called once per frame
	void Update ()
	{
		if(this.gameObject.activeInHierarchy)
			transform.Rotate(axis, speed*Time.deltaTime);
	}
}
