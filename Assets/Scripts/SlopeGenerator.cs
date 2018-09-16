using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlopeGenerator : MonoBehaviour {

    public GameObject currentSlope;

    public float slopeLength;

    // Update is called once per frame
    private void Update () {
        GenerateNewSlope ();
    }

    private void GenerateNewSlope () {
        float x = currentSlope.transform.position.x;
        float y = currentSlope.transform.position.y;
        float z = currentSlope.transform.position.z + slopeLength;

        Vector3 newPosition = new Vector3 (x, y, z);
        Quaternion newRotation = currentSlope.transform.rotation;

        GameObject nextSlope = Instantiate (currentSlope, newPosition, newRotation);
        currentSlope = nextSlope;
    }
}