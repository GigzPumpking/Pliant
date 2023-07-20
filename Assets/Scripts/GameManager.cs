using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] int health;

    public void LoseHealth(int amount) => health -= amount;

    public void GainHealth(int amount) => health += amount;
}
