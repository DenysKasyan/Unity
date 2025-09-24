using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkinCat : MonoBehaviour
{
    public Sprite[] Skin;

    public Button SPbutton;
    public Image SPbutton2;
    public Image Effect;

    public Button[] SkinBt;
    public int[] Cent;
    public bool[] isSkinBought;
    public TMP_Text[] Price;

    public Image[] ImagePrice; // Фото мішечку біля ціни
    public TMP_Text[] text; // текст *Куплено* коли гравець купив скін
    public TMP_Text[] textused; // текст *використовується* коли гравець купив скін

    public Button[] cmnSkins;       // Звичайні
    public Button[] rareSkins;       // Рідкісні
    public Button[] epicSkins;       // Епічні
    public Button[] legendarySkins;  // Легендарні


    public static int floatImage = 0;

    void Start()
    { 
        LoadSpriteIndex();
        SPbutton.image.sprite = Skin[floatImage];
        SPbutton2.sprite = Skin[floatImage];
        Effect.sprite = Skin[floatImage];
        
    }

    void Update()
    {
        UpdateButtonStates();
    }

    public void skinbt()
    {
        
    }

    public void ChangeSprite(int index)
    {
        if (isSkinBought[index])
        {
            // Сховати "використовується" з усіх скінів та показати "куплено", де треба
            for (int i = 0; i < isSkinBought.Length; i++)
            {
                if (isSkinBought[i])
                {
                    text[i].gameObject.SetActive(true);       // Показати "Куплено"
                    textused[i].gameObject.SetActive(false);  // Сховати "Використовується"
                }
            }

            // Застосувати новий скин
            SPbutton.image.sprite = Skin[index];
            SPbutton2.sprite = Skin[index];
            Effect.sprite = Skin[index];
            floatImage = index;

            // Показати лише "використовується" на вибраному
            text[index].gameObject.SetActive(false);
            textused[index].gameObject.SetActive(true);
            ApplySkinBonus(index);

            SaveSpriteIndex();
            return;
        }

        // Покупка
        if (GRNupgrade.money >= Cent[index])
        {
            GRNupgrade.money -= Cent[index];
            SPbutton.image.sprite = Skin[index];
            SPbutton2.sprite = Skin[index];
            Effect.sprite = Skin[index];
            floatImage = index;
            isSkinBought[index] = true;

            Price[index].text = "";
            ImagePrice[index].gameObject.SetActive(false);
            text[index].gameObject.SetActive(false);
            textused[index].gameObject.SetActive(true);

            // Сховати "використовується" з інших
            for (int i = 0; i < isSkinBought.Length; i++)
            {
                if (i != index && isSkinBought[i])
                {
                    text[i].gameObject.SetActive(true);
                    textused[i].gameObject.SetActive(false);
                }
            }

            // 🎁 Додаємо бонус від типу скіна
            ApplySkinBonus(index);

            SaveSpriteIndex();
            PlayerPrefs.Save();
        }
    }

    private void ApplySkinBonus(int index)
    {
        if (Array.Exists(rareSkins, b => b == SkinBt[index]))
            GRNupgrade.skinBonus = 10;
        else if (Array.Exists(epicSkins, b => b == SkinBt[index]))
            GRNupgrade.skinBonus = 30;
        else if (Array.Exists(legendarySkins, b => b == SkinBt[index]))
            GRNupgrade.skinBonus = 50;
        else
            GRNupgrade.skinBonus = 0;


        // Твій код перевірки
        if (Array.Exists(epicSkins, b => b == SkinBt[index]))
        {
            // Якщо купив епік — порівнюємо з поточним best
            GRNupgrade.bestEnergyRegenInterval = Mathf.Min(GRNupgrade.bestEnergyRegenInterval, 2.4f);
        }
        else if (Array.Exists(legendarySkins, b => b == SkinBt[index]))
        {
            // Якщо купив легендарний — порівнюємо з поточним best
            GRNupgrade.bestEnergyRegenInterval = Mathf.Min(GRNupgrade.bestEnergyRegenInterval, 0.5f);
        }

        GRNupgrade.energyRegenInterval = GRNupgrade.bestEnergyRegenInterval;
    }






    public void UpdateButtonStates()
    {
        for (int i = 0; i < SkinBt.Length; i++)
        {
            SkinBt[i].interactable = isSkinBought[i] || GRNupgrade.money >= Cent[i];
        }
    }

    public void SaveSpriteIndex()
    {
        int purchaseData = 0;
        for (int i = 0; i < isSkinBought.Length; i++)
        {
            if (isSkinBought[i])
            {
                purchaseData |= 1 << i;
            }
        }

        int purchaseData2 = 0;
        string priceTextData = "";

        for (int i = 0; i < isSkinBought.Length; i++)
        {
            if (isSkinBought[i])
            {
                purchaseData2 |= 1 << i;
                priceTextData += "�������;";
            }
            else
            {
                priceTextData += Cent[i] + ";";
            }
        }

        PlayerPrefs.SetString("PriceTextData", priceTextData);

        PlayerPrefs.SetInt("SkinPurchaseData3", purchaseData);
        PlayerPrefs.SetInt("SpriteIndex", floatImage);
        PlayerPrefs.Save();
    }

    public void LoadSpriteIndex()
{
    floatImage = PlayerPrefs.GetInt("SpriteIndex", 0); // За замовчуванням скін 0
    isSkinBought[0] = true;
    ApplySkinBonus(floatImage);

    string priceTextData = PlayerPrefs.GetString("PriceTextData", "");
    int purchaseData = PlayerPrefs.GetInt("SkinPurchaseData3", 0);

    string[] priceTexts = priceTextData.Split(';');

    for (int i = 0; i < isSkinBought.Length; i++)
    {
        if (i != 0) // 0 скин завжди куплений
            isSkinBought[i] = (purchaseData & (1 << i)) != 0;

        if (isSkinBought[i])
        {
            Price[i].text = "";
            ImagePrice[i].gameObject.SetActive(false);

            if (i == floatImage)
            {
                text[i].gameObject.SetActive(false);
                textused[i].gameObject.SetActive(true);
            }
            else
            {
                text[i].gameObject.SetActive(true);
                textused[i].gameObject.SetActive(false);
            }
        }
        else
        {
            ImagePrice[i].gameObject.SetActive(true);
            text[i].gameObject.SetActive(false);
            textused[i].gameObject.SetActive(false);

            if (i < priceTexts.Length && !string.IsNullOrEmpty(priceTexts[i]))
                Price[i].text = priceTexts[i];
            else
                Price[i].text = "" + Cent[i];
        }
    }
}
}