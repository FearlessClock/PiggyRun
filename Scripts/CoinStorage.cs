using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class CoinStorage : MonoBehaviour
{
    public string coinsKey = IDStorage.COIN_STORAGE;
    public GameStateManager GSM;
    public Text coinsShow;

    public Text collectedCoinsEndScreenText;

    private int amountOfCoinsThisRound = 0;
    private int thisRoundsCoins = 0;
    private int coinsCollected = 0;
    private bool firstRestart = true;

    public bool CanBuy(int amount)
    {
        if(amount <= coinsCollected)
        {
            return true;
        }
        return false;
    }

    public bool BuyItems(int amount)
    {
        Debug.Log(amount);
        if (CanBuy(amount)){
            RemoveFromCoins(amount);
            return true;
        }
        else
        {
            return false;
        }
    }
    private void Awake()
    {
        coinsCollected = LoadCoins();
        coinsShow.text = coinsCollected.ToString();
    }

    private void Update()
    {
        //Check state and save coins on restart
        if(GSM.gameState == GameState.GAME_OVER)
        {
            if (firstRestart)
            {
                thisRoundsCoins = amountOfCoinsThisRound;
                amountOfCoinsThisRound = 0;
                SaveCollectedCoins();
                firstRestart = false;
            }
            collectedCoinsEndScreenText.text = thisRoundsCoins.ToString();
            coinsShow.text = coinsCollected.ToString();
        }
        else if(GSM.gameState == GameState.GAMEPLAY)
        {
            coinsShow.text = (coinsCollected + amountOfCoinsThisRound).ToString();
            firstRestart = true;
        }
    }

    internal void DoubleCoins()
    {
        coinsCollected += thisRoundsCoins;
        thisRoundsCoins += thisRoundsCoins;
        SaveCoins(coinsCollected);
    }

    internal void SaveCollectedCoins()
    {
        coinsCollected += thisRoundsCoins;
        SaveCoins(coinsCollected);
    }

    internal void addToCoins(int amount)
    {
        amountOfCoinsThisRound += amount;
    }

    internal void RemoveFromCoins(int amount)
    {
        coinsCollected -= amount;
    }

    private int LoadCoins()
    {
        return PlayerPrefs.GetInt(coinsKey, 0);
    }

    private void SaveCoins(int amount)
    {
        PlayerPrefs.SetInt(coinsKey, amount);
    }

}