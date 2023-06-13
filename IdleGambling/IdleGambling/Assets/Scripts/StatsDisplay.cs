                                                                                                        using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StatsDisplay : MonoBehaviour
{

    [Tooltip("The text to display the amount of money the player has")]
    [SerializeField] private TMP_Text statsText;

    private int gambleMachineBettingAmount;

    private int gambleMachineRegularWins;
    private int gambleMachineJackpotWins;
    private int gambleMachineTCoinWins;
 

    private int gambleMachineLoses;
    private int gambleMachineTCoinLoses;

    private int TcoinClickAmount;
    private int TcoinGainedAmount;

    private int TcoinGainedFromAFK;
   
    private void Awake()
    {
/*        PlayerPrefs.DeleteAll();
        PlayerPrefs.SetString("TutorialComplete", "True");
        PlayerPrefs.SetInt("PrestigeLevel", 7);*/
        //Get the updated results of the stats
        UpdateStats();

        //Display the stats according to the level
        if (PlayerPrefs.GetInt("PrestigeLevel") >= 2)
        {
            statsText.text = "Stats:\n \n" + "Gamble Machine:\n" + "Times pressed: " + gambleMachineBettingAmount + "\nRegular wins: " + gambleMachineRegularWins + "\nJackpot wins: " + gambleMachineJackpotWins + "\nTCoins earned: " + gambleMachineTCoinWins +
           "\nTimes lost: " + gambleMachineLoses + "\nTCoins Lost: " + gambleMachineTCoinLoses + "\n\nTCoin Button:\n" + "Times pressed: " + TcoinClickAmount + "\nTCoins earned: " + TcoinGainedAmount + "\n\nAFK Mode:\n" + "TCoins earned: " + TcoinGainedFromAFK;
        }
        else
        {
            statsText.text = "Stats:\n \n" + "Gamble Machine:\n" + "Times pressed: " + gambleMachineBettingAmount + "\nRegular wins: " + gambleMachineRegularWins + "\nJackpot wins: " + gambleMachineJackpotWins + "\nTCoins earned: " + gambleMachineTCoinWins +
          "\nTimes lost: " + gambleMachineLoses + "\nTCoins Lost: " + gambleMachineTCoinLoses + "\n\nTCoin Button:\n" + "Times pressed: " + TcoinClickAmount + "\nTCoins earned: " + TcoinGainedAmount;
        }
       
    }

    private void UpdateStats()
    {
        gambleMachineTCoinWins = PlayerPrefs.GetInt("StatsTCoinsWon");
        gambleMachineRegularWins = PlayerPrefs.GetInt("StatsMachineWins");
        gambleMachineJackpotWins = PlayerPrefs.GetInt("StatsMachineJackpotWins");

        gambleMachineLoses = PlayerPrefs.GetInt("StatsMachineLoses");
        gambleMachineTCoinLoses = PlayerPrefs.GetInt("StatsTCoinsLost");

        TcoinClickAmount = PlayerPrefs.GetInt("StatsTCoinPresses");
        TcoinGainedAmount = PlayerPrefs.GetInt("StatsTCoinGained");

        TcoinGainedFromAFK = PlayerPrefs.GetInt("StatsTCoinGainedFromAFK");

        gambleMachineBettingAmount = gambleMachineRegularWins + gambleMachineJackpotWins + gambleMachineLoses ;
    }
}
