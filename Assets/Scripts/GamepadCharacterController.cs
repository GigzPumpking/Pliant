using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GamepadCharacterController : MonoBehaviour
{
    ControllerInputs controls;
    public IsometricCharacterController playerScript;
    public FormManager formScript;

    void Awake()
    {
        controls = new ControllerInputs();
        Debug.Log(controls);

        controls.Gameplay.Move.performed += ctx => playerScript.setMovement(ctx.ReadValue<Vector2>());
        controls.Gameplay.Move.canceled += ctx => playerScript.setMovement(Vector2.zero);

        controls.Gameplay.Transform.performed += ctx => Transform();
        controls.Gameplay.CycleRight.performed += ctx => formScript.NextChoice();
        controls.Gameplay.CycleLeft.performed += ctx => formScript.PrevChoice();
        controls.Gameplay.Jump.performed += ctx => Jump();

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
}
