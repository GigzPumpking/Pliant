using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GamepadCharacterController : MonoBehaviour
{
    ControllerInputs controls;
    public IsometricCharacterController playerScript;
    public FormManager formScript;
    public Dialogue dialogueScript;
    public PauseMenu pauseMenu;
    public GameObject introText;

    void Awake()
    {
        controls = new ControllerInputs();

        controls.Gameplay.Move.performed += ctx => {
            playerScript.setMovement(ctx.ReadValue<Vector2>());
            playerScript.SetLastInputDevice(1);
        };
        controls.Gameplay.Move.canceled += ctx => {
            playerScript.setMovement(Vector2.zero);
            playerScript.SetLastInputDevice(1);
        };

        controls.Gameplay.Transform.performed += ctx => Transform();
        controls.Gameplay.CycleRight.performed += ctx => {
            formScript.NextChoice();
            playerScript.SetLastInputDevice(1);
        };
        controls.Gameplay.CycleLeft.performed += ctx => {
            formScript.PrevChoice();
            playerScript.SetLastInputDevice(1);
        };

        controls.Gameplay.Jump.performed += ctx => Jump();
        controls.Gameplay.Interact.performed += ctx => gamepadInteract();
        controls.Gameplay.Pause.performed += ctx => gamepadPause();
    }

    void OnEnable() {
        controls.Gameplay.Enable();
        playerScript.SetLastInputDevice(1);
    }

    void OnDisable() {
        controls.Gameplay.Disable();
        playerScript.SetLastInputDevice(0);
    }

    void Transform() {
        playerScript.SetLastInputDevice(1);
        if (playerScript.TransformationChecker()) {
            formScript.SelectChoice();
        } else playerScript.TransformationHandler();
    }

    void Jump() {
        playerScript.SetLastInputDevice(1);
        if (playerScript.transformation == Transformation.FROG) {
            playerScript.setJump();
        }
    }

    void gamepadInteract() 
    {
        playerScript.SetLastInputDevice(1);
        if (introText.gameObject.activeSelf) {
            introText.gameObject.SetActive(false);
            return;
        }

        if (dialogueScript.gameObject.activeSelf) {
            dialogueScript.checkNext();
        } else {
            playerScript.Interact();
        }
    }

    void gamepadPause() 
    {
        playerScript.SetLastInputDevice(1);
        if (pauseMenu.checkPause())
            pauseMenu.ResumeGame();
        else
            pauseMenu.PauseGame();
    }
}
