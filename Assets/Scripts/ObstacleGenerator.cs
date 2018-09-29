using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleGenerator : MonoBehaviour {

    // The obstacle section to generate obstacles
    private ObstacleSection obstacleSection;

    /// <summary>
    /// Use this for initialization.
    /// </summary>
    private void Start () {
        Vector3 position = gameObject.transform.position;
        Vector3 size = gameObject.GetComponent<Renderer> ().bounds.size;

        this.obstacleSection = new ObstacleSection (position, size.x, size.z);
    }

    /// <summary>
    /// Updates the obstacles to lie at the new location.
    /// </summary>
    public void UpdateObstacles () {
        Vector3 position = gameObject.transform.position;
        obstacleSection.UpdateCoordinates (position);
    }
}