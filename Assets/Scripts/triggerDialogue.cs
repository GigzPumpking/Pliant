using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class triggerDialogue : MonoBehaviour
{
    public Dialogue dialogue;
    public string[] dialogueLines;
    public IsometricCharacterController playerScript;
    public GameObject interactButton;

    void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player") && playerScript.transformation == Transformation.TERRY) {
            dialogue.setSentences(dialogueLines);
            playerScript.canTalk();
            interactButton.SetActive(true);
        }
    }

    void OnTriggerExit2D(Collider2D other) {
        if (other.CompareTag("Player")) {
            playerScript.cannotTalk();
            interactButton.SetActive(false);
        }
    }
}
