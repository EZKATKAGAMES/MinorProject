using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatController : MonoBehaviour
{
    public bool leap;
    public bool grounded;
    public bool smack;
    public Rigidbody2D myRB;
    public GameObject arm;
    
    Vector3 p1;
    Vector3 p2;

    Vector3 input;
    public float moveSpeed;
    public float timeToJumpApex;
    public float maxJumpHeight;
    public float multiplier;
    float gravity;
    float jumpVelocity;
    


    private void Awake()
    {
        myRB = gameObject.GetComponent<Rigidbody2D>();
        arm = GameObject.Find("arm");
       
    }

    // Use this for initialization
    void Start()
    {
        gravity = (2 * maxJumpHeight / Mathf.Pow(timeToJumpApex, 2)); // Set gravity;
        jumpVelocity = Mathf.Abs(gravity) * timeToJumpApex; // Set jumpVelocity

       
    }

    // Update is called once per frame
    void Update()
    {

        Vector3 mousePos = Input.mousePosition;
        mousePos = Camera.main.ScreenToWorldPoint(mousePos);

        Vector2 direction = new Vector2(mousePos.x - transform.position.x, mousePos.y - transform.position.y);

        

        if (Input.GetMouseButtonDown(1))
        {
            leap = true;
        }

        if (Input.GetMouseButton(0))
        {
            smack = true;
        }

        if (Input.GetMouseButtonUp(0))
        {
            smack = false;
            
        }
        

        

        float v = Input.GetAxis("Vertical") * 2;
        float h = Input.GetAxis("Horizontal") * 2;

        

        if (grounded)
        {
            input = new Vector3(h, 0, v);
            
        }
        else input = new Vector3(h, gravity * -Time.deltaTime * multiplier, v); // Movement stored into vector

        if(!leap)
        myRB.velocity = input * moveSpeed;
        //myRB.AddForce(input * moveSpeed, ForceMode2D.Force); // Apply the force to our rigidbody


        if (leap)
        {
            StartCoroutine(leapTimer());

            if (Input.GetMouseButtonUp(1))
            {
                grounded = false;
                myRB.AddForce(direction * maxJumpHeight, ForceMode2D.Impulse);
                
            }
        }

    }

    IEnumerator leapTimer()
    {
        yield return new WaitForSeconds(2);
        leap = false;
    }


    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.activeSelf == true)
        {
            grounded = true;
        }

    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        grounded = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (GameManager.GM.levelCompleted)
        {
            Debug.Log("You escaped");
        }
    }

}
