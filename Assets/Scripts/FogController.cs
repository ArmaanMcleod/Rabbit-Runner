using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FogController : MonoBehaviour {
    // Standard assets dust storm attached
    public GameObject dustStormPrefab;
    public GameObject lightningPrefab;

    // Particle systems to play fog
    // Also include lightning in fog
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

        // Create duststorm prefab
        GameObject dustStorm = Instantiate (dustStormPrefab);
        dustParticles = dustStorm.GetComponent<ParticleSystem> ();

        // Create lightning prefab
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
        newPostion.y = middle;
        newPostion.z += distance;

        particles.transform.position = newPostion;
    }

}