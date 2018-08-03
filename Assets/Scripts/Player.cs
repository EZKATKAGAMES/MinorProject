using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header ("Player Settings")]
    public float maxSpeed = 10f; // Fastest speed the player can travel
    public bool airControl = false; // Allow steering while in air
    public LayerMask whatIsGround; //A layer mask to indicate ground

    private bool facingRight = true; // Which way is the player facing?
    private Transform groundCheck;
    private float groundRadius = 0.2f;
    private bool grounded = false; // Checking if we are grounded
    private Transform ceilingCheck;
    private float ceilingRadius = 0.1f;
    private Animator anim;
    private Rigidbody2D rigid;

    // Use this for initialization
    void Awake()
    {
        // Set up all our references
        groundCheck = transform.Find("GroundCheck");
        ceilingCheck = transform.Find("CeilingCheck");
        anim = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // Performing ground check (using Physics2D)
        grounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, whatIsGround);

        //anim.SetBool("Ground", grounded);
        //anim.SetFloat("vSpeed", rigid.velocity.y);
    }

    void Flip()
    {
        // Switch the way the player is facing
        facingRight = !facingRight;

        // Invert the scale of the player on X
        Vector3 scale = transform.localScale;
        scale.x *= -1; // Inverts X
        transform.localScale = scale;
    }

    public void Move(float move)
    {
        //Only control player if grounded or airControl is on
        if (grounded || airControl)
        {
            //anim.SetFloat("Speed", Mathf.Abs(move));

            // Move the character
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
}
