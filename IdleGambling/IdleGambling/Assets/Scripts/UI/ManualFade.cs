using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ManualFade : MonoBehaviour
{
    private AudioSource audioSource;

    void Start()
    {
        audioSource = this.gameObject.GetComponent<AudioSource>();
        StartCoroutine(FadeIn());
    }

    //Fade the song in at the start
    private IEnumerator FadeIn()
    {
        yield return new WaitForSeconds(1.5f);
        //Sets the music to 0
        audioSource.volume = 0;
        for (int i = 0; i < 25; i++)
        {
            yield return new WaitForSeconds(0.1f);
            audioSource.volume += 0.01f;
        }
        //Verify the music is at 0.25
        audioSource.volume = 0.25f;
    }
    //Fade the music out
    public IEnumerator FadeOut()
    {
        for (int i = 0; i < 25; i++)
        {
            yield return new WaitForSeconds(0.1f);
            audioSource.volume -= 0.02f;
        }
        //Verify the music is at 0
        audioSource.volume = 0;
    }
}
