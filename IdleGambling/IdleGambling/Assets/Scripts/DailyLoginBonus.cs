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
        if(PlayerPrefs.GetInt("PrestigeLevel") >= 8)
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
        
        currentDay = DateTime.Today.ToString("yyyy,MM,dd");

        lastLogin = PlayerPrefs.GetString("LastLoginDate", currentDay);

        loginStreak = PlayerPrefs.GetInt("LoginStreak", 1);

        lastLoginDateTime = DateTime.ParseExact(lastLogin, "yyyy,MM,dd",CultureInfo.CurrentCulture);
        currentDateTime = DateTime.Today;

        //******************* FOR TESTING  *************************** //
        //currentDateTime = DateTime.ParseExact(currentDay, "yyyy,MM,dd", CultureInfo.CurrentCulture);
        //******************* FOR TESTING  *************************** //

        if (lastLogin == currentDay)
        {
            //Player has already logged in today
            return;
        }
        if (lastLoginDateTime.AddDays(1) < currentDateTime)
        {
            loginStreak = 0;
            PlayerPrefs.SetInt("StreakReward", 1);
        }
        loginStreak++;
        PlayerPrefs.SetInt("LoginStreak", loginStreak);
        PlayerPrefs.SetString("LastLoginDate", currentDay);
    }
}
