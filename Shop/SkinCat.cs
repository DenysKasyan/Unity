using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkinCat : MonoBehaviour
{
    [Header("Основні ресурси")]
    public Sprite[] Skin;
    public Button SPbutton;
    public Image SPbutton2;
    public Image Effect;

    [Header("Скін кнопки та дані")]
    public Button[] SkinBt;
    public int[] Cent;
    public bool[] isSkinBought;
    public TMP_Text[] Price;

    [Header("UI Елементи")]
    public Image[] ImagePrice;
    public TMP_Text[] text;
    public TMP_Text[] textused;

    [Header("Категорії скінів")]
    public Button[] cmnSkins;
    public Button[] rareSkins;
    public Button[] epicSkins;
    public Button[] legendarySkins;

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

    public void ChangeSprite(int index)
    {
        if (isSkinBought[index])
        {
            UseSkin(index);
            return;
        }

        if (GameManager.money >= Cent[index])
        {
            GameManager.money -= Cent[index];
            isSkinBought[index] = true;
            UseSkin(index);
            SaveSpriteIndex();
        }
    }

    private void UseSkin(int index)
    {
        for (int i = 0; i < isSkinBought.Length; i++)
        {
            if (isSkinBought[i])
            {
                text[i].gameObject.SetActive(true);
                textused[i].gameObject.SetActive(false);
            }
        }

        SPbutton.image.sprite = Skin[index];
        SPbutton2.sprite = Skin[index];
        Effect.sprite = Skin[index];
        floatImage = index;

        text[index].gameObject.SetActive(false);
        textused[index].gameObject.SetActive(true);

        ApplySkinBonus(index);
        SaveSpriteIndex();

        // 🟢 миттєво оновлюємо панель магазину
        SkinPanelShop shop = FindObjectOfType<SkinPanelShop>();
        if (shop != null)
            shop.RefreshSkinButtons();
    }

    private void ApplySkinBonus(int index)
    {
        if (Array.Exists(rareSkins, b => b == SkinBt[index]))
            GameManager.skinBonus = 10;
        else if (Array.Exists(epicSkins, b => b == SkinBt[index]))
            GameManager.skinBonus = 30;
        else if (Array.Exists(legendarySkins, b => b == SkinBt[index]))
            GameManager.skinBonus = 50;
        else
            GameManager.skinBonus = 0;
    }

    public void UpdateButtonStates()
    {
        for (int i = 0; i < SkinBt.Length; i++)
        {
            SkinBt[i].interactable = isSkinBought[i] || GameManager.money >= Cent[i];
        }
    }

    public void SaveSpriteIndex()
    {
        int purchaseData = 0;
        for (int i = 0; i < isSkinBought.Length; i++)
        {
            if (isSkinBought[i])
                purchaseData |= 1 << i;
        }

        PlayerPrefs.SetInt("SkinPurchaseData3", purchaseData);
        PlayerPrefs.SetInt("SpriteIndex", floatImage);
        PlayerPrefs.Save();
    }

    public void LoadSpriteIndex()
    {
        int purchaseData = PlayerPrefs.GetInt("SkinPurchaseData3", 1);
        floatImage = PlayerPrefs.GetInt("SpriteIndex", 0);

        for (int i = 0; i < isSkinBought.Length; i++)
            isSkinBought[i] = (purchaseData & (1 << i)) != 0;

        // 🟢 Базовий скін завжди куплений
        isSkinBought[0] = true;

        ApplySkinBonus(floatImage);

        for (int i = 0; i < isSkinBought.Length; i++)
        {
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
                Price[i].text = Cent[i] + "";
                ImagePrice[i].gameObject.SetActive(true);
                text[i].gameObject.SetActive(false);
                textused[i].gameObject.SetActive(false);
            }
        }

        // 🟢 Додаємо миттєве оновлення панелі магазину при старті
        SkinPanelShop shop = FindObjectOfType<SkinPanelShop>();
        if (shop != null)
        {
            shop.RefreshSkinButtons();
        }
    }


}
