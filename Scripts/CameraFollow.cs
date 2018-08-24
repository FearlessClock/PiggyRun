using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {
    public GameObject toFollow;
    public Vector3 offset;
    private Vector3 velocity;
    private Vector3 GoToPoint;
    public float cameraMoveSpeedX;
    public float cameraMoveSpeedY;

    bool isRunning = false;
    float timer = 0;

    public GameStateManager GSM;

	// Use this for initialization
	void Start () {
        GoToPoint = new Vector3(toFollow.transform.position.x + offset.x, toFollow.transform.position.y + offset.y, -10);
        velocity = Vector3.zero;
	}
	
	// Update is called once per frame
	void Update () {
        if(GSM.gameState == GameState.GAMEPLAY)
        {
            FollowPlayer();
        }
        else if (GSM.gameState == GameState.MAIN_MENU)
        {
            this.transform.position = GoToPoint;
        }
        else if(GSM.gameState == GameState.SHOPPE)
        {
            this.transform.position = GoToPoint + Vector3.right * 3;
        }
        else if(GSM.gameState == GameState.RESTART)
        {
            if (isRunning == false)
            {
                isRunning = true;
                timer = 0;
            }
            timer += Time.deltaTime;
            if (timer > 1)
            {
                GoToPoint = new Vector3(toFollow.transform.position.x + offset.x, toFollow.transform.position.y + offset.y, -10);
                this.transform.position = Vector3.SmoothDamp(this.transform.position, toFollow.transform.position + offset, ref velocity, cameraMoveSpeedX / 20);
            }
        }
        else if(GSM.gameState == GameState.STARTING)
        {
            isRunning = false;
            timer = 0;
            FollowPlayer();
        }

    }

    void FollowPlayer()
    {
        GoToPoint = new Vector3(toFollow.transform.position.x + offset.x, toFollow.transform.position.y + offset.y, -10);
        this.transform.position = Vector3.SmoothDamp(this.transform.position, toFollow.transform.position + offset, ref velocity, cameraMoveSpeedX);
    }
}
