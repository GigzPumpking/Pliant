using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinTransition : MonoBehaviour
{
    // When player enters the trigger, load Win Scene
    void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player")) {
            UnityEngine.SceneManagement.SceneManager.LoadScene("WinScene");
        }
    }
}
