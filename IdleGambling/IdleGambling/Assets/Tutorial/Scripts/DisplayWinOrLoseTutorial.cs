using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DisplayWinOrLoseTutorial : MonoBehaviour
{
    private RandomizerTutorial updateMoney;


    [Header("Sounds")]
    [Tooltip("The SFX for losing the gamble")]
    [SerializeField] private AudioSource gambleLosingSFX;

    [Tooltip("The SFX for winning the gamble")]
    [SerializeField] private AudioSource winningCoinsSFX;

    [Tooltip("The SFX for the first slot")]
    [SerializeField] private AudioSource firstSlotSFX;

    [Tooltip("The SFX for the second slot")]
    [SerializeField] private AudioSource secondSlotSFX;

    [Tooltip("The SFX for the third slot")]
    [SerializeField] private AudioSource thirdSlotSFX;

    [Header("Gameobject")]
    [SerializeField] private GameObject coinPrefab;
    [SerializeField] private Transform coinSpawnerTransfom;

    [Header("---UI---")]
    [Tooltip("The buttons that is over the mechine which makes the player able to gamble")]
    [SerializeField] private Button machineButton;

    [Tooltip("The text to display the amount of money the player has")]
    [SerializeField] private TMP_Text playerMoneyText;


    [Header("Slot Position")]
    [SerializeField] private SpriteRenderer firstSlot;
    [SerializeField] private SpriteRenderer secondSlot;
    [SerializeField] private SpriteRenderer thirdSlot;

    [Header("Animators")]
    [SerializeField] private Animator firstSlotAnimator;
    [SerializeField] private Animator secondSlotAnimator;
    [SerializeField] private Animator thirdSlotAnimator;

    [Header("Icons List")]

    [SerializeField] private List<Sprite> smallWin;
    [SerializeField] private List<Sprite> mediumWin;
    [SerializeField] private List<Sprite> bigWin;

    [SerializeField] private int selectRandomIcon;
    [SerializeField] private float selectingRandomTime;

    [SerializeField] private int randomLoseChooser;
    [SerializeField] private int randomLoseCounter;
    private void Awake()
    {
        updateMoney = GameObject.Find("Randomize_Number").GetComponent<RandomizerTutorial>();
    }
    public IEnumerator DisplayTheWin(int result, int winningAmount)
    {
        //Disabling the button to stop the player from betting
        machineButton.interactable = false;
        ReadyToGamble();
        selectRandomIcon = Random.Range(0, 4);
        switch (result)
        {
            case 60:

                //Waiting 1 second before starting to display the win
                yield return new WaitForSeconds(1);

                //Disabling the animator
                firstSlotAnimator.enabled = false;

                //Setting the correct icon
                firstSlot.sprite = smallWin[selectRandomIcon];

                //Playing SFX if the player didnt disable it
                if (firstSlotSFX.isActiveAndEnabled)
                {
                    firstSlotSFX.Play();
                }

                //Randomizing the amount of seconds before starting to display the win
                selectingRandomTime = Random.Range(0.5f, 1.5f);
                yield return new WaitForSeconds(selectingRandomTime);

                //Disabling the animator
                secondSlotAnimator.enabled = false;

                //Setting the correct icon
                secondSlot.sprite = smallWin[selectRandomIcon];

                //Playing SFX if the player didnt disable it
                if (secondSlotSFX.isActiveAndEnabled)
                {
                    secondSlotSFX.Play();
                }

                //Randomizing the amount of seconds before starting to display the win
                selectingRandomTime = Random.Range(0.5f, 1.5f);
                yield return new WaitForSeconds(selectingRandomTime);

                //Disabling the animator
                thirdSlotAnimator.enabled = false;

                //Setting the correct icon
                thirdSlot.sprite = smallWin[selectRandomIcon];

                //Playing SFX if the player didnt disable it
                if (thirdSlotSFX.isActiveAndEnabled)
                {
                    thirdSlotSFX.Play();
                }
                break;
            //-------------------------------------------------
            case 30:

                //Waiting 1 second before starting to display the win
                yield return new WaitForSeconds(1);

                //Disabling the animator
                firstSlotAnimator.enabled = false;

                //Setting the correct icon
                firstSlot.sprite = mediumWin[selectRandomIcon];

                //Playing SFX if the player didnt disable it
                if (firstSlotSFX.isActiveAndEnabled)
                {
                    firstSlotSFX.Play();
                }

                //Randomizing the amount of seconds before starting to display the win
                selectingRandomTime = Random.Range(0.5f, 1.5f);
                yield return new WaitForSeconds(selectingRandomTime);

                //Disabling the animator
                secondSlotAnimator.enabled = false;

                //Setting the correct icon
                secondSlot.sprite = mediumWin[selectRandomIcon];

                //Playing SFX if the player didnt disable it
                if (secondSlotSFX.isActiveAndEnabled)
                {
                    secondSlotSFX.Play();
                }

                //Randomizing the amount of seconds before starting to display the win
                selectingRandomTime = Random.Range(0.5f, 1.5f);
                yield return new WaitForSeconds(selectingRandomTime);

                //Disabling the animator
                thirdSlotAnimator.enabled = false;

                //Setting the correct icon
                thirdSlot.sprite = mediumWin[selectRandomIcon];

                //Playing SFX if the player didnt disable it
                if (thirdSlotSFX.isActiveAndEnabled)
                {
                    thirdSlotSFX.Play();
                }
                break;
            //-------------------------------------------------
            case 10:

                //Waiting 1 second before starting to display the win
                yield return new WaitForSeconds(1);

                //Disabling the animator
                firstSlotAnimator.enabled = false;

                //Setting the correct icon
                firstSlot.sprite = bigWin[selectRandomIcon];

                //Playing SFX if the player didnt disable it
                if (firstSlotSFX.isActiveAndEnabled)
                {
                    firstSlotSFX.Play();
                }

                //Randomizing the amount of seconds before starting to display the win
                selectingRandomTime = Random.Range(0.5f, 1.5f);
                yield return new WaitForSeconds(selectingRandomTime);

                //Disabling the animator
                secondSlotAnimator.enabled = false;

                //Setting the correct icon
                secondSlot.sprite = bigWin[selectRandomIcon];

                //Playing SFX if the player didnt disable it
                if (secondSlotSFX.isActiveAndEnabled)
                {
                    secondSlotSFX.Play();
                }

                //Randomizing the amount of seconds before starting to display the win
                selectingRandomTime = Random.Range(0.5f, 1.5f);
                yield return new WaitForSeconds(selectingRandomTime);

                //Disabling the animator
                thirdSlotAnimator.enabled = false;

                //Setting the correct icon
                thirdSlot.sprite = bigWin[selectRandomIcon];

                //Playing SFX if the player didnt disable it
                if (thirdSlotSFX.isActiveAndEnabled)
                {
                    thirdSlotSFX.Play();
                }
                break;
        }

        //Update the amount of money in the save file and the game
        updateMoney.UpdatePlayerMoney(winningAmount);

        //Enable the SFX To loop and plays it
        winningCoinsSFX.loop = true;
        if (winningCoinsSFX.isActiveAndEnabled)
        {
            winningCoinsSFX.Play();
        }

        //Make it to not spawn alot of coins at level 7 or lower
        winningAmount = Mathf.RoundToInt(winningAmount / 2);
        
        for (int i = 0; i < winningAmount; i++)
        {
            yield return new WaitForEndOfFrame();
            Instantiate(coinPrefab, coinSpawnerTransfom.position, Quaternion.identity);
        }
        //Stops the SFX
        winningCoinsSFX.loop = false;
        yield return new WaitForSeconds(1f);

        //Enables the gambling
        machineButton.interactable = true;
        ReadyToGamble();
   
    }
    public IEnumerator DisplayingTheLose()
    {
        //Reset the counter and cleaning the screen
        randomLoseCounter = 0;
        ReadyToGamble();

        //Disabling the button to stop the player from betting
        machineButton.interactable = false;

        //Setting the images for each slot place
        for (int i = 0; i < 3; i++)
        {
            //Selecting a random icon from the pool of icons and will change from each loop
            selectRandomIcon = Random.Range(0, 4);

            //setting a waiting time that will change each loop
            selectingRandomTime = Random.Range(0.5f, 1.5f);
            yield return new WaitForSeconds(selectingRandomTime);

            //Choosing what List<> to take from (Small win, Medium win, Big win)
            randomLoseChooser = Random.Range(0, 3);

            //Checking which loop is it on and setting the images correctly
            if (i == 0)
            {
                if (firstSlotSFX.isActiveAndEnabled)
                {
                    firstSlotSFX.Play();
                }
                if (randomLoseChooser == 0)
                {
                    firstSlotAnimator.enabled = false;

                    //Sets the image from the Small win List<>
                    firstSlot.sprite = smallWin[selectRandomIcon];


                    //Add 1 to the counter to track the amount of Small win List<> usage
                    randomLoseCounter += 1;
                }

                else if (randomLoseChooser == 1)
                {
                    firstSlotAnimator.enabled = false;

                    //Sets the image from the Medim win List<>
                    firstSlot.sprite = mediumWin[selectRandomIcon];

                    //Add 3 to the counter to track the amount of Medium win List<> usage
                    randomLoseCounter += 3;
                }

                else
                {
                    firstSlotAnimator.enabled = false;

                    //Sets the image from the Big win List<>
                    firstSlot.sprite = bigWin[selectRandomIcon];

                    //Add 7 to the counter to track the amount of Big win List<> usage
                    randomLoseCounter += 7;
                }
            }
            //Checking which loop is it on and setting the images correctly
            else if (i == 1)
            {
                if (secondSlotSFX.isActiveAndEnabled)
                {
                    secondSlotSFX.Play();
                }
                if (randomLoseChooser == 0)
                {
                    secondSlotAnimator.enabled = false;

                    //Sets the image from the Small win List<>
                    secondSlot.sprite = smallWin[selectRandomIcon];

                    //Add 1 to the counter to track the amount of Small win List<> usage
                    randomLoseCounter += 1;
                }
                else if (randomLoseChooser == 1)
                {
                    secondSlotAnimator.enabled = false;

                    //Sets the image from the Medim win List<>
                    secondSlot.sprite = mediumWin[selectRandomIcon];

                    //Add 3 to the counter to track the amount of Medium win List<> usage
                    randomLoseCounter += 3;
                }
                else
                {
                    secondSlotAnimator.enabled = false;

                    //Sets the image from the Big win List<>
                    secondSlot.sprite = bigWin[selectRandomIcon];

                    //Add 7 to the counter to track the amount of Big win List<> usage
                    randomLoseCounter += 7;
                }
            }
            //Checking which loop is it on and setting the images correctly
            else
            {

                //Checks to see if it rolled 3 times in a row from the same List<>
                if (randomLoseChooser == 0 && randomLoseCounter != 2)
                {
                    thirdSlotAnimator.enabled = false;

                    //Sets the image from the Small win List<>
                    thirdSlot.sprite = smallWin[selectRandomIcon];
                    break;
                }
                //If it did roll the same number 3 times
                //reroll the number and sets the image from another List<>
                else if (randomLoseChooser == 0 && randomLoseCounter == 2)
                {
                    print("In special else of random 0");
                    randomLoseChooser = Random.Range(0, 2);
                    if (randomLoseChooser == 0)
                    {
                        thirdSlotAnimator.enabled = false;

                        //Sets the image from the Medium win List<>
                        thirdSlot.sprite = mediumWin[selectRandomIcon];
                        break;
                    }
                    else
                    {
                        thirdSlotAnimator.enabled = false;

                        //Sets the image from the Big win List<>
                        thirdSlot.sprite = bigWin[selectRandomIcon];
                        break;
                    }
                }
                //Checks to see if it rolled 3 times in a row from the same List<>
                if (randomLoseChooser == 1 && randomLoseCounter != 6)
                {
                    thirdSlotAnimator.enabled = false;

                    //Sets the image from the Small win List<>
                    thirdSlot.sprite = mediumWin[selectRandomIcon];
                    break;
                }
                //If it did roll the same number 3 times
                //reroll the number and sets the image from another List<>
                else if (randomLoseChooser == 1 && randomLoseCounter == 6)
                {
                    randomLoseChooser = Random.Range(0, 2);
                    print("In special else of random 1");
                    if (randomLoseChooser == 0)
                    {
                        thirdSlotAnimator.enabled = false;

                        //Sets the image from the Small win List<>
                        thirdSlot.sprite = smallWin[selectRandomIcon];
                        break;
                    }
                    else
                    {
                        thirdSlotAnimator.enabled = false;

                        //Sets the image from the Big win List<>
                        thirdSlot.sprite = bigWin[selectRandomIcon];
                        break;
                    }
                }
                //Checks to see if it rolled 3 times in a row from the same List<>
                if (randomLoseChooser == 2 && randomLoseCounter != 14)
                {
                    thirdSlotAnimator.enabled = false;

                    //Sets the image from the Big win List<>
                    thirdSlot.sprite = bigWin[selectRandomIcon];
                    break;
                }
                //If it did roll the same number 3 times
                //reroll the number and sets the image from another List<>
                else if (randomLoseChooser == 2 && randomLoseCounter == 14)
                {
                    print("In special else of random 2");
                    randomLoseChooser = Random.Range(0, 2);
                    if (randomLoseChooser == 0)
                    {
                        thirdSlotAnimator.enabled = false;

                        //Sets the image from the Small win List<>
                        thirdSlot.sprite = smallWin[selectRandomIcon];
                        break;
                    }
                    else
                    {
                        thirdSlotAnimator.enabled = false;

                        //Sets the image from the Medium win List<>
                        thirdSlot.sprite = mediumWin[selectRandomIcon];
                        break;
                    }
                }
            }
        }
        if (gambleLosingSFX.isActiveAndEnabled)
        {
            gambleLosingSFX.Play();
        }
        yield return new WaitForSeconds(1f);
        updateMoney.UpdatePlayerMoneyOnLost();
        machineButton.interactable = true;
        ReadyToGamble();
    }
    public void ReadyToGamble()
    {
        //Setting the sprite to null
        firstSlot.sprite = null;
        secondSlot.sprite = null;
        thirdSlot.sprite = null;

        firstSlotAnimator.enabled = true;
        secondSlotAnimator.enabled = true;
        thirdSlotAnimator.enabled = true;

        /* //Disable in build
       ClearingEditorLog();*/
    }
    //Disable in build
    /* public void ClearingEditorLog()
     {
         var assembly = System.Reflection.Assembly.GetAssembly(typeof(UnityEditor.Editor));
         var type = assembly.GetType("UnityEditor.LogEntries");
         var method = type.GetMethod("Clear");
         method.Invoke(new object(), null);
     }*/
}
