using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkinBack : MonoBehaviour
{
    public Image Back;
    public Sprite[] Image;

    public Button[] SkinBt;
    public int[] Cent;
    public bool[] isSkinBought;
    public TMP_Text[] Price;

    public Image[] ImagePrice;
    public TMP_Text[] text; // "Куплено"
    public TMP_Text[] textused; // "Використовується"

    public static int floatImage = 0;

    void Start()
    {
        LoadSpriteIndex();
        Back.sprite = Image[floatImage];
    }

    void Update()
    {
        UpdateButtonStates();
    }

    public void ChangeSprite(int index)
    {
        if (isSkinBought[index])
        {
            // Оновити стан всіх текстів
            for (int i = 0; i < isSkinBought.Length; i++)
            {
                if (isSkinBought[i])
                {
                    text[i].gameObject.SetActive(true);
                    textused[i].gameObject.SetActive(false);
                }
            }

            // Застосувати новий фон
            Back.sprite = Image[index];
            floatImage = index;

            // Оновити текст для вибраного фону
            text[index].gameObject.SetActive(false);
            textused[index].gameObject.SetActive(true);

            SaveSpriteIndex();
            return;
        }

        // Покупка
        if (GRNupgrade.money >= Cent[index])
        {
            GRNupgrade.money -= Cent[index];
            Back.sprite = Image[index];
            floatImage = index;
            isSkinBought[index] = true;

            Price[index].text = "";
            ImagePrice[index].gameObject.SetActive(false);
            text[index].gameObject.SetActive(false);
            textused[index].gameObject.SetActive(true);

            // Оновити інші кнопки
            for (int i = 0; i < isSkinBought.Length; i++)
            {
                if (i != index && isSkinBought[i])
                {
                    text[i].gameObject.SetActive(true);
                    textused[i].gameObject.SetActive(false);
                }
            }

            SaveSpriteIndex();
            PlayerPrefs.Save();
        }
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

        string priceTextData = "";
        for (int i = 0; i < isSkinBought.Length; i++)
        {
            if (isSkinBought[i])
            {
                priceTextData += ";";
            }
            else
            {
                priceTextData += Cent[i] + ";";
            }
        }

        PlayerPrefs.SetInt("SpriteIndex2", floatImage);
        PlayerPrefs.SetInt("SkinPurchaseData2", purchaseData);
        PlayerPrefs.SetString("PriceTextData2", priceTextData);
        PlayerPrefs.Save();
    }

    public void LoadSpriteIndex()
    {
        floatImage = PlayerPrefs.GetInt("SpriteIndex2", 0);
        isSkinBought[0] = true;
        int purchaseData = PlayerPrefs.GetInt("SkinPurchaseData2", 0);
        string priceTextData = PlayerPrefs.GetString("PriceTextData2", "");

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