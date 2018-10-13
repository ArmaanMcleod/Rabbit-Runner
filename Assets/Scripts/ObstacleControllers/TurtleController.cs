using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurtleController : MonoBehaviour {

    // Speed the object moves at
    public float speed = 2;

    private float turningSpeed = 5;

    private bool turning = false;

    private readonly float MIN_DIFF = 0.01f;

    private Quaternion desiredRot;

    // Game controller
    private MainGameController gameController;

    // Use this for initialization
    void Start () {
        GameObject gameControllerObj = GameObject.FindGameObjectWithTag ("GameController");
        gameController = gameControllerObj.GetComponent<MainGameController> ();
    }

    /// <summary>
    /// Update is called once per frame
    /// </summary>
    void Update () {
        if (gameController.isGameOver () || gameController.isPaused ()) {
            return;
        }

        // Smooth rotation
        if (turning) {
            if (Mathf.Abs (transform.rotation.y - desiredRot.y) <= MIN_DIFF) {
                turning = false;
            } else {
                transform.rotation = Quaternion.Lerp (transform.rotation, desiredRot, Time.deltaTime * turningSpeed);
            }
        }

        // Move forward
        transform.position += transform.forward * speed * Time.deltaTime;
    }

    /// <summary>
    /// If the turtle collides with anything other than the player, set it to rotate it's
    /// position randomly so it doesn't stay stuck there
    /// </summary>
    /// <param name="col">The Collision data associated with this collision event.</param>
    void OnCollisionEnter (Collision col) {
        if (col.gameObject.tag != "Player") {
            float turningAngle = UnityEngine.Random.Range (20, 340);
            turning = true;
            desiredRot = Quaternion.Euler (transform.localRotation.x, turningAngle, transform.localRotation.z);
        }
    }
}