using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformTriggerDetection : MonoBehaviour
{
    public IsometricCharacterController playerScript;
    public EdgeWorldDetection edgeScript1;
    public EdgeWorldDetection edgeScript2;

    void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag == "Player" && playerScript.transformation != Transformation.BULLDOZER) {
            playerScript.onPlatform = true;
        }
    }

    void OnTriggerExit2D(Collider2D other) {
        if (other.gameObject.tag == "Player" && !edgeScript1.GetWall() && !edgeScript2.GetWall()) {
            playerScript.onPlatform = false;
        }
    }
}
