using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RampMoveable : MonoBehaviour
{
    public IsometricCharacterController playerScript;
    private Rigidbody2D rbody;
    private GameObject colliderWalkable;
    private GameObject colliderPushable;

    void Awake()
    {
        rbody = GetComponent<Rigidbody2D>();
        colliderWalkable = GameObject.Find("ColliderWalkable");
        colliderPushable = GameObject.Find("ColliderPushable");

        if (gameObject.layer == 7) pushableState();
        else if (gameObject.layer == 8) walkableState();
    }

    void FixedUpdate()
    {
        if (playerScript.transformation == Transformation.BULLDOZER) {
            rbody.constraints = RigidbodyConstraints2D.FreezeRotation;
            if (gameObject.layer == 8) pushableState();
        } else {
            rbody.constraints = RigidbodyConstraints2D.FreezeAll;
            if (gameObject.layer == 8) walkableState();
        }
    }

    public void pushableState() {
        colliderWalkable.SetActive(false);
        colliderPushable.SetActive(true);
    }

    public void walkableState() {
        colliderWalkable.SetActive(true);
        colliderPushable.SetActive(false);
    }
}
