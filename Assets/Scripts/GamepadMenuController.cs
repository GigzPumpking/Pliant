using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class GamepadMenuController : MonoBehaviour
{
    ControllerInputs controls;

    void Awake()
    {
        controls = new ControllerInputs();

        controls.Gameplay.Interact.performed += ctx => gamepadInteract();
    }

    void OnEnable() {
        controls.Gameplay.Enable();
    }

    void OnDisable() {
        controls.Gameplay.Disable();
    }

    void gamepadInteract() 
    {
        // click the selected button
        if (EventSystem.current.currentSelectedGameObject != null) {
            EventSystem.current.currentSelectedGameObject.GetComponent<Button>().onClick.Invoke();
        }
    }
}
