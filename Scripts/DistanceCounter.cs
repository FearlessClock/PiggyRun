using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DistanceCounter : MonoBehaviour {
    public Transform player;
    public Transform start;
    public Vector3 startOffset;
    public Text UIText; 
    float distanceFromStart;
    public GameStateManager GSM;
	// Use this for initialization
	void Start () {
	    	
	}
	
	// Update is called once per frame
	void Update () {
        
        if(GSM.gameState == GameState.GAMEPLAY)
        {
            distanceFromStart = Mathf.Max(Mathf.Floor(Vector2.Distance(player.position, start.position) - startOffset.x), 0);
            UIText.text = distanceFromStart.ToString();
        }
        else if(GSM.gameState == GameState.STARTING)
        {
            startOffset = player.position - start.position;
        }
        else if(GSM.gameState == GameState.RESTART)
        {
            distanceFromStart = 0;
            UIText.text = distanceFromStart.ToString();
        }
    }
}
