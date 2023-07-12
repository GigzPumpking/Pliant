using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsometricCharacterController : MonoBehaviour
{
    // Collision Variables
    private Rigidbody2D rbody;
    private GameObject hitbox;
    private Collider2D collider;

    // Animation Variables
    [SerializeField] Sprite frontSprite;
    [SerializeField] Sprite backSprite;
    [SerializeField] Sprite frogSprite;
    [SerializeField] Sprite bulldozerSprite;
    private SpriteRenderer TerrySprite;
    private GameObject sprite;

    // Jumping and Movement Variables
    [SerializeField] AnimationCurve curveY;
    [SerializeField] float movementSpeed = 1f;
    Vector2 movement;
    Vector2 currPos;
    Vector2 landPos;
    float landDis;
    float timeElapsed = 0f;
    bool isGrounded = true;
    bool jump = false;

    // Transformation Variables
    [SerializeField] string transformation = "none";
    [SerializeField] float transformationTime = 1f;
    private GameObject smoke;

    private void Awake()
    {
        rbody = GetComponent<Rigidbody2D>();
        hitbox = GameObject.Find("Collider");
        collider = hitbox.GetComponent<Collider2D>();
        sprite = GameObject.Find("Sprite");
        TerrySprite = sprite.GetComponent<SpriteRenderer>();
        smoke = GameObject.Find("Smoke");
        smoke.SetActive(false);
    }

    void Update() {
        InputHandler();
        if (TransformationHandler()) {
            smoke.SetActive(true);
            StartCoroutine(TransformationTimer());
        }
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

        // set sprite according to movement direction
        if (movement.x > 0) TerrySprite.flipX = true;
        else if (movement.x < 0) TerrySprite.flipX = false;

        if (transformation == "none") {
            if (movement.y > 0) TerrySprite.sprite = backSprite;
            else if (movement.y < 0) TerrySprite.sprite = frontSprite;
        }

        if (Input.GetKeyDown(KeyCode.Space) && transformation == "frog") {
            jump = true;
        }
    }

    bool TransformationHandler() {
        if (Input.GetKeyDown(KeyCode.F)) {
            transformation = "frog";
            TerrySprite.sprite = frogSprite;
        } else if (Input.GetKeyDown(KeyCode.B)) {
            transformation = "bulldozer";
            TerrySprite.sprite = bulldozerSprite;
        } else if (Input.GetKeyDown(KeyCode.Escape)) {
            transformation = "none";
            TerrySprite.sprite = frontSprite;
        } else return false;

        smoke.SetActive(true);
        return true;
    }

    private IEnumerator TransformationTimer() {
        yield return new WaitForSeconds(transformationTime);
        smoke.SetActive(false);
    }
}
