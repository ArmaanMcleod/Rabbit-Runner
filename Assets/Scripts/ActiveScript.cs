using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveScript : MonoBehaviour {

    private bool active;

    // Use this for initialization
    void Start() {
        active = false;
    }

    public bool GetActive() {
        return active;
    }

    public void SetActive(bool active) {
        this.active = active;
    }
}
