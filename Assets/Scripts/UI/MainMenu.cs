using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject loader;
    [SerializeField] private GameObject auidoSound;

    [SerializeField] private string levelSceneName;

    private SceneLoader sceneLoader;
    private MainMenuAudio audioPlayer;

    private void Awake()
    {
        sceneLoader = loader.GetComponent<SceneLoader>();
        audioPlayer = auidoSound.GetComponent<MainMenuAudio>();
    }

    private void Start()
    {
        FindAnyObjectByType<AudioManager>().Play("Main Menu BGM");
        FindAnyObjectByType<AudioManager>().Play("Main Menu Ambience");
    }

    public void PlayGame()
    {
        FindAnyObjectByType<AudioManager>().Stop("Main Menu BGM");
        FindAnyObjectByType<AudioManager>().Stop("Main Menu Ambience");
        FindAnyObjectByType<AudioManager>().Play("Main Menu Play");
        sceneLoader.LoadNextScene(levelSceneName);
    }

    public void PlayCrumpleSound()
    {
        FindAnyObjectByType<AudioManager>().Play("Crumple");
    }

    public void QuitGame()
    {
        PlayCrumpleSound();
        sceneLoader.QuitFade();
    }
}
