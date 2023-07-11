using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsometricCharacterController : MonoBehaviour
{

    public float movementSpeed = 1f;
    //IsometricCharacterRenderer isoRenderer;
    [SerializeField] AnimationCurve curveY;
    Rigidbody2D rbody;
    [SerializeField] GameObject hitbox;
    CapsuleCollider2D collider;
    Vector2 movement;
    Vector2 currPos;
    Vector2 landPos;
    float landDis;
    float timeElapsed = 0f;
    bool isGrounded = true;
    bool jump = false;


    private void Awake()
    {
        rbody = GetComponent<Rigidbody2D>();
        collider = hitbox.GetComponent<CapsuleCollider2D>();
        //isoRenderer = GetComponentInChildren<IsometricCharacterRenderer>();
    }

    void Update() {
        InputHandler();
    }


    // Update is called once per frame
    void FixedUpdate()
    {
        if (jump) {
            JumpHandler();
        } else {
            MoveHandler();
        }
    }

    void JumpHandler() {
        if (isGrounded) {
            currPos = rbody.position;
            landPos = currPos + movement.normalized * movementSpeed;
            landDis = Vector2.Distance(currPos, landPos);
            timeElapsed = 0f;
            isGrounded = false;
        } else {
            timeElapsed += Time.deltaTime * movementSpeed / landDis;
            if (timeElapsed <= 1f) {
                // turn off character collider
                collider.enabled = false;
                currPos = Vector2.MoveTowards(currPos, landPos, Time.fixedDeltaTime*movementSpeed);
                rbody.MovePosition(new Vector2(currPos.x, currPos.y + curveY.Evaluate(timeElapsed)));
            } else {
                // turn on character collider
                collider.enabled = true;
                jump = false;
                isGrounded = true;
            }
        }
    }

    void MoveHandler() {
        rbody.MovePosition(rbody.position + movement.normalized * movementSpeed * Time.fixedDeltaTime);
    }

    void InputHandler() {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        movement = new Vector2(horizontal, vertical);
        movement = Vector2.ClampMagnitude(movement, 1);

        if (Input.GetKeyDown(KeyCode.Space)) {
            jump = true;
        }
    }
}
