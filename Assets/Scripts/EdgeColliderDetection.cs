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
        Debug.Log("edgeCollider1: " + edgeCollider1.IsTouchingLayers());
        Debug.Log("edgeCollider2: " + edgeCollider2.IsTouchingLayers());
        Debug.Log("edgeCollider3: " + edgeCollider3.IsTouchingLayers());
        Debug.Log("edgeCollider4: " + edgeCollider4.IsTouchingLayers());
    }
}
