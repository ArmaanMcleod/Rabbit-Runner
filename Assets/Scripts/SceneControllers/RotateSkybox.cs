using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateSkybox : MonoBehaviour {
	public float speed;

	private Skybox skybox;

	// Use this for initialization
	void Start () {
		skybox =  GetComponent<Skybox>();
	}
	
	// Update is called once per frame
	void Update () {
		skybox.material.SetFloat("_Rotation", Time.time * speed);
	}
}
