using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatObject : MonoBehaviour {

    public float floatingAmount;
    public float speed;
    private Vector3 startPosition;
    private float time;

	// Use this for initialization
	void Start () {
        startPosition = this.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
        this.transform.position = startPosition + Vector3.up * (Mathf.Sin(time) * floatingAmount);
        time += speed * Time.deltaTime;
	}
}
