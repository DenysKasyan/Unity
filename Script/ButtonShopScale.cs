using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonShopScale : MonoBehaviour
{
    public GameObject[] prefabsToScale; 
    public float scaleFactor = 0.9f; 
    public float scaleDuration = 0.1f; 

    private Vector3[] initialScales; 

    void Start()
    {
        initialScales = new Vector3[prefabsToScale.Length];
        for (int i = 0; i < prefabsToScale.Length; i++)
        {
            initialScales[i] = prefabsToScale[i].transform.localScale;
        }
    }

    public void OnButtonClick(int prefabIndex)
    {
        if (prefabIndex >= 0 && prefabIndex < prefabsToScale.Length)
        {
            StartCoroutine(ScalePrefab(prefabIndex));
        }
    }

    IEnumerator ScalePrefab(int prefabIndex)
    {
        GameObject prefab = prefabsToScale[prefabIndex];
        Vector3 initialScale = initialScales[prefabIndex];
        float elapsedTime = 0f;

        while (elapsedTime < scaleDuration)
        {
            prefab.transform.localScale = Vector3.Lerp(initialScale, initialScale * scaleFactor, elapsedTime / scaleDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        elapsedTime = 0f;
        while (elapsedTime < scaleDuration)
        {
            prefab.transform.localScale = Vector3.Lerp(initialScale * scaleFactor, initialScale, elapsedTime / scaleDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
    }
}
