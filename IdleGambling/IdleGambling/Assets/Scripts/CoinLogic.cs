using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinLogic : MonoBehaviour
{  
    private Rigidbody2D rigidBody;
    private int verticalRandomize;
    private int horizontalRandomize;
    private Vector3 rotateRandom;
    private int spinDirection;
    private void Awake()
    {
        //Choose a direction to spin the Clover, Clicker, Coin
        spinDirection = Random.Range(1, 3);
        //Setting a random force of vertical and horizontal

        //If the game object is a coin then Give it a lower spining force
        if (gameObject.name == "Coin_Prefab(Clone)" || gameObject.name == "Coin_Prefab")
        {
            verticalRandomize = Random.Range(100, 300);
            horizontalRandomize = Random.Range(-300, 300);
        }
        else
        {
            verticalRandomize = Random.Range(50, 350);
            horizontalRandomize = Random.Range(-200, 200);
        }
     
        //Getting the rigidbody component
        rigidBody = gameObject.GetComponent<Rigidbody2D>();

        //Adding force in a random direction
        rigidBody.AddForce(new Vector2(horizontalRandomize, verticalRandomize));

        //Destroy the game object after 5 seoconds
        Destroy(gameObject, 10f);

        //Give the gameobject a random rotation
        rotateRandom = new Vector3(Random.Range(2, 10), Random.Range(2, 10), Random.Range(2, 10));
    }
    private void FixedUpdate()
    {
        //If the gameobject is a coin 
        if (gameObject.name == "Coin_Prefab(Clone)" || gameObject.name == "Coin_Prefab")
        {
            //Rotate the coin in a random direction
            gameObject.transform.Rotate(rotateRandom);
        }
        //If the gameobject is a clover 
        if (gameObject.name == "Clover_Prefab(Clone)" || gameObject.name == "Clover_Prefab")
        {
            if(spinDirection == 1)
            {
                gameObject.transform.Rotate(new Vector3(0, 0, 100) * Time.fixedDeltaTime);
            }
            else
            {
                gameObject.transform.Rotate(new Vector3(0, 0, -100) * Time.fixedDeltaTime);
            }
            
        }
        //If the gameobject is a clicker 
        if (gameObject.name == "Clicker_Prefab(Clone)" || gameObject.name == "Clicker_Prefab")
        {
            if (spinDirection == 1)
            {
                gameObject.transform.Rotate(new Vector3(0, 0, 100) * Time.fixedDeltaTime);
            }
            else
            {
                gameObject.transform.Rotate(new Vector3(0, 0, -100) * Time.fixedDeltaTime);
            }
            
        }
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Destroy the coin/clover/clicker if it leaves the screen
        if (collision.gameObject.CompareTag("Coin Destroyer"))
        {
            Destroy(gameObject);
        }
    }
}
