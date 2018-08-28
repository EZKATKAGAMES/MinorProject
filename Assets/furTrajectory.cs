using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class furTrajectory : MonoBehaviour
{
    Player pRef;
    Rigidbody2D rigi;

    // Use this for initialization
    void Start()
    {
        rigi = GetComponent<Rigidbody2D>();
        pRef = GameObject.Find("MainCat").GetComponent<Player>();
        
        rigi.AddForce(pRef.furDirection.normalized * pRef.furBallStrength, ForceMode2D.Impulse);
    }

    // Update is called once per frame
    void Update()
    {
        
    
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Instantiate stuff
        // If we arent colliding with an interactable layer, delete
        if(collision.otherCollider.gameObject.layer != 8 || collision.otherCollider.gameObject.layer != 9)
        {
            //
            Destroy(this.gameObject);
        }
       
    }
}
