using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;
using TMPro;

public class LoadingScreen : MonoBehaviour
{
    public TMP_Text progressText;

    public Animator animator;


    private float targetProgress = 0f;
    private float displayedProgress = 0f;

    void Start()
    {
        StartCoroutine(LoadAsyncScene("MainGameScene"));
    }


    void Update()
    {
        float progress = Mathf.Clamp01(displayedProgress); // 0..1
        animator.Play("LoadingProgress", 0, progress); // керуємо часом анімації
    }

    IEnumerator LoadAsyncScene(string sceneName)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);
        operation.allowSceneActivation = false;

        float fakeDelay = 1f;

        while (!operation.isDone)
        {
            targetProgress = Mathf.Clamp01(operation.progress / 0.9f);

            // Плавне оновлення
            displayedProgress = Mathf.MoveTowards(displayedProgress, targetProgress, Time.deltaTime * 2f);
            progressText.text = (displayedProgress * 100f).ToString("F0") + "%";

            if (operation.progress >= 0.9f && displayedProgress >= 1f)
            {
                yield return new WaitForSeconds(fakeDelay);
                operation.allowSceneActivation = true;
            }

            yield return null;
        }
    }
}
