using System.Collections;
using TMPro;
using UnityEngine;


public class IncreaseLuckTimer : MonoBehaviour
{
    private static IncreaseLuckTimer instance;
    private Randomizer randomizer;

    [Tooltip("The text the amount of autoclicks left")]
    private TMP_Text buffCounterText;



    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        buffCounterText = GameObject.Find("Buff_Counter_Text").GetComponent<TMP_Text>();
        randomizer = GameObject.Find("Randomize_Number").GetComponent<Randomizer>();
    }

    private void OnLevelWasLoaded(int level)
    {
        if (level == 0)
        {
            buffCounterText = GameObject.Find("Buff_Counter_Text").GetComponent<TMP_Text>();
            randomizer = GameObject.Find("Randomize_Number").GetComponent<Randomizer>();
        }

    }
    public IEnumerator LuckTimer()
    {
        randomizer.ToggleBetterLuck(true);
        for (int i = 60; i > 0; i--)
        {
            buffCounterText.text = "Seconds: \n" + i.ToString();
            yield return new WaitForSeconds(1);
        }
        randomizer.ToggleBetterLuck(false);
        buffCounterText.text = null;
    }
}
