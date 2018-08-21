using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class HazardEnemy : MonoBehaviour
{

    GameObject target;
    BoxCollider2D los;
    Rigidbody2D rigi;
    GameObject player;
    Vector2 velocity;

    public bool isFree; // Captured or ? not

    
    void Start()
    {
        rigi = GetComponent<Rigidbody2D>();
        los = GetComponentInChildren<BoxCollider2D>();
        player = GameObject.Find("MainCat");

    }

    
    void Update()
    {
        if(isFree == true)
        {
            rigi.MovePosition(target.transform.position * 2 * Time.deltaTime);
        }
    }
}
