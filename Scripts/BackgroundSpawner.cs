using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundSpawner : MonoBehaviour {
    public Transform startPosition;
    public GameStateManager GSM;
    public GameObject background;
    public GameObject[] backgroundTrees;
    public GameObject foreground;
    public GameObject[] foregroundTrees;
    public Transform player;
    public Transform levelStart;
    public LayerMask foreGroundFloorLayerMask;
    public LayerMask backGroundFloorLayerMask;

    public Vector3 treeOffsetBackground;
    public Vector3 lastBackgroundTreePos;
    private float distanceToNextBackgroundTree;
    public float minDistanceToNextBackgroundTree;
    public float heightToFirstPlaceBackgroundTree = 5;

    public Vector3 treeOffsetForeground;
    public Vector3 lastForegroundTreePos;
    private float distanceToNextForegroundTree;
    public float minDistanceToNextForegroundTree;
    public float heightToFirstPlaceForegroundTree = 5;

    private float timer = 0;
    private bool isRunning = false;
    private bool firstRestart = true;
    // Use this for initialization
    void Start () {
        distanceToNextBackgroundTree = minDistanceToNextBackgroundTree + Random.Range(0, 5);
        //lastBackgroundTreePos = levelStart.position + treeOffsetBackground;


        distanceToNextForegroundTree = minDistanceToNextForegroundTree + Random.Range(0, 5);
        //astForegroundTreePos = levelStart.position + treeOffsetForeground;
    }
	
	// Update is called once per frame
	void Update () {
        if(GSM.gameState == GameState.GAMEPLAY)
        {
            GamePlay();
        }
        else if(GSM.gameState == GameState.GAME_OVER)
        {
            
        }
        else if(GSM.gameState == GameState.RESTART)
        {
            if (!isRunning && firstRestart)
            {
                timer = 0;
                isRunning = true;
            }
            timer += Time.deltaTime;

            if (firstRestart && timer > 1)
            {
                foreach (Transform child in background.transform)
                {
                    GameObject.Destroy(child.gameObject);
                }
                foreach (Transform child in foreground.transform)
                {
                    GameObject.Destroy(child.gameObject);
                }

                lastBackgroundTreePos = startPosition.position + Vector3.right * 15 ;
                lastForegroundTreePos = startPosition.position + Vector3.right * 15;

                GamePlay();

                firstRestart = false;
                timer = 0;
                isRunning = false;

            }
        }
	    
    }

    void GamePlay()
    {
        if (lastBackgroundTreePos.x - player.position.x < 6)
        {
            distanceToNextBackgroundTree = minDistanceToNextBackgroundTree + Random.Range(0, 10);
            float uniformScale = Random.Range(0, 6);
            
            SpawnTree(backgroundTrees[0], ref lastBackgroundTreePos, distanceToNextBackgroundTree, heightToFirstPlaceBackgroundTree, treeOffsetBackground, uniformScale, background.transform, backGroundFloorLayerMask);
        }

        if (lastForegroundTreePos.x - player.position.x < 4)
        {
            distanceToNextForegroundTree = minDistanceToNextForegroundTree + Random.Range(0, 10);
            float uniformScale = Random.Range(3, 6);

            SpawnTree(foregroundTrees[0], ref lastForegroundTreePos, distanceToNextForegroundTree, heightToFirstPlaceForegroundTree, treeOffsetForeground, uniformScale, foreground.transform, foreGroundFloorLayerMask);

        }
    }

    void SpawnTree(GameObject treePrefab, ref Vector3 lastTreeLocation, 
                float distanceToNextTree, float heightToFirstPlaceTree, Vector3 treeOffset,
                float uniformScale, Transform parent, LayerMask floorLayerMask)
    {
        GameObject tree = Instantiate<GameObject>(treePrefab,
                            new Vector3(((int)((lastTreeLocation.x + distanceToNextTree) / 3)) * 3 - Random.Range(0.4f, 2.5f),
                                heightToFirstPlaceTree, 0), Quaternion.identity);

        tree.transform.localScale = Vector3.one * uniformScale;

        tree.transform.parent = parent;

        RaycastHit2D onGround = Physics2D.Raycast(tree.transform.position, Vector3.down, 30, floorLayerMask);
        tree.transform.position += Vector3.down * (onGround.distance + treeOffset.y);

        lastTreeLocation = tree.transform.position;

    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawRay(lastForegroundTreePos, Vector3.down);
    }
}
