using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DistanceCounter : MonoBehaviour {
    public Transform player;
    public Transform start;
    public Text UIText; 
    float distanceFromStart;    
	// Use this for initialization
	void Start () {
	    	
	}
	
	// Update is called once per frame
	void Update () {
		distanceFromStart = Mathf.Floor(Vector2.Distance(player.position, start.position));
        UIText.text = distanceFromStart.ToString();

    }
}
