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
            GoToPoint = new Vector3(toFollow.transform.position.x + offset.x, toFollow.transform.position.y + offset.y, -10);
            this.transform.position = Vector3.SmoothDamp(this.transform.position, toFollow.transform.position + offset, ref velocity, cameraMoveSpeedX);
            //this.transform.position = new Vector3(Mathf.Lerp(this.transform.position.x, GoToPoint.x, Time.deltaTime * cameraMoveSpeedX),
            //                                       Mathf.Lerp(this.transform.position.y, GoToPoint.y, Time.deltaTime * cameraMoveSpeedY), -10);
        }
        else if (GSM.gameState == GameState.MAIN_MENU)
        {
            this.transform.position = GoToPoint;
        }
        else if(GSM.gameState == GameState.RESTART)
        {
            GoToPoint = new Vector3(toFollow.transform.position.x + offset.x, toFollow.transform.position.y + offset.y, -10);
            this.transform.position = Vector3.SmoothDamp(this.transform.position, toFollow.transform.position + offset, ref velocity, cameraMoveSpeedX);
        }

    }
}
