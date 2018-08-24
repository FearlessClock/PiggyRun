using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Apple : MonoBehaviour {
    private GameStateManager GSM;

    public LayerMask player;
	// Use this for initialization
	void Start () {
        GSM = FindObjectOfType<GameStateManager>();
	}
	
	// Update is called once per frame
	void Update () {
		if(GSM.gameState == GameState.GAMEPLAY)
        {
            Collider2D isPickedUp = Physics2D.OverlapCircle(this.transform.position, 0.7f, player);
            if (isPickedUp)
            {
                GSM.addToCoins(1);
                Destroy(this.gameObject);
            }
        }
	}
}
