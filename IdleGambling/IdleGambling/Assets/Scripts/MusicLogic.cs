using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MusicLogic : MonoBehaviour
{
    [SerializeField] private AudioClip[] musicQueue;
    [SerializeField] private Toggle musicToggle;
    private AudioSource audioSource;
    public int[] clipArray = new int[20];
    public int clipCounter;
    public bool isFadingSong;

    private void Awake()
    {
        isFadingSong = false;
        audioSource = this.gameObject.GetComponent<AudioSource>();
        //Set the 4 in random order each time the level is loaded
        ListMaker();
    }
    //Stupid and useless logic for setting the order of the songs
    private void ListMaker()
    {
        for (int i = 0; i < clipArray.Length; i++)
        {
            clipArray[i] = Random.Range(0, 4);
            if (i <= 2 && i != 0)
            {
                while (clipArray[i - 1] == clipArray[i])
                {
                    clipArray[i] = Random.Range(0, 4);
                }
            }
            else if ( i >= 2)
            { 
                while (clipArray[i - 1] == clipArray[i] || clipArray[i - 2] == clipArray[i] || clipArray[i - 3] == clipArray[i])
                {
                    clipArray[i] = Random.Range(0, 4);
                }
            }
        }
        //Starts the playing the song
        StartCoroutine(SelectRandomSong());
    }
    private void Update()
    {
        //Lower or higher the volume of music manager according to the direction
        if (musicToggle.isOn && audioSource.volume < 0.25f && !isFadingSong)
        {
            audioSource.volume += 0.15f * Time.deltaTime;
        }
        else if (!musicToggle.isOn && audioSource.volume > 0 && !isFadingSong)
        {
            audioSource.volume -= 0.15f * Time.deltaTime;
        }
        
    }
    private void OnDisable()
    {
        audioSource.volume = 0;
    }
    //Playing the song
    public IEnumerator SelectRandomSong()
    {
        //Playing the songs if the music isnt playing
        yield return new WaitForSeconds(0.2f);
        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(musicQueue[clipArray[clipCounter]]);
        }
        //Checks to see if there is still a song playing
        while (audioSource.isPlaying)
        {
            yield return new WaitForSeconds(0.5f);
        }

        //Pass to the next song
        clipCounter++;
        //if it got to the max reset the list
        if(clipCounter == clipArray.Length)
        {
            clipCounter = 0;
        }
        StartCoroutine(SelectRandomSong());
    }
    //Fading out when moving to another scene
    public IEnumerator FadeOut()
    {
        isFadingSong = true;
        for (int i = 0; i < 25; i++)
        {
            yield return new WaitForSeconds(0.04f);
            audioSource.volume -= 0.01f;
        }
        audioSource.mute = true;
    }
}
