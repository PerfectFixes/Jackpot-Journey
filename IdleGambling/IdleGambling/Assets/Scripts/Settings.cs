using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class Settings : MonoBehaviour
{

    [Header("Settings Related")]

    [SerializeField] private GameObject settingsGameObject;

    [Tooltip("The toggle button of enabling/disabling Music in the settings")]
    [SerializeField] private Toggle musicToggle;

    [Tooltip("The toggle button of enabling/disabling SFX in the settings")]
    [SerializeField] private Toggle sfxToggle;

    [Tooltip("The button to activate sleep mode")]
    [SerializeField] private Button sleepModeButton;

    [Tooltip("The toggle button of the auto gambling mode in the settings")]
    [SerializeField] private Toggle autoGambleToggle;

    

    private void Awake()
    {
        settingsGameObject.SetActive(false);

        int prestigeLevel = PlayerPrefs.GetInt("PrestigeLevel");

        if (prestigeLevel >= 2)
        {
            sleepModeButton.interactable = true;
        }
        else
        {
            sleepModeButton.interactable = false;
        }
        if (prestigeLevel >= 5)
        {

            autoGambleToggle.interactable = true;
        }
        else
        {
            autoGambleToggle.interactable = false;
        }
    }

    private void Update()
    {
    
      
    }
    public void OpenOrCloseSettingsTab(string settingsState)
    {
        if(settingsState == "Open")
        {
            settingsGameObject.SetActive(true);
        }
        else
        {
            settingsGameObject.SetActive(false);
        }
    }

    public void SleepMode()
    {
        SceneManager.LoadScene("AFK_Scene");
    }
    public void ResetPlayerPrefs()
    {
        PlayerPrefs.DeleteAll();
        SceneManager.LoadScene(0);
    }
    
}
