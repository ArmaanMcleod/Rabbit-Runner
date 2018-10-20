using System;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSection {

    private static readonly int MAX_OBSTACLES = 18;

    /// <summary>
    /// Number of each obstacle type held by this object
    /// </summary>
    private readonly int NUM_BIRDS = 1;
    private readonly int NUM_CONIFERS = 2;
    private readonly int NUM_ROCKS = 18;
    private readonly int NUM_ROCKS_2 = 9;
    private readonly int NUM_TURTLES = 2;
    private readonly int NUM_TURRETS = 1;

    /// <summary>
    /// References to the cube and cylinder prefabs.
    /// </summary>
    private readonly GameObject CONIFER_PREFAB = Resources.Load<GameObject>("Prefabs/ObstaclePrefabs/Tree_Conifer_01");
    private readonly GameObject ROCK_PREFAB = Resources.Load<GameObject>("Prefabs/ObstaclePrefabs/Rock_Chunk_01");
    private readonly GameObject ROCK_PREFAB_2 = Resources.Load<GameObject>("Prefabs/ObstaclePrefabs/Rock_Medium_02");
    private readonly GameObject TURTLE_PREFAB = Resources.Load<GameObject>("Prefabs/ObstaclePrefabs/SCharacter_Turtle");
    private readonly GameObject BIRD_PREFAB = Resources.Load<GameObject>("Prefabs/ObstaclePrefabs/SCharacter_Bird1");
    private readonly GameObject TURRET_PREFAB = Resources.Load<GameObject>("Prefabs/SmallTurret");

    private readonly GameObject HEALTH_ITEM_PREFAB = Resources.Load<GameObject> ("Prefabs/ItemPrefabs/aid_box");
    private readonly GameObject INVINCIBILITY_ITEM_PREFAB = Resources.Load<GameObject> ("Prefabs/ItemPrefabs/Shield");

    /// <summary>
    /// Position of this section's slope.
    /// </summary>
    private Vector3 position;

    /// <summary>
    /// Number of obstacles per section.
    /// </summary>
    private int numPerSection = 6;

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
    private List<GameObject> rocks = new List<GameObject>();
    private List<GameObject> rocks2 = new List<GameObject>();
    private List<GameObject> conifers = new List<GameObject>();
    private List<GameObject> turtles = new List<GameObject>();
    private List<GameObject> birds = new List<GameObject>();
    private List<GameObject> turrets = new List<GameObject>();

    private GameObject healthItem;
    private GameObject invincibilityItem;

    private List<GameObject> currentObstacles = new List<GameObject> ();
    private GameObject currentItem;

    public ObstacleSection (Vector3 position, float xLen, float zLen) {
        this.position = position;
        this.xLen = xLen;
        this.zLen = zLen;

        InstantiateObstacles(rocks, ROCK_PREFAB, NUM_ROCKS);
        InstantiateObstacles(rocks2, ROCK_PREFAB_2, NUM_ROCKS_2);
        InstantiateObstacles(conifers, CONIFER_PREFAB, NUM_CONIFERS);
        InstantiateObstacles(turtles, TURTLE_PREFAB, NUM_TURTLES);
        InstantiateObstacles(birds, BIRD_PREFAB, NUM_BIRDS);
        InstantiateObstacles(turrets, TURRET_PREFAB, NUM_TURRETS);

        InstantiateItems ();
    }

    /// <summary>
    /// Deactivates the obstacles.
    /// </summary>
    public void DeactivateObstacles () {
        currentObstacles.ForEach (obj => obj.SetActive (false));
        currentObstacles.Clear ();
    }

    /// <summary>
    /// Updates the coordinates of this section and randomises the obstacle locations.
    /// </summary>
    /// <param name="position">The new position for this section of obstacles.</param>
    public void UpdateCoordinates (Vector3 position) {
        this.position = position;

        // Increase the number of obstacles whenever this is recycled, up to a max
        if (numPerSection < MAX_OBSTACLES) {
            numPerSection += 2;
        }

        RandomiseObstacles ();
        RandomiseItem ();
    }

    /// <summary>
    /// Randomises the obstacles locations and make-up of 
    /// </summary>
    private void RandomiseObstacles () {
        DeactivateObstacles ();

        // Pick some random obstacles
        int rockIndex = 0;
        int rock2Index = 0;
        int coniferIndex = 0;
        int turtleIndex = 0;
        int birdIndex = 0;
        int turretIndex = 0;

        for (int i = 0; i < numPerSection; i++) {
            float randomValue = UnityEngine.Random.Range (1, 100);

            if (randomValue < 5 && birdIndex < NUM_BIRDS) {
                ActivateObstacle (birds[birdIndex], 15);
                birds[birdIndex].GetComponent<BirdController> ().ResetCall ();
                birdIndex++;
            } else if (randomValue < 8 && turretIndex < NUM_TURRETS) {
                ActivateObstacle (turrets[turretIndex], 1);
                turretIndex++;
            } else if (randomValue < 15 && turtleIndex < NUM_TURTLES) {
                ActivateObstacle (turtles[turtleIndex], 0);
                turtleIndex++;
            } else if (randomValue < 40 && coniferIndex < NUM_CONIFERS) {
                ActivateObstacle (conifers[coniferIndex], 0);
                coniferIndex++;
            } else {
                if(randomValue % 2 == 0 && rock2Index < NUM_ROCKS_2){
                    ActivateObstacle(rocks2[rock2Index], 0.5f);
                    rock2Index++;
                }else{
                    ActivateObstacle(rocks[rockIndex], 0.5f);
                    rockIndex++;
                }
                
            }
        }
    }

    /// <summary>
    /// Rotates an obstacle.
    /// </summary>
    /// <param name="obstacle">The obstacle to rotate.</param>
    private void RotateObstacle (GameObject obstacle) {
        float newYRot = UnityEngine.Random.Range (0, 360);
        obstacle.transform.rotation = Quaternion.Euler (new Vector3 (0, newYRot, 0));
    }

    /// <summary>
    /// Randomly chooses whether to spawn an item and what that item will be.
    /// </summary>
    private void RandomiseItem () {
        if (currentItem != null) {
            currentItem.SetActive (false);
        }

        UnityEngine.Random.InitState (DateTime.Now.Millisecond);
        bool willSpawnItem = UnityEngine.Random.Range (1, 10) > 7.5;
        if (willSpawnItem) {
            float val = UnityEngine.Random.Range (1, 10);
            currentItem = val < 5 ? invincibilityItem : healthItem;
            RandomisePosition (currentItem, UnityEngine.Random.Range (0, 2f));
            currentItem.SetActive (true);
        }
    }

    /// <summary>
    /// Adds obstacle to the current obstacle list and activates it in the game in a random position
    /// </summary>
    /// <param name="obstacle">The obstacle to be activated.</param>
    /// <param name="maxZPos">The obstacle's position on the y=axis</param>
    private void ActivateObstacle (GameObject obj, float yPos) {
        currentObstacles.Add (obj);
        RandomisePosition (obj, yPos);
        RotateObstacle (obj);
        obj.SetActive (true);
    }

    /// <summary>
    /// Randomises the position and rotation of an obstacle.
    /// </summary>
    /// <param name="obstacle">The obstacle whose position will be randomised.</param>
    /// <param name="maxZPos">The obstacle's position on the y=axis</param>
    private void RandomisePosition (GameObject obstacle, float yPos) {
        // The maximum and minimum possible X and Z positions of the obstacle
        float maxZPos = position.z + (zLen / 2);
        float minZPos = position.z - (zLen / 2);
        float maxXPos = position.x + (xLen / 2) - 5;
        float minXPos = position.x - (xLen / 2) + 5;

        float newX = UnityEngine.Random.Range (minXPos, maxXPos);
        float newZ = UnityEngine.Random.Range (minZPos, maxZPos);
        obstacle.transform.position = new Vector3 (newX, yPos, newZ);
    }

    /// <summary>
    /// Instantiates the items.
    /// </summary>
    private void InstantiateItems () {
        invincibilityItem = UnityEngine.Object.Instantiate (INVINCIBILITY_ITEM_PREFAB);
        invincibilityItem.SetActive (false);

        healthItem = UnityEngine.Object.Instantiate (HEALTH_ITEM_PREFAB);
        healthItem.SetActive (false);
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