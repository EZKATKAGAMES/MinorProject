using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class HazardEnemy : MonoBehaviour
{

    GameObject target;
    BoxCollider2D los;
    Rigidbody2D rigi;
    GameObject player;
    Padlock cageStatus;
    BoxCollider2D checkForTarget;
    public bool free;
    public bool attackEntitiy;

    // Use this for initialization
    void Start()
    {
        checkForTarget = GameObject.Find("detectArea").GetComponent<BoxCollider2D>();
        cageStatus = GameObject.Find("Padlock").GetComponent<Padlock>();
        rigi = GetComponent<Rigidbody2D>();
        los = GetComponentInChildren<BoxCollider2D>();
        player = GameObject.Find("MainCat");
        
    }

    // Update is called once per frame
    void Update()
    {
        free = !cageStatus.alive;

        // If the hazard is free
        if(cageStatus.alive == false)
        {
            // Perform attack
            if(attackEntitiy == true)
            {
                // Play animation

            }
        }
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Kitchen")
        {
            SceneManager.LoadScene("1");
        }
        
        if (col.tag == "LivingRoom")
        {
            SceneManager.LoadScene("0");
        }
    }
}
