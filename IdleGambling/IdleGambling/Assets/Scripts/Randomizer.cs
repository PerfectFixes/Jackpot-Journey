using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Randomizer : MonoBehaviour
{

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




    [Header("Text")]
    [Header("---UI---")]
    [Tooltip("The text to display the amount of money the player has")]
    [SerializeField] private TMP_Text playerMoneyText;

    [Tooltip("The text of the of the prestige button")]
    [SerializeField] private TMP_Text prestigeText;  

    [Tooltip("The text of the of the prestige button")]
    [SerializeField] private TMP_Text prestigeGoalText;




    [Header("Button")]
    [Tooltip("The buttons that is over the mechine which makes the player able to gamble")]
    [SerializeField] private Button machineButton;

    [Tooltip("The button that generates money")]
    [SerializeField] private Image coinButtonImage;

    [Tooltip("The different states of the coin when pressed")]
    [SerializeField] private Sprite[] coinSpriteAnimation;

/*    [Tooltip("The text to display the amount of money the player get from winning or betting")]
    [SerializeField] private TMP_Text rewardAndLoseText;

    [Tooltip("The animator who controls the animation of the text of the winnings or the bettings")]
    [SerializeField] private Animator rewardAndLoseAnimator;*/

    //Setting the animation of the winning/losing
    private DisplayingTheWinOrLose displayingResult;
    private void Awake()
    {

        //Checks to see if a playerPrefs exists if so set the correct amount of money or level
        if (!PlayerPrefs.HasKey("PlayerMoney"))
        {  
            PlayerPrefs.SetInt("PlayerMoney", 0);
        }
        else
        {
            playerMoney = PlayerPrefs.GetInt("PlayerMoney");
        }

        if (!PlayerPrefs.HasKey("PrestigeLevel"))
        {
           
            PlayerPrefs.SetInt("PrestigeLevel", 1);
        }
        else
        {
            prestigeLevel = PlayerPrefs.GetInt("PrestigeLevel");
        }

        //Get the game master component 
        displayingResult = GameObject.Find("Game_Master").GetComponent<DisplayingTheWinOrLose>();

        //Display the level of the player
        prestigeText.text = "Prestige Level: " + PlayerPrefs.GetInt("PrestigeLevel").ToString();

        //Setting the amount of clicks to 0 
        clickerCount = 0;


        //Setting the amount of earning and losing
        bettingAmount = -1 * prestigeLevel;
        smallWinReward = 3 * prestigeLevel;
        mediumWinReward = 6 * prestigeLevel;
        bigWinReward = 16 * prestigeLevel;
    }
    void Start()
    {
        //Update the player money in the UI
        playerMoneyText.text = playerMoney.ToString();
        playerMoneyText.text = $"{playerMoney:N0}";
    }
    void Update()
    {
        
    }
    IEnumerator RandomizeNumber()
    {
        //Disabling the button to stop the player from betting
        machineButton.interactable = false;

        playerMoneyText.text =  playerMoney.ToString();
        playerMoneyText.text = $"{playerMoney:N0}";

        //Randomizing the number to know if the play can win a prize
        isWinningNumber = Random.Range(1, 11);

        if(isWinningNumber >= 5)
        {
            //Randomizing the prize that the player will get 
            randomNumberPicker = Random.Range(1, 11);
            if (randomNumberPicker >= 1 && randomNumberPicker <= 6)
            { 
                print("The player got " + smallWinReward + " Coins");
                StartCoroutine(displayingResult.DisplayTheWin(60));
                playerMoney += smallWinReward;
                PlayerPrefs.SetInt("PlayerMoney", playerMoney);
            }
            else if (randomNumberPicker >= 7 && randomNumberPicker <= 9)
            {
                print("The player got " + mediumWinReward + " Coins");
                StartCoroutine(displayingResult.DisplayTheWin(30));
                playerMoney += mediumWinReward;
                PlayerPrefs.SetInt("PlayerMoney", playerMoney);
            }
            else
            {
                print("The player got " + bigWinReward + " Coins");
                StartCoroutine(displayingResult.DisplayTheWin(10));
                playerMoney += bigWinReward;
                PlayerPrefs.SetInt("PlayerMoney", playerMoney);
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

        //Waiting half a second until letting the player gamble again and change the money
        yield return new WaitForSeconds(0.5f);

        //Update the amount of money
        playerMoneyText.text = playerMoney.ToString();
        playerMoneyText.text = $"{playerMoney:N0}";

        //Enabling the gambling again
        yield return null;
        displayingResult.ReadyToGamble();
        machineButton.interactable = true;
        ClearingEditorLog(); //Disable in build
    }

    public void StartGambling()
    {
        if(playerMoney >= 1)
        {
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

                playerMoney += 2 * prestigeLevel;//Multiply by prestige level to add more money


                //Display the new currency of the player
                if (machineButton.interactable)
                {
                    //Update the amount of money
                    playerMoneyText.text = playerMoney.ToString();
                    playerMoneyText.text = $"{playerMoney:N0}";
                }
                PlayerPrefs.SetInt("PlayerMoney", playerMoney);
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
        if (playerMoney >= 200 && prestigeLevel == 1)
        {
            //Incase the loading takes a long time disable the option to gamble
            clickerCount = -69420;
            playerMoney -= 200;

            //Update the amount of money and set it to minus so he wont be able to gamble again
            playerMoneyText.text = playerMoney.ToString();
            playerMoneyText.text = $"{playerMoney:N0}";
            playerMoney = -1000;

            //Reseting the amount of money the player has
            PlayerPrefs.DeleteKey("PlayerMoney");

            //Leveling up the player
            PlayerPrefs.SetInt("PrestigeLevel", 2);

            //Loading the same scene again to update the the game properties
            SceneManager.LoadScene(0);
        }

        else if (playerMoney >= 500 && prestigeLevel == 2)
        {
            //Incase the loading takes a long time disable the option to gamble
            clickerCount = -69420;
            playerMoney -= 500;

            //Update the amount of money and set it to minus so he wont be able to gamble again
            playerMoneyText.text = playerMoney.ToString();
            playerMoneyText.text = $"{playerMoney:N0}";
            playerMoney = -1000;

            //Reseting the amount of money the player has
            PlayerPrefs.DeleteKey("PlayerMoney");

            //Leveling up the player
            PlayerPrefs.SetInt("PrestigeLevel", 3);

            //Loading the same scene again to update the the game properties
            SceneManager.LoadScene(0);
        }

        else if (playerMoney >= 2000 && prestigeLevel == 3)
        {
            //Incase the loading takes a long time disable the option to gamble
            clickerCount = -69420;
            playerMoney -= 2000;

            //Update the amount of money and set it to minus so he wont be able to gamble again
            playerMoneyText.text = playerMoney.ToString();
            playerMoneyText.text = $"{playerMoney:N0}";
            playerMoney = -1000;

            //Reseting the amount of money the player has
            PlayerPrefs.DeleteKey("PlayerMoney");

            //Leveling up the player
            PlayerPrefs.SetInt("PrestigeLevel", 4);

            //Loading the same scene again to update the the game properties
            SceneManager.LoadScene(0);
        }

        else if (playerMoney >= 10000 && prestigeLevel == 4)
        {
            //Incase the loading takes a long time disable the option to gamble
            clickerCount = -69420;
            playerMoney -= 10000;

            //Update the amount of money and set it to minus so he wont be able to gamble again
            playerMoneyText.text = playerMoney.ToString();
            playerMoneyText.text = $"{playerMoney:N0}";
            playerMoney = -1000;

            //Reseting the amount of money the player has
            PlayerPrefs.DeleteKey("PlayerMoney");

            //Leveling up the player
            PlayerPrefs.SetInt("PrestigeLevel", 5);

            //Loading the same scene again to update the the game properties
            SceneManager.LoadScene(0);
        }
        else if (playerMoney >= 50000 && prestigeLevel == 5)
        {
            //Incase the loading takes a long time disable the option to gamble
            clickerCount = -69420;
            playerMoney -= 50000;

            //Update the amount of money and set it to minus so he wont be able to gamble again
            playerMoneyText.text = playerMoney.ToString();
            playerMoneyText.text = $"{playerMoney:N0}";
            playerMoney = -1000;

            //Reseting the amount of money the player has
            PlayerPrefs.DeleteKey("PlayerMoney");

            //Leveling up the player
            PlayerPrefs.SetInt("PrestigeLevel", 6);

            //Loading the same scene again to update the the game properties
            SceneManager.LoadScene(0);
        }
        else if (playerMoney >= 100000 && prestigeLevel == 6)
        {
            //Incase the loading takes a long time disable the option to gamble
            clickerCount = -69420;
            playerMoney -= 100000;

            //Update the amount of money and set it to minus so he wont be able to gamble again
            playerMoneyText.text = playerMoney.ToString();
            playerMoneyText.text = $"{playerMoney:N0}";
            playerMoney = -1000;

            //Reseting the amount of money the player has
            PlayerPrefs.DeleteKey("PlayerMoney");

            //Leveling up the player
            PlayerPrefs.SetInt("PrestigeLevel", 7);

            //Loading the same scene again to update the the game properties
            SceneManager.LoadScene(0);
        }
        else if (playerMoney >= 250000 && prestigeLevel == 7)
        {
            //Incase the loading takes a long time disable the option to gamble
            clickerCount = -69420;
            playerMoney -= 250000;

            //Update the amount of money and set it to minus so he wont be able to gamble again
            playerMoneyText.text = playerMoney.ToString();
            playerMoneyText.text = $"{playerMoney:N0}";
            playerMoney = -1000;

            //Reseting the amount of money the player has
            PlayerPrefs.DeleteKey("PlayerMoney");

            //Leveling up the player
            PlayerPrefs.SetInt("PrestigeLevel", 8);

            //Loading the same scene again to update the the game properties
            SceneManager.LoadScene(0);
        }
        else if (playerMoney >= 1000000 && prestigeLevel == 8)
        {
            //Incase the loading takes a long time disable the option to gamble
            clickerCount = -69420;
            playerMoney -= 1000000;

            //Update the amount of money and set it to minus so he wont be able to gamble again
            playerMoneyText.text = playerMoney.ToString();
            playerMoneyText.text = $"{playerMoney:N0}";
            playerMoney = -1000;

            //Reseting the amount of money the player has
            PlayerPrefs.DeleteKey("PlayerMoney");

            //Leveling up the player
            PlayerPrefs.SetInt("PrestigeLevel", 9);

            //Loading the same scene again to update the the game properties
            SceneManager.LoadScene(0);
        }
        else if (playerMoney >= 10000000 && prestigeLevel == 9)
        {
            //Incase the loading takes a long time disable the option to gamble
            clickerCount = -69420;
            playerMoney -= 10000000;

            //Update the amount of money and set it to minus so he wont be able to gamble again
            playerMoneyText.text = playerMoney.ToString();
            playerMoneyText.text = $"{playerMoney:N0}";
            playerMoney = -1000;

            //Reseting the amount of money the player has
            PlayerPrefs.DeleteKey("PlayerMoney");

            //Leveling up the player
            PlayerPrefs.SetInt("PrestigeLevel", 10);

            //Loading the same scene again to update the the game properties
            SceneManager.LoadScene(0);
        }

    }
    //Disable in build
    public void ClearingEditorLog()
    {
        var assembly = System.Reflection.Assembly.GetAssembly(typeof(UnityEditor.Editor));
        var type = assembly.GetType("UnityEditor.LogEntries");
        var method = type.GetMethod("Clear");
        method.Invoke(new object(), null);
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

}
