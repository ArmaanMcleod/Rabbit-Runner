using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretController : MonoBehaviour {
    private GameObject player;
    private LineRenderer lineRenderer;

    public float lineHeightOffset;

    private Color lineColour = Color.red;

    private void Awake () {
        player = GameObject.FindGameObjectWithTag ("Player");
        lineRenderer = gameObject.GetComponent<LineRenderer> ();
        lineRenderer.enabled = false;
        lineRenderer.material = new Material (Shader.Find ("Particles/Additive"));
        lineRenderer.startColor = lineColour;
        lineRenderer.endColor = lineColour;
    }

    /// <summary>
    /// Update is called once per frame
    /// </summary>
    private void Update () {
        UpdateRotation ();
        DrawLaser ();
    }

    /// <summary>
    /// Draws laser to player
    /// </summary>
    private void DrawLaser () {
        Vector3 newPosition = transform.position;
        newPosition.y += lineHeightOffset;

        lineRenderer.SetPosition (0, newPosition);
        lineRenderer.SetPosition (1, player.transform.position);
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
            lineRenderer.enabled = true;
        }
    }

    /// <summary>
    /// OnTriggerExit is called when the Collider other has stopped touching the trigger.
    /// </summary>
    /// <param name="other">The other Collider involved in this collision.</param>
    private void OnTriggerExit (Collider other) {
        if (other.gameObject.tag == "Player") {
            lineRenderer.enabled = false;
        }
    }
}