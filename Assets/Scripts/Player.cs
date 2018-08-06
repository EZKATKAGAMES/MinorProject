using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    #region Variables
    [Header ("Player Settings")]
    public float maxSpeed = 10f; // Fastest speed the player can travel
    public float leapStrength = 1f;
    public float slowMultiplier = 2;

    public LayerMask whatIsGround; //A layer mask to indicate ground
    public LayerMask climbObject = 9; // You may climb objects on this layer.
    public bool airControl = false; // Allow steering while in air
    public bool grounded = false; // Checking if we are grounded

    // indicate what actions the player is doing
    public bool leaping;
    public bool leaped;
    public bool cuteMode;
    public bool furBall;

    private bool facingRight = true; // Which way is the player facing?
    private float groundRadius = 0.02f;
    private float ceilingRadius = 0.1f;
    

    // References
    private Vector2 leapDirection; // Direction our velocity will change to.
    private Vector2 direction; // The Live direction of the mouse.
    private Transform ceilingCheck;
    private Transform groundCheck;
    private Animator anim;
    private Rigidbody2D rigid;
    private SpriteRenderer rend;
    private BoxCollider2D arm;
    #endregion

    // Use this for initialization
    void Awake()
    {
        // Component Reference
        groundCheck = transform.Find("GroundCheck");
        ceilingCheck = transform.Find("CeilingCheck");
        anim = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody2D>();
        rend = GetComponent<SpriteRenderer>();

        arm = Transform.FindObjectOfType<BoxCollider2D>();

    }

    private void Update()
    {

        

        #region Mouse Direction
        // Leap direction is based on the 2D position of cursor in worldspace.
        Vector3 mousePos = Input.mousePosition;
        mousePos = Camera.main.ScreenToWorldPoint(mousePos);
        // Direction of mouse from the gameObject.
        direction = new Vector2(mousePos.x - transform.position.x, mousePos.y - transform.position.y);
        #endregion
    }

    void FixedUpdate()
    {
        // Performing ground check (using Physics2D)
        grounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, whatIsGround);
        airControl = !grounded;

        #region MouseDebug
        // Leap direction is based on the 2D position of cursor in worldspace.
        Vector3 mousePos = Input.mousePosition;
        mousePos = Camera.main.ScreenToWorldPoint(mousePos);
        // Direction of mouse from the gameObject.
        Vector2 direction = new Vector2(mousePos.x - transform.position.x, mousePos.y - transform.position.y);

        Debug.DrawLine(Vector2.zero, direction * 1000);
        #endregion

        
        //anim.SetFloat("vSpeed", rigid.velocity.y);
    }

    private void OnDrawGizmos()
    {
       // Gizmos.DrawWireSphere(groundCheck.transform.position, groundRadius);
    }

    void Flip()
    {
        // Switch the way the player is facing
        facingRight = !facingRight;
        rend.flipX = !facingRight;
    }

    public void Move(float move)
    {
        

        //Only control player if grounded or airControl is on
        if (grounded || airControl)
        {
            //anim.SetFloat("Speed", Mathf.Abs(move));

            // Move the character
            if(grounded && !leaping)
            rigid.velocity = new Vector2(move * maxSpeed, rigid.velocity.y);

            if (airControl && !leaping)
            rigid.velocity = new Vector2(move * maxSpeed, rigid.velocity.y);

            // If the input is moving player right
            if (move > 0 && !facingRight)
            {
                Flip();
            }
            else if (move < 0 && facingRight)
            {
                Flip();
            }
        }
    }

    public void LeapSetup()
    {
        leaping = true;
        // Stop movement;

        // Increase leap str
        if (Input.GetKey(GameManager.GM.Leap))
        {
            leapStrength += Time.deltaTime;
            if(leapStrength >= 3.5f)
            {
                leapStrength = 3.5f;
            }
        }

        rigid.velocity = Vector2.zero;
        // Draw predicted trajectory

    }

    public void Leap()
    {
        leapDirection = direction;

        // Play leap animation


        // Leap into the air
        #region Leap Velocity
        Debug.Log("jumping");
        rigid.AddForce(leapDirection * leapStrength, ForceMode2D.Impulse);
        #endregion

        // return leap str
        leapStrength = 1;

        
        // Play land animation

    }


    public void Jab()
    {
        // Play animation

        // Short jab to interact with objects.

        // Activate Collider

        // Deactivate Collider
    }

    public void Furball()
    {
        // Play animation

        // Become stationary

        // Fire a furball to interact with objects.
    }

    public void CuteMode()
    {
        cuteMode = true;
        StartCoroutine(cute());
        // Play animation

        // Lure human targets towards you.

        // Lower movementspeed & animation speed
        if (cuteMode)
            maxSpeed /= slowMultiplier;
        
       
               
            
        // Restrict functionality of abilities, using them will cancel this mode.
            
    }

    IEnumerator cute()
    {
        yield return new WaitForSeconds(3f);
        maxSpeed *= slowMultiplier;
        cuteMode = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // If we are leaping and collide with someting, cancel the leap.
        if(leaping && collision.enabled)
        {
            leaping = false;
            if(collision.gameObject.layer == climbObject)
            {
                // grapple object.
            }
        }
    }

}
