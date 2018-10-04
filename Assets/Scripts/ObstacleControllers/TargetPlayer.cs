using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetPlayer : MonoBehaviour {

	// Player gameobject
	private GameObject player;

	// Game controller
	private MainGameController gameController;

	// Is the aim of the enemy set
	private bool locked = false; 

	// The distance (away from the player) the enemy starts moving and tracking the player
	public int distanceStartAttack;

	// The distance from the player the enemy sets its aim
	public int distanceLockAttack;

	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag("Player");
		GameObject gameControllerObj = GameObject.FindGameObjectWithTag("GameController");
		gameController = gameControllerObj.GetComponent<MainGameController>();
	}
	
	/// <summary>
	/// Update is called once per frame
	/// </summary>
	void Update () {
		if(locked || gameController.isGameOver() || gameController.isPaused()){
			return;
		}

		// Calculate the distance from the player in the z-axis
		float distanceZPlayer = gameObject.transform.position.z - player.transform.position.z;
		if(distanceZPlayer < distanceStartAttack && distanceZPlayer > 0){
			Vector3 playerPos = player.transform.position;
			
			TrackPlayer(playerPos);
			
			if(distanceZPlayer < distanceLockAttack){
				locked = true;
			}
		}
	}

	/// <summary>
	/// Tracks the position of the player and moves towards it
	/// <param name="pos">The position of the player </param>
	/// </summary>
	void TrackPlayer(Vector3 pos){
		transform.LookAt(pos);
		gameObject.GetComponent<MoveForward>().enabled = true;
	}
}
