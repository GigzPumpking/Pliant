using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EdgeWorldDetection : MonoBehaviour
{
    public GameObject collider;
    public GameObject otherCollider;
    private bool Wall = false;

    void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.layer == 7 && otherCollider.activeSelf) {
            collider.SetActive(false);
            Wall = true;
        }
    }

    void OnTriggerExit2D(Collider2D other) {
        if (other.gameObject.layer == 7) {
            collider.SetActive(true);
            Wall = false;
        }
    }

    public bool GetWall() {
        return Wall;
    }
}
