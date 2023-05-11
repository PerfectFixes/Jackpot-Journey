using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Globalization;

public class DailyLoginBonus : MonoBehaviour
{
    private int loginStreak;
    private string currentDay;
    private string lastLogin;
    private DateTime lastLoginDateTime;
    private DateTime currentDateTime;

    void Awake()
    {

        //Checks to see if the player got to level 2 and only then starts the counting the streak
        if(PlayerPrefs.GetInt("PrestigeLevel") >= 2)
        {
            DailyStreakBonus();
        }
        else
        {
            PlayerPrefs.SetInt("LoginStreak", 1);
            gameObject.SetActive(false);
        }
        
    }
    private void DailyStreakBonus()
    {
        //Setting the day as the current day
        currentDay = DateTime.Today.ToString("yyyy,MM,dd");

        //Setting the last login date and if it doesnt exists sets it as the current day
        if (!PlayerPrefs.HasKey("LastLoginDate"))
        {
            lastLogin = currentDay;
            PlayerPrefs.SetString("LastLoginDate", currentDay);
        }
        else
        {
           
            lastLogin = PlayerPrefs.GetString("LastLoginDate");
        }

        //Setting the login streak
        loginStreak = PlayerPrefs.GetInt("LoginStreak", 1);

        //Parsing the last login date to be able to manipulate and check if there is a streak
        lastLoginDateTime = DateTime.ParseExact(lastLogin, "yyyy,MM,dd",CultureInfo.CurrentCulture);
        currentDateTime = DateTime.Today;

        //Check to see if the player has already logged in today
        if (lastLogin == currentDay)
        {
            //If so do nothing and close the mathod
            return;
        }
        //Checks to see if the player didnt log in for more then 1 day or less
        if (lastLoginDateTime.AddDays(1) < currentDateTime || lastLoginDateTime.AddDays(-1) >= currentDateTime)
        {
            //Reset the streak
            loginStreak = 0;
            PlayerPrefs.SetInt("StreakReward", 1);
        }
        //Add 1 to the streak and save it
        loginStreak++;
        PlayerPrefs.SetInt("LoginStreak", loginStreak);
        PlayerPrefs.SetString("LastLoginDate", currentDay);
    }
}
