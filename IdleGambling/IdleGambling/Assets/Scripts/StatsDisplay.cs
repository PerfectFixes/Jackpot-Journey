using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StatsDisplay : MonoBehaviour
{

    [Tooltip("The text to display the amount of money the player has")]
    [SerializeField] private TMP_Text statsText;

    private int gambleMachineBettingAmount;

    private int gambleMachineWins;
    private int gambleMachineTCoinWins;

    private int gambleMachineLoses;
    private int gambleMachineTCoinLoses;

    private int TcoinClickAmount;
    private int TcoinGainedAmount;
   
    private void Awake()
    {
        //Get the updated results of the stats
        UpdateStats();
        
        //Display the stats
        statsText.text = "Stats:\n \n" + "Gamble machine pressed: " + gambleMachineBettingAmount + "\nGamble machine won: " + gambleMachineWins + "\nTCoins won from the gamble machine: " + gambleMachineTCoinWins + 
            "\nGamble machine lost: " + gambleMachineLoses + "\nTCoins Lost from the gamble machine: " + gambleMachineTCoinLoses + "\nTCoins button times pressed: " + TcoinClickAmount + "\nTCoins earned from TCoin button: " + TcoinGainedAmount;
    }

    private void UpdateStats()
    {
        gambleMachineTCoinWins = PlayerPrefs.GetInt("StatsTCoinsWon");
        gambleMachineWins = PlayerPrefs.GetInt("StatsMachineWins");

        gambleMachineLoses = PlayerPrefs.GetInt("StatsMachineLoses");
        gambleMachineTCoinLoses = PlayerPrefs.GetInt("StatsTCoinsLost");

        TcoinClickAmount = PlayerPrefs.GetInt("StatsTCoinPresses");
        TcoinGainedAmount = PlayerPrefs.GetInt("StatsTCoinGained");

        gambleMachineBettingAmount = gambleMachineWins + gambleMachineLoses;
    }
}
