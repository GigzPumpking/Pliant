using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RampTriggerDetection : MonoBehaviour
{
    public IsometricCharacterController playerScript;

    void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.layer == 6) {
            playerScript.onRamp = true;
            Physics2D.IgnoreLayerCollision(6, 7, true);
            Physics2D.IgnoreLayerCollision(6, 10, true);
        }
    }

    void OnTriggerExit2D(Collider2D other) {
        if (other.gameObject.layer == 6) {
            playerScript.onRamp = false;
            Physics2D.IgnoreLayerCollision(6, 7, false);
            Physics2D.IgnoreLayerCollision(6, 10, false);
        }
    }
}
