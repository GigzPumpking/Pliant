using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerColliderScript : MonoBehaviour
{
    private Collider2D terryCollider;
    private Collider2D frogCollider;
    private Collider2D bulldozerCollider;

    void Awake() {
        terryCollider = transform.Find("TerryCollider").GetComponent<Collider2D>();
        frogCollider = transform.Find("FrogCollider").GetComponent<Collider2D>();
        bulldozerCollider = transform.Find("BulldozerCollider").GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Collider2D GetTerryCollider() {
        return terryCollider;
    }

    public Collider2D GetFrogCollider() {
        return frogCollider;
    }

    public Collider2D GetBulldozerCollider() {
        return bulldozerCollider;
    }

    public void SetTerryCollider() {
        terryCollider.enabled = true;
        frogCollider.enabled = false;
        bulldozerCollider.enabled = false;
    }

    public void SetFrogCollider() {
        frogCollider.enabled = true;
        terryCollider.enabled = false;
        bulldozerCollider.enabled = false;
    }

    public void SetBulldozerCollider() {
        bulldozerCollider.enabled = true;
        terryCollider.enabled = false;
        frogCollider.enabled = false;
    }
}
