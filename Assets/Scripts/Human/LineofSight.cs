using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineofSight : MonoBehaviour
{

    // Create a cone for vision in the direction the human is facing.
    // This cone is restricted to a radius.
    // Scan this cone for the player.

    Player playerRef;
    Vector2 direction;

    // Use this for initialization
    void Start()
    {
        playerRef = GameObject.Find("MainCat").GetComponent<Player>();

    }

    // Update is called once per frame
    void Update()
    {

        //Calculate Direction between the player and the human.
        direction = playerRef.transform.position - transform.position;
        // Compare the angle of 


    }
}
