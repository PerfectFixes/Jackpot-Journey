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

    private void Awake()
    {
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
        if(whatIsTheToggler == "Music")
        {
            if (isToggleOn.isOn)
            {
                if (toggleSFX.isActiveAndEnabled)
                {
                    toggleSFX.Play();
                }
                PlayerPrefs.SetString("MusicToggleState", "True");
                musicControl.SetActive(true);
                spriteColor.color = new Color(255, 255, 255, 0);
            }
            else
            {
                if (toggleSFX.isActiveAndEnabled)
                {
                    toggleSFX.Play();
                }
                PlayerPrefs.SetString("MusicToggleState", "False");
                musicControl.SetActive(false);
                spriteColor.color = new Color(255, 255, 255, 255);
            }
        }
        else if(whatIsTheToggler == "SFX")
        {
            if (isToggleOn.isOn)
            {
                if (toggleSFX.isActiveAndEnabled)
                {
                    toggleSFX.Play();
                }
                PlayerPrefs.SetString("SFXToggleState","True");
                sfxControl.SetActive(true);
                spriteColor.color = new Color(255, 255, 255, 0);
            }
            else
            {
                if (toggleSFX.isActiveAndEnabled)
                {
                    toggleSFX.Play();
                }
                PlayerPrefs.SetString("SFXToggleState", "False");
                sfxControl.SetActive(false);    
                spriteColor.color = new Color(255, 255, 255, 255);
            }
        }
        else
        {
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
        
    }
}
