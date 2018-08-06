using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager GM = null;
    public bool levelCompleted { get; set; }
    public float escapeTime;


    public KeyCode Leap { get; set; }
    public KeyCode Jab { get; set; }
    public KeyCode Furball { get; set; }
    public KeyCode Cute { get; set; }
    public KeyCode MoveRight { get; set; }
    public KeyCode MoveLeft { get; set; }
    public KeyCode NavigateNext { get; set; }
    public KeyCode NavigatePrevious { get; set; }
    

    private void Awake()
    {
        #region no duplicates
        if (GM == null)
        {
            GM = this;  
        }
        else if (GM != this)
        {
            Destroy(gameObject);
        }
        #endregion

        levelCompleted = false;
        
        // Ability
        Furball = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Furball", "LeftShift"));
        Cute = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("CuteMode", "Space"));
        // Movement
        MoveRight = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Mrkey", "D"));
        MoveLeft = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Mlkey", "A"));
        // Action
        NavigateNext = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Next", "UpArrow"));
        NavigatePrevious = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Prev", "DownArrow"));
        // Mouse Actions
        Leap = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Leap", "Mouse1"));
        Jab = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Jab", "Mouse0"));

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
