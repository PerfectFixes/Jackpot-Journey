using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IncreaseLuckTimer : MonoBehaviour
{
    private Randomizer randomizer;

    [Tooltip("The text the amount of autoclicks left")]
    private TMP_Text buffCounterText;

     private EffectSpawner effectSpawnerRight;
     private EffectSpawner effectSpawnerMiddle;
     private EffectSpawner effectSpawnerLeft;


    private void Awake()
    {
        buffCounterText = GameObject.Find("Buff_Counter_Text").GetComponent<TMP_Text>();
        randomizer = GameObject.Find("Randomize_Number").GetComponent<Randomizer>();
        effectSpawnerRight = GameObject.Find("Effect_Spawner_Right").GetComponent<EffectSpawner>();
        effectSpawnerMiddle = GameObject.Find("Effect_Spawner_Middle").GetComponent<EffectSpawner>();
        effectSpawnerLeft = GameObject.Find("Effect_Spawner_Left").GetComponent<EffectSpawner>();
    }
    //Display the amount of time left for the buff of luck
    public IEnumerator LuckTimer()
    {
        //Change the odds of winning
        randomizer.ToggleBetterLuck(true);

        //Spawning the clovers
        StartCoroutine(effectSpawnerRight.SpawnItems("IncreaseLuck",1));
        StartCoroutine(effectSpawnerMiddle.SpawnItems("IncreaseLuck", 1));
        StartCoroutine(effectSpawnerLeft.SpawnItems("IncreaseLuck", 1));

        //Counts 60 seconds
        for (int i = 60; i > 0; i--)
        {
            //Display the seconds in the UI
            buffCounterText.text = "Seconds: \n" + i.ToString();
            yield return new WaitForSeconds(1);
        }
        //Change back the odds
        randomizer.ToggleBetterLuck(false);

        //Disable the UI
        buffCounterText.text = null;
    }
}
