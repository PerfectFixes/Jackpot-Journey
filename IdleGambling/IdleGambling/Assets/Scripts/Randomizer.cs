using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Randomizer : MonoBehaviour
{
    
    #region Public Stats

    [SerializeField] private GameObject coinPrefab;
    [SerializeField] private EffectSpawner effectSpawnerRight;
    [SerializeField] private EffectSpawner effectSpawnerMiddle;
    [SerializeField] private EffectSpawner effectSpawnerLeft;
    //[SerializeField] private Transform coinSpawnerTransfom;

    [Tooltip("The AFK reward popup when you log in and get a reward")]
    [SerializeField] private GameObject afkRewardGameObject;

    private bool increasedLuck;
    private bool isStreakOn;
    private int multiplierAmount;
    private int prestigeGoal;
    private int autoClickerAmount;
    private IronSourceScript ironSourceScript;

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

    [Tooltip("The text of the prestige current goal")]
    [SerializeField] private TMP_Text prestigeGoalText;

    [Tooltip("The text of the reward after being afk ")]
    [SerializeField] private TMP_Text afkRewardText;

    [Tooltip("The text of the small win in the settings")]
    [SerializeField] private TMP_Text smallRewardText;

    [Tooltip("The text of the medium win in the settings")]
    [SerializeField] private TMP_Text mediumRewardText;

    [Tooltip("The text of the big win in the settings")]
    [SerializeField] private TMP_Text bigRewardText;

    [Tooltip("The text the amount of autoclicks left")]
    [SerializeField] private TMP_Text buffCounterText;


    [Header("Button")]
    [Tooltip("The button that generates money")]
    [SerializeField] private Image coinButtonImage;

    [SerializeField] private Button machineButton;

    [SerializeField] private Button prestigeButton;


    [Header("Toggler")]
    [Tooltip("The toggle button of the auto gambling mode in the settings")]
    [SerializeField] private Toggle autoGambleToggle;



    [Header("Animator")]

    [Tooltip("The animator that changes the colors or the prestige button")]
    [SerializeField] private Animator prestigeAnimator;

    [Tooltip("The animator of the scene loader to load new scene")]
    [SerializeField] private Animator sceneLoader;

    [Tooltip("The Auto clicker sprite that shows the auto clicking")]
    [SerializeField] private GameObject autoClickerGameObject;

    [Tooltip("The different states of the coin when pressed")]
    [SerializeField] private Sprite[] coinSpriteAnimation;



    #endregion

    //Setting the animation of the winning/losing
    private DisplayWinOrLoseIcon displayingResult;
    private void Awake()
    {
        ironSourceScript = GameObject.Find("IronSource").GetComponent<IronSourceScript>();

        increasedLuck = false;
        //Disabling the autoclicker text
        buffCounterText.text = null;

        //Sends the player to the Tutorial Scene if he didnt complete it
        if (PlayerPrefs.GetString("TutorialComplete") == "False" || PlayerPrefs.GetString("TutorialComplete") == "")
        {
            SceneManager.LoadScene("Tutorial");
        }

        autoClickerGameObject.SetActive(false);
        //Disable the prestige button
        prestigeButton.interactable = false;

        //Checks to see if a playerPrefs exists if so set the correct amount of money or level
        playerMoney = PlayerPrefs.GetInt("PlayerMoney", 0); 

        //Sets the prestige level to 1 if the player has no level.
        prestigeLevel = PlayerPrefs.GetInt("PrestigeLevel", 1);
     
        if(prestigeLevel >= 8)
        {
            if ((PlayerPrefs.GetInt("LoginStreak") >= 2) && (PlayerPrefs.GetInt("LoginStreak") <= 9))
            {
                PlayerPrefs.SetInt("StreakReward", 2);
            }
            else if ((PlayerPrefs.GetInt("LoginStreak") >= 10) && (PlayerPrefs.GetInt("LoginStreak") <= 29))
            {
                PlayerPrefs.SetInt("StreakReward", 3);
            }
            else if ((PlayerPrefs.GetInt("LoginStreak") >= 30) && (PlayerPrefs.GetInt("LoginStreak") <= 59))
            {
                PlayerPrefs.SetInt("StreakReward", 5);
            }
            else if (PlayerPrefs.GetInt("LoginStreak") >= 60)
            {
                PlayerPrefs.SetInt("StreakReward", 10);
            }

            isStreakOn = true;

            multiplierAmount = PlayerPrefs.GetInt("StreakReward",1);
        }
        else
        {
            isStreakOn = false;
        }

        #region Setting the prestige goal
        if (prestigeLevel == 1)
        {
            prestigeGoal = 100;         
        }
        else if (prestigeLevel == 2)
        {
            prestigeGoal = 500;
        }
        else if (prestigeLevel == 3)
        {
            prestigeGoal = 1000;
        }
        else if (prestigeLevel == 4)
        {
            prestigeGoal = 2500;
        }
        else if (prestigeLevel == 5)
        {
            prestigeGoal = 5000;
        }
        else if (prestigeLevel == 6)
        {
            prestigeGoal = 25000;
        }
        else if (prestigeLevel == 7)
        {
            prestigeGoal = 75000;
        }
        else if (prestigeLevel == 8)
        {
            prestigeGoal = 250000;
        }
        else if (prestigeLevel == 9)
        {
            prestigeGoal = 1000000;
        }
        else if (prestigeLevel == 10)
        {
            prestigeGoalText.text = " Target Coins: ??????";

            //maybe add text like break the game and let hell loss (multiplier by a lot and stuff like this)
        }
        prestigeGoalText.text = " Target Coins: " + prestigeGoal;
        #endregion

        //Get the game master component 
        displayingResult = GameObject.Find("Game_Master").GetComponent<DisplayWinOrLoseIcon>();

        //Display the level of the player
        prestigeLevelText.text = "Prestige Level: " + PlayerPrefs.GetInt("PrestigeLevel" , 1).ToString();

        //Setting the amount of clicks to 0 
        clickerCount = 0;


        int rewardAmount = PlayerPrefs.GetInt("AFK Reward");
        if (isStreakOn)
        {
            //Setting the amount of earning and losing
            bettingAmount = -1 * prestigeLevel;
            smallWinReward = 3 * prestigeLevel * multiplierAmount;
            mediumWinReward = 6 * prestigeLevel * multiplierAmount;
            bigWinReward = 16 * prestigeLevel * multiplierAmount;

            smallRewardText.text = "Reward Amount: " + smallWinReward;
            mediumRewardText.text = "Reward Amount: " + mediumWinReward;
            bigRewardText.text = "Reward Amount: " + bigWinReward;

            rewardAmount  = rewardAmount * PlayerPrefs.GetInt("PrestigeLevel") * multiplierAmount;
        }
        else
        {
            //Setting the amount of earning and losing
            bettingAmount = -1 * prestigeLevel;
            smallWinReward = 3 * prestigeLevel;
            mediumWinReward = 6 * prestigeLevel;
            bigWinReward = 16 * prestigeLevel;

            smallRewardText.text = "Reward Amount: " + smallWinReward + " Coins";
            mediumRewardText.text = "Reward Amount: " + mediumWinReward + " Coins"; ;
            bigRewardText.text = "Reward Amount: " + bigWinReward + " Coins"; ;

            //Setting the amount of coins the player will get for being afk
            rewardAmount *= PlayerPrefs.GetInt("PrestigeLevel");
        }

        //If there is no money gained from being afk dont display a message
        if (rewardAmount != 0)
        {
            //Display the message
            afkRewardGameObject.SetActive(true);

            //Different message for if you have different amount
            if (rewardAmount == 1)
            {
                afkRewardText.text = "You've Got only 1 coin";
            }
            else
            {
                //Display an easteregg on selected numbers
                if (rewardAmount == 69)
                {
                    afkRewardText.text = "You've Got 69 coins! Nice ;)";
                }
                else if (rewardAmount == 420)
                {
                    afkRewardText.text = "You've Got 420 coins! LIT :3";
                }
                else
                {
                    afkRewardText.text = "You've Got: " + rewardAmount + " Coins";
                }

            }
        }
        else
        {
            afkRewardGameObject.SetActive(false);
        }

    }
    void Start()
    {
        //Update the player money in the UI
        playerMoneyText.text = playerMoney.ToString();
        playerMoneyText.text = $"{playerMoney:N0}";
     
    }

    // ********************** DELETE ME BEFORE FINAL BUILD **************************
    public void ResetPlayerPrefs()
    {
        // ********************** DELETE ME BEFORE FINAL BUILD **************************
        PlayerPrefs.DeleteAll();
        // ********************** DELETE ME BEFORE FINAL BUILD **************************
        //PlayerPrefs.SetString("TutorialComplete", "True");
        // ********************** DELETE ME BEFORE FINAL BUILD **************************
        SceneManager.LoadScene(0);
        // ********************** DELETE ME BEFORE FINAL BUILD **************************
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            StartCoroutine(SceneTransaction());
        }
        if (autoGambleToggle.isOn && machineButton.interactable && playerMoney >= 1)
        {
            StartGambling();          
        }
        if(playerMoney >= prestigeGoal && !prestigeButton.interactable)
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
        isWinningNumber = Random.Range(1, 11);

        //If the ad of increasing luck has been activated the player has better odds of winning
        if (increasedLuck)
        {
            isWinningNumber = Random.Range(3, 11);
            print("SSS " + isWinningNumber);
        }
        
        // ************** FOR TESTING ONLY **************** //
        //Test the logic
        //isWinningNumber = 6;
        // ************** FOR TESTING ONLY **************** //

        if (isWinningNumber >= 6)
        {
            //Randomizing the prize that the player will get 
            randomNumberPicker = Random.Range(1, 11);
            
            //If the ad of increasing luck has been activated choose a better reward
            if (increasedLuck)
            {         
                randomNumberPicker = Random.Range(3, 11);
                print("SSS " + randomNumberPicker);
            }
            // ************** FOR TESTING ONLY **************** //
            //Test the logic
            //randomNumberPicker = 11;
            //randomNumberPicker = Random.Range(1, 11);
            // ************** FOR TESTING ONLY **************** //
            if (randomNumberPicker >= 1 && randomNumberPicker <= 6)
            {
                winningAmount = smallWinReward;
                print("The player got " + smallWinReward + " Coins");
                StartCoroutine(displayingResult.DisplayTheWin(60, winningAmount));
            }
            else if (randomNumberPicker >= 7 && randomNumberPicker <= 9)
            {
                winningAmount = mediumWinReward;
                print("The player got " + mediumWinReward + " Coins");
                StartCoroutine(displayingResult.DisplayTheWin(30, winningAmount));
                
            }
            else
            {
                winningAmount = bigWinReward;
                print("The player got " + bigWinReward + " Coins");
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
    public void ToggleBetterLuck(bool isOn)
    {
        if (isOn)
        {
            increasedLuck = true;
        }
        else
        {
            increasedLuck = false;
        }
    }
    public void UpdatePlayerMoney()
    {
        playerMoney = PlayerPrefs.GetInt("PlayerMoney");
        playerMoneyText.text = PlayerPrefs.GetInt("PlayerMoney").ToString();
        playerMoneyText.text = $"{PlayerPrefs.GetInt("PlayerMoney"):N0}";
    }
    public void StartGambling()
    {
        playerMoney = PlayerPrefs.GetInt("PlayerMoney");
        if (playerMoney >= 1)
        {
            if (insertingCoinsSFX.isActiveAndEnabled)
            {
                insertingCoinsSFX.Play();
            }
            //Lowering the money and starting the animation
            playerMoney += bettingAmount;
            PlayerPrefs.SetInt("PlayerMoney", playerMoney);

            //StartCoroutine(TextAnimation(bettingAmount));
            StartCoroutine(RandomizeNumber());
        }
        else
        {
            print("The player has no money");
        }
  
    }
    //Gives the player auto clicker
    public IEnumerator AutoClicker()
    {
        //Decide how much clicks the player will gain
        int randomClicks = Random.Range(1,4);
        if (randomClicks == 1)
        {
            autoClickerAmount = 50;
        }
        else if (randomClicks == 2)
        {
            autoClickerAmount = 100;
        }
        else if (randomClicks == 3)
        {
            autoClickerAmount = 150;
        }
        //Set the "auto clicker hand" to active
        autoClickerGameObject.SetActive(true);

        StartCoroutine(effectSpawnerRight.SpawnItems("AutoClicker", randomClicks));
        StartCoroutine(effectSpawnerMiddle.SpawnItems("AutoClicker", randomClicks));
        StartCoroutine(effectSpawnerLeft.SpawnItems("AutoClicker", randomClicks));

        //Click until the amount of click is over
        while (autoClickerAmount > 0)
        {
            //Lower the amount of clickss
            autoClickerAmount--;
            //Click the button
            GainMoneyButton();
            //Wait 0.1 seconds
            yield return new WaitForSeconds(0.1f);
            //Update the UI to display the amount of clicks left
            buffCounterText.text = "Clicks: \n" + autoClickerAmount.ToString();
        }
        //Disable the UI text of the amount of clicks and the auto clicker hand
        buffCounterText.text = null;
        autoClickerGameObject.SetActive(false);

    }

    public void GainMoneyButton()
    {
        if(clickerCount == -69420)
        {
            print("the player is Prestigins");
        }
        else if (clickerCount >= 0 && clickerCount <= 9)
        {
            //Adds a click
            clickerCount++;

            //Set the sprite according to the click number
            coinButtonImage.sprite = coinSpriteAnimation[clickerCount];

            //Reset the amount of clicks and gives money
            if (clickerCount >= 10)
            {
                clickerCount = 0;

                playerMoney = PlayerPrefs.GetInt("PlayerMoney");

                if (isStreakOn)
                {
                    playerMoney += 2 * prestigeLevel * multiplierAmount;//Multiply by prestige level and streak to add more money
                }
                else
                {
                    playerMoney += 2 * prestigeLevel;//Multiply by prestige level to add more money
                }
                StartCoroutine(effectSpawnerRight.SpawnItems("Coins", 1));
                StartCoroutine(effectSpawnerMiddle.SpawnItems("Coins", 1));
                StartCoroutine(effectSpawnerLeft.SpawnItems("Coins", 1));
                /*                for (int i = 0; i < prestigeLevel * 2; i++)
                                {
                                    Instantiate(coinPrefab, coinSpawnerTransfom.position, Quaternion.identity);
                                }*/
                //Update the player money in the UI
                PlayerPrefs.SetInt("PlayerMoney", playerMoney);
                playerMoneyText.text = playerMoney.ToString();
                playerMoneyText.text = $"{playerMoney:N0}";
            }

        }
        else
        {
            print("Error, Resetting scene");
            SceneManager.LoadScene(0);
        }
          
    }
    public void CloseAFKRewardTab()
    {
        if (isStreakOn)
        {
            //Sets the amount of money the player has got from being afk and multiply it by the Prestige Level and the streak amount
            playerMoney += PlayerPrefs.GetInt("AFK Reward") * prestigeLevel * multiplierAmount;
        }
        else
        {
            //Sets the amount of money the player has got from being afk and multiply it by the Prestige Level
            playerMoney += PlayerPrefs.GetInt("AFK Reward") * prestigeLevel;
        }
        //Sets the reward value to 0
        PlayerPrefs.SetInt("AFK Reward", 0);

        //Saves the amount of coins
        PlayerPrefs.SetInt("PlayerMoney", playerMoney);

        //Update the player money in the UI
        playerMoneyText.text = playerMoney.ToString();
        playerMoneyText.text = $"{playerMoney:N0}";

        //Sets the gameobject to inactive
        afkRewardGameObject.SetActive(false);
    }
    public void PrestigeLevelUp()
    {
        #region Prestige setting and saving
        if (playerMoney >= prestigeGoal && prestigeLevel == 1)
        {
            //Leveling up the player
            PlayerPrefs.SetInt("PrestigeLevel", 2);
        }

        else if (playerMoney >= prestigeGoal && prestigeLevel == 2)
        {
            //Leveling up the player
            PlayerPrefs.SetInt("PrestigeLevel", 3);
        }

        else if (playerMoney >= prestigeGoal && prestigeLevel == 3)
        {
            //Leveling up the player
            PlayerPrefs.SetInt("PrestigeLevel", 4);
        }

        else if (playerMoney >= prestigeGoal && prestigeLevel == 4)
        {
            //Leveling up the player
            PlayerPrefs.SetInt("PrestigeLevel", 5);
        }
        else if (playerMoney >= prestigeGoal && prestigeLevel == 5)
        {
            //Leveling up the player
            PlayerPrefs.SetInt("PrestigeLevel", 6);
        }
        else if (playerMoney >= prestigeGoal && prestigeLevel == 6)
        {
            //Leveling up the player
            PlayerPrefs.SetInt("PrestigeLevel", 7);
        }
        else if (playerMoney >= prestigeGoal && prestigeLevel == 7)
        {
            //Starting to count the login streak and making sure it is reseted
            PlayerPrefs.SetInt("LoginStreak", 1);
            PlayerPrefs.DeleteKey("LastLoginDate");

            //Leveling up the player
            PlayerPrefs.SetInt("PrestigeLevel", 8);
        }
        else if (playerMoney >= prestigeGoal && prestigeLevel == 8)
        {
            //Leveling up the player
            PlayerPrefs.SetInt("PrestigeLevel", 9);
        }
        else if (playerMoney >= prestigeGoal && prestigeLevel == 9)
        {
            //Leveling up the player
            PlayerPrefs.SetInt("PrestigeLevel", 10);
        }
        #endregion

        //Incase the loading takes a long time disable the option to gamble
        clickerCount = -69420;
        playerMoney -= prestigeGoal;

        //Update the amount of money and set it to minus so he wont be able to gamble again
        playerMoneyText.text = playerMoney.ToString();
        playerMoneyText.text = $"{playerMoney:N0}";
        playerMoney = -1000;

        //Reseting the amount of money the player has
        PlayerPrefs.DeleteKey("PlayerMoney");

        StartCoroutine(SceneTransaction());
    }
    IEnumerator SceneTransaction()
    {
        StartCoroutine(ironSourceScript.RestartAdTimer());
        sceneLoader.SetTrigger("Load_Scene");
        yield return new WaitForSeconds(1.25f);
        SceneManager.LoadScene("Game_Scene");
    }

}


//Animation the text of adding and subtracting amount from the player money
/*    IEnumerator TextAnimation(int amount)
    { 
        if(amount > 0)
        {
            yield return new WaitForSeconds(1.2f);
            rewardAndLoseText.text = "+ " + amount.ToString();
            rewardAndLoseAnimator.Play("FloatingUp");
            yield return new WaitForSeconds(0.3f);
            rewardAndLoseText.text = null;
        }
        else
        {
            rewardAndLoseText.text = amount.ToString();
            rewardAndLoseAnimator.Play("FloatingDown");
            yield return new WaitForSeconds(0.5f);
            rewardAndLoseText.text = null;
        }

    }*/