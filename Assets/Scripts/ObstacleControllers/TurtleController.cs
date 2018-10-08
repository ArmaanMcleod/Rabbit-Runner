using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurtleController : MonoBehaviour {

    // Speed the object moves at
    public float speed = 2;


    /// <summary>
    /// Update is called once per frame
    /// </summary>
    void Update() {
        transform.position += transform.forward * speed * Time.deltaTime;
    }

    void OnCollisionEnter(Collision col){
        if(col.gameObject.tag!="Player"){
            transform.localRotation *= Quaternion.Euler(0, 180, 0);
        }
    }
}
