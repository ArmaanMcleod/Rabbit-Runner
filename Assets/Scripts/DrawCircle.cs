using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawCircle : MonoBehaviour {
    // Number of segments for circle
    public int segments;

    // Width of line to be drawn
    public float lineWidth;

    // Radius of circle
    public float radius;

    // Height of circle around object
    public float height;

    // Number of points for circle arcs
    private int pointCount;

    // A line renderer to be attached during runtime
    private LineRenderer lineRenderer;

    /// <summary>
    /// Awake is used to initialize any variables or game state before the game starts.
    /// </summary>
    private void Awake () {
        lineRenderer = gameObject.AddComponent<LineRenderer> ();
        lineRenderer.useWorldSpace = false;
        lineRenderer.startWidth = lineWidth;
        lineRenderer.endWidth = lineWidth;

        // Update colour of line
        lineRenderer.material = new Material (Shader.Find ("Particles/Additive"));
        lineRenderer.startColor = Color.red;
        lineRenderer.endColor = Color.red;

        pointCount = segments + 1;
        lineRenderer.positionCount = pointCount;
    }

    /// <summary>
    /// Use this for initialization.
    /// </summary>
    private void Start () {
        UpdateRotation ();
        DrawLine ();
    }

    /// <summary>
    /// Renders circle lines to the object.
    /// </summary>
    private void DrawLine () {
        Vector3[] points = new Vector3[pointCount];

        for (int i = 0; i < pointCount; i++) {
            float rad = Mathf.Deg2Rad * (i * 360f / segments);
            points[i] = new Vector3 (Mathf.Sin (rad) * radius, height, Mathf.Cos (rad) * radius);
        }

        lineRenderer.SetPositions (points);
    }

    /// <summary>
    /// Updates rotation of object so circle is on ground.
    /// </summary>
    private void UpdateRotation () {
        Quaternion newRotation = transform.rotation;
        newRotation.eulerAngles = new Vector3 (-90.0f, newRotation.eulerAngles.y, newRotation.eulerAngles.z);
        transform.rotation = newRotation;
    }

}