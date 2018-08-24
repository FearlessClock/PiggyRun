using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopItem : MonoBehaviour {

    public CoinStorage coinStorage;
    public int itemPrice;

    public void Awake()
    {
        coinStorage = GameObject.FindObjectOfType<CoinStorage>();
    }

    public void BuyItem()
    {
        if (coinStorage.CanBuy(itemPrice))
        {
            coinStorage.BuyItems(itemPrice);
            GetComponentInChildren<Button>().enabled = false;
        }
        else
        {
            Debug.Log("You are too poor");
        }
    }
}
