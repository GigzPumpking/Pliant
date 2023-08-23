using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    [SerializeField] private float maxHealth = 100;
    [SerializeField] private float health;
    [SerializeField] private float damage = 2.5f;
    [SerializeField] private float damageTimer = 1;
    [SerializeField] private float damageWait = 1.5f;
    [SerializeField] private GameObject player;
    [SerializeField] private Sprite[] healtSprites;
    [SerializeField] private Image healthImage;
    [SerializeField] private bool isTerry = true;
    [SerializeField] private bool formDamage = false;

    [SerializeField] private GameObject deathUI;

    private static GameManager instance;

    private IsometricCharacterController playerScript;

    private AudioManager am;

    private bool dead = false;

    private float hstage1, hstage2, hstage3, hstage4;

    public HealthState hState { get; private set; }

    public Text healthText;
    public float resetDelay = 1;

    public CheckPoint lastCheckPoint;

    public void LoseHealth(float amount) => SetHealth(health - amount);

    public void GainHealth(float amount) => SetHealth(health + amount);

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this);

        playerScript = player.GetComponent<IsometricCharacterController>();

        hstage1 = maxHealth;
        hstage2 = 3 * (maxHealth / 4);
        hstage3 = maxHealth / 2;
        hstage4 = maxHealth / 4;
    }
    private void Start()
    {
        deathUI.SetActive(false);

        SetHealth(maxHealth);

        PlayBGSound();
    }

    private void Update()
    {
        if (health <= 0)
        {
            if (!dead)
            {
                dead = true;
                playerScript.Die();
                Invoke(nameof(Death), resetDelay);
            }
        }

        if (playerScript.transformation != Transformation.TERRY)
        {
            isTerry = false;
            if (!formDamage)
            {
                formDamage = true;
                DamageHandler();
            }
        }
        else
        {
            isTerry = true;
            formDamage = false;
        }

    }

    private void PlayBGSound()
    {
        FindAnyObjectByType<AudioManager>().Play("Ambience");
        FindAnyObjectByType<AudioManager>().Play("Radio");
    }

    private void StopBGSound()
    {
        FindAnyObjectByType<AudioManager>().Stop("Ambience");
        FindAnyObjectByType<AudioManager>().Stop("Radio");
    }

    private void Respawn()
    {
        SetHealth(maxHealth);
        player.transform.position = lastCheckPoint.transform.position;
        Debug.Log("Player Respawned");

        deathUI.SetActive(false);
    }

    private void ResetGame()
    {
        Debug.Log("Game Restarted");
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); 
    }

    public void delayDeath() {
        Invoke(nameof(Death), 50f);
    }

    public void Death()
    {
        deathUI.SetActive(true);
        playerScript.transformation = Transformation.TERRY;
    }

    public void Retry()
    {
        if (lastCheckPoint == null)
        {
            ResetGame();
        }
        else
        {
            Respawn();

            dead = false;
        }

    }

    public void Quit()
    {
        deathUI.SetActive(false);

        StopBGSound();

        SceneManager.LoadScene("Main Menu");
    }

    private void SetHealth(float value)
    {
        health = value;
        if (health > maxHealth)
            health = maxHealth;
        if (health < 0)
            health = 0;

        healthText.text = "Health: " + health.ToString();

        UpdateHealth();
    }

    public float GetHealth()
    {
        return health;
    }

    public float GetMaxHealth()
    {
        return maxHealth;
    }

    private void UpdateHealth()
    {
        if( health <= hstage4)
        {
            healthImage.sprite = healtSprites[3];
            hState = HealthState.QUART;
        }
        else if( health <= hstage3)
        {
            healthImage.sprite = healtSprites[2];
            hState = HealthState.HALF;
        }
        else if( health <= hstage2)
        {
            healthImage.sprite = healtSprites[1];
            hState = HealthState.THREEQUART;
        }
        else
        {
            healthImage.sprite = healtSprites[0];
            hState = HealthState.FULL;
        }

        Debug.Log("hstage 1 " + hstage1);
        Debug.Log("hstage 2 " + hstage2);
        Debug.Log("hstage 3 " + hstage3);
        Debug.Log("hstage 4 " + hstage4);
    }

    private void DamageHandler()
    {
        InvokeRepeating(nameof(FormDamage), damageWait, damageTimer);
    }

    private void FormDamage()
    {
        if (!isTerry && health > 0)
        {
            LoseHealth(damage);
        }
        else
            CancelInvoke(nameof(FormDamage));
    }

    public void EndPoint() => Win();

    private void Win()
    {
        StopBGSound();

        SceneManager.LoadScene("Win Screen");
    }

}
