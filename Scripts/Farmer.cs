using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Farmer : MonoBehaviour {
    public Transform catchPig;
    public LayerMask pigLayer;
    public Animator farmerAnimator;
    private int animatorJumpParam;
    private float distance;
    private Vector3 direction;

    
	// Use this for initialization
	void Start () {
        direction = catchPig.position - this.transform.position;
        distance = Vector3.Distance(catchPig.position, this.transform.position);
        animatorJumpParam = Animator.StringToHash("Jump");
        Debug.Log(direction + " " + distance);

    }
	
	// Update is called once per frame
	void Update () {
        bool isPig = Physics2D.BoxCast(this.transform.position, new Vector2(5, 3), 0, direction, distance, pigLayer);
        if (isPig)
        {
            Debug.Log("Found");
            farmerAnimator.SetBool(animatorJumpParam, true);
        }
	}

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireCube(this.transform.position, new Vector3(5, 3, 0));
    }
}
