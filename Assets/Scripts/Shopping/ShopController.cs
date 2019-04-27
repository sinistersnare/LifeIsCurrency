using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ShopController : MonoBehaviour
{
    public Text moneyText;
    public GameObject hpShopper;

    void Start()
    {
        hpShopper.GetComponentInChildren<Button>().onClick.AddListener(this.BuyMoreHp);
        this.SetUpShop(this.hpShopper, ShopItems.hpPrices[SaveData.healthIdx], ShopItems.hpValues[SaveData.healthIdx]);
        this.UpdateMoney();
    }

    public void UpdateMoney()
    {
        moneyText.text = "Zombies' Lives: " + SaveData.money;
    }

    public void SetUpShop<T>(GameObject itemBase, int curPrice, T value)
    {
        Text priceText = itemBase.transform.Find("PriceText").GetComponent<Text>();
        Text valueText = itemBase.transform.Find("CurrentValueText").GetComponent<Text>();
        if (curPrice == -1)
        {
            priceText.text = "Bought all.";
            itemBase.GetComponentInChildren<Button>().interactable = false;
        }
        else
        {
            priceText.text = "Heads: " + curPrice;
        }
        valueText.text = value.ToString();
    }

    public void BackToFight()
    {
        SceneManager.LoadScene("FightScene");
    }

    public void BuyMoreHp()
    {
        int price = ShopItems.hpPrices[SaveData.healthIdx];
        if (SaveData.money >= price)
        {
            SaveData.money -= price;
            SaveData.healthIdx++;
            this.SetUpShop(this.hpShopper, ShopItems.hpPrices[SaveData.healthIdx], SaveData.Health);
            this.UpdateMoney();
        }
    }
}
