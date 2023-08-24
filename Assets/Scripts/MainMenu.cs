using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject loader;

    [SerializeField] private string levelSceneName;

    private SceneLoader sceneLoader;

    private void Awake()
    {
        sceneLoader = loader.GetComponent<SceneLoader>();
    }

    public void PlayGame()
    {
        sceneLoader.LoadNextScene(levelSceneName);
    }

    public void QuitGame()
    {
        sceneLoader.QuitFade();
    }
}
