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

    private bool isPrestiging;
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
    [SerializeField] private int JackpotReward;

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

    [SerializeField] private MusicLogic musicLogic;



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

    [Tooltip("The text of the Jackpot win in the settings")]
    [SerializeField] private TMP_Text JackpotText;

    [Tooltip("The text the amount of autoclicks left")]
    [SerializeField] private TMP_Text buffCounterText;



    [Header("Button")]
    [Tooltip("The button that generates money")]
    [SerializeField] private Image coinButtonImage;

    [SerializeField] private Button machineButton;

    [SerializeField] private Button prestigeButton;

    [SerializeField] private Button sleepModeButton;

    [SerializeField] private Button creditsSceneButton;

    [SerializeField] private Button statsSceneButton;


    [Header("Toggler")]
    [Tooltip("The toggle button of the auto gambling mode in the settings")]
    [SerializeField] private Toggle autoGambleToggle;



    [Header("Animator")]

    [Tooltip("The animator that changes the colors or the prestige button")]
    [SerializeField] private Animator prestigeAnimator;

    [Tooltip("The animator of the scene loader to load new scene")]
    [SerializeField] private Animator sceneLoader;
    
    [Tooltip("The animator of the Coin when pressing the button")]
    [SerializeField] private Animator coinButtonAnimator;

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

        isPrestiging = false;
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

        #region Setting the prestige goal
        if (prestigeLevel == 1)
        {
            prestigeGoal = 50;         
        }
        else if (prestigeLevel == 2)
        {
            prestigeGoal = 200;
        }
        else if (prestigeLevel == 3)
        {
            prestigeGoal = 600;
        }
        else if (prestigeLevel == 4)
        {
            prestigeGoal = 3000;
        }
        else if (prestigeLevel == 5)
        {
            prestigeGoal = 5000;
        }
        else if (prestigeLevel == 6)
        {
            prestigeGoal = 10000;
        }
        else if (prestigeLevel == 7)
        {
            prestigeGoal = 25000;
        }
        else if (prestigeLevel == 8)
        {
            prestigeGoal = 50000;
        }
        else if (prestigeLevel == 9)
        {
            prestigeGoal = 100000;
        }
        prestigeGoalText.text = " Target Coins: " + prestigeGoal;
        if (prestigeLevel == 10)
        {
            prestigeGoalText.text = " Target TCoins: INT OVERFLOW";
            //maybe add text like break the game and let hell loss (multiplier by a lot and stuff like this)
            prestigeGoal = 2147483640;
        }      
        #endregion

        //Get the game master component 
        displayingResult = GameObject.Find("Game_Master").GetComponent<DisplayWinOrLoseIcon>();

        //Display the level of the player
        prestigeLevelText.text = "Prestige Level: " + PlayerPrefs.GetInt("PrestigeLevel" , 1).ToString();

        //Setting the amount of clicks to 0 
        clickerCount = 0;
    }
    void Start()
    {
        
        //Sets the login streak reward and gives the correct buff according to the amount of days logged in
        if (prestigeLevel >= 4)
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

            multiplierAmount = PlayerPrefs.GetInt("StreakReward", 1);
        }
        else
        {
            isStreakOn = false;
        }

        //Sets the amount of coins the player needs to get from being afk
        int rewardAmount = PlayerPrefs.GetInt("AFK Reward");
        //if the player is level 4 enable the multiplier 
        if (prestigeLevel >= 4)
        {
            //Setting the amount of earning and losing
            bettingAmount = -1 * prestigeLevel;
            smallWinReward = 5 * prestigeLevel * multiplierAmount;
            mediumWinReward = 10 * prestigeLevel * multiplierAmount;
            bigWinReward = 25 * prestigeLevel * multiplierAmount;
            JackpotReward = 150 * prestigeLevel * multiplierAmount;

            smallRewardText.text = "Reward Amount:" + smallWinReward + " TCoins";
            mediumRewardText.text = "Reward Amount:" + mediumWinReward + " TCoins";
            bigRewardText.text = "Reward Amount:" + bigWinReward + " TCoins";
            JackpotText.text = "Reward Amount:" + JackpotReward + " TCoins";

            //Gives the correct amount of coins from being afk
            rewardAmount = rewardAmount * 2 * prestigeLevel * multiplierAmount;
        }
        else
        {
            //Setting the amount of earning and losing
            bettingAmount = -1 * prestigeLevel;
            smallWinReward = 5 * prestigeLevel;
            mediumWinReward = 10 * prestigeLevel;
            bigWinReward = 25 * prestigeLevel;
            JackpotReward = 150 * prestigeLevel;

            smallRewardText.text = "Reward Amount:" + smallWinReward + " TCoins";
            mediumRewardText.text = "Reward Amount:" + mediumWinReward + " TCoins";
            bigRewardText.text = "Reward Amount:" + bigWinReward + " TCoins";
            JackpotText.text = "Reward Amount:" + JackpotReward + " TCoins";

            //Gives the correct amount of coins from being afk
            rewardAmount = rewardAmount * 2 * prestigeLevel;
        }
        
        //If there is no money gained from being afk dont display a message
        if (rewardAmount != 0)
        {
            //Display the message
            afkRewardGameObject.SetActive(true);

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
        else
        {
            afkRewardGameObject.SetActive(false);
        }

        //Update the player money in the UI
        playerMoneyText.text = playerMoney.ToString();
        playerMoneyText.text = $"{playerMoney:N0}";
    }
    void Update()
    {
        //If the autogamble is on and the player has money
        if (autoGambleToggle.isOn && machineButton.interactable && playerMoney >= 1)
        {
            StartGambling();          
        }

        //Cycle the animation when getting to the prestige goal
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
        isWinningNumber = Random.Range(prestigeLevel, 101);

        //If the ad of increasing luck has been activated the player has better odds of winning
        if (increasedLuck)
        {
            isWinningNumber = Random.Range(20, 101);
        }
        if (isWinningNumber >= 66)
        {
            //Randomizing the prize that the player will get 
            randomNumberPicker = Random.Range(prestigeLevel, 101);

            //If the ad of increasing luck has been activated choose a better reward
            if (increasedLuck)
            {
                randomNumberPicker = Random.Range(20 + prestigeLevel, 101);
            }
            if (randomNumberPicker >= 1 && randomNumberPicker <= 55)
            {
                winningAmount = smallWinReward;
                StartCoroutine(displayingResult.DisplayTheWin(55, winningAmount));
            }
            else if (randomNumberPicker >= 56 && randomNumberPicker <= 88)
            {
                winningAmount = mediumWinReward;
                StartCoroutine(displayingResult.DisplayTheWin(33, winningAmount));
                
            }
            else if(randomNumberPicker >= 89 && randomNumberPicker <= 99)
            {
                winningAmount = bigWinReward;
                StartCoroutine(displayingResult.DisplayTheWin(11, winningAmount));              
            }
            else if(randomNumberPicker == 100)
            {
                winningAmount = JackpotReward;
                StartCoroutine(displayingResult.DisplayTheWin(1, winningAmount));

                //Amount of time the player won stat
                int statsJackpotWins = PlayerPrefs.GetInt("StatsMachineJackpotWins", 0);
                statsJackpotWins++;
                PlayerPrefs.SetInt("StatsMachineJackpotWins", statsJackpotWins);
            }
            else
            {
                winningAmount = smallWinReward;
                StartCoroutine(displayingResult.DisplayTheWin(55, winningAmount));
            }
        }
        else
        {
            //When the player rolls a bad number he loses
            StartCoroutine(displayingResult.DisplayingTheLose());
        }
        //Waiting 1 second before reseting the stats of the gambling number
        yield return new WaitForSeconds(1);
        isWinningNumber = 0;
        randomNumberPicker = 0;
    }
    //Activate the better luck when the player watch a video
    public void ToggleBetterLuck(bool isOn)
    {
        if (isOn)
        {
            increasedLuck = true;
            sleepModeButton.interactable = false;
            creditsSceneButton.interactable = false;
        }
        else
        {
            increasedLuck = false;
            sleepModeButton.interactable = true;
            creditsSceneButton.interactable = true;
        }
    }
    //A function to update the player money
    public void UpdatePlayerMoney()
    {
        playerMoney = PlayerPrefs.GetInt("PlayerMoney");
        playerMoneyText.text = PlayerPrefs.GetInt("PlayerMoney").ToString();
        playerMoneyText.text = $"{PlayerPrefs.GetInt("PlayerMoney"):N0}";
    }
    //Gamble machine logic
    public void StartGambling()
    {
        //Updates the amount of money the player has before starting to gamble
        playerMoney = PlayerPrefs.GetInt("PlayerMoney");
        if (playerMoney >= prestigeLevel)
        {
            //Play SFX if can
            if (insertingCoinsSFX.isActiveAndEnabled)
            {
                insertingCoinsSFX.Play();
            }
            //Lowering the money and starting the animation
            playerMoney += bettingAmount;
            PlayerPrefs.SetInt("PlayerMoney", playerMoney);

            //Updating stat of amount of coins lost from gamble
            int statsTcoinUpdated = PlayerPrefs.GetInt("StatsTCoinsLost", 0);
            statsTcoinUpdated += bettingAmount;
            PlayerPrefs.SetInt("StatsTCoinsLost", statsTcoinUpdated);

            //StartCoroutine(TextAnimation(bettingAmount));
            StartCoroutine(RandomizeNumber());
        }
    }
    //Gives the player auto clicker
    public IEnumerator AutoClicker()
    {
        //Disable the sleep mode scene & credit & stats
        sleepModeButton.interactable = false;
        creditsSceneButton.interactable = false;
        statsSceneButton.interactable = false;

        //Decide how much clicks the player will gain
        int randomClicks = Random.Range(1,4);
        if (randomClicks == 1)
        {
            autoClickerAmount = 45;
        }
        else if (randomClicks == 2)
        {
            autoClickerAmount = 105;
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
        //Reanble the buttons
        sleepModeButton.interactable = true;
        creditsSceneButton.interactable = true;
        statsSceneButton.interactable = true;

        //Disable the UI text of the amount of clicks and the auto clicker hand
        buffCounterText.text = null;
        autoClickerGameObject.SetActive(false);
    }
    //The TCoin button logic
    public void GainMoneyButton()
    {
        //playes the animation of the TCoin that is filling up
        if (clickerCount >= 0 && clickerCount <= 14 && !isPrestiging)
        {
            //Make the button smaller then bigger animation
            coinButtonAnimator.Play("Button_Press");
            //Adds a click
            clickerCount++;

            //Amount of clicks on the TCoin stat
            int statsTcoinClickUpdated = PlayerPrefs.GetInt("StatsTCoinPresses", 0);
            statsTcoinClickUpdated++;
            PlayerPrefs.SetInt("StatsTCoinPresses", statsTcoinClickUpdated);

            //Set the sprite according to the click number
            coinButtonImage.sprite = coinSpriteAnimation[clickerCount];

            //Reset the amount of clicks and gives money
            if (clickerCount >= 15)
            {
                clickerCount = 0;

                playerMoney = PlayerPrefs.GetInt("PlayerMoney");

                if (isStreakOn)
                {
                    playerMoney += 2 * prestigeLevel * multiplierAmount;//Multiply by prestige level and streak to add more money

                    //Amount of clicks on the TCoin stat
                    int statsTcoinGained = PlayerPrefs.GetInt("StatsTCoinGained", 0);
                    statsTcoinGained += 2 * prestigeLevel * multiplierAmount;
                    PlayerPrefs.SetInt("StatsTCoinGained", statsTcoinGained);
                }
                else
                {
                    playerMoney += 2 * prestigeLevel;//Multiply by prestige level to add more money

                    //Amount of clicks on the TCoin stat
                    int statsTcoinGained = PlayerPrefs.GetInt("StatsTCoinGained", 0);
                    statsTcoinGained += 2 * prestigeLevel;
                    PlayerPrefs.SetInt("StatsTCoinGained", statsTcoinGained);
                }
               
                //Spawn coins
                StartCoroutine(effectSpawnerRight.SpawnItems("Coins", 1));
                StartCoroutine(effectSpawnerMiddle.SpawnItems("Coins", 1));
                StartCoroutine(effectSpawnerLeft.SpawnItems("Coins", 1));

                //Update the player money in the UI
                PlayerPrefs.SetInt("PlayerMoney", playerMoney);
                playerMoneyText.text = playerMoney.ToString();
                playerMoneyText.text = $"{playerMoney:N0}";
            }

        }
        else if (isPrestiging)
        {
            return;
        }
        else
        {
            //If there is an error in the animation restart the scene
            SceneManager.LoadScene(0);
        }
          
    }
    public void CloseAFKRewardTab()
    {
        //Get the amount of reward
        int rewardAmount = PlayerPrefs.GetInt("AFK Reward");


        if (isStreakOn)
        {
            //Multiple the amount of reward to be biger
            rewardAmount = rewardAmount * 2 * PlayerPrefs.GetInt("PrestigeLevel") * multiplierAmount;
        }
        else
        {
            //Multiple the amount of reward to be biger
            rewardAmount = rewardAmount * 2 * PlayerPrefs.GetInt("PrestigeLevel");
        }
        
        //Add the amount to the player money
        playerMoney += rewardAmount;

        PlayerPrefs.SetInt("StatsTCoinGainedFromAFK", rewardAmount);

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
            //Starting to count the login streak and making sure it is reseted
            PlayerPrefs.SetInt("LoginStreak", 1);
            PlayerPrefs.DeleteKey("LastLoginDate");

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
        isPrestiging = true;
        playerMoney -= prestigeGoal;

        //Saves the amount of coins
        PlayerPrefs.SetInt("PlayerMoney", playerMoney);

        //Update the amount of money and set it to minus so he wont be able to gamble again
        playerMoneyText.text = playerMoney.ToString();
        playerMoneyText.text = $"{playerMoney:N0}";
        playerMoney = 0;

        if(PlayerPrefs.GetInt("PrestigeLevel") != 10)
        {
            StartCoroutine(SceneTransaction());
        }
        else
        {
            StartCoroutine(TheEndScene());
        }
    }
    IEnumerator TheEndScene()
    {
        //Fade out the music 
        StartCoroutine(musicLogic.FadeOut());

        //Reset the timer of the ads
        StartCoroutine(ironSourceScript.RestartAdTimer());

        //Play the animation to change scene
        sceneLoader.SetTrigger("Load_Scene");

        yield return new WaitForSeconds(1.25f);

        //Reload the scene
        SceneManager.LoadScene("The_End");

    }
    IEnumerator SceneTransaction()
    {
        //Fade out the music 
        StartCoroutine(musicLogic.FadeOut());

        //Reset the timer of the ads
        StartCoroutine(ironSourceScript.RestartAdTimer());

        //Play the animation to change scene
        sceneLoader.SetTrigger("Load_Scene");

        yield return new WaitForSeconds(1.25f);

        //Reload the scene
        SceneManager.LoadScene("Game_Scene");
    }

}