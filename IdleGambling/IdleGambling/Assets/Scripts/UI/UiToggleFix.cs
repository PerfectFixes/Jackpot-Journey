using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiToggleFix : MonoBehaviour
{
    //delte in build
    private int resetGame;

    private bool musicState;

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

        resetGame = 0;
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
    private void Update()
    {
 
    }
    public void ToggleIsOn(string whatIsTheToggler)
    {
        if(whatIsTheToggler == "Music")
        {
            // ********************** DELETE ME BEFORE FINAL BUILD **************************
            resetGame++;
            if (resetGame >= 10)
            {
                PlayerPrefs.DeleteAll();
            }
            // ********************** DELETE ME BEFORE FINAL BUILD **************************

            if (isToggleOn.isOn)
            {

                if (toggleSFX.isActiveAndEnabled)
                {
                    toggleSFX.Play();
                }
                PlayerPrefs.SetString("MusicToggleState", "True");

                spriteColor.color = new Color(255, 255, 255, 0);
                if (this.gameObject.activeInHierarchy)
                {
                    //StartCoroutine(musicLogic.SelectRandomSong());
                }

            }
            else
            {
                if (toggleSFX.isActiveAndEnabled)
                {
                    toggleSFX.Play();
                }
                PlayerPrefs.SetString("MusicToggleState", "False");
                //StartCoroutine(musicLogic.FadeOut());
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
        if(whatIsTheToggler == "AutoGamble")
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
        else
        {

            if (isToggleOn.isOn)
            {

                if (toggleSFX.isActiveAndEnabled)
                {
                    toggleSFX.Play();
                }
                PlayerPrefs.SetString("MusicToggleState", "True");
                spriteColor.color = new Color(255, 255, 255, 0);
            }
            else
            {
                if (toggleSFX.isActiveAndEnabled)
                {
                    toggleSFX.Play();
                }
                PlayerPrefs.SetString("MusicToggleState", "False");
                spriteColor.color = new Color(255, 255, 255, 255);
            }
        }
        
    }
}
