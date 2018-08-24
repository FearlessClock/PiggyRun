using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunShop : MonoBehaviour {

    public GameObject[] shopItems;
    public CoinStorage coinStorage;
    // Use this for initialization
	void Start () {
		foreach(GameObject item in shopItems)
        {
            GameObject obj = Instantiate<GameObject>(item);
            obj.transform.SetParent(this.transform, false);
        }
	}
}
