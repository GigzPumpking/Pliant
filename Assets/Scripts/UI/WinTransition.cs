using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinTransition : MonoBehaviour
{
    public IsometricCharacterController playerScript;
    // When player enters the trigger, load Win Scene
    void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player")) {
            playerScript.Win();
        }
    }
}
