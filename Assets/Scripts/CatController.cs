using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatController : MonoBehaviour
{
    public bool leap;
    public bool leapComplete;
    public bool grounded;
    public bool smack;
    public Rigidbody2D myRB;
    public GameObject arm;
    
    Vector3 p1;
    Vector3 p2;

    Vector3 input;
    float h;
    float v;
    public float moveSpeed;
    public float timeToJumpApex;
    public float maxJumpHeight;
    public float multiplier;
    float gravity;
    float jumpVelocity;
    HorizontalVerticalVelocity axes;
    


    private void Awake()
    {
        myRB = gameObject.GetComponent<Rigidbody2D>();
        arm = GameObject.Find("arm");
        axes = GetComponent<HorizontalVerticalVelocity>();

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
        h = axes.horizontalVelocity;
        v = axes.verticalVelocity;

        #region Get Mouse Direction
        // Leap direction is based on the 2D position of cursor in worldspace.
        Vector3 mousePos = Input.mousePosition;
        mousePos = Camera.main.ScreenToWorldPoint(mousePos);
        // Direction of mouse from the gameObject.
        Vector2 direction = new Vector2(mousePos.x - transform.position.x, mousePos.y - transform.position.y);
        #endregion

        #region Activate Leap, Swipe, Furball and Cutemode?
        // On right mouse, prepare to leap
        if (Input.GetMouseButtonDown(1))
        {
            leap = true;
            Debug.Log("jump");
        }
        // On left mouse, jab with your paw.
        if (Input.GetMouseButtonDown(0))
        {
            smack = true;
        }
        // Furball

        // Cutemode
        if(GameManager.GM)

        #endregion


        
       
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
                myRB.AddForce(direction * jumpVelocity, ForceMode2D.Impulse);
                
            }
        }

    }

    IEnumerator leapTimer()
    {

        

        if (leapComplete)
        {
            leap = false;
        }
        yield return new WaitForSeconds(0);
       
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

        // Check if the leap has completed.
        // If we collide with a non Trigger entity.
        if (leapComplete == false && collision.isTrigger == false)
        {
            // end the leap
            leapComplete = true;
        }
    }

}
