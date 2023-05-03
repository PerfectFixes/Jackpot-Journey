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
        ListMaker();
    }
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
    }
    private void Update()
    {
        if (musicToggle.isOn && audioSource.volume < 0.25f && !isFadingSong)
        {
            print("Volume Up");
            audioSource.volume += 0.15f * Time.deltaTime;
        }
        else if (!musicToggle.isOn && audioSource.volume > 0 && !isFadingSong)
        {
            print("Volume Down");
            audioSource.volume -= 0.15f * Time.deltaTime;
        }
        
    }
    private void OnDisable()
    {
        //StopAllCoroutines();
        audioSource.volume = 0;
    }
    public IEnumerator SelectRandomSong()
    {

        yield return new WaitForSeconds(0.2f);
        //audioSource.mute = false;
        if (!audioSource.isPlaying)
        {
            print("new clip");
            audioSource.PlayOneShot(musicQueue[clipArray[clipCounter]]);
        }

        while (audioSource.isPlaying)
        {
            print("Playing");
            yield return new WaitForSeconds(0.5f);
        }

        clipCounter++;
        if(clipCounter == clipArray.Length)
        {
            clipCounter = 0;
        }
        print("Done");
        StartCoroutine(SelectRandomSong());
    }
    public IEnumerator FadeOut()
    {
        isFadingSong = true;
        for (int i = 0; i < 25; i++)
        {
            yield return new WaitForSeconds(0.04f);
            audioSource.volume -= 0.01f;
        }
        audioSource.mute = true;
        print("volumeLowered");
    }
}
