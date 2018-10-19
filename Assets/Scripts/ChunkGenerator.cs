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
    private int currentChunkIndex;
    private float currentChunkPosition = 0.0f;

    private bool firstChunk = true;
    private bool secondChunk = true;

    /// <summary>
    /// Awake is used to initialize any variables or game state before the game starts.
    /// </summary>
    private void Awake() {
        // Create chunk queue 
        InitializeChunks();

        // Set the game controller
        gameController = gameControllerObj.GetComponent<MainGameController>();
    }

    /// <summary>
    /// Update is called once per frame
    /// </summary>
    private void Update() {
        // Do not update if game is paused or over
        if (gameController.isGameOver() || gameController.isPaused()) {
            return;
        }

        currentChunkIndex = GetCurrentChunkIndex();

        // Activate new chunks
        ActivateChunks();

        // Delete previous chunks
        SweepPreviousChunks();
    }

    /// <summary>
    /// Initialise chunk queue.
    /// </summary>
    private void InitializeChunks() {
        chunks = new Queue<GameObject>();

        for (int i = 0; i < numberOfChunks; i++) {
            GameObject chunk = Instantiate(chunkPrefab) as GameObject;
            chunk.transform.position = CalculateNextChunkPosition();

            // All chunks except first are activated
            if (i != 0) {
                chunk.SetActive(false);
            }

            chunks.Enqueue(chunk);
        }
    }

    /// <summary>
    /// Activate chunks left in hierachy
    /// </summary>
    private void ActivateChunks() {
        int threshold = currentChunkIndex + activeChunks;

        for (int i = currentChunkIndex; i < threshold; i++) {
            int minimumIndex = Mathf.Clamp(i, 0, chunks.Count - 1);
            GameObject chunk = chunks.ElementAt(minimumIndex);

            // If not activiated in hierachy, activate it
            if (!chunk.activeInHierarchy) {
                chunk.SetActive(true);

                // Only add obstacles on slopes
                // Don't put any obstacles on the first or second slopes
                if (chunk.tag.Equals("Slope") && !(firstChunk || secondChunk)) {
                    chunk.GetComponent<ObstacleGenerator>().UpdateObstacles();
                }

                // Change the status of the first and second chunks
                secondChunk &= firstChunk;
                firstChunk = false;
            }
        }

    }

    /// <summary>
    /// Calculates next chunk position.
    /// </summary>
    /// <returns>New chunk position in world</returns>
    private Vector3 CalculateNextChunkPosition() {
        Vector3 newPosition = chunkPrefab.transform.position;
        newPosition.z = currentChunkPosition;
        currentChunkPosition += chunkLength;
        return newPosition;
    }

    /// <summary>
    /// Sweeps aside chunks we have passed.
    /// </summary>
    private void SweepPreviousChunks() {
        // Sweep aside previous chunks were finished with
        if (currentChunkIndex > 0) {
            Vector3 previousPosition = chunks.ElementAt(currentChunkIndex - 1).transform.position;
            float distance = Vector3.Distance(transform.position, previousPosition);

            // If distance is greater, recycle chunk
            if (distance > (chunkLength * distanceRatio)) {
                RecycleChunk();
            }
        }
    }

    /// <summary>
    /// Recycle chunk to be used again.
    /// </summary>
    private void RecycleChunk() {
        GameObject chunk = chunks.Dequeue();
        if (chunk.tag.Equals("Slope")) {
            chunk.GetComponent<ObstacleGenerator>().DeactivateObstacles();
        }

        chunk.SetActive(false);
        chunk.transform.position = CalculateNextChunkPosition();
        chunks.Enqueue(chunk);
    }

    /// <summary>
    /// Gets current chunk and index.
    /// </summary>
    private int GetCurrentChunkIndex() {

        for (int i = 0; i < chunks.Count; i++) {
            GameObject chunk = chunks.ElementAt(i);

            // If we are at the midpoint of chunk, were on this chunk
            if (Vector3.Distance(transform.position, chunk.transform.position) <= chunkLength) {
                return i;
            }
        }

        return 0;
    }
}