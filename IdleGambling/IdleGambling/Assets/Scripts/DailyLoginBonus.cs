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

    void Start()
    {
        //******************* FOR TESTING  *************************** //
        //currentDay = DateTime.Today.ToString("2023,02,17");
        //******************* FOR TESTING  *************************** //

        //Checks to see if the player got to level 2 and only then starts the counting the streak
        if(PlayerPrefs.GetInt("PrestigeLevel") >= 2)
        {
            DailyStreakBonus();
        }
        else
        {
            print("Resetting the streak, too early");
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

        //******************* FOR TESTING  *************************** //
        //currentDateTime = DateTime.ParseExact(currentDay, "yyyy,MM,dd", CultureInfo.CurrentCulture);
        //******************* FOR TESTING  *************************** //

        //Check to see if the player has already logged in today
        if (lastLogin == currentDay)
        {
            print("Logged in today");
            //If so do nothing and close the mathod
            return;
        }
        //Checks to see if the player didnt log in for more then 1 day
        if (lastLoginDateTime.AddDays(1) < currentDateTime)
        {
            print("Reset streak");
            //Reset the streak
            loginStreak = 0;
            PlayerPrefs.SetInt("StreakReward", 1);
        }
        print("Add 1 to the streak");
        //Add 1 to the streak and save it
        loginStreak++;
        PlayerPrefs.SetInt("LoginStreak", loginStreak);
        PlayerPrefs.SetString("LastLoginDate", currentDay);
    }
}
