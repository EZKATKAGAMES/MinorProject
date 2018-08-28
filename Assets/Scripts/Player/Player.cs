﻿using System.Collections;
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
    int climbObject = 9; // You may climb objects on this layer.
    public bool airControl = false; // Allow steering while in air
    public bool grounded = false; // Checking if we are grounded
    public bool climb;
    public bool climbing;

    // indicate what actions the player is doing
    public bool leaping;
    public bool leaped;
    public bool cuteMode;
    public bool furBall;

    GameObject furballProjectile;

    private bool facingRight = true; // Which way is the player facing?
    private float groundRadius = 0.02f;
    private float ceilingRadius = 0.1f;
    private Vector2 initialPosition; // Position when leap is initiated.

    // References
    private Vector2 leapDirection; // Direction our velocity will change to.
    public Vector2 direction; // The Live direction of the mouse.
    public Vector2 furDirection; // recorded direction the furball will face.
    public Transform cameraPeek;
    public bool climbMovement;
    public float furBallStrength =5f;

    public bool usedFurball;
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
        furballProjectile = Resources.Load("Prefabs/Cat/Furball") as GameObject;
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

        if (!climb)
        {
            airControl = !grounded;
        }
        else airControl = false;

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
        float airDir = 0;

        //Only control player if grounded or airControl is on
        if (grounded || airControl)
        {
            //anim.SetFloat("Speed", Mathf.Abs(move));

            // Velocity when grounded.
            if(grounded && !leaping && !climb)
            rigid.velocity = new Vector2(move * maxSpeed, rigid.velocity.y);

            // Velocity when in air.
            if (airControl && !leaping && !climb)
            rigid.velocity = new Vector2(move * maxSpeed, rigid.velocity.y);

            if(climb && climbMovement)
            {
                rigid.velocity = new Vector2(move * maxSpeed / 3 , rigid.velocity.y);
            }

            // If the input is moving player right
            if (move > 0 && !facingRight)
            {
                Flip();
                airDir = 1;
            }
            else if (move < 0 && facingRight)
            {
                Flip();
                airDir = -1;
            }
        }

        
    }

    public void LeapSetup()
    {
        leaping = true;
        // Stop movement;
        rigid.velocity = Vector2.zero;
        // Record initial position.
        initialPosition = transform.position;

        // Increase leap str
        if (Input.GetKey(GameManager.GM.Leap))
        {
            leapStrength += Time.deltaTime * 10;
            if(leapStrength >= 20.5f)
            {
                leapStrength = 20.5f;
            }
        }


        // Draw predicted trajectory


        DrawTrajectory.Plot(rigid, transform.position, rigid.velocity, 30);

        

    }

    public void Leap()
    {
        leapDirection = direction;

        // Play leap animation


        // Leap into the air
        #region Leap Velocity
        Debug.Log("jumping");
        rigid.AddForce(leapDirection.normalized * leapStrength, ForceMode2D.Impulse);
        #endregion

        // return leap str
        leapStrength = 5;

        
        // Play land animation

    }


   

    public void Jab()
    {
        // Play animation

        // Short jab to interact with objects.

        // Activate Collider
        StartCoroutine(JabCD());
        // Deactivate Collider
    }

    IEnumerator JabCD()
    {
        //arm.enabled = true;
        yield return new WaitForSeconds(0.2f);
        //arm.enabled = false;
    }

    public void FurballCharge()
    {
        
        // Stop player movement
        rigid.velocity = Vector2.zero;
        // record direction
        furDirection = direction;
        
        // Charge the strength modifier.
        furBallStrength += Time.deltaTime * 5;

        if(furBallStrength >= 25f)
        {
            furBallStrength = 25;
        }
        

    }

    public void FurballRelease()
    {
        Instantiate(furballProjectile, transform.position, Quaternion.identity);
        StartCoroutine(FurballCD());
        
    }

    IEnumerator FurballCD()
    {
        usedFurball = true;
        yield return new WaitForSeconds(2);
        
        usedFurball = false;
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


    // Exit leap
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // If we are leaping and collide with someting, cancel the leap.
        if(leaping && collision.enabled)
        {
            leaping = false;
        }
    }

    // When entering a climbale object
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // When we touch this object, we must currently be leaping to enter climb
        if (collision.gameObject.layer == climbObject && leaping == true)
        {
            climb = true;

            // Stop gravity
            rigid.gravityScale = 0;
            // Stop cat (ignore previous velocity)
            if(GameManager.HVvel.inputRecieved == false)
            {
                rigid.velocity = Vector2.zero;
                climbMovement = false;
            }
            else if (GameManager.HVvel.inputRecieved)
            {
                Debug.Log("works");
                climbMovement = true;
            }
                
            
        }  
    }

    // Exiting a climbable trigger.
    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.layer == climbObject)
        {
            climb = false;
            // Return back to normal gravity.
            rigid.gravityScale = 3;
        }
        
    }

}
