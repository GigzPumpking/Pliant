using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMoveable : MonoBehaviour {
    private Rigidbody2D rbody;

    void Awake()
    {
        rbody = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        if (IsometricCharacterController.Instance.transformation == Transformation.BULLDOZER) {
            rbody.constraints = RigidbodyConstraints2D.FreezeRotation;
        } else {
            rbody.constraints = RigidbodyConstraints2D.FreezeAll;
        }
    }
}
