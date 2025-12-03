using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ButtonScale : MonoBehaviour
{
    public GameObject prefabToScale;
    public float scaleFactor = 0.9f;
    public float scaleDuration = 0.1f;

    private Vector3 initialScale;


    private const string ScaleFactorKey = "ScaleFactor";
    private const string ScaleDurationKey = "ScaleDuration";

    void Start()
    {
        initialScale = prefabToScale.transform.localScale;

        if (PlayerPrefs.HasKey(ScaleFactorKey))
        {
            scaleFactor = PlayerPrefs.GetFloat(ScaleFactorKey);
        }
        if (PlayerPrefs.HasKey(ScaleDurationKey))
        {
            scaleDuration = PlayerPrefs.GetFloat(ScaleDurationKey);
        }
    }

    public void OnButtonClick()
    {
        StartCoroutine(ScalePrefab());
    }

    IEnumerator ScalePrefab()
    {
        float elapsedTime = 0f;
        while (elapsedTime < scaleDuration)
        {
            prefabToScale.transform.localScale = Vector3.Lerp(initialScale, initialScale * scaleFactor, elapsedTime / scaleDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        elapsedTime = 0f;
        while (elapsedTime < scaleDuration)
        {
            prefabToScale.transform.localScale = Vector3.Lerp(initialScale * scaleFactor, initialScale, elapsedTime / scaleDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
    }

    public void ONanim()
    {
        scaleFactor = 0.9f;
        scaleDuration = 0.1f;

        
        PlayerPrefs.SetFloat(ScaleFactorKey, scaleFactor);
        PlayerPrefs.SetFloat(ScaleDurationKey, scaleDuration);
        PlayerPrefs.Save(); 
    }

    public void OFFanim()
    {
        scaleFactor = 0f;
        scaleDuration = 0f;

        
        PlayerPrefs.SetFloat(ScaleFactorKey, scaleFactor);
        PlayerPrefs.SetFloat(ScaleDurationKey, scaleDuration);
        PlayerPrefs.Save();     
    }
}
