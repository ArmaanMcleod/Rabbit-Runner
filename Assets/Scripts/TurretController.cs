using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretController : MonoBehaviour {
    // Keep track of the player for lifetime of turret
    private GameObject player;

    // Used to render the laser to the screen
    private LineRenderer lineRenderer;

    // Tue offset height for line renderer
    public float lineHeightOffset;

    // Turret damage
    public int turretDamage;

    // Trigger flag indiciating if the player is currenting being hit
    private bool hitting = false;

    // Registers first hit from laser
    // Needed as game will be too hard if damage is taken on each laser hit
    private bool firstHit = false;

    // The audio source attached
    private AudioSource audioSource;

    /// <summary>
    /// Awake is used to initialize any variables or game state before the game starts.
    /// </summary>
    private void Awake () {
        player = GameObject.FindGameObjectWithTag ("Player");
        lineRenderer = gameObject.GetComponent<LineRenderer> ();
        lineRenderer.enabled = false;
        audioSource = gameObject.GetComponent<AudioSource> ();
    }

    /// <summary>
    /// Update is called once per frame
    /// </summary>
    private void Update () {

        // First fix the rotation of the turret to be facing player
        UpdateRotation ();

        // Draw laser
        DrawLaser ();
    }

    /// <summary>
    /// Draws laser to player
    /// </summary>
    private void DrawLaser () {

        // Update the start position height of laser
        Vector3 startPosition = transform.position;
        startPosition.y += lineHeightOffset;

        Vector3 endPosition = player.transform.position;

        // Draw the laser from start to end
        lineRenderer.SetPosition (0, startPosition);
        lineRenderer.SetPosition (1, endPosition);

        // Check if the laser is hitting the player
        // Only want to take damage once to not make game too difficult
        RaycastHit hit;
        if (Physics.Raycast (startPosition, endPosition - startPosition, out hit) && hitting && !firstHit) {
            if (hit.transform.gameObject.tag == "Player") {
                PlayerHealth playerHealth = hit.transform.GetComponent<PlayerHealth> ();
                playerHealth.TakeDamage (turretDamage);
                firstHit = true;
            }
        }
    }

    /// <summary>
    /// Update rotation of turret to move the turret left and right;
    /// </summary>
    private void UpdateRotation () {
        transform.LookAt (player.transform);
        Quaternion newRotation = transform.rotation;
        newRotation.eulerAngles = new Vector3 (-90.0f, newRotation.eulerAngles.y, newRotation.eulerAngles.z);
        transform.rotation = newRotation;
    }

    /// <summary>
    /// OnTriggerEnter is called when the Collider other enters the trigger.
    /// </summary>
    /// <param name="other">The other Collider involved in this collision.</param>
    private void OnTriggerEnter (Collider other) {
        if (other.gameObject.tag == "Player") {
            audioSource.Play ();
            lineRenderer.enabled = true;
            hitting = true;
        }
    }

    /// <summary>
    /// OnTriggerExit is called when the Collider other has stopped touching the trigger.
    /// </summary>
    /// <param name="other">The other Collider involved in this collision.</param>
    private void OnTriggerExit (Collider other) {
        if (other.gameObject.tag == "Player") {
            lineRenderer.enabled = false;
            hitting = false;
        }
    }
}