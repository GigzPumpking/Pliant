using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // to make use of UI image class and sprite and make use of UI through code.
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private static int maxHealth = 4;
    [SerializeField] private int health = maxHealth;
    [SerializeField] private GameObject player;
    [SerializeField] private Transform lastSpawnPoint;
    [SerializeField] private Image[] healthSprites = new Image[maxHealth];

    public void LoseHealth(int amount) => SetHealth(health - amount);

    public void GainHealth(int amount) => SetHealth(health + amount);

    private void Start()
    {
        NewGame();

        FindAnyObjectByType<AudioManager>().Play("Ambience");
    }

    private void Update()
    {
        if (health <= 0)
            NewGame();
    }

    private void NewGame()
    {
        SetHealth(maxHealth);
        ResetState();
    }

    private void ResetState()
    {
        Debug.Log("Game Restarted");
      //  player.transform = lastSpawnPoint;
    }

    private void SetHealth(int value)
    {
        health = value;
        if (health > maxHealth)
            health = maxHealth;
        UpdateHealth();
    }

    public int GetHealth()
    {
        return health;
    }

    private void UpdateHealth()
    {
        for (int itr = 0; itr < healthSprites.Length; itr++)
        {
            if (itr <= health - 1)
                healthSprites[itr].gameObject.SetActive(true);
            else
                healthSprites[itr].gameObject.SetActive(false);
        }
    }

}
