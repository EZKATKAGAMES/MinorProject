using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorizontalVerticalVelocity : MonoBehaviour
{
    // The purpose of this script is to controll values which are used as a multiplier to control our velocity.
    // TODO: Make it better.

    public bool inputRecieved;
    public float horizontalVelocity;
    float hT = 0.2f;
    public float verticalVelocity;
    float vT = 0.2f;
    public float hVelocityPositiveLimit = 1; // By defualt we cannot bypass this multiplier for calculating our speed.
    public float vVelocityPositiveLimit = 1;
    public bool unCapVelocity;

    private void Update()
    {
        MoveMoveRightMoveLeft();
        VelocityCap();

        if (horizontalVelocity == 0 && verticalVelocity == 0)
        {
            inputRecieved = false;
        }
        else inputRecieved = true;


    }

    void VelocityCap()
    {
        if (unCapVelocity == false)
        {
            if (horizontalVelocity > hVelocityPositiveLimit) // Precausion to stop velocity going outside bounds.
            {
                horizontalVelocity = hVelocityPositiveLimit;
                return;
            }


            if (horizontalVelocity < 0 && Input.GetKeyDown(GameManager.GM.MoveRight))
            {
                horizontalVelocity = 0;
            }

            if (horizontalVelocity > 0 && Input.GetKeyDown(GameManager.GM.MoveLeft))
            {
                horizontalVelocity = 0;
            }

            if (verticalVelocity > vVelocityPositiveLimit)
            {
                verticalVelocity = vVelocityPositiveLimit;
                return;
            }

            if (verticalVelocity < 0 && Input.GetKeyDown(GameManager.GM.MoveRight))
            {
                verticalVelocity = 0;
            }

            if (verticalVelocity > 0 && Input.GetKeyDown(GameManager.GM.MoveLeft))
            {
                verticalVelocity = 0;
            }

        }
    }

    void MoveMoveRightMoveLeft()
    {
        #region Horizontal

        if (Input.GetKey(GameManager.GM.MoveRight))
        {
            //horizontalVelocity = 1f;
            horizontalVelocity = 1f;
        }
        else if (Input.GetKey(GameManager.GM.MoveLeft))
        {
            horizontalVelocity = -1f;
        }
        else // Reset to zero when no input is recieved.
        {
            if (horizontalVelocity == 0)
                return; // dont run the code if we are stationary!
            horizontalVelocity = Mathf.Lerp(horizontalVelocity, 0, 1f);
        }
        #endregion
        // Clamp velocity to a reasonable limit.
        horizontalVelocity = Mathf.Clamp(horizontalVelocity, -1, 1);
    }

    
}