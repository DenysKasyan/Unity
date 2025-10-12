using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Togler6 : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private Animator anomitor;

    [Header("Settings")]
    [Tooltip("Toggler value.")]

    [Header("Main Settings")]
    public Button mainButton;

    public bool value;

    private static readonly int Value = Animator.StringToHash("Value6");

    private const string ToggleKey = "ToggleValue6";
    private const string AnimatorKey = "AnimatorState6";

    private void Awake()
    {
        if (anomitor == null)
        {
            anomitor = GetComponent<Animator>();
        }
    }

    public void Start()
    {
        if (PlayerPrefs.HasKey(ToggleKey))
        {
            value = PlayerPrefs.GetInt(ToggleKey) == 1;
        }

        if (PlayerPrefs.HasKey(AnimatorKey))
        {
            bool animatorState = PlayerPrefs.GetInt(AnimatorKey) == 1;
            anomitor.SetBool(Value, animatorState);
        }

        if (anomitor != null)
        {
            anomitor.SetBool(Value, value);
        }

    }

    public void Toggle6()
    {
        value = !value;

        anomitor.SetBool(Value, value);

        PlayerPrefs.SetInt(ToggleKey, value ? 1 : 0);
        PlayerPrefs.SetInt(AnimatorKey, value ? 1 : 0);
        PlayerPrefs.Save();
    }

    

}
