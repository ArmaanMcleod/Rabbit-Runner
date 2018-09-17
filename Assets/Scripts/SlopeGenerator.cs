using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SlopeGenerator : MonoBehaviour {

    public GameObject currentSlope;

    public float slopeLength;

    public float threshold;

    private float TimeCounter = 0.0f;

    private List<GameObject> previous = new List<GameObject> ();

    private void Start () {
        previous.Add (GameObject.FindGameObjectWithTag ("Slope"));
    }

    // Update is called once per frame
    private void Update () {
        if (Time.time - TimeCounter > threshold) {
            GenerateNewSlope ();
            TimeCounter = Time.time;
            RemoveSlopes ();
        }
    }

    private void RemoveSlopes () {
        foreach (GameObject slope in previous.ToList ()) {
            if (slope.transform.position.z < transform.position.z) {
                Destroy (slope);
                previous.Remove (slope);
            }
        }
    }

    private void GenerateNewSlope () {
        float x = currentSlope.transform.position.x;
        float y = currentSlope.transform.position.y;
        float z = currentSlope.transform.position.z + slopeLength;

        Vector3 newPosition = new Vector3 (x, y, z);
        Quaternion newRotation = currentSlope.transform.rotation;

        GameObject nextSlope = Instantiate (currentSlope, newPosition, newRotation) as GameObject;

        currentSlope = nextSlope;

        previous.Add (nextSlope);
    }
}