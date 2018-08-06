using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(Player))]
public class UserControl : MonoBehaviour
{
    private Player character;
    private HorizontalVerticalVelocity axes;

    // Use this for initialization
    void Awake()
    {
        character = GetComponent<Player>();
        axes = GetComponent<HorizontalVerticalVelocity>();
    }

    // Update is called once per frame
    void Update()
    {
        #region Apply Movement
        // Read inputs
        float horizontalMovement = axes.horizontalVelocity;

        //Pass all parameters to the character move
        character.Move(horizontalMovement);
        #endregion

        #region Leap & jab & qtmode & furfire

        if (Input.GetKey(GameManager.GM.Leap)) // Mouse 1 | 
        {
            character.LeapSetup();
        }

        if (Input.GetKeyUp(GameManager.GM.Leap))
        {
            character.Leap();
            
        }

        


        if (Input.GetKeyDown(GameManager.GM.Jab)) // Mouse 0
        {
            character.Jab();
        }

        if (Input.GetKeyDown(GameManager.GM.Cute)) // Space
        {
            character.CuteMode();
        }

        #endregion

    }

    
}
