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
            if (gameObject.layer == 8) {
                Physics2D.IgnoreLayerCollision(6, 7, true);
                Physics2D.IgnoreLayerCollision(6, 10, true);
            } else {
                rampMoveableScript.walkableState();
            }
        }
    }

    void OnTriggerExit2D(Collider2D other) {
        if (other.gameObject.layer == 6) {
            playerScript.onRamp = false;
            if (gameObject.layer == 8) {
                Physics2D.IgnoreLayerCollision(6, 7, false);
                Physics2D.IgnoreLayerCollision(6, 10, false);
            } else {
                rampMoveableScript.pushableState();
            }
        }
    }
}
