using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Togler3 : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private Animator anomitor;

    [Header("Settings")]
    [Tooltip("Toggler value.")]
    public bool value;

    private static readonly int Value = Animator.StringToHash("Value3");

    private ButtonScale buttonScale;

    private const string ToggleKey = "ToggleValue3";
    private const string AnimatorKey = "AnimatorState3";

    public void Start()
    {
        buttonScale = FindObjectOfType<ButtonScale>();

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

        if (buttonScale != null)
        {
            if (value)
            {
                buttonScale.OFFanim();
            }
            else
            {
                buttonScale.ONanim();
            }
        }
    }

    private void Awake()
    {
        if (anomitor == null)
        {
            anomitor = GetComponent<Animator>();
        }
    }

    public void Toggle3()
    {
        value = !value;

        anomitor.SetBool(Value, value);

        if (buttonScale != null)
        {
            if (value)
            {
                buttonScale.OFFanim();
            }
            else
            {
                buttonScale.ONanim();
            }
        }

        PlayerPrefs.SetInt(ToggleKey, value ? 1 : 0);
        PlayerPrefs.SetInt(AnimatorKey, value ? 1 : 0);
        PlayerPrefs.Save();
    }
}
