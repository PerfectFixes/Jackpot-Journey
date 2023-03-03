using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class RandomizerTutorial : MonoBehaviour
{

    #region Public Stats

    private bool isLostAllMoney;
    private bool isPressingOnCoin;

    [SerializeField] private DialogueManager dialogueManager;

    [SerializeField] private GameObject coinPrefab;
    [SerializeField] private Transform coinSpawnerTransfom;

    private int prestigeGoal;


    [Header("Randomizing Number")]
    [Tooltip("Picks a number between 1 - 10 the higher the number the bigger the prize")]
    [SerializeField] private int randomNumberPicker = 0;

    [Tooltip("Picks a number if its in the correct range the player wins a prize, else the player lose")]
    [SerializeField] private int isWinningNumber = 0;


    [Header("Player Stats")]
    [Tooltip("The amout of money the player has")]
    [SerializeField] private int playerMoney;

    [Tooltip("Counts the amount of clicks the player has made")]
    public int clickerCount;

    [Tooltip("Prestige is the level of the player and each time he achives the amount of money " +
        "needed to prestige the game resets and he gets a multiplier according to the level of the player ")]
    [SerializeField] private int prestigeLevel;




    [Header("Money and Winnings")]
    [Space(15)]

    [Tooltip("The amout of money the player will get from a BIG WIN")]
    [SerializeField] private int bigWinReward;

    [Tooltip("The amout of money the player will get from a MEDIUM WIN")]
    [SerializeField] private int mediumWinReward;

    [Tooltip("The amout of money the player will get from a SMALL WIN")]
    [SerializeField] private int smallWinReward;

    [Tooltip("The amout of money the player will LOSE from betting")]
    [SerializeField] private int bettingAmount;


    [Header("Sounds")]
    [Tooltip("The SFX for pressing the buttons")]
    [SerializeField] private AudioSource insertingCoinsSFX;



    [Header("Texts")]
    [Header("---UI---")]
    [Tooltip("The text to display the amount of money the player has")]
    [SerializeField] private TMP_Text playerMoneyText;

    [Tooltip("The text of the of the prestige button")]
    [SerializeField] private TMP_Text prestigeLevelText;

    [Tooltip("The text of of target amount to achive the prestige unlock")]
    [SerializeField] private TMP_Text prestigeGoalText;

    [Tooltip("The text of of target amount to achive the prestige unlock")]
    [SerializeField] private TMP_Text afkRewardText;

    [Tooltip("The text of of target amount to achive the prestige unlock")]
    [SerializeField] private TMP_Text smallRewardText;

    [Tooltip("The text of of target amount to achive the prestige unlock")]
    [SerializeField] private TMP_Text mediumRewardText;

    [Tooltip("The text of of target amount to achive the prestige unlock")]
    [SerializeField] private TMP_Text bigRewardText;





    [Header("Button")]
    [Tooltip("The button that generates money")]
    [SerializeField] private Image coinButtonImage;

    //[SerializeField] private Button machineButton;



    [SerializeField] private Button prestigeButton;
    [SerializeField] private Animator prestigeAnimator;

    [Tooltip("The AFK reward popup when you log in and get a reward")]
    [SerializeField] private GameObject afkRewardGameObject;

    [Tooltip("The toggle button of the auto gambling mode in the settings")]
    [SerializeField] private Toggle autoGambleToggle;

    [Tooltip("The different states of the coin when pressed")]
    [SerializeField] private Sprite[] coinSpriteAnimation;


    #endregion

    //Setting the animation of the winning/losing
    private DisplayWinOrLoseTutorial displayingResult;
    private void Awake()
    {
        PlayerPrefs.SetString("SFXToggleState", "True");

        PlayerPrefs.SetString("MusicToggleState", "True");
        //dialogueManager.StartDialogue(dialogueTrigger.dialogue);
        PlayerPrefs.SetInt("PrestigeLevel", 1);
        prestigeButton.interactable = false;

        //Checks to see if a playerPrefs exists if so set the correct amount of money or level
        playerMoney = 0;

        prestigeLevel = PlayerPrefs.GetInt("PrestigeLevel", 1);

      
        #region Setting the prestige goal
        if (prestigeLevel == 1)
        {
            prestigeGoal = 20;
        }
        
        prestigeGoalText.text = " Target TCoins: " + prestigeGoal;
        #endregion

        //Get the game master component 
        displayingResult = GameObject.Find("Game_Master").GetComponent<DisplayWinOrLoseTutorial>();

        //Display the level of the player
        prestigeLevelText.text = "Prestige Level: " + PlayerPrefs.GetInt("PrestigeLevel", 1).ToString();

        //Setting the amount of clicks to 0 
        clickerCount = 0;
        
            //Setting the amount of earning and losing
            bettingAmount = -1 * prestigeLevel;
            smallWinReward = 3 * prestigeLevel;
            mediumWinReward = 6 * prestigeLevel;
            bigWinReward = 16 * prestigeLevel;

            smallRewardText.text = "Reward Amount: " + smallWinReward + " TCoins";
            mediumRewardText.text = "Reward Amount: " + mediumWinReward + " TCoins"; ;
            bigRewardText.text = "Reward Amount: " + bigWinReward + " TCoins"; ;

        isLostAllMoney = false;
        isPressingOnCoin = false;
    }
    void Start()
    {
        //Update the player money in the UI
        playerMoneyText.text = playerMoney.ToString();
        playerMoneyText.text = $"{playerMoney:N0}";
        
        //coinButton.interactable = false;
        //settingsButton.interactable = false;
        
    }
    void Update()
    {  
        if (playerMoney >= prestigeGoal && !prestigeButton.interactable)
        {
            prestigeButton.interactable = true;
            prestigeAnimator.Play("Cycle_Prestige_Animation");
        }
        else if (playerMoney < prestigeGoal && prestigeButton.interactable)
        {
            prestigeAnimator.Play("Off_Prestige_Animation");
            prestigeButton.interactable = false;
        }
      
    }
    IEnumerator RandomizeNumber()
    {
        //Saving the amount of wins in a temp INT to add only at the end of the coroutine
        int winningAmount;

        playerMoneyText.text = playerMoney.ToString();
        playerMoneyText.text = $"{playerMoney:N0}";

        //Randomizing the number to know if the play can win a prize
        

        if (!isLostAllMoney)
        {
            isWinningNumber = 0;
        }
        else
        {
            isWinningNumber = Random.Range(3, 11);
        }
        // ************** FOR TESTING ONLY **************** //
        //Test the logic
        //isWinningNumber = 6;
        // ************** FOR TESTING ONLY **************** //

        if (isWinningNumber >= 5)
        {
            //Randomizing the prize that the player will get 
            randomNumberPicker = Random.Range(1, 11);
            // ************** FOR TESTING ONLY **************** //
            //Test the logic
            //randomNumberPicker = 10;
            // ************** FOR TESTING ONLY **************** //
            if (randomNumberPicker >= 1 && randomNumberPicker <= 6)
            {
                winningAmount = smallWinReward;
                print("The player got " + smallWinReward + " TCoins");
                StartCoroutine(displayingResult.DisplayTheWin(60, winningAmount));
            }
            else if (randomNumberPicker >= 7 && randomNumberPicker <= 9)
            {
                winningAmount = mediumWinReward;
                print("The player got " + mediumWinReward + " TCoins");
                StartCoroutine(displayingResult.DisplayTheWin(30, winningAmount));

            }
            else
            {
                winningAmount = bigWinReward;
                print("The player got " + bigWinReward + " TCoins");
                StartCoroutine(displayingResult.DisplayTheWin(10, winningAmount));
            }
        }
        else
        {
            //When the player rolls a bad number he loses
            print("Lost the bet");
            StartCoroutine(displayingResult.DisplayingTheLose());
        }
        //Waiting 1 second before reseting the stats of the gambling number
        yield return new WaitForSeconds(1);
        isWinningNumber = 0;
        randomNumberPicker = 0;

      
    }
    public void UpdatePlayerMoney(int money)
    {   
        playerMoney += money;
        playerMoneyText.text = playerMoney.ToString();

    }
    public void UpdatePlayerMoneyOnLost()
    {
        playerMoneyText.text = playerMoney.ToString();
        if (playerMoney == 0 && !isLostAllMoney)
        {
            //coinButton.interactable = true;
            isLostAllMoney = true;
            dialogueManager.StartDialogue(dialogueManager.dialogueTrigger.dialogue);
        }
    }
    public void GiveMoneyToPlayer()
    {
        if (playerMoney != 3)
        {
            for (int i = 0; i < 3; i++)
            {
                Instantiate(coinPrefab, coinSpawnerTransfom.position, Quaternion.identity);
            }
        }
        playerMoney = 3;
        playerMoneyText.text = playerMoney.ToString();    
    }
    public void StartGambling()
    {
        if (playerMoney >= 1)
        {
            if (insertingCoinsSFX.isActiveAndEnabled)
            {
                insertingCoinsSFX.Play();
            }
            //Lowering the money and starting the animation
            playerMoney += bettingAmount;

            //StartCoroutine(TextAnimation(bettingAmount));
            StartCoroutine(RandomizeNumber());
        }
        else
        {
            print("The player has no money");
        }

    }
    public void GainMoneyButton()
    {
        if (clickerCount == -69420)
        {
            print("the player is Prestigins");
        }
        else if (clickerCount >= 0 && clickerCount <= 9)
        {
            //machineButton.interactable = false;
            //Adds a click
            clickerCount++;

            //Set the sprite according to the click number
            coinButtonImage.sprite = coinSpriteAnimation[clickerCount];

            //Reset the amount of clicks and gives money
            if (clickerCount >= 10)
            {

                //coinButton.interactable = false;
                clickerCount = 0;

                playerMoney += 2 * prestigeLevel;//Multiply by prestige level to add more money

                for (int i = 0; i < 2; i++)
                {
                    Instantiate(coinPrefab, coinSpawnerTransfom.position, Quaternion.identity);
                }

                //Update the amount of money
                playerMoneyText.text = playerMoney.ToString();
                playerMoneyText.text = $"{playerMoney:N0}";

                if (!isPressingOnCoin)
                {
                    dialogueManager.StartDialogue(dialogueManager.dialogueTrigger.dialogue);
                    isPressingOnCoin = true;
                }
            }
        }
        else
        {
            print("Error, Resetting scene");
            SceneManager.LoadScene(0);
        }
    }
    public void PrestigeLevelUp()
    {
        if (playerMoney >= prestigeGoal && prestigeLevel == 1)
        {
            //Incase the loading takes a long time disable the option to gamble
            clickerCount = -69420;
            playerMoney -= prestigeGoal;

            //Update the amount of money and set it to minus so he wont be able to gamble again
            playerMoneyText.text = playerMoney.ToString();
            playerMoneyText.text = $"{playerMoney:N0}";
            playerMoney = -1000;

            //Reseting the amount of money the player has
            PlayerPrefs.DeleteKey("PlayerMoney");

            //Leveling up the player
            PlayerPrefs.SetInt("PrestigeLevel", 1);

            PlayerPrefs.SetString("TutorialComplete", "True");

            //Loading the same scene again to update the the game properties
            SceneManager.LoadScene("Game_Scene");
        }


    }


}

