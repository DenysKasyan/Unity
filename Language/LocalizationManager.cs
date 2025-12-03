
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LocalizationManager : MonoBehaviour
{
    public static LocalizationManager Instance;

    public enum Language
    {
        English,
        Ukrainian,
        Norway
    }
    

    private Language currentLanguage = Language.Ukrainian;
    private const string LanguageKey = "SelectedLanguage";

    private List<LocalizedTMPText> localizedTexts = new List<LocalizedTMPText>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            if (PlayerPrefs.HasKey(LanguageKey))
                currentLanguage = (Language)PlayerPrefs.GetInt(LanguageKey);
            else
                currentLanguage = Language.Ukrainian;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void RegisterText(LocalizedTMPText text)
    {
        if (!localizedTexts.Contains(text))
        {
            localizedTexts.Add(text);
            text.UpdateText(currentLanguage);
        }
    }

    public void UnregisterText(LocalizedTMPText text)
    {
        if (localizedTexts.Contains(text))
            localizedTexts.Remove(text);
    }

    public void SetLanguage(Language language)
    {
        currentLanguage = language;
        PlayerPrefs.SetInt(LanguageKey, (int)language);
        PlayerPrefs.Save();

        foreach (var text in localizedTexts)
            text.UpdateText(currentLanguage);

    }
}