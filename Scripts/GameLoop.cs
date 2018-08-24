using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;
using UnityEngine.UI;

public class GameLoop : MonoBehaviour {

    public GameStateManager GSM;
    public ScreenManager screenManager;
    public Animator FadeToBlackMenu;
    private RawImage rawImage;

    public Animator gameplayMenu;
    bool isRunning = false;
    float timer = 0;

    bool adShown = false;
    int adLimitCounter = 0;

    private AdManager AdManager;

    private bool doubleCoinsOnce = false;

	// Use this for initialization
	void Start () {
        rawImage = FadeToBlackMenu.transform.GetChild(0).gameObject.GetComponent<RawImage>();
        AdManager = new AdManager();
    }
	
	// Update is called once per frame
	void Update () {
        if(GSM.gameState == GameState.MAIN_MENU)
        {

        }
        else if(GSM.gameState == GameState.RESTART)
        {
            if (adLimitCounter > 5 && !AdManager.IsShowing && !adShown)
            {
                AdManager.ShowAd();
                adShown = true;
                adLimitCounter = 0;
            }
            else if (AdManager.isRunning)
            {

            }
            else
            {
                if (isRunning == false)
                {
                    screenManager.OpenPanel(FadeToBlackMenu);
                    isRunning = true;
                    timer = 0;
                }
                timer += Time.deltaTime;
                if (timer > 2)
                {
                    isRunning = false;
                    timer = 0;
                    GSM.SetGameState(GameState.STARTING);
                    adShown = false;
                    adLimitCounter++;
                    screenManager.CloseCurrent();
                }
                else
                {
                    rawImage.color = new Color(0, 0, 0, (-0.45f * timer * timer + 2));// -timer * timer + 2 * timer));
                    if (rawImage.color.a < 0.1)
                    {
                        rawImage.color = new Color(0, 0, 0, 0);
                    }
                }
            }
        }
        else if (GSM.gameState == GameState.STARTING)
        {
            doubleCoinsOnce = false;
        }
    }
    
    public void DoubleCoins()
    {
        if (!doubleCoinsOnce)
        {
            AdManager.ShowAdTillEnd();
            doubleCoinsOnce = true;
        }
    }
}
