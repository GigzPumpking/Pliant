using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsometricCharacterController : MonoBehaviour
{
    public GameManager gameManager;
    public PlayerColliderScript playerColliderScript;

    // Collision Variables
    private Rigidbody2D rbody;

    // Animation Variables
    private Animator animator;
    private Animator smokeAnimator;
    private SpriteRenderer TerrySprite;
    private Transform sprite;

    enum Direction {
        UP,
        DOWN
    }
    private Direction direction = Direction.DOWN;

    enum JumpDirection {
        UP,
        DOWN,
        LEFT,
        RIGHT,
        NONE
    }
    
    private JumpDirection[] jumpDirection = new JumpDirection[2];

    bool isMoving = false;

    // Jumping and Movement Variables
    [SerializeField] AnimationCurve curveY;
    private Vector3 curvePos;
    [SerializeField] float movementSpeed = 1f;
    Vector2 movement;
    Vector2 currPos;
    Vector2 prevPos;
    Vector2 nextPos;
    Vector2 landPos;
    Vector2 fallPos;
    float fallDist;
    bool fall = false;
    
    private Vector2 jumpStartPos;
    float landDis;
    float timeElapsed = 0f;
    float fallTimeElapsed = 0f;
    bool isGrounded = true;
    bool jump = false;
    float lastX = 0f;
    float lastY = 1f;
    public bool onRamp = false;
    public bool onPlatform = false;

    public static readonly string[] staticDirections = { "Idle Front", "Hurt Idle Front 1", "Hurt Idle Front 2", "Hurt Idle Front 3", "Idle Back"};
    public static readonly string[] staticFrogDirections = { "Idle Front Frog", "Idle Back Frog"};
    public static readonly string[] jumpFrogDirections = { "Jump Front Frog", "Walk Front Frog", "Jump Back Frog", "Walk Back Frog"};
    public static readonly string[] staticBulldozerDirections = { "Idle Front Bulldozer", "Idle Back Bulldozer"};
    public static readonly string[] runDirections = {"Walk Front", "Walk Back"};

    // Transformation Variables
    public Transformation transformation = Transformation.TERRY;
    private Transform smoke;
    private Transform transformationBubble;
    private Transform shadow;

    private void Awake()
    {
        rbody = GetComponent<Rigidbody2D>();
        sprite = transform.Find("Sprite");
        animator = sprite.GetComponent<Animator>();
        TerrySprite = sprite.GetComponent<SpriteRenderer>();
        smoke = transform.Find("Smoke");
        smokeAnimator = smoke.GetComponent<Animator>();
        smoke.gameObject.SetActive(false);
        shadow = transform.Find("Shadow");
        transformationBubble = transform.Find("Transformation Bubble");
    }

    void Update() {
        InputHandler();
        TransformationHandler();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (jump) {
            if (!fall) JumpHandler();
            else FallHandler();
        } else {
            MoveHandler();
            AnimationHandler();
        }

        CollisionHandler();
    }


    void OnDrawGizmos() {
        Gizmos.color = Color.red;
        int numSpheres = 20;
        float radius = 0.1f;
        for (int i = 0; i < numSpheres; i++)
        {
            float t = (1f / (float) numSpheres) * (float)i;
            curvePos = new Vector2(Mathf.Lerp(jumpStartPos.x, landPos.x, t), jumpStartPos.y + curveY.Evaluate(t));
            Gizmos.DrawSphere(curvePos, radius);
        }
    }

    void JumpHandler() {
        if (isGrounded) {
            currPos = rbody.position;
            landPos = currPos + movement.normalized * movementSpeed;

            if (movement.x != 0) {
                if (movement.x > 0) {
                    jumpDirection[1] = JumpDirection.RIGHT;
                } else {
                    jumpDirection[1] = JumpDirection.LEFT;
                }
            } else jumpDirection[1] = JumpDirection.NONE;

            if (movement.y != 0) {
                if (movement.y > 0) {
                    jumpDirection[0] = JumpDirection.UP;
                } else {
                    jumpDirection[0] = JumpDirection.DOWN;
                }
            }
            else jumpDirection[0] = JumpDirection.NONE;

            landDis = Vector2.Distance(currPos, landPos);
            timeElapsed = 0f;
            isGrounded = false;
            isMoving = true;
            if (direction == Direction.DOWN) animator.Play(jumpFrogDirections[0]);
            else animator.Play(jumpFrogDirections[2]);

            Physics2D.IgnoreLayerCollision(6, 7, true);
            Physics2D.IgnoreLayerCollision(6, 13, true);
            Physics2D.IgnoreLayerCollision(6, 14, true);
        } else {
            timeElapsed += Time.deltaTime * movementSpeed / landDis;

            if (timeElapsed <= 0.666f) {
                prevPos = currPos;
                currPos = Vector2.MoveTowards(currPos, landPos, Time.fixedDeltaTime*movementSpeed);
                nextPos = new Vector2(currPos.x, currPos.y + curveY.Evaluate(timeElapsed));
                rbody.MovePosition(nextPos);
                // keep shadow's y position at jumpStartPos.y
                if (landPos.y == jumpStartPos.y) shadow.transform.position = new Vector2(sprite.transform.position.x, jumpStartPos.y);
                else {
                    shadow.transform.position = new Vector2(sprite.transform.position.x, sprite.transform.position.y - curveY.Evaluate(timeElapsed));
                }
            } else {
                Debug.Log("landed");
                // turn on collision between player layer and world layer
                Physics2D.IgnoreLayerCollision(6, 7, false);
                Physics2D.IgnoreLayerCollision(6, 13, false);
                Physics2D.IgnoreLayerCollision(6, 14, false);
                jump = false;
                isGrounded = true;
                isMoving = false;
            }
        }
    }

    void FallHandler() {
        fallTimeElapsed += Time.deltaTime * movementSpeed / fallDist;
        if (fallTimeElapsed <= (1f + fallDist)) {
            currPos = Vector2.MoveTowards(currPos, fallPos, Time.fixedDeltaTime*movementSpeed / fallDist);
            rbody.MovePosition(currPos);
            shadow.transform.position = fallPos;
        } else {
            // turn on collision between player layer and world layer
            Physics2D.IgnoreLayerCollision(6, 7, false);
            jump = false;
            isGrounded = true;
            isMoving = false;
            fall = false;
            fallTimeElapsed = 0f;
        }
    }

    void MoveHandler() {
        switch (transformation) {
            case(Transformation.TERRY):
                shadow.transform.position = new Vector2(sprite.transform.position.x, sprite.transform.position.y - 0.8f);
                break;
            case(Transformation.FROG):
                shadow.transform.position = new Vector2(sprite.transform.position.x, (sprite.transform.position.y - 0.2f));
                break;
            case(Transformation.BULLDOZER):
                shadow.transform.position = new Vector2(sprite.transform.position.x, sprite.transform.position.y - 0.4f);
                break;
        }

        rbody.MovePosition(rbody.position + movement.normalized * movementSpeed * Time.fixedDeltaTime);
        currPos = rbody.position;
        landPos = currPos + movement.normalized * movementSpeed;

	    jumpStartPos = currPos;

        if (lastY < 0) direction = Direction.DOWN;
        else if (lastY > 0) direction = Direction.UP;

        if (movement.x != 0) lastX = movement.x;
        if (movement.y != 0) lastY = movement.y;
    }

    void InputHandler() {
        float horizontal = 0f;
        float vertical = 0f;
        
        if (Input.GetKey(KeyCode.A)) horizontal = -1f;
        else if (Input.GetKey(KeyCode.D)) horizontal = 1f;
        if (Input.GetKey(KeyCode.W)) vertical = 1f;
        else if (Input.GetKey(KeyCode.S)) vertical = -1f;
        
        movement = new Vector2(horizontal, vertical);
        movement = Vector2.ClampMagnitude(movement, 1);

        if (movement.magnitude > 0) isMoving = true;
        else isMoving = false;

        // set sprite according to movement direction
        if (movement.x > 0) TerrySprite.flipX = true;
        else if (movement.x < 0) TerrySprite.flipX = false;

        if (Input.GetKeyDown(KeyCode.Space) && transformation == Transformation.FROG) {
            jump = true;
        }
    }

    void TransformationHandler() {
        if (Input.GetKeyDown(KeyCode.T)) {
            if (!transformationBubble.gameObject.activeSelf) {
                transformationBubble.gameObject.SetActive(true);
            }
        }
    }

    void AnimationHandler() {
        switch(transformation) {
            case Transformation.TERRY:
                if (isMoving) {
                    if (direction == Direction.DOWN) animator.Play(runDirections[0]);
                    else animator.Play(runDirections[1]);
                }
                else {
                    if (direction == Direction.DOWN) {
                        switch(gameManager.GetHealth()) {
                            case 4:
                                animator.Play(staticDirections[0]);
                                break;
                            case 3:
                                animator.Play(staticDirections[1]);
                                break;
                            case 2:
                                animator.Play(staticDirections[2]);
                                break;
                            case 1:
                                animator.Play(staticDirections[3]);
                                break;
                        }
                    }
                    else animator.Play(staticDirections[4]);
                }
                break;
            case Transformation.FROG:
                if (isMoving) {
                    if (direction == Direction.DOWN) {
                        animator.Play(jumpFrogDirections[1]);
                    }
                    else animator.Play(jumpFrogDirections[3]);
                } else {
                    if (direction == Direction.DOWN) {
                        animator.Play(staticFrogDirections[0]);
                    }
                    else animator.Play(staticFrogDirections[1]);
                }
                break;
            case Transformation.BULLDOZER:
                if (direction == Direction.DOWN) animator.Play(staticBulldozerDirections[0]);
                else animator.Play(staticBulldozerDirections[1]);
                break;
        }
    }
    void CollisionHandler() {
        Vector2 lastMovement = new Vector2(lastX, lastY);
        
        RaycastHit2D hit = Physics2D.Raycast(transform.position, -lastMovement, 0.1f);

        Debug.DrawRay(transform.position, -lastMovement, Color.red, 0.1f);

        if (!jump) {
            if (onPlatform) {
                Physics2D.IgnoreLayerCollision(6, 7, true);
            } else {
                Physics2D.IgnoreLayerCollision(6, 7, false);
            }
        }
    }
}
