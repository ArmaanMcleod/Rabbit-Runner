using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleGenerator : MonoBehaviour {

    private ObstacleSection obstacleSection;

    private bool alreadyUpdated = false;

    // Use this for initialization
    void Start() {
        Vector3 position = gameObject.transform.position;
        Vector3 size = gameObject.GetComponent<Renderer>().bounds.size;

        this.obstacleSection = new ObstacleSection(position, size.x, size.z);
    }

    // Update is called once per frame
    void Update() {
        if (!alreadyUpdated && SlopeActive()) {
            Debug.Log("Updating obstacle locations");
            obstacleSection.UpdateCoordinates(gameObject.transform.position);
            alreadyUpdated = true;
        }

        if (alreadyUpdated && !SlopeActive()) {
            Debug.Log("Preparing for next update");
            alreadyUpdated = false;
        }
    }

    private bool SlopeActive() {
        return gameObject.GetComponent<ActiveScript>().GetActive();
    }
}