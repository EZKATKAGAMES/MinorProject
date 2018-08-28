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

       

        if (character.climb == true)
        {
            character.Move(axes.verticalVelocity);
        } else if (character.climb == false)
        {
            character.Move(axes.horizontalVelocity);
        }

        #endregion

        #region Leap & jab & qtmode & furfire

        if (Input.GetKey(GameManager.GM.Leap) && character.airControl == false) // Rightmouse press
        {
            character.LeapSetup();
        }

        if (Input.GetKeyUp(GameManager.GM.Leap) && character.airControl == false) // Rightmouse release
        {
            character.Leap();
            
        }

        // Begin charging
        if (Input.GetKey(GameManager.GM.Furball))
        {
            if (character.usedFurball == false)
            {
                character.FurballCharge();
            }
             
           
        }

        // On release (unfinished charge)
        if (Input.GetKeyUp(GameManager.GM.Furball))
        {
            if(character.usedFurball == false)
            {
                character.FurballRelease();
                
            }
           
        }

        if (Input.GetKeyDown(GameManager.GM.Jab)) // Leftmouse
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
