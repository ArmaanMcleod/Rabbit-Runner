using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {
    public PlayerController playerController;

    private float offset;

    // Use this for initialization
    private void Start () {
        offset = transform.position.z - playerController.transform.position.z;
    }
    // Update is called once per frame
    private void Update () {
        float x = transform.position.x;
        float y = transform.position.y;
        float z = playerController.transform.position.z + offset;

        transform.position = new Vector3 (x, y, z);
    }
}