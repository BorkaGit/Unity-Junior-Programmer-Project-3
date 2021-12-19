using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] obstaclePrefab;
    private Vector3 spawnPos = new Vector3(25, 0, 0);
    private float startDelay = 2.0f;
    private float repeatRate = 2.0f;
    private PlayerController playerControllerScript;
    void Start()
    {
        InvokeRepeating("SpawnObstacle",startDelay,repeatRate);
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    
    void Update()
    {
        
    }

    void SpawnObstacle()
    {
        if (playerControllerScript.gameOver == false)
        {
            int obstaclePrefabIndex=Random.Range(0,obstaclePrefab.Length);
            Instantiate(obstaclePrefab[obstaclePrefabIndex], spawnPos,
                obstaclePrefab[obstaclePrefabIndex].transform.rotation);
        }
    
    }
}
