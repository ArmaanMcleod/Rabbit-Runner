using System;
using System.Collections.Generic;

using UnityEngine;

public class ObstacleSection {

    /// <summary>
    /// Number of each obstacle type held by this object
    /// </summary>
    private readonly int NUM_EACH_TYPE = 10;

    // TODO replace with 'real' obstacles.
    /// <summary>
    /// References to the cube and cylinder prefabs.
    /// </summary>
    private readonly GameObject CUBE_PREFAB = Resources.Load<GameObject>("Prefabs/Cube");
    private readonly GameObject CYLINDER_PREFAB = Resources.Load<GameObject>("Prefabs/Cylinder");

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

    private List<GameObject> currentGameObjects = new List<GameObject>();

    public ObstacleSection(Vector3 position, float xLen, float zLen) {
        this.position = position;
        this.xLen = xLen;
        this.zLen = zLen;

        InstantiateObstacles(cylinders, CYLINDER_PREFAB);
        InstantiateObstacles(cubes, CUBE_PREFAB);

        // TODO temp testing of cylinder prefab
        cylinders[0].SetActive(true);

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

    // TODO 
    /// <summary>
    /// Randomises the obstacles locations and make-up of 
    /// </summary>
    private void RandomiseObstacles() {
        cylinders[0].transform.position = position;
        Debug.Log("cylinder location: " + position.ToString());
    }

    

    // TODO create obstacle prefabs and use those instead here
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