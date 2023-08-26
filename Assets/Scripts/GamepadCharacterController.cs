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

    void Awake()
    {
        controls = new ControllerInputs();

        controls.Gameplay.Move.performed += ctx => playerScript.setMovement(ctx.ReadValue<Vector2>());
        controls.Gameplay.Move.canceled += ctx => playerScript.setMovement(Vector2.zero);

        controls.Gameplay.Transform.performed += ctx => Transform();
        controls.Gameplay.CycleRight.performed += ctx => formScript.NextChoice();
        controls.Gameplay.CycleLeft.performed += ctx => formScript.PrevChoice();
        controls.Gameplay.Jump.performed += ctx => Jump();
        controls.Gameplay.Interact.performed += ctx => gamepadInteract();
        controls.Gameplay.Pause.performed += ctx => gamepadPause();
    }

    void OnEnable() {
        controls.Gameplay.Enable();
    }

    void OnDisable() {
        controls.Gameplay.Disable();
    }

    void Transform() {
        if (playerScript.TransformationChecker()) {
            formScript.SelectChoice();
        } else playerScript.TransformationHandler();
    }

    void Jump() {
        if (playerScript.transformation == Transformation.FROG) {
            playerScript.setJump();
        }
    }

    void gamepadInteract() 
    {
        if (dialogueScript.gameObject.activeSelf) {
            dialogueScript.checkNext();
        } else {
            playerScript.Interact();
        }
    }

    void gamepadPause() 
    {
        if (pauseMenu.checkPause())
            pauseMenu.ResumeGame();
        else
            pauseMenu.PauseGame();
    }
}
