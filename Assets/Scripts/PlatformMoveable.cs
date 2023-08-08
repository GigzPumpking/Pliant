using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMoveable : MonoBehaviour {
    public IsometricCharacterController playerScript;
    private Rigidbody2D rbody;

    void Awake()
    {
        rbody = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        if (playerScript.transformation == Transformation.BULLDOZER) {
            rbody.constraints = RigidbodyConstraints2D.FreezeRotation;
        } else {
            rbody.constraints = RigidbodyConstraints2D.FreezeAll;
        }
    }
}
