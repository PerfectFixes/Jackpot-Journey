using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.SceneManagement;
public class TimeCounter : MonoBehaviour
{
    private float timeInScene;
    private int minutes = 0;
    private int seconds = 0;
    private int hours = 0;

    private void Start()
    {
        StartCoroutine(UpdateTime());
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            SceneManager.LoadScene(0);
        }  
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
        print(timeString);

        //Repeat the function
        StartCoroutine(UpdateTime());
        
    }
}
