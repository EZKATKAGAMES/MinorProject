using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GUISkin mainMenuSkin;
    public GUIStyle muteButton, playButton, optionsButton, exitButton;
    public Texture2D muteTex, unmuteTex;
    public bool showOP;
    public bool mute;
    public float audioSlider;
    public float dirSlider;
    public float unmuteVolume;
    public AudioSource audi;
    public Light dirLight;

    void Start()
    {
        // Added the feature to mute the volume inside the game (used all NoCopyrightSounds music on youtube)
        audi = GameObject.Find("Audio Source").GetComponent <AudioSource>();
        dirLight = GameObject.Find("Directional Light").GetComponent<Light>();

        // Lighting and the Volume for the game
        if (PlayerPrefs.HasKey("Volume"))
        {
            dirLight.intensity = PlayerPrefs.GetFloat("Directional Light");
            audi.volume = PlayerPrefs.GetFloat("Volume");
            if (PlayerPrefs.GetInt("mute") == 0)
            {
                // Muting the volume of the player checks the mute button
                mute = true;
                unmuteVolume = PlayerPrefs.GetFloat("Volume");
                audi.volume = 0;
                muteButton.normal.background = muteTex;
            }
            else
            {
                //Unmuting the volume if the player checks the unmute button
                mute = false;
                audioSlider = PlayerPrefs.GetFloat("Volume");
                muteButton.normal.background = unmuteTex;
            }
        }
            // Sliders for the Options menu to change the value of the certain settings
            audioSlider = audi.volume;
            dirSlider = dirLight.intensity;
    }

    // Update is called once per frame
    void Update()
    {
        // Changes the volume of game to match the volume that the user set
        if (audi.volume != audioSlider)
        {
            audi.volume = audioSlider;
        }

        // Changes the directional light of game to match the directional light that the user set
        if (dirLight.intensity != dirSlider)
        {
            dirLight.intensity = dirSlider;
        }
    }

    void OnGUI()
    {
        // Setting the screen resolution for the game
        float scrW = Screen.width / 16;
        float scrH = Screen.height / 9;

        GUI.skin = mainMenuSkin;

        // Setting the values for the buttons inside the main menu
        GUI.Box(new Rect(0f * scrW, 6.4f * scrH, 1.7f * scrW, 0.5f * scrH), "CONTROLS:");
        GUI.Box(new Rect(1.7f * scrW, 6.4f * scrH, 4.2f * scrW, 0.5f * scrH), "WASD to move the camera");
        GUI.Box(new Rect(5.9f * scrW, 6.4f * scrH, 4.2f * scrW, 0.5f * scrH), "Tab to freeze camera");
        GUI.Box(new Rect(10.1f * scrW, 6.4f * scrH, 4.2f * scrW, 0.5f * scrH), "Escape to pause game");
        if (!showOP)
        {
            if (GUI.Button(new Rect (1.7f * scrW, 3.5f * scrH, 4.2f * scrW, 2.5f * scrH), "", playButton))
            {
                SceneManager.LoadScene(1);
            }
            if (GUI.Button(new Rect (5.9f * scrW, 3.5f * scrH, 4.2f * scrW, 2.5f * scrH), "", optionsButton))
            {
                showOP = true;
            }
            if (GUI.Button(new Rect (10.1f * scrW, 3.5f * scrH, 4.2f * scrW, 2.5f * scrH), "", exitButton))
            {
                Application.Quit();
            }
        }

        else // else we are in the options menu
        {
            // Setting the values for the buttons inside the settings menu
            if (!mute)
            {
                audioSlider = GUI.HorizontalSlider(new Rect(5 * scrW, 4.8f * scrH, 6 * scrW, 1 * scrH), audioSlider, 0f, 1f);
            }    
            else
            {
                GUI.HorizontalSlider(new Rect(5 * scrW, 4.8f * scrH, 6 * scrW, 1 * scrH), audioSlider, 0f, 1f);
            }          
            GUI.Label (new Rect (6.5f * scrW, 4.3f * scrH, 3 * scrW, 0.5f * scrH), "Volume!");


            dirSlider = GUI.HorizontalSlider (new Rect(5 * scrW, 5.6f * scrH, 6 * scrW, 0.25f * scrH), dirSlider, 0f, 1f);

            GUI.Label(new Rect(7f * scrW, 5.1f * scrH, 2 * scrW, 0.5f * scrH), "Brightness!");


            if (GUI.Button (new Rect (7 * scrW, 6 * scrH, 2 * scrW, 1 * scrH), ""))
            {
                SaveOptions();
                showOP = false;
            }

            GUI.Label(new Rect(6.75f * scrW, 2.7f * scrH, 2.5f * scrW, 0.4f * scrH), "Mute!");

            if (GUI.Button(new Rect(7.0f * scrW, 3.05f * scrH, 2f * scrW, 1f * scrH), "", muteButton))
            {
                Mute(); 
            }
            GUI.skin = mainMenuSkin;
        }
    }

    // Saves the player's settings
    void SaveOptions()
    {
        PlayerPrefs.SetFloat("Volume", audioSlider);
        PlayerPrefs.SetFloat("Directional Light", dirSlider);
        if (mute)
        {
            PlayerPrefs.SetInt("mute", 0);
        }
        else
        {
            PlayerPrefs.SetInt("mute", 1);
        }
   }

    // Toggling the mute of the in game sounds if the player presses the mute button
    public bool Mute()
    {
        // If the player presses the mute button, it will mute   
        if (mute == true)
        {
            audioSlider = unmuteVolume;
            mute = false;
            muteButton.normal.background = unmuteTex;
            return false;
        }
        else
        {
            // If the player presses the unmute button, it will unmute
            unmuteVolume = audioSlider;
            audioSlider = 0;
            mute = true;
            muteButton.normal.background = muteTex;
            return true;
        }
    }
}
