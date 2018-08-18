using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorSpawner : MonoBehaviour {
    public GameObject floorPrefab;
    public GameObject underFloorPrefab;
    public Transform preplacedUnderFloor;
    public Transform preplacedFloor;
    public Transform player;
    public float distanceBetweenEachFloorCell;
    public float distanceToLoadAhead;
    public float maxHeight;
    public float minHeight;

    private Vector3 lastTilePlace;
    private Vector3 lastUnderfloorPlaced;


	// Use this for initialization
	void Start () {
        lastTilePlace = preplacedFloor.position;
        lastUnderfloorPlaced = preplacedUnderFloor.position;
    }
	
	// Update is called once per frame
	void Update () {
		if(Mathf.Abs(player.position.x - lastTilePlace.x) < distanceToLoadAhead)
        {
            float randomDistance = Random.Range(minHeight, maxHeight);
            Vector3 moveTo = new Vector3(distanceBetweenEachFloorCell + lastTilePlace.x, lastTilePlace.y + randomDistance, 0);
            GameObject floor = Instantiate<GameObject>(floorPrefab, moveTo, Quaternion.identity);
            floor.transform.parent = this.transform;
            lastTilePlace = moveTo;

            moveTo = new Vector3(distanceBetweenEachFloorCell + lastUnderfloorPlaced.x, lastUnderfloorPlaced.y + randomDistance, 2);
            GameObject underFloor = Instantiate<GameObject>(underFloorPrefab, moveTo, Quaternion.identity);
            underFloor.transform.parent = floor.transform;
            lastUnderfloorPlaced = moveTo;
        }
	}
}
