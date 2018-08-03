using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager GM = null;
    public bool levelCompleted { get; set; }
    public float escapeTime;

    private void Awake()
    {

        if (GM == null)
        {
            GM = this;
            
            
        }
        else if (GM != this)
        {
            Destroy(gameObject);
        }

        levelCompleted = false;

    }

        // Use this for initialization
    void Start()
    {
        levelCompleted = false;
    }
    
    

    // Update is called once per frame
    void Update()
    {
        if(levelCompleted == true)
        {
            escapeTime -= Time.deltaTime;
        }
    }
}
