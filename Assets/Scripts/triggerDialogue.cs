using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class triggerDialogue : MonoBehaviour
{
    public Dialogue dialogue;
    public string[] dialogueLines;
    public IsometricCharacterController playerScript;

    void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player")) {
            dialogue.setSentences(dialogueLines);
            playerScript.canTalk();
        }
    }

    void OnTriggerExit2D(Collider2D other) {
        if (other.CompareTag("Player")) {
            playerScript.cannotTalk();
        }
    }
}
