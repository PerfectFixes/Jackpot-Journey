using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectSpawner : MonoBehaviour
{
    [SerializeField] private GameObject coinsGameObject;
    [SerializeField] private GameObject cloverGameObject;
    [SerializeField] private GameObject ClickerGameObject;

    public IEnumerator SpawnItems(string item, int amount)
    {
        if (item == "Coins")
        {
            Instantiate(coinsGameObject, transform.position, Quaternion.identity);
            yield return new WaitForSeconds(0.2f);
            Instantiate(coinsGameObject, transform.position, Quaternion.identity);
            //print("111 Spawning 2 coins");
        }
        else if (item == "SmallCoins")
        {
            for (int i = 0; i < 8; i++)
            {
                Instantiate(coinsGameObject, transform.position, Quaternion.identity);
                yield return new WaitForSeconds(0.1f);
            }
            //print("111 Spawning 5 coins");
        }
        else if (item == "MediumCoins")
        {
            for (int i = 0; i < 2; i++)
            {
                Instantiate(coinsGameObject, transform.position, Quaternion.identity);
                yield return new WaitForSeconds(0.1f);
            }
            //print("111 Spawning 10 coins");
        }
        else if (item == "BigCoins")
        {
            for (int i = 0; i < 60; i++)
            {
                Instantiate(coinsGameObject, transform.position, Quaternion.identity);
                yield return new WaitForSeconds(0.05f);
            }
            //print("111 Spawning 20 coins");
        }
        else if (item == "AutoClicker")
        {
            if(amount == 1)
            {
                for (int i = 0; i < 5; i++)
                {
                    Instantiate(ClickerGameObject, transform.position, Quaternion.identity);
                    yield return new WaitForSeconds(1f);
                }
                print("111 Spawning 1 autoclicker");
            }
            else if (amount == 2)
            {
                for (int i = 0; i < 10; i++)
                {
                    Instantiate(ClickerGameObject, transform.position, Quaternion.identity);
                    yield return new WaitForSeconds(1f);
                }
                print("111 Spawning 2 autoclicker");
            }
            else
            {
                for (int i = 0; i < 15; i++)
                {
                    Instantiate(ClickerGameObject, transform.position, Quaternion.identity);
                    yield return new WaitForSeconds(1f);
                }
                print("111 Spawning 3 autoclicker");
            }
            
        }
        else if(item == "IncreaseLuck")
        {
            for (int i = 0; i < 55; i++)
            {
                Instantiate(cloverGameObject, transform.position, Quaternion.identity);
                yield return new WaitForSeconds(1f);
            }
            print("111 Spawning 1 clover");
        }
    }
}
