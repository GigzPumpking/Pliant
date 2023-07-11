using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public Rigidbody2D rb; //rigidboy
    public BoxCollider2D bc;
    public SpriteRenderer spriteRenderer;

    [SerializeField] float speed = 5; //speed of the character
    [SerializeField] float jumpForce = 10;
    [SerializeField] float timeElapsed = 0;
    [SerializeField] float landingDis;
    [SerializeField] Vector2 currentPos;
    [SerializeField] Vector2 landingPos;
    [SerializeField] private Vector2 direction; // current direction of player
    [SerializeField] private bool jump = false; // bool for jump state
    [SerializeField] private bool onGround = true; // bool for jump state

    [SerializeField] AnimationCurve curveY; //curve to control animation of jumping

    /// <summary>
    /// several lists of temp placeholder for sprites in different directions 
    /// naming convetions are direction + sprites
    /// n= north
    /// e= east
    /// ne = northeast
    /// ex: southeat = seSprites
    /// </summary>
    ////////////////////////////////////////////////////////////////////////////////
    public List<Sprite> nSprites;
    public List<Sprite> neSprites;
    public List<Sprite> eSprites;
    public List<Sprite> seSprites;
    public List<Sprite> sSprites;
    public List<Sprite> swSprites;
    public List<Sprite> wSprites;
    public List<Sprite> nwSprites;
    ////////////////////////////////////////////////////////////////////////////////


    /// <summary>
    /// Similar naming coventions to the sprites lists except Direction instead of Sprites for direction. Temporary placement
    /// </summary>
    /////////////////////////////////////////////////////////////////////////////////////////////////////////
    private float nDirection;
    private float neDirection;
    private float eDirection;
    private float seDirection;
    private float sDirection;
    private float swDirection;
    private float wDirection;
    private float nwDirection;
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////

    //On wake up get Rigidbody2D and PlayerInput
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        bc = GetComponent<BoxCollider2D>();
        GetComponent<PlayerInput>();
    }

    //change direction based on input for movement
    void FixedUpdate()
    {
        rb.MovePosition(rb.position + direction.normalized * speed * Time.deltaTime);

        // if object is touching ground layer then the object is grounded
        if (bc.IsTouchingLayers(LayerMask.GetMask("Ground")))
            onGround = true;
    }

    /// ///////////////////////////////////////////////////////////////////////////
    // Input Handlers
    //wait for input to change direction according to values inputted
    public void OnMove(InputValue value) => direction = value.Get<Vector2>();

    //when jump input is read jump handler is called
    public void OnJump(InputValue value) => JumpHandler();

    /// ///////////////////////////////////////////////////////////////////////////

    void JumpHandler()
    {
        if (onGround) //if object is grounded make jump action
        {
            currentPos = rb.position;
            landingPos = currentPos + direction.normalized * speed;
            landingDis = Vector2.Distance(landingPos, currentPos);
            jump = true;
        }
        else 
        {
            timeElapsed += Time.fixedDeltaTime * speed / landingDis;
            if (timeElapsed <= 1f)
            {
                currentPos = Vector2.MoveTowards(currentPos, landingPos, Time.fixedDeltaTime * speed);
                rb.MovePosition(new Vector2(currentPos.x, currentPos.y + curveY.Evaluate(timeElapsed)));
            }
            else
                jump = false;
        }
    }

}
