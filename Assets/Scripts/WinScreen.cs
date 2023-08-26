using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinScreen : MonoBehaviour
{
    [SerializeField] private GameObject loader;

    private SceneLoader sceneLoader;

    private void Awake()
    {
        sceneLoader = loader.GetComponent<SceneLoader>();
    }

    public void Transition()
    {
        sceneLoader.LoadNextScene("Main Menu");
    }
}
