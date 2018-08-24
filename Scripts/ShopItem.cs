using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopItem : MonoBehaviour {

    public CoinStorage coinStorage;
    public string skinName;
    public int ID;
    public int itemPrice;
    private bool isBought;
    private bool isUsed;
    public RunShop shopManager;

    public void Start()
    {
        coinStorage = GameObject.FindObjectOfType<CoinStorage>();
        isBought = PlayerPrefs.GetInt(skinName + IDStorage.NAME_ITEM_BOUGHT, 0) == 0 ? false : true;
        isUsed = PlayerPrefs.GetInt(IDStorage.EQUIPED_SKIN_ID, 0) == ID ? true : false;
        if (isBought)
        {
            Button buyButton = GetComponentInChildren<Button>();
            if (isUsed)
            {
                buyButton.GetComponentInChildren<Text>().text = "Used";
                buyButton.enabled = false;
            }
            else
            {
                buyButton.GetComponentInChildren<Text>().text = "Use";
                buyButton.enabled = true;
            }
            buyButton.onClick.RemoveAllListeners();
            buyButton.onClick.AddListener(UseItem);
        }
        else
        {

            Button buyButton = GetComponentInChildren<Button>();

            buyButton.onClick.RemoveAllListeners();
            buyButton.onClick.AddListener(BuyItem);
        }
    }

    public void BuyItem()
    {
        if (coinStorage.CanBuy(itemPrice))
        {
            coinStorage.BuyItems(itemPrice);
            PlayerPrefs.SetInt(skinName + IDStorage.NAME_ITEM_BOUGHT, 1);
            Button buy = GetComponentInChildren<Button>();
            buy.GetComponentInChildren<Text>().text = "Use";
            buy.onClick.RemoveAllListeners();
            buy.onClick.AddListener(UseItem);
        }
        else
        {
            Debug.Log("You are too poor");
        }
    }

    public void UseItem()
    {
        PlayerPrefs.SetInt(IDStorage.EQUIPED_SKIN_ID, ID);
        shopManager.UpdateAllFlags();
    }

    internal void UpdateFlags()
    {
        isBought = PlayerPrefs.GetInt(skinName + IDStorage.NAME_ITEM_BOUGHT) == 0 ? false : true;
        isUsed = PlayerPrefs.GetInt(IDStorage.EQUIPED_SKIN_ID) == ID ? true : false;
        Button buy = GetComponentInChildren<Button>();
        if (isUsed)
        {
            buy.GetComponentInChildren<Text>().text = "Used";
        }
        else
        {
            buy.GetComponentInChildren<Text>().text = "Use";
            buy.enabled = true;
        }
    }
}
