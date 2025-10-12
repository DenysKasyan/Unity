
using UnityEngine;
using UnityEngine.UI;

public class Togler5 : MonoBehaviour
{
    [Header("Footer Images")]
    public UnityEngine.UI.Image[] footers = new UnityEngine.UI.Image[4];

    [Header("Components")]
    [SerializeField] private Animator animator;

    [Header("Settings")]
    [Tooltip("Toggle Value.")]
    public bool value;

    private static readonly int Value = Animator.StringToHash("Value5");
    private Color[] originalColors = new Color[4];

    private const string ToggleKey = "Toggle5_State"; 

    void Awake()
    {
        if (animator == null)
            animator = GetComponent<Animator>();

        value = PlayerPrefs.GetInt(ToggleKey, 0) == 1;

        animator.SetBool(Value, value);

        for (int i = 0; i < footers.Length; i++)
        {
            if (footers[i] != null)
                originalColors[i] = footers[i].color;
        }

        ApplyColorState();
    }

    public void Toggle5()
    {
        value = !value;
        animator.SetBool(Value, value);

        PlayerPrefs.SetInt(ToggleKey, value ? 1 : 0);
        PlayerPrefs.Save();

        ApplyColorState();
    }

    private void ApplyColorState()
    {
        for (int i = 0; i < footers.Length; i++)
        {
            if (footers[i] != null)
            {
                footers[i].color = value ? new Color(0f, 0f, 0f, 0.5f) : originalColors[i];
            }
        }
    }
}
