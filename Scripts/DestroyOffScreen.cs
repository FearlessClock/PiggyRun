using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOffScreen : MonoBehaviour {

    public GameStateManager GSM;
    public Transform hitPosition;
    public Vector2 hitSize;
    public LayerMask mask;
    private Collider2D[] hits;
    private void Start()
    {
        hits = new Collider2D[100];
    }
    private void Update()
    {
        if(GSM.gameState == GameState.GAMEPLAY)
        {
            int nmbrOfHits = Physics2D.OverlapBoxNonAlloc(hitPosition.position, hitSize, 0, hits, mask);

            if (nmbrOfHits > 0)
            {
                for (int i = 0; i < nmbrOfHits; i++)
                {
                    Destroy(hits[i].gameObject);
                }
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawCube(hitPosition.position, hitSize);
    }
}
