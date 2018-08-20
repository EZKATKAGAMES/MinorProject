using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Pathfinding;

public class HazardEnemy : MonoBehaviour
{

    GameObject target;
    BoxCollider2D los;
    Rigidbody2D rigi;
    GameObject player;

    // Use this for initialization
    void Start()
    {
        rigi = GetComponent<Rigidbody2D>();
        los = GetComponentInChildren<BoxCollider2D>();
        player = GameObject.Find("MainCat");
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        rigi.AddForce(new Vector2(1, 0));
    }
}
