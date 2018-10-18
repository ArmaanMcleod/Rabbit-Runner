using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateSkybox : MonoBehaviour {
	// speed that the skybox is to be rotated
	public float speed;
	private Skybox skybox;

	/// <summary>
	/// Initialisation
	/// </summary>
	void Start () {
		skybox =  GetComponent<Skybox>();
	}
	
	/// <summary>
	/// Updates the rotation of the skybox
	/// </summary>
	void Update () {
		skybox.material.SetFloat("_Rotation", Time.time * speed);
	}
}
