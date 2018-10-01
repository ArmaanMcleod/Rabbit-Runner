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
    private readonly int NUM_EACH_TYPE = 10;

    // TODO replace with 'real' obstacles.
    /// <summary>
    /// References to the cube and cylinder prefabs.
    /// </summary>
    private readonly GameObject CUBE_PREFAB = Resources.Load<GameObject> ("Prefabs/ObstaclePrefabs/Tree_Conifer_01");
    private readonly GameObject CYLINDER_PREFAB = Resources.Load<GameObject> ("Prefabs/ObstaclePrefabs/Tree_Generic_Spring_01");

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
    private List<GameObject> cylinders = new List<GameObject>();
    private List<GameObject> cubes = new List<GameObject>();

    private List<GameObject> currentObstacles = new List<GameObject>();

    public ObstacleSection(Vector3 position, float xLen, float zLen) {
        this.position = position;
        this.xLen = xLen;
        this.zLen = zLen;

        InstantiateObstacles(cylinders, CYLINDER_PREFAB);
        InstantiateObstacles(cubes, CUBE_PREFAB);

        RandomiseObstacles();
    }

    /// <summary>
    /// Updates the coordinates of this section and randomises the obstacle locations.
    /// </summary>
    /// <param name="position">The new position for this section of obstacles.</param>
    public void UpdateCoordinates(Vector3 position) {
        this.position = position;
        RandomiseObstacles();
    }

    /// <summary>
    /// Randomises the obstacles locations and make-up of 
    /// </summary>
    private void RandomiseObstacles() {
        float maxZPos = position.z + (zLen / 2);
        float minZPos = position.z - (zLen / 2);
        float maxXPos = position.x + (xLen / 2);
        float minXPos = position.x - (xLen / 2);

        // Clear the last set of obstacles
        currentObstacles.ForEach(obj => obj.SetActive(false));
        currentObstacles = new List<GameObject>();

        // Pick some random obstacles
        int cubeIndex = 0;
        int cylinderIndex = 0;
        for (int i = 0; i < NUM_PER_SECTION; i++) {
            float randomValue = UnityEngine.Random.value;
            if (randomValue < 0.5) {
                currentObstacles.Add(cylinders[cylinderIndex]);
                cylinderIndex++;
            } else {
                currentObstacles.Add(cubes[cubeIndex]);
                cubeIndex++;
            }
        }

        // Activate the new set of obstacles and randomise their positions
        currentObstacles.ForEach(obj => {
            RandomisePosition(obj, maxZPos, minZPos, maxXPos, minXPos);
            obj.SetActive(true);
        });
    }

    /// <summary>
    /// Randomises the position of an obstacle.
    /// </summary>
    /// <param name="obstacle">The obstacle whose position will be randomised.</param>
    /// <param name="maxZPos">The upper limit of the Z axis for the random position to be in.</param>
    /// <param name="minZPos">The lower limit of the Z axis for the random position to be in.</param>
    /// <param name="maxXPos">The upper limit of the X axis for the random position to be in.</param>
    /// <param name="minXPos">The lower limit of the X axis for the random position to be in.</param>
    private void RandomisePosition(GameObject obstacle,
        float maxZPos,
        float minZPos,
        float maxXPos,
        float minXPos) {
        float newX = UnityEngine.Random.Range(minXPos, maxXPos);
        float newZ = UnityEngine.Random.Range(minZPos, maxZPos);
        obstacle.transform.position = new Vector3(newX, 0, newZ);
    }

    /// <summary>
    /// Instantiates the obstacles.
    /// </summary>
    /// <param name="obstacleList">The obstacles container to populate.</param>
    /// <param name="prefab">The obstacle prefab to instantiate from.</param>
    private void InstantiateObstacles(List<GameObject> obstacleList, GameObject prefab) {
        for (int i = 0; i < NUM_EACH_TYPE; i++) {
            GameObject gameObject = UnityEngine.Object.Instantiate(prefab);
            gameObject.SetActive(false);
            obstacleList.Add(gameObject);
        }
    }
}