using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleGenerator : MonoBehaviour {

    // The obstacle section to generate obstacles
    private ObstacleSection obstacleSection;

    /// <summary>
    /// Awake is used to initialize any variables or game state before the game starts.
    /// </summary>
    private void Awake() {
        Vector3 position = gameObject.transform.position;
        Vector3 size = gameObject.GetComponent<Renderer>().bounds.size;
        this.obstacleSection = new ObstacleSection(position, size.x, size.z);
    }

    /// <summary>
    /// Updates the obstacles to lie at the new location.
    /// </summary>
    public void UpdateObstacles() {
        Vector3 position = gameObject.transform.position;
        this.obstacleSection.UpdateCoordinates(position);
    }

    public void DeactivateObstacles() {
        obstacleSection.DeactivateObstacles();
    }
}