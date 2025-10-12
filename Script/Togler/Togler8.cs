using UnityEngine;
using UnityEngine.UI;

public class Togler8 : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private Animator anomitor;
    [SerializeField] private MusicManager musicManager;

    [Header("Settings")]
    public bool value;

    private static readonly int Value = Animator.StringToHash("Value8");

    private const string ToggleKey = "ToggleValue8";
    private const string AnimatorKey = "AnimatorState8";

    private void Awake()
    {
        if (anomitor == null) anomitor = GetComponent<Animator>();
    }

    private void Start()
    {
        // За замовчуванням вимкнено (0)
        value = PlayerPrefs.GetInt(ToggleKey, 0) == 1;

        if (anomitor != null)
        {
            anomitor.SetBool(Value, value);
        }
        PlayerPrefs.SetInt(AnimatorKey, value ? 1 : 0);

        if (musicManager != null)
        {
            musicManager.SetMusicEnabled(value);
        }
    }

    public void Toggle8()
    {
        value = !value;

        if (anomitor != null)
        {
            anomitor.SetBool(Value, value);
        }

        PlayerPrefs.SetInt(ToggleKey, value ? 1 : 0);
        PlayerPrefs.SetInt(AnimatorKey, value ? 1 : 0);
        PlayerPrefs.Save();

        if (musicManager != null)
        {
            musicManager.SetMusicEnabled(value);
        }
    }
}
