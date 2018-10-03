using System;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSection {

    /// <summary>
    /// Number of obstacles per section.
    /// </summary>
    private readonly int NUM_PER_SECTION = 10;

    /// <summary>
    /// Number of each obstacle type held by this object
    /// </summary>
    private readonly int NUM_BIRDS = 2;
    private readonly int NUM_CONIFERS = 3;
    private readonly int NUM_ROCKS = 10;
    private readonly int NUM_HARES = 2;

    /// <summary>
    /// References to the cube and cylinder prefabs.
    /// </summary>
    private readonly GameObject CONIFER_PREFAB = Resources.Load<GameObject> ("Prefabs/ObstaclePrefabs/Tree_Conifer_01");
    private readonly GameObject ROCK_PREFAB = Resources.Load<GameObject> ("Prefabs/ObstaclePrefabs/Rock_Chunk_01");
    private readonly GameObject HARE_PREFAB = Resources.Load<GameObject> ("Prefabs/ObstaclePrefabs/SCharacter_Turtle");
    private readonly GameObject BIRD_PREFAB = Resources.Load<GameObject> ("Prefabs/ObstaclePrefabs/SCharacter_Bird1");

    /// <summary>
    /// Position of this section's slope.
    /// </summary>
    private Vector3 position;

    /// <summary>
    /// The length of the x dimension of the slope.
    /// </summary>
    private float xLen;

    /// <summary>
    /// The length of the z dimension of the slop.
    /// </summary>
    private float zLen;

    /// <summary>
    /// These two fields hold references to the obstacles so that they can be
    /// recycled.
    /// </summary>
    private List<GameObject> rocks = new List<GameObject> ();
    private List<GameObject> conifers = new List<GameObject> ();
    private List<GameObject> hares = new List<GameObject> ();
    private List<GameObject> birds = new List<GameObject> ();


    private List<GameObject> currentObstacles = new List<GameObject> ();

    public ObstacleSection (Vector3 position, float xLen, float zLen) {
        this.position = position;
        this.xLen = xLen;
        this.zLen = zLen;

        InstantiateObstacles (rocks, ROCK_PREFAB, NUM_ROCKS);
        InstantiateObstacles (conifers, CONIFER_PREFAB, NUM_CONIFERS);
        InstantiateObstacles (hares, HARE_PREFAB, NUM_HARES);
        InstantiateObstacles (birds, BIRD_PREFAB, NUM_BIRDS);


        RandomiseObstacles ();
    }

    /// <summary>
    /// Updates the coordinates of this section and randomises the obstacle locations.
    /// </summary>
    /// <param name="position">The new position for this section of obstacles.</param>
    public void UpdateCoordinates (Vector3 position) {
        this.position = position;
        RandomiseObstacles ();
    }

    /// <summary>
    /// Randomises the obstacles locations and make-up of 
    /// </summary>
    private void RandomiseObstacles () {
        

        // Clear the last set of obstacles
        currentObstacles.ForEach (obj => obj.SetActive (false));
        currentObstacles = new List<GameObject> ();

        // Pick some random obstacles
        int rockIndex = 0;
        int coniferIndex = 0;
        int hareIndex = 0;
        int birdIndex = 0;
        int i = 0;
        while (i < NUM_PER_SECTION) {
            float randomValue = UnityEngine.Random.value;
            
            if(randomValue < 0.07 && birdIndex<NUM_BIRDS){
                ActivateObstacle(birds[birdIndex], 10);
                birdIndex++;
            } else if (randomValue < 0.1 && hareIndex<NUM_HARES) {
                ActivateObstacle(hares[hareIndex], 0);
                hareIndex++;
            } else if (randomValue < 0.3 && coniferIndex<NUM_CONIFERS) {
                ActivateObstacle(conifers[coniferIndex], 0);
                coniferIndex++;
            } else {
                ActivateObstacle(rocks[rockIndex], 0);
                rockIndex++;
            }

            i++;
        }
    }

    private void ActivateObstacle(GameObject obj, float yPos){
        currentObstacles.Add(obj);
        RandomisePosition(obj, yPos);
        obj.SetActive(true);
        
    }

    /// <summary>
    /// Randomises the position of an obstacle.
    /// </summary>
    /// <param name="obstacle">The obstacle whose position will be randomised.</param>
    /// <param name="maxZPos">The upper limit of the Z axis for the random position to be in.</param>
    /// <param name="minZPos">The lower limit of the Z axis for the random position to be in.</param>
    /// <param name="maxXPos">The upper limit of the X axis for the random position to be in.</param>
    /// <param name="minXPos">The lower limit of the X axis for the random position to be in.</param>
    private void RandomisePosition (GameObject obstacle, float yPos) {

        float maxZPos = position.z + (zLen / 2);
        float minZPos = position.z - (zLen / 2);
        float maxXPos = position.x + (xLen / 2);
        float minXPos = position.x - (xLen / 2);

        float newX = UnityEngine.Random.Range (minXPos, maxXPos);
        float newZ = UnityEngine.Random.Range (minZPos, maxZPos);
        obstacle.transform.position = new Vector3 (newX, yPos, newZ);
    }

    /// <summary>
    /// Instantiates the obstacles.
    /// </summary>
    /// <param name="obstacleList">The obstacles container to populate.</param>
    /// <param name="prefab">The obstacle prefab to instantiate from.</param>
    /// <param name="num">The number of prefabs to instantiate.</param>
    private void InstantiateObstacles (List<GameObject> obstacleList, GameObject prefab, int num) {
        for (int i = 0; i < num; i++) {
            GameObject gameObject = UnityEngine.Object.Instantiate (prefab);
            gameObject.SetActive (false);
            obstacleList.Add (gameObject);
        }
    }
}