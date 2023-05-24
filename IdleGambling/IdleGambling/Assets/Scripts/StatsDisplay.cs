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

        //Display the stats
        if (PlayerPrefs.GetInt("PrestigeLevel") >= 2)
        {
            statsText.text = "Stats:\n \n" + "Gamble machine pressed: " + gambleMachineBettingAmount + "\nGamble machine regular wins: " + gambleMachineRegularWins + "\nGamble machine Jackpot wins: " + gambleMachineJackpotWins + "\nTCoins won from the gamble machine: " + gambleMachineTCoinWins +
           "\nGamble machine lost: " + gambleMachineLoses + "\nTCoins Lost from the gamble machine: " + gambleMachineTCoinLoses + "\nTCoins button times pressed: " + TcoinClickAmount + "\nTCoins earned from TCoin button: " + TcoinGainedAmount + "\nTCoins gained from being AFK: " + TcoinGainedFromAFK;
        }
        else
        {
            statsText.text = "Stats:\n \n" + "Gamble machine pressed: " + gambleMachineBettingAmount + "\nGamble machine regular wins: " + gambleMachineRegularWins + "\nGamble machine Jackpot wins: " + gambleMachineJackpotWins + "\nTCoins won from the gamble machine: " + gambleMachineTCoinWins +
           "\nGamble machine lost: " + gambleMachineLoses + "\nTCoins Lost from the gamble machine: " + gambleMachineTCoinLoses + "\nTCoins button times pressed: " + TcoinClickAmount + "\nTCoins earned from TCoin button: " + TcoinGainedAmount;
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
