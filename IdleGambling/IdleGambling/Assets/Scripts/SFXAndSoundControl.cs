using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SFXAndSoundControl : MonoBehaviour
{
    [SerializeField] private GameObject SfxControl;
    [SerializeField] private MusicLogic musicLogic;
    [SerializeField] private Toggle musicToggle;
    [SerializeField] private Toggle SfxToggle;
    [SerializeField] private AudioSource muteSettings;


    private void Awake()
    {
        //Sets the togglers to the currect state depends on how the player left them when he last logged in
        if (PlayerPrefs.GetString("MusicToggleState") == "True")
        {
            if (SceneManager.GetActiveScene().buildIndex == 0)
            {
                musicToggle.isOn = true;
            }      
        }
        else
        {
            musicToggle.isOn = false;
        }
        if (PlayerPrefs.GetString("SFXToggleState") == "True")
        {
            muteSettings.mute = true;
            SfxToggle.isOn = true;
            SfxControl.SetActive(true);
            muteSettings.mute = false;
        }
        else
        {
            muteSettings.mute = true;
            SfxControl.SetActive(false);
            SfxToggle.isOn = false;
            muteSettings.mute = false;
        }
    }
}
