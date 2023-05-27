using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DisplayWinOrLoseIcon : MonoBehaviour
{
    private Randomizer updateMoney;

    private int selectRandomIcon;
    private int sfxCalled;

    [Header("Animators")]
    [SerializeField] private AnimatorController firstSlotControl;
    [SerializeField] private AnimatorController secondSlotControl;
    [SerializeField] private AnimatorController thirdSlotControl;

    [Tooltip("The SFX for winning the gamble")]
    [SerializeField] private AudioSource winningCoinsSFX;

    [Tooltip("The SFX for winning the gamble")]
    [SerializeField] private AudioSource jackpotSFX;

    [Header("Gameobject")]
    [SerializeField] private GameObject coinPrefab;
    [SerializeField] private GameObject jackpotGameObject;
    [SerializeField] private Animator jackpotAnimator;
    [SerializeField] private EffectSpawner effectSpawnerRight;
    [SerializeField] private EffectSpawner effectSpawnerMiddle;
    [SerializeField] private EffectSpawner effectSpawnerLeft;

    [Header("Button")]
    [Tooltip("The buttons that is over the mechine which makes the player able to gamble")]
    [SerializeField] private Button machineButton;
    private void Awake()
    {
        //Counts the amount of times that the SFX need to be played
        sfxCalled = 0;
        updateMoney = GameObject.Find("Randomize_Number").GetComponent<Randomizer>();
        jackpotGameObject.SetActive(false);
    }
    public IEnumerator DisplayTheWin(int result, int winningAmount)
    {
        //Disabling the button to stop the player from betting
        machineButton.interactable = false;

        //Set which group of icon to display according to the win amount
        switch (result)
        {
            case 55:

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
            case 33:

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
            case 11:

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

                case 1:
                //Choose an icon from the Jackpot win 
                selectRandomIcon = 13;
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
        if (!jackpotGameObject.activeInHierarchy && result == 1)
        {
            jackpotGameObject.SetActive(true);
        }
        if(result == 1)
        {
            jackpotAnimator.Play("Jackpot_Fade_In");
            jackpotSFX.Play();
        }
        

        //***** Update stats *****

        //Winning coins from the machine stat
        int statsTcoinUpdated = PlayerPrefs.GetInt("StatsTCoinsWon", 0);
        statsTcoinUpdated += winningAmount;
        PlayerPrefs.SetInt("StatsTCoinsWon", statsTcoinUpdated);

        if(result != 1)
        {
            //Amount of time the player won stat
            int statsMachineWins = PlayerPrefs.GetInt("StatsMachineWins", 0);
            statsMachineWins++;
            PlayerPrefs.SetInt("StatsMachineWins", statsMachineWins);
        }
       


        //Update the amount of money in the save file and the game
        int updatePlayerMoney = PlayerPrefs.GetInt("PlayerMoney") + winningAmount;
        PlayerPrefs.SetInt("PlayerMoney", updatePlayerMoney);
        updateMoney.UpdatePlayerMoney();

        //Disable the option to stop the sfx to early because of multiple wins
        sfxCalled++;

        //Spawn coins and play the SFX while continue the code
        StartCoroutine(SpawnCoinsAndSFX(result));


        //Waiting more and then resuming the animation
        yield return new WaitForSeconds(1.3f);

        firstSlotControl.StopAnimation(false);
        yield return new WaitForSeconds(0.15f);
        secondSlotControl.StopAnimation(false);
        yield return new WaitForSeconds(0.15f);
        thirdSlotControl.StopAnimation(false);

        if(result == 1)
        {
            jackpotAnimator.Play("Jackpot_Fade_out");
        }

        //Enables the gambling
        machineButton.interactable = true;
    }
    private IEnumerator SpawnCoinsAndSFX(int winningAmount)
    {
        //Spawn the amount of coins related to the win
        if (winningAmount == 55)
        {
            StartCoroutine(effectSpawnerRight.SpawnItems("SmallCoins", 1));
            StartCoroutine(effectSpawnerMiddle.SpawnItems("SmallCoins", 1));
            StartCoroutine(effectSpawnerLeft.SpawnItems("SmallCoins", 1));
        }
        else if (winningAmount == 33)
        {
            StartCoroutine(effectSpawnerRight.SpawnItems("MediumCoins", 1));
            StartCoroutine(effectSpawnerMiddle.SpawnItems("MediumCoins", 1));
            StartCoroutine(effectSpawnerLeft.SpawnItems("MediumCoins", 1));
        }
        else if(winningAmount == 11)
        {
            StartCoroutine(effectSpawnerRight.SpawnItems("BigCoins", 1));
            StartCoroutine(effectSpawnerMiddle.SpawnItems("BigCoins", 1));
            StartCoroutine(effectSpawnerLeft.SpawnItems("BigCoins", 1));
        }
        else if (winningAmount == 1)
        {
            StartCoroutine(effectSpawnerRight.SpawnItems("JackpotCoins", 1));
            StartCoroutine(effectSpawnerMiddle.SpawnItems("JackpotCoins", 1));
            StartCoroutine(effectSpawnerLeft.SpawnItems("JackpotCoins", 1));
        }
        //Enable the SFX To loop and plays it
        winningCoinsSFX.loop = true;
        if (winningCoinsSFX.isActiveAndEnabled)
        {
            winningCoinsSFX.Play();
        }
        yield return null;

        sfxCalled--;
        //Stops only at the last time the function is called
        if (sfxCalled == 0)
        {
            winningCoinsSFX.loop = false;
        }
    }
    public IEnumerator DisplayingTheLose()
    {
        //Disabling the button to stop the player from betting
        machineButton.interactable = false;

        //Choosing an icon to display when losing
        selectRandomIcon = Random.Range(1, 14);
        int randomOne = Random.Range(1, 14);
        int randomTwo = Random.Range(1, 14);
        int randomThree = Random.Range(1, 14);


        //Choosing to bait or not to bait the player
        if (selectRandomIcon <= 4)
        {
            //Making sure it wont pick the same image 3 times
            while (randomTwo == randomOne)
            {
                yield return null;
                randomOne = Random.Range(1, 14);
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
                randomTwo = Random.Range(1, 14);
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

        //Amount of time the player lost stat
        int statsMachineLoses = PlayerPrefs.GetInt("StatsMachineLoses", 0);
        statsMachineLoses++;
        PlayerPrefs.SetInt("StatsMachineLoses", statsMachineLoses);


        //resuming the slot machine
        firstSlotControl.StopAnimation(false);
        yield return new WaitForSeconds(0.1f);

        secondSlotControl.StopAnimation(false);
        yield return new WaitForSeconds(0.1f);

        thirdSlotControl.StopAnimation(false);


        //enable the gambling
        machineButton.interactable = true;
    }

}