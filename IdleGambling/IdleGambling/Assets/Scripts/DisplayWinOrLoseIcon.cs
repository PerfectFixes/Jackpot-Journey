using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DisplayWinOrLoseIcon : MonoBehaviour
{
    [Header("---UI---")]
    [Tooltip("The buttons that is over the mechine which makes the player able to gamble")]
    [SerializeField] private Button machineButton;
  
    [Tooltip("The text to display the amount of money the player has")]
    [SerializeField] private TMP_Text playerMoneyText;


    [Header("Slot Position")]
    [SerializeField] private SpriteRenderer firstSlot;
    [SerializeField] private SpriteRenderer secondSlot;
    [SerializeField] private SpriteRenderer thirdSlot;

    [Header("Icons List")]

    [SerializeField] private List<Sprite> smallWin;
    [SerializeField] private List<Sprite> mediumWin;
    [SerializeField] private List<Sprite> bigWin;

    [SerializeField] private int selectRandomIcon;
    [SerializeField] private float selectingRandomTime;

    [SerializeField] private int randomLoseChooser;
    [SerializeField] private int randomLoseCounter;

    public IEnumerator DisplayTheWin(int result)
    {
        //Disabling the button to stop the player from betting
        machineButton.interactable = false;
        ReadyToGamble();
        selectRandomIcon = Random.Range(0, 4);
        switch (result)
        {
            case 60:

                yield return new WaitForSeconds(0.5f);
                firstSlot.sprite = smallWin[selectRandomIcon];

                selectingRandomTime = Random.Range(0.5f, 1.5f);
                yield return new WaitForSeconds(selectingRandomTime);
                secondSlot.sprite = smallWin[selectRandomIcon];

                selectingRandomTime = Random.Range(0.5f, 1.5f);
                yield return new WaitForSeconds(selectingRandomTime);
                thirdSlot.sprite = smallWin[selectRandomIcon];

                

                break;
            //-------------------------------------------------
            case 30:

                yield return new WaitForSeconds(0.5f);
                firstSlot.sprite = mediumWin[selectRandomIcon];

                selectingRandomTime = Random.Range(0.5f, 1.5f);
                yield return new WaitForSeconds(selectingRandomTime);
                secondSlot.sprite = mediumWin[selectRandomIcon];

                selectingRandomTime = Random.Range(0.5f, 1.5f);
                yield return new WaitForSeconds(selectingRandomTime);
                thirdSlot.sprite = mediumWin[selectRandomIcon];

                break;
            //-------------------------------------------------
            case 10:

                yield return new WaitForSeconds(0.5f);
                firstSlot.sprite = bigWin[selectRandomIcon];

                selectingRandomTime = Random.Range(0.5f, 1.5f);
                yield return new WaitForSeconds(selectingRandomTime);
                secondSlot.sprite = bigWin[selectRandomIcon];

                selectingRandomTime = Random.Range(0.5f, 1.5f);
                yield return new WaitForSeconds(selectingRandomTime);
                thirdSlot.sprite = bigWin[selectRandomIcon];


                break;
        }
        machineButton.interactable = true;

        playerMoneyText.text = PlayerPrefs.GetInt("PlayerMoney").ToString();
        playerMoneyText.text = $"{PlayerPrefs.GetInt("PlayerMoney"):N0}";
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
                    //Sets the image from the Small win List<>
                    firstSlot.sprite = smallWin[selectRandomIcon];

                    //Add 1 to the counter to track the amount of Small win List<> usage
                    randomLoseCounter += 1;
                }
                
                else if (randomLoseChooser == 1)
                {
                    //Sets the image from the Medim win List<>
                    firstSlot.sprite = mediumWin[selectRandomIcon];

                    //Add 3 to the counter to track the amount of Medium win List<> usage
                    randomLoseCounter += 3;
                }
                
                else
                {
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
                    //Sets the image from the Small win List<>
                    secondSlot.sprite = smallWin[selectRandomIcon];

                    //Add 1 to the counter to track the amount of Small win List<> usage
                    randomLoseCounter += 1;
                }
                else if (randomLoseChooser == 1)
                {
                    //Sets the image from the Medim win List<>
                    secondSlot.sprite = mediumWin[selectRandomIcon];

                    //Add 3 to the counter to track the amount of Medium win List<> usage
                    randomLoseCounter += 3;
                }
                else
                {
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
                        //Sets the image from the Medium win List<>
                        thirdSlot.sprite = mediumWin[selectRandomIcon];
                        break;
                    }
                    else
                    {
                        //Sets the image from the Big win List<>
                        thirdSlot.sprite = bigWin[selectRandomIcon];
                        break;
                    }
                }
                //Checks to see if it rolled 3 times in a row from the same List<>
                if (randomLoseChooser == 1 && randomLoseCounter != 6)
                {
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
                        //Sets the image from the Small win List<>
                        thirdSlot.sprite = smallWin[selectRandomIcon];
                        break;
                    }
                    else
                    {
                        //Sets the image from the Big win List<>
                        thirdSlot.sprite = bigWin[selectRandomIcon];
                        break;
                    }
                }
                //Checks to see if it rolled 3 times in a row from the same List<>
                if (randomLoseChooser == 2 && randomLoseCounter != 14)
                {
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
                        //Sets the image from the Small win List<>
                        thirdSlot.sprite = smallWin[selectRandomIcon];
                        break;
                    }
                    else
                    {
                        //Sets the image from the Medium win List<>
                        thirdSlot.sprite = mediumWin[selectRandomIcon];
                        break;
                    }
                }
            }
        }
        machineButton.interactable = true;
    }
    public void ReadyToGamble()
    {
        //Disable in build
        ClearingEditorLog();

        //Setting the sprite to null
        firstSlot.sprite = null;
        secondSlot.sprite = null;
        thirdSlot.sprite = null;
    }
    //Disable in build
    public void ClearingEditorLog()
    {
        var assembly = System.Reflection.Assembly.GetAssembly(typeof(UnityEditor.Editor));
        var type = assembly.GetType("UnityEditor.LogEntries");
        var method = type.GetMethod("Clear");
        method.Invoke(new object(), null);
    }
}
