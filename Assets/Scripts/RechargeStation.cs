using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RechargeStation : MonoBehaviour
{
    public GameObject GameManager;

    private void Awake()
    {
        if (GameManager == null)
            GameManager = GameObject.FindWithTag("GM");
    }

    void OnTriggerEnter2D(Collider2D other) {
        // If the object is on player layer and the player is in the Terry form inform Game Manager to heal.
        if (other.gameObject.layer == 6 && 
            other.GetComponentInParent<IsometricCharacterController>().transformation == Transformation.TERRY) {
            GameManager.GetComponent<GameManager>().GainHealth(4);
        }
    }
}
