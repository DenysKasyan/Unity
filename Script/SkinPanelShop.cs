using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SkinPanelShop : MonoBehaviour
{
    public GameObject panelSkinss;

    [System.Serializable]
    public class SkinData
    {
        public string name;
        public Sprite image;
        [TextArea] public string description;
        public int price;

        public Button SkinBut;
    }

    [System.Serializable]
    public class SkinCategoryUI
    {
        public GameObject panel;        
        public TMP_Text nameText;
        public TMP_Text descriptionText;
        public TMP_Text priceText;
        public Image skinImage;
        public Button SaleBut;

        public Transform buttonContainer;
    }

    [Header("Buttons")]
    public Button[] Skin;
    public Button[] RareSkin;
    public Button[] EpicSkin;
    public Button[] LegendarySkin;

    [Header("Data")]
    public SkinData[] skinData_Normal;
    public SkinData[] skinData_Rare;
    public SkinData[] skinData_Epic;
    public SkinData[] skinData_Legendary;

    [Header("UI Panels")]
    public SkinCategoryUI normalUI;
    public SkinCategoryUI rareUI;
    public SkinCategoryUI epicUI;
    public SkinCategoryUI legendaryUI;

    [Header("RenderTexture Settings")]
    public RenderTexture sharedRT; // Твоя спільна RT для всіх
    public Camera renderCamera;    // Камера яка малює в RT

    void Start()
    {
        // При запуску ховаємо всі панелі
        HideAllPanels();
        DisableAllSkinButtons();

        // --- Прив'язуємо кнопки ---
        for (int i = 0; i < Skin.Length; i++)
        {
            int tempIndex = i;
            Skin[i].onClick.AddListener(() => ShowSkinInfo(skinData_Normal[tempIndex], normalUI));
        }

        for (int i = 0; i < RareSkin.Length; i++)
        {
            int tempIndex = i;
            RareSkin[i].onClick.AddListener(() => ShowSkinInfo(skinData_Rare[tempIndex], rareUI));
        }

        for (int i = 0; i < EpicSkin.Length; i++)
        {
            int tempIndex = i;
            EpicSkin[i].onClick.AddListener(() => ShowSkinInfo(skinData_Epic[tempIndex], epicUI));
        }

        for (int i = 0; i < LegendarySkin.Length; i++)
        {
            int tempIndex = i;
            LegendarySkin[i].onClick.AddListener(() => ShowSkinInfo(skinData_Legendary[tempIndex], legendaryUI));
        }
    }

void LateUpdate()
{
    if (sharedRT == null) return;

    // Якщо жодна панель не активна → очищаємо RT
    if (!normalUI.panel.activeSelf &&
        !rareUI.panel.activeSelf &&
        !epicUI.panel.activeSelf &&
        !legendaryUI.panel.activeSelf)
    {
        RenderTexture.active = sharedRT;
        GL.Clear(true, true, Color.clear);
        RenderTexture.active = null;
    }
}

    void ShowSkinInfo(SkinData data, SkinCategoryUI ui)
    {
        panelSkinss.SetActive(false);

        ui.nameText.text = data.name;
        ui.descriptionText.text = data.description;
        ui.priceText.text = data.price.ToString();
        ui.skinImage.sprite = data.image;

        // Ховаємо або заміняємо стару кнопку
        if (ui.SaleBut != null)
            ui.SaleBut.gameObject.SetActive(false);

        if (data.SkinBut != null)
        {
            data.SkinBut.transform.SetParent(ui.buttonContainer, false);

            RectTransform rt = data.SkinBut.GetComponent<RectTransform>();
            if (rt != null)
            {
                rt.anchoredPosition = Vector2.zero;
                rt.localScale = Vector3.one;
                rt.localRotation = Quaternion.identity;
                rt.anchorMin = rt.anchorMax = new Vector2(0.5f, 0.5f);
                rt.pivot = new Vector2(0.5f, 0.5f);
            }

            data.SkinBut.gameObject.SetActive(true);
            ui.SaleBut = data.SkinBut;
        }

        ui.panel.SetActive(true);
    }

    void DisableAllSkinButtons()
    {
        foreach (var skin in skinData_Normal)
            if (skin.SkinBut != null)
                skin.SkinBut.gameObject.SetActive(false);

        foreach (var skin in skinData_Rare)
            if (skin.SkinBut != null)
                skin.SkinBut.gameObject.SetActive(false);

        foreach (var skin in skinData_Epic)
            if (skin.SkinBut != null)
                skin.SkinBut.gameObject.SetActive(false);

        foreach (var skin in skinData_Legendary)
            if (skin.SkinBut != null)
                skin.SkinBut.gameObject.SetActive(false);
    }

    public void HideAllPanels()
    {
        normalUI.panel.SetActive(false);
        rareUI.panel.SetActive(false);
        epicUI.panel.SetActive(false);
        legendaryUI.panel.SetActive(false);
        panelSkinss.SetActive(true);
    }

    public void HidePanel(SkinCategoryUI ui)
    {
        ui.panel.SetActive(false);
    }
}
