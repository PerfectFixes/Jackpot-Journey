using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ManualFade : MonoBehaviour
{
    private AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = this.gameObject.GetComponent<AudioSource>();
        StartCoroutine(FadeIn());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private IEnumerator FadeIn()
    {
        yield return new WaitForSeconds(1.5f);
        audioSource.volume = 0;
        for (int i = 0; i < 25; i++)
        {
            yield return new WaitForSeconds(0.1f);
            audioSource.volume += 0.01f;
        }
        audioSource.volume = 0.25f;
    }
    public IEnumerator FadeOut()
    {
        for (int i = 0; i < 25; i++)
        {
            yield return new WaitForSeconds(0.1f);
            audioSource.volume -= 0.02f;
        }
        audioSource.volume = 0;
    }
}
