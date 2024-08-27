using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RampMoveable : MonoBehaviour
{
    private Rigidbody2D rbody;
    private Transform colliderWalkable;
    private Transform colliderPushable;

    void Awake()
    {
        rbody = GetComponent<Rigidbody2D>();
        colliderWalkable = transform.Find("ColliderWalkable");
        colliderPushable = transform.Find("ColliderPushable");

        walkableState();
    }

    void FixedUpdate()
    {
        if (IsometricCharacterController.Instance.transformation == Transformation.BULLDOZER) {
            rbody.constraints = RigidbodyConstraints2D.FreezeRotation;
            pushableState();
        } else {
            rbody.constraints = RigidbodyConstraints2D.FreezeAll;
            walkableState();
        }
    }

    public void pushableState() {
        colliderWalkable.gameObject.SetActive(false);
        colliderPushable.gameObject.SetActive(true);
    }

    public void walkableState() {
        colliderWalkable.gameObject.SetActive(true);
        colliderPushable.gameObject.SetActive(false);
    }
}
