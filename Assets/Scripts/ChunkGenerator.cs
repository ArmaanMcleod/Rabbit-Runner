using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ChunkGenerator : MonoBehaviour {

    // Main game controller to check game state
    private MainGameController gameController;
    public GameObject gameControllerObj;

    // Slope prefab attached to player
    public GameObject chunkPrefab;

    // Slope length of each prefab
    public float chunkLength;

    // Number of active chunks in the scene
    public int activeChunks;

    // Number of chunks queued
    public int numberOfChunks;

    // Distance ratio allowed before generating new chunk
    public float distanceRatio;

    // LIFO queue of chunks
    private Queue<GameObject> chunks;

    // Current chunk state variables
    private GameObject currentChunk;
    private int currentChunkIndex;
    private float currentChunkPosition = 0.0f;

    /// <summary>
    /// Awake is used to initialize any variables or game state before the game starts.
    /// </summary>
    private void Awake () {

        // Create chunk queue 
        InitializeChunks ();

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

        // Update current chunk and index
        currentChunk = GetCurrentChunk ();
        currentChunkIndex = GetCurrentChunkIndex ();

        // Activate new chunks
        ActivateChunks ();

        // Delete previous chunks
        SweepPreviousSlope ();
    }

    /// <summary>
    /// Initialise chunk queue.
    /// </summary>
    private void InitializeChunks () {
        chunks = new Queue<GameObject> ();

        for (int i = 0; i < numberOfChunks; i++) {
            GameObject chunk = Instantiate (chunkPrefab) as GameObject;
            chunk.transform.position = CalculateNextSlopePosition ();

            // All chunks except first are activated
            if (i != 0) {
                chunk.SetActive (false);
            }

            chunks.Enqueue (chunk);
        }
    }

    /// <summary>
    /// Activate chunks left in hierachy
    /// </summary>
    private void ActivateChunks () {
        int threshold = currentChunkIndex + activeChunks;

        for (int i = currentChunkIndex; i < threshold; i++) {
            int minimumIndex = Mathf.Clamp (i, 0, chunks.Count - 1);
            GameObject chunk = chunks.ElementAt (minimumIndex);

            // If not activiated in hierachy, activate it
            if (!chunk.activeInHierarchy) {
                chunk.SetActive (true);
            }
        }
    }

    /// <summary>
    /// Calculates next chunk position.
    /// </summary>
    /// <returns>New chunk position in world</returns>
    private Vector3 CalculateNextSlopePosition () {
        Vector3 newPosition = chunkPrefab.transform.position;
        newPosition.z = currentChunkPosition;
        currentChunkPosition += chunkLength;
        return newPosition;
    }

    /// <summary>
    /// Sweeps aside chunks we have passed.
    /// </summary>
    private void SweepPreviousSlope () {
        // Sweep aside previous chunks were finished with
        if (currentChunkIndex > 0) {
            Vector3 previousPosition = chunks.ElementAt (currentChunkIndex - 1).transform.position;
            float distance = Vector3.Distance (transform.position, previousPosition);

            // If distance is greater, recycle chunk
            if (distance > (chunkLength * distanceRatio)) {
                RecycleChunk ();
            }
        }
    }

    /// <summary>
    /// Recycle chunk to be used again.
    /// </summary>
    private void RecycleChunk () {
        GameObject chunk = chunks.Dequeue ();
        chunk.transform.position = CalculateNextSlopePosition ();

        if (chunk.tag == "Slope") {
            chunk.GetComponent<ObstacleGenerator> ().UpdateObstacles ();
        }

        chunk.SetActive (false);
        chunks.Enqueue (chunk);
    }

    /// <summary>
    /// Gets current chunk game object player is on in world.
    /// </summary>
    /// <returns>The chunk player is on.</returns>
    private GameObject GetCurrentChunk () {
        GameObject newCurrentChunk = null;

        foreach (GameObject chunk in chunks) {

            // If we are at the midpoint of chunk, were on this chunk
            if (Vector3.Distance (transform.position, chunk.transform.position) <= (chunkLength / 2)) {
                newCurrentChunk = chunk;
                break;
            }
        }

        return newCurrentChunk;
    }

    /// <summary>
    /// Gets index of current chunk in world
    /// </summary>
    /// <returns>The index of the current chunk</returns>
    private int GetCurrentChunkIndex () {
        int index = 0;

        for (int i = 0; i < chunks.Count; i++) {
            GameObject chunk = chunks.ElementAt (i);

            // If object is the same, this is the current chunk
            if (chunk.Equals (currentChunk)) {
                index = i;
                break;
            }
        }

        return index;
    }
}