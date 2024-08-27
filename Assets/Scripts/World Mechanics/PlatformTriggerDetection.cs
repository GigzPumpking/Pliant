using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformTriggerDetection : MonoBehaviour
{
    public EdgeWorldDetection edgeScript1;
    public EdgeWorldDetection edgeScript2;

    void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag == "Player" && IsometricCharacterController.Instance.transformation != Transformation.BULLDOZER) {
            IsometricCharacterController.Instance.onPlatform = true;
        }
    }

    void OnTriggerExit2D(Collider2D other) {
        if (other.gameObject.tag == "Player" && !edgeScript1.GetWall() && !edgeScript2.GetWall()) {
            IsometricCharacterController.Instance.onPlatform = false;
        }
    }
}
