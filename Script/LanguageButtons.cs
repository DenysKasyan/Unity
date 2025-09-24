
using UnityEngine;

public class LanguageButtons : MonoBehaviour
{

    public void SetEnglish()
    {
        LocalizationManager.Instance.SetLanguage(LocalizationManager.Language.English);
    }

    public void SetUkrainian()
    {
        LocalizationManager.Instance.SetLanguage(LocalizationManager.Language.Ukrainian);
    }
}
