using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetPlayer : MonoBehaviour {

	private GameObject player;

	private Vector3 playerPos;

	private float distanceZPlayer;

	private bool locked = false; 

	public int distanceStartAttack;

	public int distanceLockAttack;

	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag("Player");
		
	}
	
	// Update is called once per frame
	void Update () {
		if(locked){
			return;
		}

		distanceZPlayer = gameObject.transform.position.z - player.transform.position.z;
		if(distanceZPlayer < distanceStartAttack && distanceZPlayer > 0){
			playerPos = player.transform.position;
			AttackPlayer();
			if(distanceZPlayer < distanceLockAttack){
				locked = true;
			}
		}
	}

	void AttackPlayer(){
		transform.LookAt(playerPos);
		gameObject.GetComponent<MoveForward>().enabled = true;
	}
}
