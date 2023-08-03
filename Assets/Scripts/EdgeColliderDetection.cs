using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EdgeColliderDetection : MonoBehaviour
{
    public Collider2D edgeCollider1;
    public Collider2D edgeCollider2;
    public Collider2D edgeCollider3;
    public Collider2D edgeCollider4;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void FixedUpdate()
    {
        // Draw a Raycast on each edgeCollider to see if it is touching a layer
        RaycastHit2D hit1 = Physics2D.Raycast(edgeCollider1.transform.position, Vector2.down, 0.1f);
        RaycastHit2D hit2 = Physics2D.Raycast(edgeCollider2.transform.position, Vector2.down, 0.1f);
        RaycastHit2D hit3 = Physics2D.Raycast(edgeCollider3.transform.position, Vector2.down, 0.1f);
        RaycastHit2D hit4 = Physics2D.Raycast(edgeCollider4.transform.position, Vector2.down, 0.1f);
        Debug.DrawRay(edgeCollider1.transform.position, Vector2.down * 0.1f, Color.red);
        Debug.DrawRay(edgeCollider2.transform.position, Vector2.down * 0.1f, Color.red);
        Debug.DrawRay(edgeCollider3.transform.position, Vector2.down * 0.1f, Color.red);
        Debug.DrawRay(edgeCollider4.transform.position, Vector2.down * 0.1f, Color.red);
        Debug.Log("edgeCollider1: " + edgeCollider1.IsTouchingLayers());
        Debug.Log("edgeCollider2: " + edgeCollider2.IsTouchingLayers());
        Debug.Log("edgeCollider3: " + edgeCollider3.IsTouchingLayers());
        Debug.Log("edgeCollider4: " + edgeCollider4.IsTouchingLayers());
    }
}
