using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SlopeGenerator : MonoBehaviour {

    // Slope prefab passed in
    public GameObject prefab;

    // The current slope being created
    private GameObject currentSlope;

    // Length of slope
    public float slopeLength;

    // Timer variables
    public float threshold;
    private float TimeCounter = 0.0f;

    // All previous slopes inserted here
    // TODO: Find a better way to handle this
    private IList<GameObject> previousSlopes = new List<GameObject> ();

    /// <summary>
    /// Use this for initialization
    /// </summary>
    private void Start () {
        // Insert initial slope into previous slopes
        previousSlopes.Add (GameObject.FindGameObjectWithTag ("InitialSlope"));

        // Instantiate a copy current slope with the prefab
        currentSlope = Instantiate (prefab) as GameObject;
        previousSlopes.Add (currentSlope);
    }

    /// <summary>
    /// Update is called once per frame
    /// </summary>
    private void Update () {

        // If timer expires, generate a new slope
        if (Time.time - TimeCounter > threshold) {
            GenerateNewSlope ();
            TimeCounter = Time.time;

            // Remove any slopes that we have passed
            RemoveSlopes ();

        }
    }

    /// <summary>
    /// Removes previous slopes that we've passed.
    /// </summary>
    private void RemoveSlopes () {
        foreach (GameObject slope in previousSlopes.ToList ()) {

            // If player distance greater than slope, destroy slope
            if (slope.transform.position.z < transform.position.z) {
                previousSlopes.Remove (slope);
                Destroy (slope);
            }
        }
    }

    /// <summary>
    /// Attaches new slope to infinite plane. 
    /// </summary>
    private void GenerateNewSlope () {

        // Update coordinates
        float x = currentSlope.transform.position.x;
        float y = currentSlope.transform.position.y;
        float z = currentSlope.transform.position.z + slopeLength;

        // Update new position and rotation
        Vector3 newPosition = new Vector3 (x, y, z);
        Quaternion newRotation = currentSlope.transform.rotation;

        // Instantiate a new prefab slope
        currentSlope = Instantiate (prefab, newPosition, newRotation) as GameObject;
        previousSlopes.Add (currentSlope);
    }
}