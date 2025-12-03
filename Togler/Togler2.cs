using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Togler2 : MonoBehaviour
{

    [Header("Components")]
    [SerializeField] private Animator anomitor;

    [Header("Settings")]
    [Tooltip("Toggler value.")]
    public bool value;

    private static readonly int Value = Animator.StringToHash(name: "Value2");


    public NumberPopup0 script1;
    public NumberPopup script2;

    private const string ToggleKey = "ToggleValue2";
    private const string AnimatorKey = "AnimatorState2";
    private const string Script1Key = "Script1Active";

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

        bool isScript1Active = PlayerPrefs.GetInt(Script1Key, 1) == 1;

        script1.enabled = isScript1Active;
        script2.enabled = !isScript1Active;

        PlayerPrefs.Save();
    }

    private void Awake()
    {
        if (this.anomitor == null) this.anomitor = GetComponent<Animator>();

        this.anomitor.SetBool(id: Value, this.value);
    }

    public void Toggle2()
    {
        this.value = !this.value;

        this.anomitor.SetBool(id: Value, this.value);

        if (value)
        {
            ActivateScript2();
            PlayerPrefs.SetInt(Script1Key, 0); 
            PlayerPrefs.SetInt(Script1Key, 0);
        }
        else
        {
            ActivateScript1();
            PlayerPrefs.SetInt(Script1Key, 1);
            PlayerPrefs.SetInt(Script1Key, 0); 
        }

        PlayerPrefs.SetInt(ToggleKey, this.value ? 1 : 0);
        PlayerPrefs.SetInt(AnimatorKey, this.value ? 1 : 0);
        PlayerPrefs.Save();
    }

    public void ActivateScript1()
    {
        script1.enabled = true;
        script2.enabled = false;
    }

    public void ActivateScript2()
    {

        script1.enabled = false;
        script2.enabled = true;
    }
}
