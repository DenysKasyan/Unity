
using UnityEngine;
using TMPro;

public class LocalizedTMPText : MonoBehaviour
{
    public string englishText = "Hello";
    public string ukrainianText = "Привіт";
    public string norwayText = "Hei";

    private TextMeshProUGUI textComponent;

    private void Awake()
    {
        textComponent = GetComponent<TextMeshProUGUI>();
        LocalizationManager.Instance.RegisterText(this);
    }

    private void OnDestroy()
    {
        if (LocalizationManager.Instance != null)
        {
            LocalizationManager.Instance.UnregisterText(this);
        }
    }

    public void UpdateText(LocalizationManager.Language language)
    {
        switch (language)
        {
            case LocalizationManager.Language.English:
                textComponent.text = englishText;
                break;
            case LocalizationManager.Language.Ukrainian:
                textComponent.text = ukrainianText;
                break;
            case LocalizationManager.Language.Norway:
                textComponent.text = norwayText;
                break;
        }
    }

    
}
