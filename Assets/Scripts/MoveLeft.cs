using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveLeft : MonoBehaviour
{
    private float speed = 20.0f;
    private float lowSpeed = 5.0f;
    private PlayerController playerControllerScript;
    private float leftBound = -15.0f;
    private float speedBoosted = 40.0f;
    void Start()
    {
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    
    void Update()
    {
        if (playerControllerScript.gameOver == false && playerControllerScript.doubleSpeed==false )
        {
            if (playerControllerScript.walkEnter == false)
            {
                transform.Translate(Vector3.left * Time.deltaTime * speed);
            }else if (playerControllerScript.walkEnter == true)
            {
                transform.Translate(Vector3.left * Time.deltaTime * lowSpeed);
            }
        }
        if (playerControllerScript.gameOver == false && playerControllerScript.doubleSpeed==true  && playerControllerScript.walkEnter==false)
        {
            transform.Translate(Vector3.left * Time.deltaTime * speed * 2 );
        }
        if (transform.position.x < leftBound && gameObject.CompareTag("Obstacle")  && playerControllerScript.walkEnter==false)
        {
            Destroy(gameObject);
        }
        
    }
}
