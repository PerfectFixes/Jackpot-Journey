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
        if (PlayerPrefs.GetString("MusicToggleState") == "True")
        {
            if(SceneManager.GetActiveScene().buildIndex == 0)
            {
                StartCoroutine(musicLogic.SelectRandomSong());
            }           
            musicToggle.isOn = true;
        }
        else
        {
            //musicLogic.SetActive(false);
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
