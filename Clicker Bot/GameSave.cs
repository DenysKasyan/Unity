using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameSave : MonoBehaviour
{
    public Button button;
    public GameTimer script;

    public TMP_Text cent;
    public TMP_Text usingg;
    public Image centImage;

    public int Cent;

    private const string Script1Key = "Script1Active66";
    private const string UsingTextKey = "UsingTextActive66";

    void Start()
    {
        if (!PlayerPrefs.HasKey(Script1Key))
        {
            PlayerPrefs.SetInt(Script1Key, 1);
            PlayerPrefs.Save();
        }

        if (!PlayerPrefs.HasKey(UsingTextKey))
        {
            PlayerPrefs.SetInt(UsingTextKey, 0);
            PlayerPrefs.Save();
        }

        bool isScriptActive = PlayerPrefs.GetInt(Script1Key, 0) == 0;
        script.enabled = isScriptActive;

        bool isUsingTextActive = PlayerPrefs.GetInt(UsingTextKey, 0) == 1;

        UpdateUI(isUsingTextActive);

        button.onClick.AddListener(ActiveScript);
    }

    private void Update()
    {
        UpdateButtonStates();
    }

    private void ActiveScript()
    {
        if (GameManager.money >= Cent)
        {
            GameManager.money -= Cent;
            script.enabled = true;
            PlayerPrefs.SetInt("money", GameManager.money);
            PlayerPrefs.SetInt(Script1Key, 0);

            UpdateUI(true);
            PlayerPrefs.SetInt(UsingTextKey, 1);

            PlayerPrefs.Save();
        }
    }

    public void UpdateButtonStates()
    {
        button.interactable = GameManager.money >= Cent;
    }

    private void UpdateUI(bool showUsingText)
    {
        if (showUsingText)
        {
            centImage.gameObject.SetActive(false);
            cent.gameObject.SetActive(false);
            usingg.gameObject.SetActive(true);
        }
        else
        {
            centImage.gameObject.SetActive(true);
            cent.gameObject.SetActive(true);
            usingg.gameObject.SetActive(false);
        }
    }
}
