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
        selectRandomIcon = 2;
        int randomOne = Random.Range(1, 13);
        int randomTwo = Random.Range(1, 13);
        int randomThree = Random.Range(1, 13);


        //Choosing to bait or not to bait the player
        if (selectRandomIcon <= 4)
        {
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
}
