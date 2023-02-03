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
    public float timeInScene;

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

    [Header("Material")]
    [Tooltip("The material who controls the animation")]
    [SerializeField] private Material cointMaterial;

    private float progress;
    private bool isReady;

    [Header("GameObjects")]
    [SerializeField] private GameObject smokeAnimation;

    private void Awake()
    {
        //disable the smoke animation
        smokeAnimation.SetActive(false);
    }
    private void Start()
    {
        //Sets the amount of reward the player will get to 0
        score = 0;

        isReady = false;
        cointMaterial.SetFloat("_Progress", 0);

        PlayerPrefs.SetInt("AFK Reward", 0);

        //Change the next reward to the curret one + 30 seconds
        targetSecondsForReward = 30;

        //Start the counter
        StartCoroutine(UpdateTime());
    }
    private void Update()
    {
        //Make the animation and make sure it will stop
        if (isReady)
        {
            float timeToComplete = 30;
            progress += Time.deltaTime / timeToComplete;

            cointMaterial.SetFloat("_Progress", progress);

            progress %= 1f;
        }


    }
    //When the button is pressed the scene will change back to the start
    public void LeaveScene()
    {
        score = Mathf.FloorToInt(Time.timeSinceLevelLoad / 30);

        //Update the reward in a playerprefs
        PlayerPrefs.SetInt("AFK Reward", score);

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

        //Update the time on screen
        yield return new WaitForFixedUpdate();

        //formatting the string into the correct format 
        string timeString = string.Format("{0:000}:{1:00}:{2:00}", hours, minutes, seconds);

        //Updating the UI
        timerText.text = timeString;

        //Repeat the function
        StartCoroutine(UpdateTime());

        //Add coins
        if (timeInScene >= targetSecondsForReward)
        {
            targetSecondsForReward += 30;

            smokeAnimation.SetActive(true);

            //Stops the animation from restarting until the next cycle
            isReady = false;

            //Reseting the progess
            progress = 0;
            cointMaterial.SetFloat("_Progress", progress);
        }
        isReady = true;
    }
    private void OnApplicationQuit()
    {
        score = Mathf.FloorToInt(Time.timeSinceLevelLoad / 30);

        //Update the reward in a playerprefs
        PlayerPrefs.SetInt("AFK Reward", score);
    }
}
