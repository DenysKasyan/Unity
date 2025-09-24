using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TogleTrigger : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private Animator anomitor;

    [Header("Settings")]
    [Tooltip("Toggler value.")]
    public bool value;

    public Image BackGround;

    public TMP_Text scoreTextColor;
    private const string ScoreTextColorKey = "ScoreTextColor";

    private static readonly int Value = Animator.StringToHash(name: "Value1");

    private const string ToggleKey = "ToggleValue";
    private const string BackgroundKey = "BackgroundValue";
    private const string AnimatorKey = "AnimatorState"; 

    public void Start()
    {
        if (PlayerPrefs.HasKey(ToggleKey))
        {
            value = PlayerPrefs.GetInt(ToggleKey) == 1;
        }

        if (PlayerPrefs.HasKey(BackgroundKey))
        {
            bool backgroundActive = PlayerPrefs.GetInt(BackgroundKey) == 1;
            BackGround.gameObject.SetActive(backgroundActive);
        }

        if (PlayerPrefs.HasKey(AnimatorKey))
        {
            bool animatorState = PlayerPrefs.GetInt(AnimatorKey) == 1;
            anomitor.SetBool(Value, animatorState);
        }

        if (PlayerPrefs.HasKey(ScoreTextColorKey))
        {
            string savedColor = PlayerPrefs.GetString(ScoreTextColorKey);
            if (ColorUtility.TryParseHtmlString("#" + savedColor, out Color loadedColor))
            {
                scoreTextColor.color = loadedColor;
            }
        }
        BackGround.gameObject.SetActive(value);
    }

    public void Update()
    {

    }

    private void Awake()
    {
        if (this.anomitor == null) this.anomitor = GetComponent<Animator>();
        this.anomitor.SetBool(id: Value, this.value);
    }

    public void Toggle()
    {
        this.value = !this.value;

        this.anomitor.SetBool(id: Value, this.value);

        if (value)
        {
            BackGround.gameObject.SetActive(true);
            scoreTextColor.color = Color.black;
        }
        else
        {
            BackGround.gameObject.SetActive(false);
            scoreTextColor.color = Color.white;
        }

        PlayerPrefs.SetInt(ToggleKey, this.value ? 1 : 0);
        PlayerPrefs.SetInt(BackgroundKey, this.value ? 1 : 0);
        PlayerPrefs.SetInt(AnimatorKey, this.value ? 1 : 0);
        string colorString = ColorUtility.ToHtmlStringRGBA(scoreTextColor.color);
        PlayerPrefs.SetString(ScoreTextColorKey, colorString);
        PlayerPrefs.Save();
    }
}
