using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssassinTarget : MonoBehaviour
{
    public bool alive;
    public float health = 50f;
    float damage = 1.2f;
    bool isLured; // Is this enemy being lured?

    // Use this for initialization
    void Start()
    {
        alive = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (alive == false)
        {
            GameManager.GM.levelCompleted = true;
        }

        if(health <= 0)
        {
            alive = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag != "Player")
        {
            health -= damage * collision.relativeVelocity.magnitude; // Take damage from objects
        }

        
       
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "CuteEffectRadius")
        {
            isLured = true;
        }
    }



    private void OnGUI()
    {
        if (!alive)
        {
            GUI.Label(new Rect(300, 300, 3000, 3000), GameManager.GM.escapeTime.ToString());
        }
    }
}
