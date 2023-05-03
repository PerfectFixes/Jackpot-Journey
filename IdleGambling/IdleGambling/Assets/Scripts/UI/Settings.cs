using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class Settings : MonoBehaviour
{
    private bool tutorialAutoClose;

    private bool settingsCooldown;

    [SerializeField] private DialogueManager dialogueManager;

   

    [Header("SFX")]
    [Tooltip("The SFX for pressing the buttons")]
    [SerializeField] private AudioSource settingPressSFX;

    [SerializeField] private MusicLogic musicLogic;

    [Header("GameObjects")]

    [Tooltip("The Settings game object")]
    [SerializeField] private GameObject settingsGameObject;

    [Tooltip("The SFX Control game object")]
    [SerializeField] private GameObject sfxControl;

    [Tooltip("The Music Control game object")]
    [SerializeField] private GameObject musicControl;

    [SerializeField] private GameObject tutorialSettingsPointer;

    [Header("Animator")]

    [SerializeField] private Animator settingsButtonAnimator;

    [SerializeField] private Animator settingsAnimator;

    [SerializeField] private Animator sceneLoader;



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
        settingsCooldown = false;
        tutorialAutoClose = true;
        settingsGameObject.SetActive(false);

        int prestigeLevel = PlayerPrefs.GetInt("PrestigeLevel");

        if (prestigeLevel >= 2)
        {
            dailyLoginBonus.SetActive(true);

            if ((PlayerPrefs.GetInt("LoginStreak") >= 2) && (PlayerPrefs.GetInt("LoginStreak") <= 9))
            {
                streakRewardText.text = "Streak Reward: 2X";
            }
            else if ((PlayerPrefs.GetInt("LoginStreak") >= 10) && (PlayerPrefs.GetInt("LoginStreak") <= 29))
            {
                streakRewardText.text = "Streak Reward: 3X";
            }
            else if ((PlayerPrefs.GetInt("LoginStreak") >= 30) && (PlayerPrefs.GetInt("LoginStreak") <= 59))
            {
                streakRewardText.text = "Streak Reward: 5X";
            }
            else if (PlayerPrefs.GetInt("LoginStreak") >= 60)
            {
                streakRewardText.text = "Streak Reward: 10X";
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
        if (prestigeLevel >= 4)
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
        if (prestigeLevel >= 7)
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
    IEnumerator SettingsSpamDisable()
    {
        settingsCooldown = true;
        yield return new WaitForSeconds(0.5f);
        settingsCooldown = false;
    }
    public void OpenOrCloseSettingsTab(string settingsState)
    {
        if (!settingsCooldown)
        {
            StartCoroutine(SettingsSpamDisable());
            //Open the settings
            if (settingsState == "Open")
            {

                //Play SFX if the player didnt disable the SFX
                if (settingPressSFX.isActiveAndEnabled)
                {
                    settingPressSFX.Play();
                }

                
                //Open the settings
                settingsGameObject.SetActive(true);
                settingsAnimator.Play("Fade_In_General_Setting");
                settingsButtonAnimator.Play("Setting_Pressing_Animation");


            }
            else
            {
                if (settingsCooldown)
                {
                    settingsAnimator.Play("Fade_Out_General_Setting");
                    //Play SFX if the player didnt disable the SFX
                    if (settingPressSFX.isActiveAndEnabled)
                    {
                        settingPressSFX.Play();
                    }
                    settingsButtonAnimator.Play("Setting_Exit_Animation");
                }
            }
        }
        
    }
    public void TutorialOpenOrCloseSettingsTab(string settingsState)
    {
        if (!settingsCooldown)
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
                settingsAnimator.Play("Fade_In_General_Setting");
                settingsButtonAnimator.Play("Setting_Pressing_Animation");

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
                settingsButtonAnimator.Play("Setting_Exit_Animation");
                settingsAnimator.Play("Fade_Out_General_Setting");
            }

        }
    }
    //Closing the settings automatically
    IEnumerator TutorialAutoCloseSetting()
    {     
        yield return new WaitForSeconds(15);
        TutorialOpenOrCloseSettingsTab("Close");
        tutorialAutoClose = false;
    }
    //Transfer to sleep mode scene
    public void LoadScene(string scene)
    {
        StartCoroutine(SceneLoader(scene));
    }
    IEnumerator SceneLoader(string scene)
    {
        sceneLoader.SetTrigger("Load_Scene");
        StartCoroutine(musicLogic.FadeOut());
        yield return new WaitForSeconds(1.25f);
        if (scene == "AFK")
        {
            SceneManager.LoadScene("AFK_Scene");
        }
        else if (scene == "Credits")
        {
            SceneManager.LoadScene("Credits");
        }
        else
        {
            SceneManager.LoadScene("Game_Scene");
        }
        
    }

    
}
