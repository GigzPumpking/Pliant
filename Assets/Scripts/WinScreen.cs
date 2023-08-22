using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinScreen : MonoBehaviour
{
    public float transitionWaitTime = 7f;
    // Start is called before the first frame update
    void Start()
    {
        Invoke(nameof(Transition), transitionWaitTime);
    }

    void Transition()
    {
        SceneManager.LoadScene("Main Menu");
    }
}
