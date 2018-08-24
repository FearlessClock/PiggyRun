using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.Advertisements;

class AdManager
{
    private string rewardId = "";
    public bool isRunning = false;

    public CoinStorage coinStorage;
    bool DidAdGoToEndCallbackResult = false;
    public AdManager()
    {
        coinStorage = GameObject.FindObjectOfType<CoinStorage>();
    }

    public void ShowAd()
    {
        if (!Advertisement.isShowing)
        {
            isRunning = true;
            ShowOptions options = new ShowOptions { resultCallback = AdCallback};
            Advertisement.Show(rewardId, options);
        }
    }

    public void ShowAdTillEnd()
    {
        if (!Advertisement.isShowing)
        {
            isRunning = true;
            ShowOptions options = new ShowOptions { resultCallback = DidGoTillEndCallback };
            Advertisement.Show(rewardId, options);
        }
    }
    void DidGoTillEndCallback(ShowResult res)
    {
        isRunning = false;
        if(res == ShowResult.Finished)
        {
            Debug.Log("Double coins");
            coinStorage.DoubleCoins();
        }
        else
        {
            Debug.Log("Coin double failed");
        }
    }

    public bool IsShowing
    {
        get { return Advertisement.isShowing; }
    }

    void AdCallback(ShowResult res)
    {
        switch (res)
        {
            case ShowResult.Finished: Debug.Log("SR Finished");
                break;
            case ShowResult.Failed: Debug.Log("SR Failed");
                break;
            case ShowResult.Skipped: Debug.Log("SR Skipped");
                break;
        }
        isRunning = false;
    }
}
