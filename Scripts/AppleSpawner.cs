using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppleSpawner : MonoBehaviour {

    public GameStateManager GSM;

    public GameObject food;
    public float chanceToSpawnFood;
    public float floatingHeight;

    public Transform player;
    public float inFrontOfPlayer;
    public float aboveTheGround;
    public LayerMask ground;

    private bool firstRestart = true;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if(GSM.gameState == GameState.GAMEPLAY)
        {
            firstRestart = true;
            if (Random.Range(0, 100) > chanceToSpawnFood)
            {
                Vector3 pos = player.position;
                pos += Vector3.right * inFrontOfPlayer;
                pos += Vector3.up * aboveTheGround;

                RaycastHit2D isGrounded = Physics2D.Raycast(pos, Vector2.down, 30, ground);
                pos += Vector3.down *( isGrounded.distance - floatingHeight);

                GameObject apple = Instantiate<GameObject>(food, pos, Quaternion.identity);

                apple.transform.parent = this.transform;
            }
        }
        else if (GSM.gameState == GameState.RESTART)
        {
            if (firstRestart)
            {
                foreach(Transform child in this.transform)
                {
                    Destroy(child.gameObject);
                }
                firstRestart = false;
            }
        }
	}
}
