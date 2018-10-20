using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuMusic : MonoBehaviour {
    private AudioSource audioSource;

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    private void Awake () {
        audioSource = gameObject.GetComponent<AudioSource> ();
        audioSource.loop = true;
    }
}