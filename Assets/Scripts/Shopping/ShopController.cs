using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ShopController : MonoBehaviour
{
    public Text moneyText;
    public GameObject hpShopper;
    public GameObject autoGunShopper;
    public GameObject bomberShopper;

    public GameObject bombText, weaponText;
    public Text highScoreText;

    void Start()
    {
        this.bombText.SetActive(SaveData.HasBomber);
        this.weaponText.SetActive(SaveData.HasAuto);
        hpShopper.GetComponentInChildren<Button>().onClick.AddListener(this.BuyMoreHp);
        autoGunShopper.GetComponentInChildren<Button>().onClick.AddListener(this.BuyAutoGun);
        bomberShopper.GetComponentInChildren<Button>().onClick.AddListener(this.BuyBomberGun);

        this.SetUpShop(this.hpShopper, ShopItems.hpPrices[SaveData.healthIdx], ShopItems.hpValues[SaveData.healthIdx]);
        this.SetUpShop(this.autoGunShopper, ShopItems.automaticPrices[SaveData.autoIdx], ShopItems.automaticValues[SaveData.autoIdx]);
        this.SetUpShop(this.bomberShopper, ShopItems.bomberPrices[SaveData.bomberIdx], ShopItems.bomberValues[SaveData.bomberIdx]);

        this.UpdateMoney();
        this.highScoreText.text = "Longest Life: " + SaveData.highScore.ToString("0.00");
    }

    public void UpdateMoney()
    {
        moneyText.text = "Zombies' Lives => currency: " + SaveData.money;
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
            priceText.text = "Price: " + curPrice;
        }
        valueText.text = value.ToString();
    }

    public void BackToFight()
    {
        SceneManager.LoadScene("FightScene");
    }

    public void BuyBomberGun()
    {
        int price = ShopItems.bomberPrices[SaveData.bomberIdx];
        if (SaveData.money >= price)
        {
            this.bombText.SetActive(true);
            SaveData.money -= price;
            SaveData.bomberIdx++;
            this.SetUpShop(this.bomberShopper, ShopItems.bomberPrices[SaveData.bomberIdx], SaveData.HasBomber);
            this.UpdateMoney();
        }
    }

    public void BuyAutoGun()
    {
        int price = ShopItems.automaticPrices[SaveData.autoIdx];
        if (SaveData.money >= price)
        {
            this.weaponText.SetActive(true);
            SaveData.money -= price;
            SaveData.autoIdx++;
            this.SetUpShop(this.autoGunShopper, ShopItems.automaticPrices[SaveData.autoIdx], SaveData.HasAuto);
            this.UpdateMoney();
        }
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
