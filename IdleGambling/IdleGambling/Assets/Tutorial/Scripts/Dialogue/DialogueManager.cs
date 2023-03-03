using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    [SerializeField] private Image machineSpriteColor;
    [SerializeField] private TMP_Text moneyTextColor;
    [SerializeField] private Image coinSpriteColor;
    [SerializeField] private SpriteRenderer slotOneColor;
    [SerializeField] private SpriteRenderer slotTwoColor;
    [SerializeField] private SpriteRenderer slotThreeColor;

    private Color lowAlpha;
    private Color normalAlpha;


    [SerializeField] private Button machineButton;
    [SerializeField] private Button coinButton;
    [SerializeField] private Button settingsButton;

    public int partNumber;
    private bool isTyping = false;
    private Queue<string> sentences;
    public DialogueTrigger dialogueTrigger;

    [Tooltip("The text of the dialogue")]
    [SerializeField] private TMP_Text dialogueText;

    [Tooltip("The animator of the dialogue box")]
    [SerializeField] private Animator dialogueAnimator;

    [Tooltip("The animator of the dialogue box")]
    [SerializeField] private Button nextButton;
    [SerializeField] private RandomizerTutorial randomizerTutorial;

    private void Awake()
    {
        lowAlpha = new Color(1, 1, 1, 0.5f);
        normalAlpha = new Color(1, 1, 1, 1);
        machineSpriteColor.color = lowAlpha;
        moneyTextColor.alpha = 0.5f;
        coinSpriteColor.color = lowAlpha;
        slotOneColor.color = lowAlpha;
        slotTwoColor.color = lowAlpha;
        slotThreeColor.color = lowAlpha;
    }
    void Start()
    {   
        partNumber = 0;
        sentences = new Queue<string>();
        StartDialogue(dialogueTrigger.dialogue);

    }
    public void StartDialogue(Dialogue dialogue)
    {
        dialogueAnimator.SetBool("IsOpen", true);
        sentences.Clear();

        partNumber++;
        switch (partNumber)
        {
            
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
                machineSpriteColor.color = lowAlpha;
                moneyTextColor.alpha = 0.5f;
                coinSpriteColor.color = lowAlpha;
                slotOneColor.color = lowAlpha;
                slotTwoColor.color = lowAlpha;
                slotThreeColor.color = lowAlpha;
                break;

            case 3:
                coinButton.interactable = false;
                foreach (string sentence in dialogue.thirdPart)
                {
                    sentences.Enqueue(sentence);
                }

                break;

            case 4:
                settingsButton.interactable = false;
                foreach (string sentence in dialogue.fourhPart)
                {
                    sentences.Enqueue(sentence);
                }

                break;
        }

        DisplayNextSentence();
    }
    public void DisplayNextSentence()
    {
        if (!isTyping)
        {
            if (sentences.Count == 0)
            {
                EndDialougue();
                return;
            }
            string sentence = sentences.Dequeue();

            StartCoroutine(LoadCharInSentence(sentence));
        }
    }
    IEnumerator LoadCharInSentence(string sentence)
    {
        isTyping = true;
        nextButton.interactable = false;
        dialogueText.text = "";

        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForEndOfFrame();
            
        }
        isTyping = false;
        nextButton.interactable = true;
    }
    public void EndDialougue()
    {
        dialogueAnimator.SetBool("IsOpen", false);

        print(partNumber);
        switch (partNumber)
        {
            case 1:
                machineSpriteColor.color = normalAlpha;
                moneyTextColor.alpha = 1f;
                coinSpriteColor.color = normalAlpha;
                slotOneColor.color = normalAlpha;
                slotTwoColor.color = normalAlpha;
                slotThreeColor.color = normalAlpha;
                machineButton.interactable = true;
                randomizerTutorial.GiveMoneyToPlayer();

                break;

            case 2:
                coinButton.interactable = true;
                machineButton.interactable = false;
                break;

            case 3:
                settingsButton.interactable = true;

                break;

            case 4:
                //Pointer at prestige
                machineSpriteColor.color = normalAlpha;
                moneyTextColor.alpha = 1f;
                coinSpriteColor.color = normalAlpha;
                slotOneColor.color = normalAlpha;
                slotTwoColor.color = normalAlpha;
                slotThreeColor.color = normalAlpha;
                machineButton.interactable = true;
                coinButton.interactable = true;
                settingsButton.interactable = true;
                break;
        }
        if (partNumber >= 5)
        {
            print("Overflow");
        }
        print("End of dialogue");
    }
}
