using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FogController : MonoBehaviour {
    // Standard assets dust storm attached
    public GameObject dustStormPrefab;

    // Time to play dust store particle system
    public float stormTime;

    /// <summary>
    /// Update is called once per frame
    /// </summary>
    private void Update () {

        // Find furthest slope and play dust storm
        GameObject furthest = FindFurthestSlope ();
        GameObject dustStorm = Instantiate (dustStormPrefab, furthest.transform.position, furthest.transform.rotation);
        Destroy (dustStorm, stormTime);
    }

    /// <summary>
    /// Returns slope with maximum z distance.
    /// </summary>
    /// <returns>The furthest slope from current position</returns>
    private GameObject FindFurthestSlope () {
        GameObject furthestSlope = null;
        float maxLength = float.MinValue;

        foreach (GameObject slope in GameObject.FindGameObjectsWithTag ("Slope")) {
            float currentLength = slope.transform.position.z;

            if (currentLength > maxLength && slope.activeInHierarchy) {
                maxLength = currentLength;
                furthestSlope = slope;
            }
        }

        return furthestSlope;
    }
}