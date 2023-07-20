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
    private Animator animator;
    private Animator smokeAnimator;
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
    float lastX = 0f;
    float lastY = 1f;

    public static readonly string[] staticDirections = { "Idle Front", "Idle Back"};
    public static readonly string[] staticFrogDirections = { "Idle Front Frog", "Idle Back Frog"};
    public static readonly string[] staticBulldozerDirections = { "Idle Front Bulldozer", "Idle Back Bulldozer"};
    public static readonly string[] runDirections = {"Walk Front", "Walk Back"};

    // Transformation Variables
    public Transformation transformation = Transformation.TERRY;
    private GameObject smoke;
    private GameObject transformationBubble;

    private void Awake()
    {
        rbody = GetComponent<Rigidbody2D>();
        hitbox = GameObject.Find("Collision");
        collider = hitbox.GetComponent<Collider2D>();
        sprite = GameObject.Find("Sprite");
        animator = sprite.GetComponent<Animator>();
        TerrySprite = sprite.GetComponent<SpriteRenderer>();
        smoke = GameObject.Find("Smoke");
        smokeAnimator = smoke.GetComponent<Animator>();
        smoke.SetActive(false);
        transformationBubble = GameObject.Find("Transformation Bubble");
    }

    void Update() {
        InputHandler();
        if (TransformationHandler()) {
            smoke.SetActive(true);
            smokeAnimator.Play("Smoke");
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

        switch(transformation) {
            case Transformation.TERRY:
                if (lastY < 0 && movement.magnitude > 0) animator.Play(runDirections[0]);
                else if (lastY > 0 && movement.magnitude > 0) animator.Play(runDirections[1]);
                else if (lastY < 0) animator.Play(staticDirections[0]);
                else if (lastY > 0) animator.Play(staticDirections[1]);
                break;
            case Transformation.FROG:
                if (lastY > 0) animator.Play(staticFrogDirections[1]);
                else if (lastY < 0) animator.Play(staticFrogDirections[0]);
                break;
            case Transformation.BULLDOZER:
                if (lastY > 0) animator.Play(staticBulldozerDirections[1]);
                else if (lastY < 0) animator.Play(staticBulldozerDirections[0]);
                break;
        }
        if (movement.x != 0) lastX = movement.x;
        if (movement.y != 0) lastY = movement.y;
    }

    void InputHandler() {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        movement = new Vector2(horizontal, vertical);
        movement = Vector2.ClampMagnitude(movement, 1);

        // set sprite according to movement direction
        if (movement.x > 0) TerrySprite.flipX = true;
        else if (movement.x < 0) TerrySprite.flipX = false;

        if (transformation == 0) {
            if (movement.y > 0) TerrySprite.sprite = backSprite;
            else if (movement.y < 0) TerrySprite.sprite = frontSprite;
        }

        if (Input.GetKeyDown(KeyCode.Space) && transformation == Transformation.FROG) {
            jump = true;
        }
    }

    bool TransformationHandler() {
        if (Input.GetKeyDown(KeyCode.T)) {
            if (transformationBubble.activeSelf) transformationBubble.SetActive(false);
            else transformationBubble.SetActive(true);
        }

        if (transformation != Transformation.FROG && Input.GetKeyDown(KeyCode.F)) {
            transformation = Transformation.FROG;
            TerrySprite.sprite = frogSprite;
        } else if (transformation != Transformation.BULLDOZER && Input.GetKeyDown(KeyCode.B)) {
            transformation = Transformation.BULLDOZER;
            TerrySprite.sprite = bulldozerSprite;
        } else if (transformation != Transformation.TERRY && Input.GetKeyDown(KeyCode.Escape)) {
            transformation = Transformation.TERRY;
            TerrySprite.sprite = frontSprite;
        } else return false;

        return true;
    }
}
