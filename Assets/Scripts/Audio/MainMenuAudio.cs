using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuAudio : MonoBehaviour
{
    public void PlayBackground()
    {
        FindAnyObjectByType<AudioManager>().Play("Main Menu BGM");
        FindAnyObjectByType<AudioManager>().Play("Main Menu Ambience");
    }

    public void StopBackgorund()
    {
        FindAnyObjectByType<AudioManager>().Stop("Main Menu BGM");
        FindAnyObjectByType<AudioManager>().Stop("Main Menu Ambience");
    }

    public void DingPlay()
    {
        FindAnyObjectByType<AudioManager>().Play("Main Menu Play");
    }

    public void CrumplePlay()
    {
        FindAnyObjectByType<AudioManager>().Play("Crumple Select");
    }
}
