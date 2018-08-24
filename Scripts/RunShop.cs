using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RunShop : MonoBehaviour {

    public GameObject shopItemPrefab;
    public Sprite[] shopItemImages;
    public string[] shopItemDesc;
    public string[] shopItemName;
    public int[] shopItemPrice;

    public string BackgroundImageName;
    public string SkinIconName;
    public string DescriptionName;
    public string BuyButtonName;

    public CoinStorage coinStorage;
    // Use this for initialization
	void Start () {
        for (int i = 0; i < shopItemImages.Length; i++)
        {
            Sprite sprite = shopItemImages[i];
            string desc = shopItemDesc[i];

            GameObject obj = Instantiate<GameObject>(shopItemPrefab);
            obj.transform.SetParent(this.transform, false);

            ShopItem shopItem = obj.GetComponent<ShopItem>();
            shopItem.shopManager = this;
            shopItem.ID = i;
            shopItem.skinName = shopItemName[i];
            shopItem.itemPrice = shopItemPrice[i];

            Transform backgroundImage = obj.transform.Find(BackgroundImageName);
            if (backgroundImage)
            {
                backgroundImage.Find(SkinIconName).gameObject.GetComponent<Image>().sprite = sprite;
            }

            Transform description = obj.transform.Find(DescriptionName);
            if (description)
            {
                description.gameObject.GetComponentInChildren<Text>().text = desc;
            }

            Transform buyButton = obj.transform.Find(BuyButtonName);
            if (buyButton)
            {
                Button button = buyButton.gameObject.GetComponent<Button>();
            }
        }
	}

    public void UpdateAllFlags()
    {
        foreach(Transform child in this.transform)
        {
            child.GetComponent<ShopItem>().UpdateFlags();
        }
    }
}
