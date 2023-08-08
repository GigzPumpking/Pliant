using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EdgeColliderDetection : MonoBehaviour
{
    private Collider2D tilemapCollider1;
    private Collider2D tilemapCollider2;
    private Collider2D tilemapCollider3a;
    private Collider2D tilemapCollider3b;
    private Collider2D tilemapCollider4a;
    private Collider2D tilemapCollider4b;
    private Collider2D[] tilemapColliders;
    public IsometricCharacterController playerScript;

    void Awake() {
        tilemapCollider1 = transform.Find("Collider1").GetComponent<Collider2D>();
        tilemapCollider2 = transform.Find("Collider2").GetComponent<Collider2D>();
        tilemapCollider3a = transform.Find("Collider3").GetComponent<Collider2D>();
        tilemapCollider3b = transform.Find("Collider3.5").GetComponent<Collider2D>();
        tilemapCollider4a = transform.Find("Collider4").GetComponent<Collider2D>();
        tilemapCollider4b = transform.Find("Collider4.5").GetComponent<Collider2D>();
        tilemapColliders = new Collider2D[] {tilemapCollider1, tilemapCollider2, tilemapCollider3a, tilemapCollider3b, tilemapCollider4a, tilemapCollider4b};
    }

    void FixedUpdate()
    {
        // log if any of the colliders hit the world
        foreach (Collider2D tilemapCollider in tilemapColliders) {
            if (tilemapCollider.IsTouchingLayers(LayerMask.GetMask("World"))) {
                tilemapCollider.enabled = false;
                if (tilemapCollider == tilemapCollider4b) tilemapCollider4a.enabled = false;
                if (tilemapCollider == tilemapCollider3b) tilemapCollider3a.enabled = false;
            }
        }

        if (playerScript.onPlatform) {
            tilemapCollider3b.enabled = false;
            tilemapCollider4b.enabled = false;
        } else {
            tilemapCollider3b.enabled = true;
            tilemapCollider4b.enabled = true;
        }
    }
}
