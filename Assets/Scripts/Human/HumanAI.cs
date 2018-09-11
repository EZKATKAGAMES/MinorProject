using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanAI : MonoBehaviour
{

    public enum Behaviour {Patrol, CageInteraction, GeneratorInteraction, PlayerSpotted}
    public Behaviour currentState;
    public Rigidbody2D rigi;
    public Vector2 velocity;

    #region Behaviour Variables

    [Header("Patrol")]
    public Vector2 p1, p2, cage, generator;
    bool isCycleComplete;
    bool move;
    public bool arrivedAtP1, arrivedAtP2;
    [SerializeField]
    public int isAtStartingPoint=0; // 0 = p1, 1 = p2
    


    #endregion

    // Use this for initialization
    void Start()
    {
        rigi = GetComponent<Rigidbody2D>();
        currentState = Behaviour.Patrol; // Default state = patrol

        #region Setting up patrol references
        p1 = GameObject.Find("P1").transform.position;
        p2 = GameObject.Find("P2").transform.position;

        cage = GameObject.Find("---Cage---").transform.position;
        generator = GameObject.Find("Generator").transform.position;

        #endregion

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // Update patrol variables


        //Run Patrol function
        if(currentState == Behaviour.Patrol)
        {
            Patrol(Random.Range(1, 5));
        }


        // CageInteraction
        if (currentState == Behaviour.CageInteraction)
        {
            Debug.Log("CAGE");
        }
        // GeneratorInteraction
        if (currentState == Behaviour.GeneratorInteraction)
        {
            Debug.Log("Generator");
        }
        //PlayerSpotted
        if (currentState == Behaviour.PlayerSpotted)
        {

        }



    }

    #region Patrol
    public void Patrol(int cycles) // number of cycles before an interaction occurs
    {
        int cycleCount = 0;
        // When we reach the patrol cycle limit.
        if(cycleCount == cycles)
        {
            
            int rand;
            rand = Random.Range(0, 11);
            // Change behaviour.
            if(rand < 5)
            currentState = Behaviour.CageInteraction;

            if(rand > 5)
                currentState = Behaviour.GeneratorInteraction;
        }

        // If we are at position 1
        if (isAtStartingPoint == 0) 
        {
            // Set velocity;
            velocity = Vector2.right;
            // Begin moving towards position 2
            rigi.MovePosition(rigi.position + velocity * Time.deltaTime * 2);
            // Check if we walk past the point

            if(arrivedAtP2)
            {
                StartCoroutine(waitAtPosition());
            }
            
        }
        // If we are at position 2
        if (isAtStartingPoint == 1) 
        {
            // Set velocity
            velocity = Vector2.right;
            // Begin moving towards position 1
            rigi.MovePosition(rigi.position + velocity * -Time.deltaTime * 2);
            // Check if we walk past the point
            if(arrivedAtP1)
            {
                cycleCount++;
              //  Debug.Log(cycleCount);
                StartCoroutine(waitAtPosition());
            }

        }
    }

    IEnumerator waitAtPosition()
    {
        move = false;

        if (move == false)
            velocity = Vector2.zero;

        // Wait for 3 seconds before starting next movement
        yield return new WaitForSeconds(3);

        move = true;

        yield return new WaitForSeconds(0.3f);
        StopCoroutine(waitAtPosition());
    }

    #endregion


    #region CageInteraction

    void CageInteraction()
    {

    }

    #endregion

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(currentState == Behaviour.Patrol)
        {
            if (collision.gameObject.tag == "P2")
            {
                arrivedAtP2 = true;
                isAtStartingPoint = 0;
                arrivedAtP1 = false;
            }
            else if (collision.gameObject.tag == "P1")
            {
                arrivedAtP1 = true;
                isAtStartingPoint = 1;
                arrivedAtP2 = false;
            }
        }
        
    }
}
