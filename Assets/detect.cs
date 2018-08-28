using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class detect : MonoBehaviour
{
    HazardEnemy talking;
    void Start()
    {
        talking = GameObject.Find("hazardanimal").GetComponent<HazardEnemy>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(talking.free == true)
        {
            if (collision.gameObject.layer == 12)
            {
                talking.attackEntitiy = true;
                
                HumanAI changeBehaviour = collision.GetComponent<HumanAI>();
                // Stop running behaviours
                changeBehaviour.isAtStartingPoint = 3;
                
            }
        }
        
    }
}
