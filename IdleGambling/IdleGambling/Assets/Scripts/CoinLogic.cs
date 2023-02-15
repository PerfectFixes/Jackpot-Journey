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
        verticalRandomize = Random.Range(200, 700); 
        horizontalRandomize = Random.Range(-300, 300); 
        coinRigidBody =  gameObject.GetComponent<Rigidbody2D>();

        coinRigidBody.AddForce(new Vector2(horizontalRandomize, verticalRandomize ));

        Destroy(gameObject, 5f);

        rotateRandom = new Vector3(Random.Range(2, 10), Random.Range(2, 10), Random.Range(2, 10));
    }
    private void FixedUpdate()
    {
        gameObject.transform.Rotate(rotateRandom);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Coin Destroyer"))
        {
            Destroy(gameObject);
        }
    }
}
