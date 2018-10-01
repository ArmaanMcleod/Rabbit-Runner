using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SlopeGenerator : MonoBehaviour {

    // Main game controller to check game state
    private MainGameController gameController;
    public GameObject gameControllerObj;

    // Slope prefab attached to player
    public GameObject slopePrefab;

    // Slope length of each prefab
    public float slopeLength;

    // Number of active slopes in the scene
    public int activeSlopes;

    // Number of slopes queued
    public int numberOfSlopes;

    // Distance ratio allowed before generating new slope
    public float distanceRatio;

    // LIFO queue of slopes
    private Queue<GameObject> slopes;

    // Current slope state variables
    private GameObject currentSlope;
    private int currentSlopeIndex;
    private float currentSlopePosition = 0.0f;

    /// <summary>
    /// Awake is used to initialize any variables or game state before the game starts.
    /// </summary>
    private void Awake () {

        // Create slope queue 
        InitializeSlopes ();

        // Set the game controller
        gameController = gameControllerObj.GetComponent<MainGameController> ();
    }

    /// <summary>
    /// Update is called once per frame
    /// </summary>
    private void Update () {
        // Do not update if game is paused or over
        if (gameController.isGameOver () || gameController.isPaused ()) {
            return;
        }

        // Update current slope and index
        currentSlope = GetCurrentSlope ();
        currentSlopeIndex = GetCurrentSlopeIndex ();

        // Activate new slopes
        ActivateSlopes ();

        // Delete previous slopes
        SweepPreviousSlope ();
    }

    /// <summary>
    /// Initialise slope queue.
    /// </summary>
    private void InitializeSlopes () {
        slopes = new Queue<GameObject> ();

        for (int i = 0; i < numberOfSlopes; i++) {
            GameObject slope = Instantiate (slopePrefab) as GameObject;
            slope.transform.position = CalculateNextSlopePosition ();

            // All slopes except first are activated
            if (i != 0) {
                slope.SetActive (false);
            }

            slopes.Enqueue (slope);
        }
    }

    /// <summary>
    /// Activate slopes left in hierachy
    /// </summary>
    private void ActivateSlopes () {
        int threshold = currentSlopeIndex + activeSlopes;

        for (int i = currentSlopeIndex; i < threshold; i++) {
            int minimumIndex = Mathf.Clamp (i, 0, slopes.Count - 1);
            GameObject slope = slopes.ElementAt (minimumIndex);

            // If not activiated in hierachy, activate it
            if (!slope.activeInHierarchy) {
                slope.SetActive (true);
            }
        }
    }

    /// <summary>
    /// Calculates next slope position.
    /// </summary>
    /// <returns>New slope position in world</returns>
    private Vector3 CalculateNextSlopePosition () {
        Vector3 newPosition = slopePrefab.transform.position;
        newPosition.z = currentSlopePosition;
        currentSlopePosition += slopeLength;
        return newPosition;
    }

    /// <summary>
    /// Sweeps aside slopes we have passed.
    /// </summary>
    private void SweepPreviousSlope () {
        // Sweep aside previous slopes were finished with
        if (currentSlopeIndex > 0) {
            Vector3 previousPosition = slopes.ElementAt (currentSlopeIndex - 1).transform.position;
            float distance = Vector3.Distance (transform.position, previousPosition);

            // If distance is greater, recycle slope
            if (distance > (slopeLength * distanceRatio)) {
                RecycleSlope ();
            }
        }
    }

    /// <summary>
    /// Recycle slope to be used again.
    /// </summary>
    private void RecycleSlope () {
        GameObject slope = slopes.Dequeue ();
        slope.transform.position = CalculateNextSlopePosition ();
        slope.GetComponent<ObstacleGenerator> ().UpdateObstacles ();
        slope.SetActive (false);
        slopes.Enqueue (slope);
    }

    /// <summary>
    /// Gets current slope game object player is on in world.
    /// </summary>
    /// <returns>The slope player is on.</returns>
    private GameObject GetCurrentSlope () {
        GameObject newCurrentSlope = null;

        foreach (GameObject slope in slopes) {

            // If we are at the midpoint of slope, were on this slope
            if (Vector3.Distance (transform.position, slope.transform.position) <= (slopeLength / 2)) {
                newCurrentSlope = slope;
                break;
            }
        }

        return newCurrentSlope;
    }

    /// <summary>
    /// Gets index of current slope in world
    /// </summary>
    /// <returns>The index of the current slope</returns>
    private int GetCurrentSlopeIndex () {
        int index = 0;

        for (int i = 0; i < slopes.Count; i++) {
            GameObject slope = slopes.ElementAt (i);

            // If object is the same, this is the current slope
            if (slope.Equals (currentSlope)) {
                index = i;
                break;
            }
        }

        return index;
    }
}