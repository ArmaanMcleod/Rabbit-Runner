using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveForward : MonoBehaviour {

	// Speed the object moves at
	public float speed = 2;

	
	/// <summary>
	/// Update is called once per frame
	/// </summary>
	void Update () {
		transform.position += transform.forward * speed * Time.deltaTime;
		
	}
}
