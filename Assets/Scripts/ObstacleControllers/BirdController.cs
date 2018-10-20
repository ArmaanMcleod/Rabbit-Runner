/// <summary>
/// Controller for the birds which targets the player up to a certain distance from
/// the player. Once the distance is reached, the x rotation of the bird is locked 
/// giving the player a chance to dodge the bird by moving left or right on the x plane.
/// The y and z rotations of the bird continue to track the player, resulting in the player
/// being hit if an attempt to move is not made or is made too late.
/// </summary>

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

    // The x-coordinate the bird will move to once the aim is set
    private float lockX;

    // The distance (away from the player) the enemy starts moving and tracking the player
    public float distanceStartAttack;

    // The distance from the player the enemy sets its aim
    public float distanceLockAttack;

    // Sound effect attached
    private AudioSource audioSource;

    // Indicates if first bird call was made
    private bool firstCall = false;

    /// <summary>
    /// Use this for initialization
    /// </summary>
    private void Start () {
        player = GameObject.FindGameObjectWithTag ("Player");
        GameObject gameControllerObj = GameObject.FindGameObjectWithTag ("GameController");
        gameController = gameControllerObj.GetComponent<MainGameController> ();
        audioSource = gameObject.GetComponent<AudioSource> ();
    }

    /// <summary>
    /// Update is called once per frame
    /// </summary>
    private void Update () {
        if (gameController.isGameOver () || gameController.isPaused ()) {
            return;
        }

        if (moving) {
            transform.position += transform.forward * speed * Time.deltaTime;

            if (!firstCall) {
                audioSource.Play ();
                firstCall = true;
            }
        }

        // Calculate the distance from the player in the z-axis
        float distanceZPlayer = gameObject.transform.position.z - player.transform.position.z;
        if (distanceZPlayer < distanceStartAttack && distanceZPlayer > 0) {
            TrackPlayer ();
            if (distanceZPlayer < distanceLockAttack) {
                if (!locked) {
                    lockX = player.transform.position.x;
                }

                locked = true;
            }
        }
    }

    /// <summary>
    /// Tracks the position of the player and moves towards it
    /// </summary>
    private void TrackPlayer () {
        Vector3 playerPos = player.transform.position;
        Vector3 lookPos;

        if (locked) {
            lookPos = new Vector3 (lockX, playerPos.y, playerPos.z);
        } else {
            lookPos = playerPos;
        }

        transform.LookAt (lookPos);
        moving = true;
    }

    /// <summary>
    /// Resets bird call
    /// </summary>
    public void ResetCall () {
        firstCall = false;
    }

    /// <summary>
    /// When the bird collides with either the player or a slope, resets its
    /// moving status.
    /// </summary>
    /// <param name="collision">The object it is colliding with.</param>
    private void OnCollisionEnter (Collision collision) {
        string collisionTag = collision.gameObject.tag;
        if (collisionTag.Equals ("Player") || collisionTag.Equals ("Slope")) {
            locked = false;
            moving = false;
        }
    }
}