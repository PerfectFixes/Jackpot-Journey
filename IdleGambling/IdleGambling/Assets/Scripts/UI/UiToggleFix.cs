using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiToggleFix : MonoBehaviour
{
    [Header("SFX")]
    [Tooltip("The SFX for pressing the buttons")]
    [SerializeField] private AudioSource toggleSFX;

    [SerializeField] private Image spriteColor;
    [SerializeField] private Toggle isToggleOn;
    [SerializeField] private GameObject sfxControl;
    [SerializeField] private GameObject musicControl;
    [SerializeField] private AudioSource musicAudioSource;

    private void Awake()
    {
        //Setts the currect game object (On/Off)
        if (isToggleOn.isOn)
        {
            if (toggleSFX.isActiveAndEnabled)
            {
                toggleSFX.Play();
            }
            spriteColor.color = new Color(255, 255, 255, 0);
        }
        else
        {
            if (toggleSFX.isActiveAndEnabled)
            {
                toggleSFX.Play();
            }
            spriteColor.color = new Color(255, 255, 255, 255);
        }
    }
    public void ToggleIsOn(string whatIsTheToggler)
    {
        //Checks what is the toggler
        if(whatIsTheToggler == "Music")
        {
            //Sets the correct game object
            if (isToggleOn.isOn)
            {
                //Player SFX if can
                if (toggleSFX.isActiveAndEnabled)
                {
                    toggleSFX.Play();
                }
                //Save the state for next load
                PlayerPrefs.SetString("MusicToggleState", "True");

                //Hide the game object thats in the background
                spriteColor.color = new Color(255, 255, 255, 0);
            }
            else
            {
                //Plays the SFX
                if (toggleSFX.isActiveAndEnabled)
                {
                    toggleSFX.Play();
                }
                //Save the state for next load
                PlayerPrefs.SetString("MusicToggleState", "False");

                //Display the game object thats in the background
                spriteColor.color = new Color(255, 255, 255, 255);
            }
        }

        if(whatIsTheToggler == "SFX")
        {
            if (isToggleOn.isOn)
            {
                //Plays the SFX
                if (toggleSFX.isActiveAndEnabled)
                {
                    toggleSFX.Play();
                }
                //Save the state for next load
                PlayerPrefs.SetString("SFXToggleState","True");

                //Display and activate the game object thats in the background
                sfxControl.SetActive(true);
                spriteColor.color = new Color(255, 255, 255, 0);
            }
            else
            {
                //Plays the SFX
                if (toggleSFX.isActiveAndEnabled)
                {
                    toggleSFX.Play();
                }
                //Save the state for next load
                PlayerPrefs.SetString("SFXToggleState", "False");

                //Hide and deactivate the game object thats in the background
                sfxControl.SetActive(false);    
                spriteColor.color = new Color(255, 255, 255, 255);
            }
        }

        if(whatIsTheToggler == "AutoGamble")
        {
            if (isToggleOn.isOn)
            {
                //Plays the SFX
                if (toggleSFX.isActiveAndEnabled)
                {
                    toggleSFX.Play();
                }
                //Hide the game object thats in the background
                spriteColor.color = new Color(255, 255, 255, 0);
            }
            else
            {
                //Plays the SFX
                if (toggleSFX.isActiveAndEnabled)
                {
                    toggleSFX.Play();
                }
                //Display the game object thats in the background
                spriteColor.color = new Color(255, 255, 255, 255);
            }
        }
       
        
    }
}
