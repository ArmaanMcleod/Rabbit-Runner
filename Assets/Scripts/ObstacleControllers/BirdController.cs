using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdController : MonoBehaviour {

    // Speed of the bird
    public float speed = 2;

    // Player gameobject
    private GameObject player;

    // Game controller
    private MainGameController gameController;

    /// <summary>
    /// Is the aim of the bird set.
    /// </summary>
    private bool locked = false;

    /// <summary>
    /// Whether the bird is moving towards the player.
    /// </summary>
    private bool moving = false;

    // The distance (away from the player) the enemy starts moving and tracking the player
    public int distanceStartAttack;

    // The distance from the player the enemy sets its aim
    public int distanceLockAttack;

    // Use this for initialization
    void Start() {
        player = GameObject.FindGameObjectWithTag("Player");
        GameObject gameControllerObj = GameObject.FindGameObjectWithTag("GameController");
        gameController = gameControllerObj.GetComponent<MainGameController>();
    }

    /// <summary>
    /// Update is called once per frame
    /// </summary>
    void Update() {
        if (gameController.isGameOver() || gameController.isPaused()) {
            return;
        }

        if (moving) {
            transform.position += transform.forward * speed * Time.deltaTime;
        }

        // Calculate the distance from the player in the z-axis
        float distanceZPlayer = gameObject.transform.position.z - player.transform.position.z;
        if (distanceZPlayer < distanceStartAttack && distanceZPlayer > 0) {
            TrackPlayer();
            locked |= distanceZPlayer < distanceLockAttack;
        }
    }

    /// <summary>
    /// Tracks the position of the player and moves towards it
    /// </summary>
    void TrackPlayer() {
        if (locked) {
            return;
        }

        Vector3 playerPos = player.transform.position;
        transform.LookAt(playerPos);
        moving = true;
    }

    /// <summary>
    /// When the bird collides with either the player or a slope, resets its
    /// moving status.
    /// </summary>
    /// <param name="collision">The object it is colliding with.</param>
    private void OnCollisionEnter(Collision collision) {
        string collisionTag = collision.gameObject.tag;
        if (collisionTag.Equals("Player") || collisionTag.Equals("Slope")) {
            locked = false;
            moving = false;
        }
    }
}
