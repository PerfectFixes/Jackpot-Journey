using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{

    public long playerMoney;
    public string playerMoneyAsString;


    void Start()
    {
        playerMoneyAsString = PlayerPrefs.GetString("PlayerMoneyTest");
        playerMoney = long.Parse(playerMoneyAsString);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            playerMoney++;
            playerMoneyAsString = playerMoney.ToString();
            PlayerPrefs.SetString("PlayerMoneyTest", playerMoneyAsString);
            playerMoneyAsString = PlayerPrefs.GetString("PlayerMoneyTest");
            playerMoney = long.Parse(playerMoneyAsString);
            print(playerMoneyAsString);
        }

        

    }
}
