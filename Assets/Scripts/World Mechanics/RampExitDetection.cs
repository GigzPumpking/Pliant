using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RampExitDetection : MonoBehaviour
{

    void OnTriggerEnter2D(Collider2D other) {
        // check if detection layer is colliding with world layer (which is a tilemap and NOT a gameobject)
        if (other.gameObject.layer == 7) {
            Physics2D.IgnoreLayerCollision(6, 9, true);
        }
    }

    void OnTriggerExit2D(Collider2D other) {
        if (other.gameObject.layer == 7) {
            Physics2D.IgnoreLayerCollision(6, 9, false);
        }
    }
}
