using System.Collections;
using TMPro;
using UnityEngine;


public class IncreaseLuckTimer : MonoBehaviour
{
    private static IncreaseLuckTimer instance;
    private Randomizer randomizer;

    [Tooltip("The text the amount of autoclicks left")]
    private TMP_Text buffCounterText;

     private EffectSpawner effectSpawnerRight;
     private EffectSpawner effectSpawnerMiddle;
     private EffectSpawner effectSpawnerLeft;


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
        effectSpawnerRight = GameObject.Find("Effect_Spawner_Right").GetComponent<EffectSpawner>();
        effectSpawnerMiddle = GameObject.Find("Effect_Spawner_Middle").GetComponent<EffectSpawner>();
        effectSpawnerLeft = GameObject.Find("Effect_Spawner_Left").GetComponent<EffectSpawner>();
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

        StartCoroutine(effectSpawnerRight.SpawnItems("IncreaseLuck",1));
        StartCoroutine(effectSpawnerMiddle.SpawnItems("IncreaseLuck", 1));
        StartCoroutine(effectSpawnerLeft.SpawnItems("IncreaseLuck", 1));

        for (int i = 60; i > 0; i--)
        {
            buffCounterText.text = "Seconds: \n" + i.ToString();
            yield return new WaitForSeconds(1);
        }
        randomizer.ToggleBetterLuck(false);
        buffCounterText.text = null;
    }
}
