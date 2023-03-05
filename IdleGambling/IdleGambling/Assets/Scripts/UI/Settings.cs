using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class Settings : MonoBehaviour
{
    private bool tutorialAutoClose;
    [SerializeField] private DialogueManager dialogueManager;

    [SerializeField] private Animator sceneLoader;

    [Header("SFX")]
    [Tooltip("The SFX for pressing the buttons")]
    [SerializeField] private AudioSource settingPressSFX;

    [Header("GameObjects")]

    [Tooltip("The Settings game object")]
    [SerializeField] private GameObject settingsGameObject;

    [Tooltip("The SFX Control game object")]
    [SerializeField] private GameObject sfxControl;

    [Tooltip("The Music Control game object")]
    [SerializeField] private GameObject musicControl;

    [SerializeField] private GameObject tutorialSettingsPointer;




    [Tooltip("The Daily Login Bonus game object")]
    [SerializeField] private GameObject dailyLoginBonus;

    [Header("Buttons")]

    [Tooltip("The button to activate sleep mode")]
    [SerializeField] private Button sleepModeButton;



    [Header("Togglers")]

    [Tooltip("The toggle button of the auto gambling mode in the settings")]
    [SerializeField] private Toggle autoGambleToggle;



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

    [Tooltip("The text of Auto Gamble")]
    [SerializeField] private TMP_Text streakRewardText;

    [Tooltip("The text under the Auto Gamble")]
    [SerializeField] private TMP_Text streakRewardUnlockLevelText;

    private void Awake()
    {
        tutorialAutoClose = true;
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
        if(prestigeLevel >= 8)
        {
            dailyLoginBonus.SetActive(true);

            if ((PlayerPrefs.GetInt("LoginStreak") >= 2) && (PlayerPrefs.GetInt("LoginStreak") <= 9))
            {
                streakRewardText.text = "Streak Reward: 2X";
                PlayerPrefs.SetInt("StreakReward", 2);
            }
            else if ((PlayerPrefs.GetInt("LoginStreak") >= 10) && (PlayerPrefs.GetInt("LoginStreak") <= 29))
            {
                streakRewardText.text = "Streak Reward: 3X";
                PlayerPrefs.SetInt("StreakReward", 3);
            }
            else if ((PlayerPrefs.GetInt("LoginStreak") >= 30) && (PlayerPrefs.GetInt("LoginStreak") <= 59))
            {
                streakRewardText.text = "Streak Reward: 5X";
                PlayerPrefs.SetInt("StreakReward", 5);
            }
            else if (PlayerPrefs.GetInt("LoginStreak") >= 60)
            {
                streakRewardText.text = "Streak Reward: 10X";
                PlayerPrefs.SetInt("StreakReward", 10);
            }
            if (PlayerPrefs.GetInt("LoginStreak") == 1 || PlayerPrefs.GetInt("LoginStreak") == 0)
            {
                streakRewardUnlockLevelText.text = "Your login streak is: 1 day";

                streakRewardText.text = "Streak Reward: 1X";
            }
            else
            {
                streakRewardUnlockLevelText.text = "Your login streak is: " + PlayerPrefs.GetInt("LoginStreak") + " days";
            }
        }
        else
        {
            //Hooks the player to make him want to know what is hiding beind level 5
            streakRewardText.text = "???? ????";
        }
    }

    public void OpenOrCloseSettingsTab(string settingsState)
    {
        //Open the settings
        if(settingsState == "Open")
        {
            //Play SFX if the player didnt disable the SFX
            if (settingPressSFX.isActiveAndEnabled)
            {
                settingPressSFX.Play();
            }
            //Open the settings
            settingsGameObject.SetActive(true);        
        }
        else
        {
            //Play SFX if the player didnt disable the SFX
            if (settingPressSFX.isActiveAndEnabled)
            {
                settingPressSFX.Play();
            }
            //Close the settings
            settingsGameObject.SetActive(false);      
        }
    }
    public void TutorialOpenOrCloseSettingsTab(string settingsState)
    {
        //Play SFX if the player didnt disable the SFX
        if (settingsState == "Open")
        {
            tutorialSettingsPointer.SetActive(false);
            //Play SFX if the player didnt disable the SFX
            if (settingPressSFX.isActiveAndEnabled)
            {
                settingPressSFX.Play();
            }
            //Open the settings
            settingsGameObject.SetActive(true);
            
            //Close automatically the settings for the first time after a few seconds
            if (tutorialAutoClose)
            {
                StartCoroutine(TutorialAutoCloseSetting());
            }
            
        }
        else
        {
            //When closing the settings starts the new dialogue
            if (tutorialAutoClose)
            {
                dialogueManager.StartDialogue(dialogueManager.dialogueTrigger.dialogue);
            }
            //Disable the automatic close
            StopAllCoroutines();
            tutorialAutoClose = false;

            //Play SFX if the player didnt disable the SFX
            if (settingPressSFX.isActiveAndEnabled)
            {
                settingPressSFX.Play();
            }
            //Close the settings
            settingsGameObject.SetActive(false);
        }
    }
    //Closing the settings automatically
    IEnumerator TutorialAutoCloseSetting()
    {     
        yield return new WaitForSeconds(25);
        TutorialOpenOrCloseSettingsTab("Close");
        tutorialAutoClose = false;
    }
    //Transfer to sleep mode scene
    public void SleepMode()
    {
        StartCoroutine(SceneTransaction());
    }
    IEnumerator SceneTransaction()
    {
        sceneLoader.SetTrigger("Load_Scene");
        yield return new WaitForSeconds(1.25f);
        SceneManager.LoadScene("AFK_Scene");
    }
    //Reset the game
    public void ResetPlayerPrefs()
    {
        PlayerPrefs.DeleteAll();
        SceneManager.LoadScene(0);
    }
    
}
