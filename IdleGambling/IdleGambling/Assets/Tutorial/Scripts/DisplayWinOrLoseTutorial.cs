using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class DisplayWinOrLoseTutorial : MonoBehaviour
{
    private RandomizerTutorial updateMoney;

    private int selectRandomIcon;

    [Header("Animators")]
    [SerializeField] private AnimatorController firstSlotControl;
    [SerializeField] private AnimatorController secondSlotControl;
    [SerializeField] private AnimatorController thirdSlotControl;

    [Tooltip("The SFX for winning the gamble")]
    [SerializeField] private AudioSource winningCoinsSFX;

    [Header("Gameobject")]
    [SerializeField] private GameObject coinPrefab;
    [SerializeField] private Transform coinSpawnerTransfom;

    [Header("Button")]
    [Tooltip("The buttons that is over the mechine which makes the player able to gamble")]
    [SerializeField] private Button machineButton;
    private void Awake()
    {
        updateMoney = GameObject.Find("Randomize_Number").GetComponent<RandomizerTutorial>();
    }
    public IEnumerator DisplayTheWin(int result, int winningAmount)
    {

        //Disabling the button to stop the player from betting
        machineButton.interactable = false;

        switch (result)
        {
            case 60:

                //Choose an icon from the small win category 
                selectRandomIcon = Random.Range(1, 5);
                firstSlotControl.iconNumber = selectRandomIcon;
                secondSlotControl.iconNumber = selectRandomIcon;
                thirdSlotControl.iconNumber = selectRandomIcon;


                //Waiting 1 second and then stopping 1 slot at a time
                yield return new WaitForSeconds(1);
                firstSlotControl.StopAnimation(true);

                yield return new WaitForSeconds(1);

                secondSlotControl.StopAnimation(true);

                yield return new WaitForSeconds(1);

                thirdSlotControl.StopAnimation(true);

                break;
            //-------------------------------------------------
            case 30:

                //Choose an icon from the medium win category 
                selectRandomIcon = Random.Range(5, 9);
                firstSlotControl.iconNumber = selectRandomIcon;
                secondSlotControl.iconNumber = selectRandomIcon;
                thirdSlotControl.iconNumber = selectRandomIcon;

                //Waiting 1 second and then stopping 1 slot at a time
                yield return new WaitForSeconds(1);
                firstSlotControl.StopAnimation(true);


                yield return new WaitForSeconds(1);

                secondSlotControl.StopAnimation(true);

                yield return new WaitForSeconds(1);

                thirdSlotControl.StopAnimation(true);

                break;
            //-------------------------------------------------
            case 10:

                //Choose an icon from the big win category 
                selectRandomIcon = Random.Range(9, 13);
                firstSlotControl.iconNumber = selectRandomIcon;
                secondSlotControl.iconNumber = selectRandomIcon;
                thirdSlotControl.iconNumber = selectRandomIcon;

                //Waiting 1 second and then stopping 1 slot at a time
                yield return new WaitForSeconds(1);
                firstSlotControl.StopAnimation(true);


                yield return new WaitForSeconds(1);

                secondSlotControl.StopAnimation(true);

                yield return new WaitForSeconds(1);

                thirdSlotControl.StopAnimation(true);
                break;
        }

        //Waiting 0.75s to start saving and playing SFX
        if (thirdSlotControl.isStop)
        {
            yield return new WaitForSeconds(0.75f);
        }

        //Update the amount of money in the save file and the game
        int updatePlayerMoney = PlayerPrefs.GetInt("PlayerMoney") + winningAmount;
        PlayerPrefs.SetInt("PlayerMoney", updatePlayerMoney);
        updateMoney.UpdatePlayerMoney(winningAmount);

        //Enable the SFX To loop and plays it
        winningCoinsSFX.loop = true;
        if (winningCoinsSFX.isActiveAndEnabled)
        {
            winningCoinsSFX.Play();
        }
        if (PlayerPrefs.GetInt("PrestigeLevel") >= 8)
        {
            //Make it to not spawn alot of coins at level 8 or higher
            winningAmount = Mathf.RoundToInt(winningAmount / 5f);
        }
        else
        {
            //Make it to not spawn alot of coins at level 7 or lower
            winningAmount = Mathf.RoundToInt(winningAmount / 2);
        }
        for (int i = 0; i < winningAmount; i++)
        {
            yield return new WaitForEndOfFrame();
            Instantiate(coinPrefab, coinSpawnerTransfom.position, Quaternion.identity);
        }
        //Stops the SFX
        winningCoinsSFX.loop = false;

        //Waiting more and then resuming the animation
        yield return new WaitForSeconds(1.25f);

        firstSlotControl.StopAnimation(false);
        yield return new WaitForSeconds(0.1f);
        secondSlotControl.StopAnimation(false);
        yield return new WaitForSeconds(0.1f);
        thirdSlotControl.StopAnimation(false);

        //Enables the gambling
        machineButton.interactable = true;
    }
    public IEnumerator DisplayingTheLose()
    {
        //Disabling the button to stop the player from betting
        machineButton.interactable = false;

        //Choosing an icon to display when losing
        selectRandomIcon = Random.Range(1, 13);
        int randomOne = Random.Range(1, 13);
        int randomTwo = Random.Range(1, 13);
        int randomThree = Random.Range(1, 13);


        //Choosing to bait or not to bait the player
        if (selectRandomIcon <= 4)
        {

            print("2 are the same");
            //Making sure it wont pick the same image 3 times
            while (randomTwo == randomOne)
            {
                yield return null;
                randomOne = Random.Range(1, 13);
            }
            //Setting 2 images that are the same and 1 that is different
            firstSlotControl.iconNumber = randomTwo;
            secondSlotControl.iconNumber = randomTwo;
            thirdSlotControl.iconNumber = randomOne;
        }
        else
        {
            //Making sure the first and the second arent the same
            while (randomOne == randomTwo)
            {
                yield return null;
                randomTwo = Random.Range(1, 13);
            }
            //Setting 3 random images
            firstSlotControl.iconNumber = randomOne;
            secondSlotControl.iconNumber = randomTwo;
            thirdSlotControl.iconNumber = randomThree;
        }


        //stopping the slot machine
        yield return new WaitForSeconds(1);
        firstSlotControl.StopAnimation(true);

        yield return new WaitForSeconds(1);

        secondSlotControl.StopAnimation(true);

        yield return new WaitForSeconds(1);

        thirdSlotControl.StopAnimation(true);
        yield return new WaitForSeconds(1f);


        //resuming the slot machine
        firstSlotControl.StopAnimation(false);
        yield return new WaitForSeconds(0.1f);

        secondSlotControl.StopAnimation(false);
        yield return new WaitForSeconds(0.1f);

        thirdSlotControl.StopAnimation(false);

        updateMoney.UpdatePlayerMoneyOnLost();
        //enable the gambling
        machineButton.interactable = true;
    }

    /* //Disable in build
   ClearingEditorLog();*/

    //Disable in build
    /* public void ClearingEditorLog()
     {
         var assembly = System.Reflection.Assembly.GetAssembly(typeof(UnityEditor.Editor));
         var type = assembly.GetType("UnityEditor.LogEntries");
         var method = type.GetMethod("Clear");
         method.Invoke(new object(), null);
     }*/
}
/*selectRandomIcon = Random.Range(1, 4);
                yield return new WaitForSeconds(1);
                //firstSlotAnimator.enabled = false;
                firstSlotControl.StopAnimation(true, selectRandomIcon);
                //firstSlot.sprite = smallWin[selectRandomIcon];
                if (firstSlotSFX.isActiveAndEnabled)
                {
                    firstSlotSFX.Play();
                }
                

                selectingRandomTime = Random.Range(0.5f, 1.5f);
                yield return new WaitForSeconds(selectingRandomTime);
                //secondSlotAnimator.enabled = false;
                secondSlotControl.StopAnimation(true, selectRandomIcon);
                //secondSlot.sprite = smallWin[selectRandomIcon];
                if (secondSlotSFX.isActiveAndEnabled)
                {
                    secondSlotSFX.Play();
                }

                selectingRandomTime = Random.Range(0.5f, 1.5f);
                yield return new WaitForSeconds(selectingRandomTime);
                //thirdSlotAnimator.enabled = false;
                thirdSlotControl.StopAnimation(true, selectRandomIcon);
                //thirdSlot.sprite = smallWin[selectRandomIcon];
                if (thirdSlotSFX.isActiveAndEnabled)
                {
                    thirdSlotSFX.Play();
                }*/





// ------- display lose ------

/*
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
}*/