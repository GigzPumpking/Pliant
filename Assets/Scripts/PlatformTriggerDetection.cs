using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformTriggerDetection : MonoBehaviour
{
    public IsometricCharacterController playerScript;

    void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.layer == 6 && playerScript.transformation != Transformation.BULLDOZER) {
            Debug.Log("Player on platform");
            playerScript.onPlatform = true;
        }
    }

    void OnTriggerExit2D(Collider2D other) {
        if (other.gameObject.layer == 6) {
            Debug.Log("Player off platform");
            playerScript.onPlatform = false;
        }
    }
}
