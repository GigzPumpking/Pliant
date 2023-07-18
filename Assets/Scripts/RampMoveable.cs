using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RampMoveable : MonoBehaviour
{
    public IsometricCharacterController playerScript;
    private Rigidbody2D rbody;

    void Awake()
    {
        rbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerScript.transformation == "bulldozer") {
            rbody.constraints = RigidbodyConstraints2D.FreezeRotation;
        } else {
            rbody.constraints = RigidbodyConstraints2D.FreezeAll;
        }
    }
}
