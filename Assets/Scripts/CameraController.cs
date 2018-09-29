using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {
    // Player reference stored here
    public PlayerController playerController;

    // Offset of camera behind player
    private float offset;

    /// <summary>
    /// Awake is used to initialize any variables or game state before the game starts.
    /// </summary>
    private void Awake () {
        offset = transform.position.z - playerController.transform.position.z;
    }

    /// <summary>
    /// Update is called once per frame
    /// </summary>
    private void Update () {
        float x = transform.position.x;
        float y = transform.position.y;
        float z = playerController.transform.position.z + offset;

        // Update camera position
        transform.position = new Vector3 (x, y, z);
    }
}