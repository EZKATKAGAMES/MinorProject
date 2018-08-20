using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineofSight : MonoBehaviour
{

    // Create a cone for vision in the direction the human is facing.
    // This cone is restricted to a radius.
    // Scan this cone for the player.

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Vector2 point1 = new Vector2(-6.17f, -1.69f);
        Vector2 point2 = new Vector2(-6.17f, -7.69f);
        float angle = Vector2.Angle(point1, point2);
        Vector2.Angle(point1, point2);
        Debug.Log(angle);
        Debug.DrawLine(transform.position, point1);
        Debug.DrawLine(transform.position, point2);


    }
}
