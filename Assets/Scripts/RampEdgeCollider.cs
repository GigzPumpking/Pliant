using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RampEdgeCollider : MonoBehaviour
{

    private EdgeCollider2D col;
    private PolygonCollider2D polyCol;

    void Awake()
    {
        col = GetComponent<EdgeCollider2D>();
        polyCol = GetComponent<PolygonCollider2D>();
    }

    void FixedUpdate()
    {
        if (polyCol.IsTouchingLayers(LayerMask.GetMask("World")) || polyCol.IsTouchingLayers(LayerMask.GetMask("Border"))) {
            Debug.Log("RampEdgeCollider is touching world or border");
            col.enabled = false;
        } else {
            col.enabled = true;
        }
    }
}
