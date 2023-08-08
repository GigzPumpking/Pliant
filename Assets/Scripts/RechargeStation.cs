using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RechargeStation : MonoBehaviour
{
    public GameObject GameManager;

    void OnTriggerEnter2D(Collider2D other) {
        Debug.Log(other.gameObject.layer);
        if (other.gameObject.layer == 6) {
            GameManager.GetComponent<GameManager>().GainHealth(4);
        }
    }
}
