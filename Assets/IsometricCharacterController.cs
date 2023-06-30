using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class IsometricCharacterController : MonoBehaviour
{
    // Create controls for the 2D Capsule to move in a 2D Isometric Z - Y plane

    Rigidbody2D rb;
    public float speed = 5f;
    public float jumpForce = 5f;
    private bool isJumping = false;

    public Tilemap currentTilemap;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
    }

    void Update()
    {
        Jump();

        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        Vector3 direction = new Vector3(x, y, 0).normalized;

        rb.velocity = direction * speed;

        if (isJumping) {
            // Set Tilemap's ComponentCollider trigger to true
            currentTilemap.GetComponent<CompositeCollider2D>().isTrigger = true;
        } else currentTilemap.GetComponent<CompositeCollider2D>().isTrigger = false;

        // The following code detects tiles from a set tilemap and stops the player from moving through them

        /*
        // check the next tile in the direction of movement
        Vector3Int nextTile = currentTilemap.WorldToCell(transform.position + direction);

        // if the next tile is not null, that is a collidable wall

        if (currentTilemap.HasTile(nextTile))
        {
            // stop the player from moving
            if (x != 0 && y != 0) rb.velocity = new Vector3(0, 0, 0);
            else if (x != 0) rb.velocity = new Vector3(0, rb.velocity.y, 0);
            else if (y != 0) rb.velocity = new Vector3(rb.velocity.x, 0, 0);
        }
        */
    }

    // Jumping

    void Jump() {
        if (Input.GetKeyDown(KeyCode.Space)) {
            if (!isJumping)
                isJumping = true;
        }
        else if (Input.GetKeyUp(KeyCode.Space))
            isJumping = false;
    }
}
