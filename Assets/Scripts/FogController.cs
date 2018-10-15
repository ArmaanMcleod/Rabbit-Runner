using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FogController : MonoBehaviour {
    // Standard assets dust storm attached
    public GameObject dustStormPrefab;

    private ParticleSystem particles;

    public float distance;

    private float middle = 10.0f;

    /// <summary>
    /// Awake is used to initialize any variables or game state before the game starts.
    /// </summary>
    private void Awake () {
        GameObject dustStorm = Instantiate (dustStormPrefab);
        particles = dustStorm.GetComponent<ParticleSystem> ();
    }

    /// <summary>
    /// Update is called once per frame.
    /// </summary>
    private void Update () {
        Vector3 newPostion = transform.position;
        newPostion.x = middle;
        newPostion.z += distance;

        particles.transform.position = newPostion;
        particles.Play ();
    }
}