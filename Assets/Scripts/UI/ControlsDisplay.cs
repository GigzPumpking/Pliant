using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControlsDisplay : MonoBehaviour
{
    public GameObject gamepadControls;
    public GameObject keyboardControls;

    public IsometricCharacterController playerScript;

    // Update is called once per frame
    void Update()
    {
        if (playerScript.GetLastInputDevice() == 0) {
            gamepadControls.SetActive(false);
            keyboardControls.SetActive(true);
        }
        else {
            gamepadControls.SetActive(true);
            keyboardControls.SetActive(false);
        }
    }
}
