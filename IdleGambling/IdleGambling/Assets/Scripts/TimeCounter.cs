using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;
using System;

public class TimeCounter : MonoBehaviour
{

    //The amount of time the player is in the scene
    private float timeInScene;
    public int targetSecondsForReward;

    //Time data
    private int minutes = 0;
    private int seconds = 0;
    private int hours = 0;
    public int score = 0;

    //Counts the amount of time the player holds the screen to leave the AFK scene

    [Header("Text")]
    [Tooltip("The text of the timer (that counts the elapsed time")]
    [SerializeField] private TMP_Text timerText;

    private void Start()
    {
        //Sets the amount of reward the player will get to 0
        score = 0;
        PlayerPrefs.SetInt("AFK Reward", 0);

        //Change the next reward to the curret one + 30 seconds
        targetSecondsForReward = 30;

        //Start the counter
        StartCoroutine(UpdateTime());
    }
    private void Update()
    {
      
    }
    //When the button is pressed the scene will change back to the start
    public void LeaveScene()
    {
        SceneManager.LoadScene("Game_Scene");
    }

    IEnumerator UpdateTime()
    {
        //Get the amount of time in the scene
        timeInScene = Time.timeSinceLevelLoad;

        //Round it into an int
        timeInScene = Mathf.RoundToInt(timeInScene);

        //Save the values into premade number to display in the game
        minutes = (int)(timeInScene / 60);
        seconds = (int)(timeInScene % 60);
        hours = (int)(minutes / 60);
        minutes = (int)(minutes % 60);

        //Wait 1 second to display the numbers on screen
        yield return new WaitForSeconds(1);
        
        //formatting the string into the correct format 
        string timeString = string.Format("{0:00}:{1:00}:{2:00}", hours, minutes, seconds);

        //Updating the UI
        timerText.text = timeString;

        //Add coins
        if (timeInScene == targetSecondsForReward)
        {
            score++;

            //Adds 30 seconds so the next reward will be after 30 seconds
            targetSecondsForReward += 30;

            //Update the reward in a playerprefs
            PlayerPrefs.SetInt("AFK Reward", score);
        }

        //Repeat the function
        StartCoroutine(UpdateTime());        
    }
}
