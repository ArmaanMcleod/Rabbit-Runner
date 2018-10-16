using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FogController : MonoBehaviour {
    // Standard assets dust storm attached
    public GameObject dustStormPrefab;
    public GameObject lightningPrefab;

    // Particle system to play fog
    private ParticleSystem dustParticles;
    private ParticleSystem lightningParticles;

    // Distance from player
    public float distance;

    // Middle of slope
    public float middle;

    /// <summary>
    /// Awake is used to initialize any variables or game state before the game starts.
    /// </summary>
    private void Awake () {
        GameObject dustStorm = Instantiate (dustStormPrefab);
        dustParticles = dustStorm.GetComponent<ParticleSystem> ();

        GameObject lightning = Instantiate (lightningPrefab);
        lightningParticles = lightning.GetComponent<ParticleSystem> ();
    }

    /// <summary>
    /// Update is called once per frame.
    /// </summary>
    private void Update () {
        drawParticles (dustParticles);
        drawParticles (lightningParticles);
    }

    /// <summary>
    /// Renders particles further away from player.
    /// </summary>
    /// <param name="particles"></param>
    private void drawParticles (ParticleSystem particles) {
        Vector3 newPostion = transform.position;
        newPostion.x = middle;
        newPostion.z += distance;

        particles.transform.position = newPostion;
    }

}