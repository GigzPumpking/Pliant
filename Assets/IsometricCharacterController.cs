using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsometricCharacterController : MonoBehaviour
{
    // Create controls for the 2D Capsule to move in a 2D Isometric Z - Y plane

    Rigidbody2D rb;
    public float speed = 5f;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        Vector2 direction = new Vector2(x, y).normalized;

        rb.velocity = direction * speed;
    }

    private void Start()
    {
    }

    void Update()
    {
    }
}
