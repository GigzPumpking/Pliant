using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DefaultButtonSelect : MonoBehaviour
{
    // Start is called before the first frame update
    public Button defaultButton;
    public ControllerInputs controls;
    public EventSystem eventSystem;

    void Awake() 
    {
        controls = new ControllerInputs();
        Debug.Log(controls);

        controls.Gameplay.Interact.performed += ctx => Click();
    }

    void Update() {
        Debug.Log(eventSystem.currentSelectedGameObject);
    }

    void OnEnable()
    {
        controls.Gameplay.Enable();
        defaultButton.Select();
    }

    void OnDisable()
    {
        controls.Gameplay.Disable();
    }

    void Click()
    {
        // call OnClick function of EventSystem's current selected button
        eventSystem.currentSelectedGameObject.GetComponent<Button>().onClick.Invoke();
    }
}
