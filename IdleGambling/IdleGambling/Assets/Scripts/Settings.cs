using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class Settings : MonoBehaviour
{

    [Header("GameObjects")]

    [SerializeField] private GameObject settingsGameObject;


    [Header("Buttons")]

    [Tooltip("The button to activate sleep mode")]
    [SerializeField] private Button sleepModeButton;



    [Header("Togglers")]

    [Tooltip("The toggle button of the auto gambling mode in the settings")]
    [SerializeField] private Toggle autoGambleToggle;

    [Tooltip("The toggle button of enabling/disabling Music in the settings")]
    [SerializeField] private Toggle musicToggle;

    [Tooltip("The toggle button of enabling/disabling SFX in the settings")]
    [SerializeField] private Toggle sfxToggle;

    [Header("Text")]

    [Tooltip("The text of AFK Mode")]
    [SerializeField] private TMP_Text afkModeOutsideText;

    [Tooltip("The text of AFK Mode inside of the button")]
    [SerializeField] private TMP_Text afkModeButtonText;

    [Tooltip("The text under the AFK Mode")]
    [SerializeField] private TMP_Text afkModeUnlockLevelText;

    [Tooltip("The text of Auto Gamble")]
    [SerializeField] private TMP_Text autoGambleOutsideText;

    [Tooltip("The text under the Auto Gamble")]
    [SerializeField] private TMP_Text autoGambleUnlockLevelText;

    private void Awake()
    {
        settingsGameObject.SetActive(false);

        int prestigeLevel = PlayerPrefs.GetInt("PrestigeLevel");

        if (prestigeLevel >= 2)
        {
            //Enables the button to go to the afk scene
            sleepModeButton.interactable = true;

            //Remove the requierment of what needed to make this unlocked
            afkModeUnlockLevelText.enabled = false;     
        }
        else
        {
            //Disable the option of pressing the button until you reach Prestige level 2
            sleepModeButton.interactable = false;

            //Hooks the player to make him want to know what is hiding beind level 2
            afkModeOutsideText.text = "???? ????";

            afkModeButtonText.text = "????";
        }

        if (prestigeLevel >= 5)
        {
            //Enables the button to start auto gambling
            autoGambleToggle.interactable = true;

            //Remove the requierment of what needed to make this unlocked
            autoGambleUnlockLevelText.enabled = false;
        }
        else
        {
            //Disable the option of pressing the button until you reach Prestige level 5
            autoGambleToggle.interactable = false;

            //Hooks the player to make him want to know what is hiding beind level 5
            autoGambleOutsideText.text = "???? ????";
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
