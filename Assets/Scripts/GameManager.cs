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
    [SerializeField] private Image[] healthSprites = new Image[maxHealth];

    private static GameManager instance;

    private bool gameOver = false;
    private bool death = false;

    public float resetDelay = 1f;

    public CheckPoint lastCheckPoint;

    public void LoseHealth(int amount) => SetHealth(health - amount);

    public void GainHealth(int amount) => SetHealth(health + amount);

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this);
    }
    private void Start()
    {
        SetHealth(maxHealth);
        death = false;
        FindAnyObjectByType<AudioManager>().Play("Ambience");
        Debug.Log(player);
    }

    private void Update()
    {
        if (health <= 0 && !death) {
            death = true;
            player.GetComponent<IsometricCharacterController>().Die();
        }
    }

    private void Respawn()
    {
        death = false;
        SetHealth(maxHealth);
        player.transform.position = lastCheckPoint.transform.position;
        Debug.Log("Player Respawned");
    }

    private void ResetGame()
    {
        Debug.Log("Game Restarted");
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); 
    }

    public void Death()
    {
        if(lastCheckPoint == null)
        {
            gameOver = true;
            Invoke("ResetGame", resetDelay);
        }
        else
            Invoke("Respawn", resetDelay);
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
