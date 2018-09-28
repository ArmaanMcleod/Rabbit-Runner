using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleGenerator : MonoBehaviour {

    private ObstacleSection obstacleSection;
    
    // Use this for initialization
    void Start() {
        Vector3 position = gameObject.transform.position;
        Vector3 size = gameObject.GetComponent<Renderer>().bounds.size;

        this.obstacleSection = new ObstacleSection(position, size.x, size.z);
    }

    /// <summary>
    /// Updates the obstacles to lie at the new location.
    /// </summary>
    public void UpdateObstacles() {
        Vector3 position = gameObject.transform.position;
        obstacleSection.UpdateCoordinates(position);
    }
}