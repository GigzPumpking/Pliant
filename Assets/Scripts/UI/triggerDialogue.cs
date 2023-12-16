using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class triggerDialogue : MonoBehaviour
{
    public Dialogue dialogue;
    public string[] dialogueLines;
    public IsometricCharacterController playerScript;
    public GameObject interactButton;

    public Sprite keyboardSprite;
    public Sprite gamepadSprite;

    void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player") && playerScript.transformation == Transformation.TERRY) {
            dialogue.setSentences(dialogueLines);
            playerScript.canTalk();
            ShowInteractButton();
        }
    }

    void OnTriggerExit2D(Collider2D other) {
        if (other.CompareTag("Player")) {
            playerScript.cannotTalk();
            interactButton.SetActive(false);
        }
    }

    void ShowInteractButton() {
        if (playerScript.GetLastInputDevice() == 0) {
            interactButton.GetComponent<SpriteRenderer>().sprite = keyboardSprite;
        }
        else {
            interactButton.GetComponent<SpriteRenderer>().sprite = gamepadSprite;
        }
        interactButton.SetActive(true);
    }
}
