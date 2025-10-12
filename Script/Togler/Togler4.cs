using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Togler4 : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private Animator anomitor;

    [Header("Settings")]
    [Tooltip("Toggler value.")]
    public bool value;

    private static readonly int Value = Animator.StringToHash(name: "Value4");

    private const string ToggleKey = "ToggleValue4";
    private const string AnimatorKey = "AnimatorState4";

    private const string Script = "Script";

    void Start()
    {
        int SaveFPS = PlayerPrefs.GetInt(Script, 0);
        Debug.Log("Saved FPS setting: " + SaveFPS);

        Application.targetFrameRate = SaveFPS == 0 ? 60 : 90;

        if (PlayerPrefs.HasKey(ToggleKey))
        {
            value = PlayerPrefs.GetInt(ToggleKey) == 1;
        }

        if (PlayerPrefs.HasKey(AnimatorKey))
        {
            bool animatorState = PlayerPrefs.GetInt(AnimatorKey) == 1;
            anomitor.SetBool(Value, animatorState);
        }

    }

    private void Awake()
    {
        if (this.anomitor == null) this.anomitor = GetComponent<Animator>();

        this.anomitor.SetBool(id: Value, this.value);
    }

    public void togler4()
    {
        this.value = !this.value;

        this.anomitor.SetBool(id: Value, this.value);

        if (value)
        {
            FPS90();
        }
        else
        {
            FPS60();
        }

        PlayerPrefs.SetInt(ToggleKey, this.value ? 1 : 0);
        PlayerPrefs.SetInt(AnimatorKey, this.value ? 1 : 0);
        PlayerPrefs.Save();
    }

    public void FPS60()
    {
        Application.targetFrameRate = 60;
        PlayerPrefs.SetInt(Script, 0);
        PlayerPrefs.Save();
    }

    public void FPS90()
    {
        Application.targetFrameRate = 90;
        PlayerPrefs.SetInt(Script, 1);
        PlayerPrefs.Save();
    }
}
