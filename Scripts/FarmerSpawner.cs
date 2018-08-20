using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FarmerSpawner : MonoBehaviour {

    public GameObject farmer;
    public GameObject player;

    public float minDistance = 5;

    public Transform startPosition;
    private Vector2 lastSpawnPosition;

    public float sizeOfBlocks;
    private float distanceToNextSpawn;
    public float randomAdd;

    public GameStateManager GSM;
    private GameState currentGameState;

    private bool firstRun = true;

    public LayerMask floorLayerMask;

    public GameState GetCurrentState()
    {
        return GSM.gameState;
    }


    private void Awake()
    {
        lastSpawnPosition = startPosition.transform.position;
        distanceToNextSpawn = Random.Range(0, randomAdd) + minDistance;
    }

    // Update is called once per frame
    void Update () {

        currentGameState = GetCurrentState();
        if(currentGameState == GameState.GAMEPLAY)
        {
            Gameplay();
        }
        else if(currentGameState == GameState.GAME_OVER)
        {
            GameOver();
        }
        else if(currentGameState == GameState.RESTART)
        {
            Restart();
        }
    }

    void Gameplay()
    {
        if(lastSpawnPosition.x - player.transform.position.x < minDistance)
        {
            Vector3 pos = new Vector3((((int)((lastSpawnPosition.x + distanceToNextSpawn)/3)) * 3 - 1.5f), lastSpawnPosition.y, 0);
            
            GameObject boer = Instantiate<GameObject>(farmer, pos, Quaternion.identity);

            //Check if the farmer is on the ground
            RaycastHit2D onGround = Physics2D.Raycast(boer.transform.position + Vector3.down*1f, Vector3.down, 20, floorLayerMask);
            //Apply gravity to make sure it is on the ground
            lastSpawnPosition = boer.transform.position;
            boer.transform.position += Vector3.down * onGround.distance;            

            boer.transform.parent = this.transform;


            distanceToNextSpawn = Random.Range(0, randomAdd) + minDistance;
        }
    }

    void GameOver()
    {
        lastSpawnPosition = startPosition.transform.position;
        //Clear all existing farmers
    }

    void Restart()
    {
        foreach (Transform child in this.transform)
        {
            GameObject.Destroy(child.gameObject);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(lastSpawnPosition, Vector3.one);
        Gizmos.DrawRay(lastSpawnPosition, Vector3.down);
    }
}
