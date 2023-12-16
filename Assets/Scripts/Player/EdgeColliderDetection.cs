using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EdgeColliderDetection : MonoBehaviour
{
    private Collider2D tilemapCollider3;
    private Collider2D tilemapCollider4;
    public IsometricCharacterController playerScript;

    void Awake() {
        tilemapCollider3 = transform.Find("Collider3.5").GetComponent<Collider2D>();
        tilemapCollider4 = transform.Find("Collider4.5").GetComponent<Collider2D>();
    }

    void FixedUpdate()
    {
        if (playerScript.onPlatform) {
            tilemapCollider3.enabled = false;
            tilemapCollider4.enabled = false;
        } else {
            tilemapCollider3.enabled = true;
            tilemapCollider4.enabled = true;
        }
    }
}
