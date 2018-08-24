using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour {
    private bool audioOnState;
    public Image soundButtonIcon;
    public Sprite soundOnIcon;
    public Sprite soundOffIcon;

    public AudioSource backgroundMusic;
	// Use this for initialization
	void Start () {
        audioOnState = PlayerPrefs.GetInt("AudioOnState", 0) == 0 ? false : true;

        SetAudioButtonSprite();

        SetBackgroundMusicState();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ChangeSoundState()
    {
        audioOnState = !audioOnState;

        SetAudioButtonSprite();

        SetBackgroundMusicState();
    }

    private void SetBackgroundMusicState()
    {
        if (audioOnState)
        {
            if (!backgroundMusic.isPlaying)
            {
                backgroundMusic.Play();
            }
        }
        else
        {
            if (backgroundMusic.isPlaying)
            {
                backgroundMusic.Stop();
            }
        }
    }

    private void SetAudioButtonSprite()
    {
        if (audioOnState)
        {
            soundButtonIcon.sprite = soundOnIcon;
        }
        else
        {
            soundButtonIcon.sprite = soundOffIcon;
        }
    }

}
