using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DefaultButtonSelect : MonoBehaviour
{
    // Start is called before the first frame update
    public Button defaultButton;

    void OnEnable()
    {
        Debug.Log(defaultButton);
        defaultButton.Select();
    }
}
