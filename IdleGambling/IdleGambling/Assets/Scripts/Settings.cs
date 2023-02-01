using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Settings : MonoBehaviour
{

    [SerializeField] private GameObject settingsGameObject;

    private void Awake()
    {
        settingsGameObject.SetActive(false);
    }

    private void Update()
    {
    
      
    }
    public void OpenOrCloseSettingsTab(string settingsState)
    {
        if(settingsState == "Open")
        {
            settingsGameObject.SetActive(true);
        }
        else
        {
            settingsGameObject.SetActive(false);
        }
    }

    public void SleepMode()
    {
        SceneManager.LoadScene("AFK_Scene");
    }
    public void ResetPlayerPrefs()
    {
        PlayerPrefs.DeleteAll();
        SceneManager.LoadScene(0);
    }
    
}
