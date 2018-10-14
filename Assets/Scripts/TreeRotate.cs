using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeRotate : MonoBehaviour {
    private Collider treeCollider;

    private void Awake () {
        treeCollider = gameObject.GetComponent<Collider> ();
    }

    private void OnCollisionEnter (Collision other) {
        if (other.gameObject.transform.tag == "Player") {
            treeCollider.enabled = false;
            StartCoroutine (RotateTree (Vector3.forward * 90.0f, 0.8f));
        }
    }

    IEnumerator RotateTree (Vector3 angle, float time) {
        Quaternion rotation = transform.rotation;
        Quaternion newAngle = Quaternion.Euler (transform.eulerAngles + angle);

        for (float t = 0f; t < 1.0f; t += Time.deltaTime / time) {
            transform.rotation = Quaternion.Slerp (rotation, newAngle, t);
            yield return null;
        }

        this.gameObject.SetActive (false);
    }

}