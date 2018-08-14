using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class CleanerRay : MonoBehaviour {

    public void OnTriggerEnter(Collider other)
    {
        //this assumes that things that can run into this thing has been limited by the collision matrix
        Destroy(other.gameObject);
    }
}
