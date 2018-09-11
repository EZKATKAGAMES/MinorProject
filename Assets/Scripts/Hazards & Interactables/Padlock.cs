using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Padlock : MonoBehaviour
{
    public bool alive = true;
    public float health = 5f;
    float damage = 1.2f;

    GameObject cageDoor;
    Animator anim;

    void Start()
    {
        alive = true;
        cageDoor = GameObject.Find("Cage Door");
        anim = cageDoor.GetComponent<Animator>();
    }

   
    void Update()
    {
        if(health <= 0)
        {
            alive = false;
            anim.SetBool("alive", false);
            // Animate lock breaking
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 10) // Collisions from this layer
        {
            health -= damage * collision.relativeVelocity.magnitude * 2; // Take damage from objects
        }

        


    }


}
