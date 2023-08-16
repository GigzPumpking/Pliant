using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RechargeStation : MonoBehaviour
{
    public GameObject GameManager;
    public GameManager gm;

    private void Awake()
    {
        if (GameManager == null)
            GameManager = GameObject.FindWithTag("GM");

        gm = GameManager.GetComponent<GameManager>();
    }

    void OnTriggerEnter2D(Collider2D other) {
        // If the object is on player layer and the player is in the Terry form inform Game Manager to heal.
        if (other.gameObject.layer == 6 && 
            other.GetComponentInParent<IsometricCharacterController>().transformation == Transformation.TERRY) 
        {
            gm.GainHealth(gm.GetMaxHealth());
        }
    }
}
