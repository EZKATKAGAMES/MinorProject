using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(Player))]
public class UserControl : MonoBehaviour
{
    private Player character;

    // Use this for initialization
    void Awake()
    {
        character = GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void FixedUpdate()
    {
        // Read inputs
        float h = Input.GetAxis("Horizontal");

        //Pass all parameters to the character move
        character.Move(h);
    }
}
