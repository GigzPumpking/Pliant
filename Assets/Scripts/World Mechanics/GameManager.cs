using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance { get { return instance; } }

    [SerializeField] private float maxHealth = 100;
    [SerializeField] private float health;
    [SerializeField] private float damage = 2.5f;
    [SerializeField] private float damageTimer = 1;
    [SerializeField] private float damageWait = 1.5f;
    [SerializeField] private Sprite[] healtSprites;
    [SerializeField] private Image healthImage;
    [SerializeField] private bool isTerry = true;
    [SerializeField] private bool formDamage = false;

    [SerializeField] private GameObject deathUI;

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
        {
            Destroy(this.gameObject);
            return;
        }


        DontDestroyOnLoad(this.gameObject);

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
                IsometricCharacterController.Instance.Die();
                //Invoke(nameof(Death), resetDelay);
            }
        }

        if (IsometricCharacterController.Instance.transformation != Transformation.TERRY)
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
        AudioManager.Instance.Play("Ambience");
        AudioManager.Instance.Play("Radio");
    }

    private void StopBGSound()
    {
        if (AudioManager.Instance == null)
            return;
        AudioManager.Instance.Stop("Ambience");
        AudioManager.Instance.Stop("Radio");
    }

    private void Respawn()
    {
        SetHealth(maxHealth);
        IsometricCharacterController.Instance.transform.position = lastCheckPoint.transform.position;

        deathUI.SetActive(false);
    }

    private void ResetGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); 
    }

    public void delayDeath() {
        Invoke(nameof(Death), 50f);
    }

    public void Death()
    {
        deathUI.SetActive(true);
        IsometricCharacterController.Instance.transformation = Transformation.TERRY;
    }

    public void Retry()
    {
        AudioManager.Instance.Play("Ambience");

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
        AudioManager.Instance.Play("Ambience");
        
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

        SceneLoader.Instance.LoadNextScene("WinScene");
    }
    public void NextLevel()
    {
        SceneLoader.Instance.LoadNextScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

}
