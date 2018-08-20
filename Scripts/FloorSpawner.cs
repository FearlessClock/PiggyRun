using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorSpawner : MonoBehaviour {
    public GameStateManager GSM;
    public Transform startPosition;
    public GameObject floorPrefab;
    public Transform floorParent;
    public Transform preplacedFloor;
    public Transform player;
    public float distanceBetweenEachFloorCell;
    public float distanceToLoadAhead;
    public float maxHeight;
    public float minHeight;

    public int nmbrOfFloors;

    private Vector3 lastFloorPlace;

    private bool firstRestart = true;


	// Use this for initialization
	void Start () {
        lastFloorPlace = preplacedFloor.position;

        for(int i = 0; i < nmbrOfFloors; i++)
        {
            Vector3 moveTo = new Vector3(distanceBetweenEachFloorCell + lastFloorPlace.x, preplacedFloor.position.y, 0);
            GameObject floor = Instantiate<GameObject>(floorPrefab, moveTo, Quaternion.identity);
            floor.transform.parent = floorParent;
            lastFloorPlace = moveTo;

        }
    }
	
	// Update is called once per frame
	void Update () {
        if(GSM.gameState == GameState.GAMEPLAY)
        {
            firstRestart = true;
            GamePlay();
        }
        else if(GSM.gameState == GameState.GAME_OVER)
        {
            lastFloorPlace = startPosition.position;
        }
        else if(GSM.gameState == GameState.RESTART)
        {
            Restart();
        }
	}

    void GamePlay()
    {
        if (Mathf.Abs(player.position.x - lastFloorPlace.x) < distanceToLoadAhead)
        {
            float randomDistance = Random.Range(minHeight, maxHeight);
            float lerpedDistance = Mathf.Lerp(lastFloorPlace.y, randomDistance, 0.1f);
            Vector3 moveTo = new Vector3(distanceBetweenEachFloorCell + lastFloorPlace.x, lerpedDistance, 0);
            GameObject floor = Instantiate<GameObject>(floorPrefab, moveTo, Quaternion.identity);
            floor.transform.parent = floorParent;
            lastFloorPlace = moveTo;
        }
    }

    void Restart()
    {
        if (firstRestart)
        {
            firstRestart = false;
            lastFloorPlace = startPosition.position;
            foreach (Transform child in floorParent)
            {
                Destroy(child.gameObject);
            }
            lastFloorPlace = new Vector3(startPosition.position.x, -2, 0);
            //while (Mathf.Abs(player.position.x - lastFloorPlace.x) < distanceToLoadAhead)
            for (int i = 0; i < 11; i++)
            {
                Vector3 moveTo = lastFloorPlace + Vector3.right * distanceBetweenEachFloorCell;
                GameObject floor = Instantiate<GameObject>(floorPrefab, moveTo, Quaternion.identity);
                Debug.Log(floor.transform.position);
                floor.transform.parent = floorParent;
                lastFloorPlace = moveTo;
            }
        }
    }
}
