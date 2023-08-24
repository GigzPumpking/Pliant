using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] private string nextScene;

    public Animator transition;

    public float transitionTimer = 1f;
   

    public void LoadNextScene()
    {
        StartCoroutine(LoadScene());
    }

    IEnumerator LoadScene()
    {
        transition.SetTrigger("Start");

        yield return new WaitForSeconds(transitionTimer);

        SceneManager.LoadScene(nextScene);
    }

}
