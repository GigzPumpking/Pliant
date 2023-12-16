using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RampTriggerDetection : MonoBehaviour
{
    public IsometricCharacterController playerScript;
    public RampMoveable rampMoveableScript;

    void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.layer == 6) {
            playerScript.onRamp = true;
        }
    }

    void OnTriggerExit2D(Collider2D other) {
        if (other.gameObject.layer == 6) {
            playerScript.onRamp = false;
        }
    }
}
