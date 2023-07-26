using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] static int startingHealth = 4;
    public int health = startingHealth;
    [SerializeField] GameObject player;
    [SerializeField] Transform lastSpawnPoint;



    public void LoseHealth(int amount) => health -= amount;

    public void GainHealth(int amount) => health += amount;

    private void Start()
    {
        NewGame();
    }

    private void Update()
    {
        if (health <= 0)
            NewGame();
    }

    private void NewGame()
    {
        SetHealth(startingHealth);
        ResetState();
    }

    private void ResetState()
    {
      //  player.transform = lastSpawnPoint;
    }

    private void SetHealth(int value)
    {
        health = startingHealth;
    }

}
