using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinLogic : MonoBehaviour
{

    
    private Rigidbody2D coinRigidBody;
    private int verticalRandomize;
    private int horizontalRandomize;
    private Vector3 rotateRandom;
    private void Awake()
    {
        //Setting a random force of vertical and horizontal
        verticalRandomize = Random.Range(200, 700); 
        horizontalRandomize = Random.Range(-300, 300); 

        //Getting the rigidbody component
        coinRigidBody =  gameObject.GetComponent<Rigidbody2D>();

        //Adding force in a random direction
        coinRigidBody.AddForce(new Vector2(horizontalRandomize, verticalRandomize));

        //Destroy the game object after 5 seoconds
        Destroy(gameObject, 5f);

        //Give the coin a random rotation
        rotateRandom = new Vector3(Random.Range(2, 10), Random.Range(2, 10), Random.Range(2, 10));
    }
    private void FixedUpdate()
    {
        //Rotate the coin in a random direction
        gameObject.transform.Rotate(rotateRandom);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Destroy the coin if it leaves the screen
        if (collision.gameObject.CompareTag("Coin Destroyer"))
        {
            Destroy(gameObject);
        }
    }
}
