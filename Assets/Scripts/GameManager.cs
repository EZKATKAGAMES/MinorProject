using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager GM = null;
    public bool levelCompleted { get; set; }
    public float escapeTime;


    public enum LayerCodes { Default0, TransparentFX1,
    IgnoreRaycast2, NULL3, Water4, UI5, NULL6, NULL7,
        Walkable8, Climb9, Hazard10}

    

    public KeyCode Leap { get; set; }
    public KeyCode Jab { get; set; }
    public KeyCode Furball { get; set; }
    public KeyCode Cute { get; set; }
    public KeyCode MoveRight { get; set; }
    public KeyCode MoveLeft { get; set; }
    public KeyCode ClimbUp { get; set; }
    public KeyCode ClimbDown { get; set; }
    public  MouseInformation mouse;
    public struct MouseInformation
    {
        public Vector2 mousePosition2D;
        public Vector2 mouseDirection;
    }
    public GameObject PlayerReference;
    public Vector2 lastClickPosition;

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
        ClimbUp = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Up", "W"));
        ClimbDown = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Down", "S"));
        // Mouse Actions
        Leap = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Leap", "Mouse1"));
        Jab = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Jab", "Mouse0"));

    }

        // Use this for initialization
    void Start()
    {
        levelCompleted = false;
        PlayerReference = GameObject.Find("MainCat");
        
    }
    
    

    // Update is called once per frame
    void Update()
    {
        if(levelCompleted == true)
        {
            escapeTime -= Time.deltaTime;
        }

        mouse.mousePosition2D = Input.mousePosition;
        mouse.mousePosition2D = Camera.main.ScreenToWorldPoint(mouse.mousePosition2D);
        mouse.mouseDirection = new Vector2(mouse.mousePosition2D.x - PlayerReference.transform.position.x,
            mouse.mousePosition2D.y - PlayerReference.transform.position.y);

        // All input calls where we may need the direction from that time.
        if (Input.GetMouseButtonDown(0) || Input.GetKeyUp(GameManager.GM.Furball))
        {
            lastClickPosition = mouse.mouseDirection;
        }
    }

    
}
