using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SlopeGenerator : MonoBehaviour {

    public GameObject prefab;

    private GameObject currentSlope;

    public float slopeLength;

    public float threshold;

    private float TimeCounter = 0.0f;

    private List<GameObject> previousSlopes = new List<GameObject> ();

    private void Start () {
        previousSlopes.Add (GameObject.FindGameObjectWithTag ("InitialSlope"));
        currentSlope = Instantiate (prefab) as GameObject;
        previousSlopes.Add (currentSlope);
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
        foreach (GameObject slope in previousSlopes.ToList ()) {
            if (slope.transform.position.z < transform.position.z) {
                Destroy (slope);
                previousSlopes.Remove (slope);
            }
        }
    }

    private void GenerateNewSlope () {
        float x = currentSlope.transform.position.x;
        float y = currentSlope.transform.position.y;
        float z = currentSlope.transform.position.z + slopeLength;

        Vector3 newPosition = new Vector3 (x, y, z);
        Quaternion newRotation = currentSlope.transform.rotation;

        currentSlope = Instantiate (prefab, newPosition, newRotation) as GameObject;

        previousSlopes.Add (currentSlope);
    }
}