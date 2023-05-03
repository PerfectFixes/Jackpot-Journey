//using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    //Checks to see what part of the tutorial the player is currently at
    private int partNumber;

    [Header("Pointers")]
    [SerializeField] private GameObject machinePointer;
    [SerializeField] private GameObject coinPointer;
    [SerializeField] private GameObject settingsPointer;
    [SerializeField] private GameObject prestigePointer;
    [SerializeField] private GameObject[] inSettingsPointer;

    //To fade the machine all of the following parts are requiered
    [Header("Fade Slot Machine")]
    [SerializeField] private Image machineSpriteColor;
    [SerializeField] private TMP_Text moneyTextColor;
    [SerializeField] private Image coinSpriteColor;
    [SerializeField] private SpriteRenderer slotOneColor;
    [SerializeField] private SpriteRenderer slotTwoColor;
    [SerializeField] private SpriteRenderer slotThreeColor;
    private Color lowAlpha;
    private Color normalAlpha;


    [Header("Buttons")]
    [SerializeField] private Button machineButton;
    [SerializeField] private Button coinButton;
    [SerializeField] private Button settingsButton;
    [SerializeField] private Image coinClickColor;

    [Header("SFX")]
    [SerializeField] private AudioSource typeSFX;


    //Dialogue logic
    private bool isTyping = false;
    private Queue<string> sentences;
    public DialogueTrigger dialogueTrigger;


    [Tooltip("The text of the dialogue")]
    [SerializeField] private TMP_Text dialogueText;


    [Tooltip("The animator of the dialogue box to fade in")]
    [SerializeField] private Animator dialogueAnimator;

    [Tooltip("The button to skip to the next dialogue box")]
    [SerializeField] private Button nextButton;

    //This is to give the player money
    [SerializeField] private RandomizerTutorial randomizerTutorial;

    private void Awake()
    {
        //Sets the correct values of the alphas
        lowAlpha = new Color(1, 1, 1, 0.5f);
        normalAlpha = new Color(1, 1, 1, 1);

        //Make the slot machine a bit transperent
        machineSpriteColor.color = lowAlpha;
        moneyTextColor.alpha = 0.5f;
        coinSpriteColor.color = lowAlpha;
        slotOneColor.color = lowAlpha;
        slotTwoColor.color = lowAlpha;
        slotThreeColor.color = lowAlpha;
        coinClickColor.color = lowAlpha;
        coinButton.interactable = false;
    }
    void Start()
    {   
        //Starting the dialogue sequence
        partNumber = 0;
        sentences = new Queue<string>();
        StartCoroutine(LoadDelay());
    }
    private IEnumerator LoadDelay()
    {
        yield return new WaitForSeconds(1.4f);
        StartDialogue(dialogueTrigger.dialogue);
    }
    public void StartDialogue(Dialogue dialogue)
    {
        //Starting the fade in animation
        dialogueAnimator.SetBool("IsOpen", true);

        //Clear the board to start fresh
        sentences.Clear();

        //change the part number
        partNumber++;
        switch (partNumber)
        {
            //Load the sentences to the dialogue logic
            case 1:
                foreach (string sentence in dialogue.firstPart)
                {
                    sentences.Enqueue(sentence);
                }
                
                break;

            case 2:
                foreach (string sentence in dialogue.secondPart)
                {
                    sentences.Enqueue(sentence);
                }
                //Lower the alpha of the slot machine at the start of the seconds dialogue
                machineSpriteColor.color = lowAlpha;
                moneyTextColor.alpha = 0.5f;
                coinSpriteColor.color = lowAlpha;
                slotOneColor.color = lowAlpha;
                slotTwoColor.color = lowAlpha;
                slotThreeColor.color = lowAlpha;
                machineButton.interactable = false;

                //Disable the pointer
                machinePointer.SetActive(false);
                break;

            case 3:
                //Disable the coin button
                coinPointer.SetActive(false);

                //Disable the pointer
                coinButton.interactable = false;
                coinClickColor.color = lowAlpha;
                foreach (string sentence in dialogue.thirdPart)
                {
                    sentences.Enqueue(sentence);
                }

                break;

            case 4:

                settingsPointer.SetActive(false);
                for (int i = 0; i < inSettingsPointer.Length; i++)
                {
                    inSettingsPointer[i].SetActive(false);
                }
                foreach (string sentence in dialogue.fourhPart)
                {
                    sentences.Enqueue(sentence);
                }
                //Disable the settings button
                settingsButton.interactable = false;

                //Disable the pointer
                prestigePointer.SetActive(true);
                break;
        }
        //Playing the next sentence
        DisplayNextSentence();
    }
    public void DisplayNextSentence()
    {
        //Play the next sentence only of it isn't typing
        if (!isTyping)
        {
            //When the queue has no more sentences go to end dialogue
            if (sentences.Count == 0)
            {
                EndDialougue();
                return;
            }
            
            string sentence = sentences.Dequeue();

            //Printing 1 char at a time
            StartCoroutine(LoadCharInSentence(sentence));
        }
     
    }
    IEnumerator LoadCharInSentence(string sentence)
    {
        //Disable the option to skip the sentece
        isTyping = true;

        //Disalbe the next button
        nextButton.interactable = false;

        //Setting the text to null
        dialogueText.text = "";

        //Each frame take 1 char from the string array and print it
        foreach (char letter in sentence.ToCharArray())
        {
            if(letter.ToString() != " "  && !typeSFX.isPlaying)
            {
                    typeSFX.Play();
            }
            dialogueText.text += letter;
            yield return new WaitForSeconds(0.02f);     
        }
        //When its done enable the next button 
        isTyping = false;
        nextButton.interactable = true;
    }
    public void EndDialougue()
    {
        //Animate the exit of the dialogue box
        dialogueAnimator.SetBool("IsOpen", false);

        print(partNumber);
        
        //At the end of the dialogue do specific actions
        switch (partNumber)
        {
            case 1:
                //Make th machine clickable
                machineSpriteColor.color = normalAlpha;
                moneyTextColor.alpha = 1f;
                coinSpriteColor.color = normalAlpha;
                slotOneColor.color = normalAlpha;
                slotTwoColor.color = normalAlpha;
                slotThreeColor.color = normalAlpha;
                machineButton.interactable = true;

                //Enable the pointer
                machinePointer.SetActive(true);
                //Give the player money
                randomizerTutorial.GiveMoneyToPlayer();

                break;

            case 2:

                machineButton.interactable = false;

                //Enable the pointer
                coinPointer.SetActive(true);
                //Make the coin clickable
                coinButton.interactable = true;
                coinClickColor.color = normalAlpha;
                break;

            case 3:

                //Enable the pointer
                settingsPointer.SetActive(true);

                //Make the settings interactable
                settingsButton.interactable = true;

                coinClickColor.color = lowAlpha;
                break;

            case 4:
                //Pointer at prestige

                //Make everything interactable
                machineSpriteColor.color = normalAlpha;
                moneyTextColor.alpha = 1f;
                coinSpriteColor.color = normalAlpha;
                slotOneColor.color = normalAlpha;
                slotTwoColor.color = normalAlpha;
                slotThreeColor.color = normalAlpha;
                machineButton.interactable = true;
                coinButton.interactable = true;
                coinClickColor.color = normalAlpha;
                settingsButton.interactable = true;
       

                //Disable the pointer
                prestigePointer.SetActive(false);
                break;

        }
        if (partNumber >= 5)
        {
            print("Overflow");
        }
    }
}
