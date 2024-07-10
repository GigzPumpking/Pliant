using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RechargeStation : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other) {
        // If the object is on player layer and the player is in the Terry form inform Game Manager to heal.
        if (other.gameObject.layer == 6 && 
            IsometricCharacterController.Instance.transformation == Transformation.TERRY) 
        {
            GameManager.Instance.GainHealth(GameManager.Instance.GetMaxHealth());
            IsometricCharacterController.Instance.HealAnim();
        }
    }
}
