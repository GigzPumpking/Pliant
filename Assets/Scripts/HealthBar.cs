using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    private Slider healthBar;
    public GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        healthBar = transform.GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        healthBar.value = gameManager.GetHealth();
    }
}
