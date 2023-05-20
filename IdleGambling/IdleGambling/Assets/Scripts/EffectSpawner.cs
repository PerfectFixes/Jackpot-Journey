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
        //Checks to see if the game object to spawn is Coins
        if (item == "Coins")
        {
            //Spawn coins in the spawner
            Instantiate(coinsGameObject, transform.position, Quaternion.identity);
            yield return new WaitForSeconds(0.2f);
            Instantiate(coinsGameObject, transform.position, Quaternion.identity);
        }
        //spawn coins corresponds to the amount of coins the player won (Fixed number)
        else if (item == "SmallCoins")
        {
            for (int i = 0; i < 20; i++)
            {
                Instantiate(coinsGameObject, transform.position, Quaternion.identity);
                yield return new WaitForSeconds(0.03f);
            }
        }
        else if (item == "MediumCoins")
        {
            for (int i = 0; i < 40; i++)
            {
                Instantiate(coinsGameObject, transform.position, Quaternion.identity);
                yield return new WaitForSeconds(0.03f);
            }
        }
        else if (item == "BigCoins")
        {
            for (int i = 0; i < 80; i++)
            {
                Instantiate(coinsGameObject, transform.position, Quaternion.identity);
                yield return new WaitForSeconds(0.03f);
            }
        }
        else if (item == "JackpotCoins")
        {
            for (int i = 0; i < 200; i++)
            {
                Instantiate(coinsGameObject, transform.position, Quaternion.identity);
                yield return new WaitForSeconds(0.03f);
            }
        }
        //Checks to see if the game object to spawn is the clicker
        else if (item == "AutoClicker")
        {
            //Spawn the amount of clickers according to the amount that the player got
            if(amount == 1)
            {
                for (int i = 0; i < 5; i++)
                {
                    Instantiate(ClickerGameObject, transform.position, Quaternion.identity);
                    yield return new WaitForSeconds(1f);
                }
            }
            else if (amount == 2)
            {
                for (int i = 0; i < 10; i++)
                {
                    Instantiate(ClickerGameObject, transform.position, Quaternion.identity);
                    yield return new WaitForSeconds(1f);
                }
            }
            else
            {
                for (int i = 0; i < 15; i++)
                {
                    Instantiate(ClickerGameObject, transform.position, Quaternion.identity);
                    yield return new WaitForSeconds(1f);
                }
            }
            
        }
        //Checks to see if the game object to spawn is the Clover
        else if (item == "IncreaseLuck")
        {
            //Spawn for the duration of the buff
            for (int i = 0; i < 55; i++)
            {
                Instantiate(cloverGameObject, transform.position, Quaternion.identity);
                yield return new WaitForSeconds(1f);
            }
        }
    }
}
