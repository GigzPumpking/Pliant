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
    }

    // Update is called once per frame
    void Update()
    {
        if (playerScript.transformation == Transformation.BULLDOZER) {
            rbody.constraints = RigidbodyConstraints2D.FreezeRotation;
            colliderWalkable.SetActive(false);
            colliderPushable.SetActive(true);
        } else {
            rbody.constraints = RigidbodyConstraints2D.FreezeAll;
            colliderWalkable.SetActive(true);
            if (colliderPushable.tag != "Floor") colliderPushable.SetActive(false);
        }
    }
}
