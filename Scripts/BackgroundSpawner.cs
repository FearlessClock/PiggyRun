using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundSpawner : MonoBehaviour {
    public GameObject background;
    public GameObject[] backgroundTrees;
    public GameObject foreground;
    public GameObject[] foregroundTrees;
    public Transform player;
    public Transform levelStart;

    public Vector3 treeOffsetBackground;
    private Vector3 lastBackgroundTreePos;
    private float distanceToNextBackgroundTree;
    public float minDistanceToNextBackgroundTree;

    public Vector3 treeOffsetForeground;
    private Vector3 lastForegroundTreePos;
    private float distanceToNextForegroundTree;
    public float minDistanceToNextForegroundTree;
    // Use this for initialization
    void Start () {
        distanceToNextBackgroundTree = minDistanceToNextBackgroundTree + Random.Range(0, 5);
        lastBackgroundTreePos = levelStart.position + treeOffsetBackground;


        distanceToNextForegroundTree = minDistanceToNextForegroundTree + Random.Range(0, 5);
        lastForegroundTreePos = levelStart.position + treeOffsetForeground;
    }
	
	// Update is called once per frame
	void Update () {
	    if(- player.position.x + lastBackgroundTreePos.x < 3)
        {
            distanceToNextBackgroundTree = minDistanceToNextBackgroundTree + Random.Range(0, 10);
            float uniformScale = Random.Range(0, 6);
            GameObject tree = Instantiate<GameObject>(backgroundTrees[0], lastBackgroundTreePos + new Vector3(distanceToNextBackgroundTree, 0, 0), Quaternion.identity);
            tree.transform.localScale = Vector3.one * uniformScale;
            tree.transform.parent = background.transform;
            lastBackgroundTreePos = new Vector3(tree.transform.position.x, treeOffsetBackground.y, lastBackgroundTreePos.z) ;
        }

        if (-player.position.x + lastForegroundTreePos.x < 3)
        {
            distanceToNextForegroundTree = minDistanceToNextForegroundTree + Random.Range(0, 10);
            float uniformScale = Random.Range(3, 6);
            GameObject tree = Instantiate<GameObject>(foregroundTrees[0], lastForegroundTreePos + new Vector3(distanceToNextForegroundTree, 0, 0), Quaternion.identity);
            tree.transform.localScale = Vector3.one * uniformScale;
            tree.transform.parent = foreground.transform;
            lastForegroundTreePos = new Vector3(tree.transform.position.x, treeOffsetForeground.y, lastForegroundTreePos.z);
        }
    }
}
