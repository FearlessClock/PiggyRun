using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLaunch : MonoBehaviour {
    public GameObject spawner;
    public Animator animator;

    private void Start()
    {
        
    }

    private void Update()
    {
        if (animator.GetBool("Open"))
        {
            spawner.SetActive(true);
        }
        else
        {
            spawner.SetActive(false);
        }
    }
}
