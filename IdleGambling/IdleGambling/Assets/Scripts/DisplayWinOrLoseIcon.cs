using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DisplayWinOrLoseIcon : MonoBehaviour
{
    private Randomizer updateMoney;

    [Header("Gameobject")]
    [SerializeField] private GameObject coinPrefab;

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
        updateMoney = GameObject.Find("Randomize_Number").GetComponent<Randomizer>();
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

                yield return new WaitForSeconds(1);
                firstSlotAnimator.enabled = false;
                firstSlot.sprite = smallWin[selectRandomIcon];

                selectingRandomTime = Random.Range(0.5f, 1.5f);
                yield return new WaitForSeconds(selectingRandomTime);
                secondSlotAnimator.enabled = false;
                secondSlot.sprite = smallWin[selectRandomIcon];

                selectingRandomTime = Random.Range(0.5f, 1.5f);
                yield return new WaitForSeconds(selectingRandomTime);
                thirdSlotAnimator.enabled = false;
                thirdSlot.sprite = smallWin[selectRandomIcon];
                break;
            //-------------------------------------------------
            case 30:

                yield return new WaitForSeconds(1);
                firstSlotAnimator.enabled = false;
                firstSlot.sprite = mediumWin[selectRandomIcon];

                selectingRandomTime = Random.Range(0.5f, 1.5f);
                yield return new WaitForSeconds(selectingRandomTime);
                secondSlotAnimator.enabled = false;
                secondSlot.sprite = mediumWin[selectRandomIcon];

                selectingRandomTime = Random.Range(0.5f, 1.5f);
                yield return new WaitForSeconds(selectingRandomTime);
                thirdSlotAnimator.enabled = false;
                thirdSlot.sprite = mediumWin[selectRandomIcon];
                break;
            //-------------------------------------------------
            case 10:

                yield return new WaitForSeconds(1);
                firstSlotAnimator.enabled = false;
                firstSlot.sprite = bigWin[selectRandomIcon];

                selectingRandomTime = Random.Range(0.5f, 1.5f);
                yield return new WaitForSeconds(selectingRandomTime);
                secondSlotAnimator.enabled = false;
                secondSlot.sprite = bigWin[selectRandomIcon];

                selectingRandomTime = Random.Range(0.5f, 1.5f);
                yield return new WaitForSeconds(selectingRandomTime);
                thirdSlotAnimator.enabled = false;
                thirdSlot.sprite = bigWin[selectRandomIcon];
                break;
        }

        //Update the amount of money in the save file
        int updatePlayerMoney = PlayerPrefs.GetInt("PlayerMoney") + winningAmount;
        PlayerPrefs.SetInt("PlayerMoney", updatePlayerMoney);
        updateMoney.UpdatePlayerMoney();

        if (PlayerPrefs.GetInt("PrestigeLevel") >= 8)
        {
            winningAmount = Mathf.RoundToInt(winningAmount / 5f);
        }
        else
        {
            winningAmount = Mathf.RoundToInt(winningAmount / 2);
        }  
        for (int i = 0; i < winningAmount ; i++)
        {
            yield return new WaitForEndOfFrame();
            Instantiate(coinPrefab, transform.position, Quaternion.identity);
        }
        
        yield return new WaitForSeconds(1f);

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
            randomLoseChooser = Random.Range(0,3);

            //Checking which loop is it on and setting the images correctly
            if(i == 0)
            {
                
                if(randomLoseChooser == 0)
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
                    if(randomLoseChooser == 0)
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
        yield return new WaitForSeconds(1f);
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
