using UnityEngine;

public class Music : MonoBehaviour
{
    public GameObject[] panelActive;
    public GameObject[] CheckMark;

    void Start()
    {
    for (int i = 0; i < panelActive.Length; i++)
    {
        string key = "Panel_" + i;
        if (PlayerPrefs.HasKey(key) && PlayerPrefs.GetInt(key) == 0)
        {
            if (panelActive[i] != null)
            {
                Destroy(panelActive[i]);
            }
        }
    }

    foreach (GameObject check in CheckMark)
    {
        if (check != null)
            check.SetActive(false);
    }

    int savedIndex;

    if (PlayerPrefs.HasKey("ActiveCheckmark"))
    {
        savedIndex = PlayerPrefs.GetInt("ActiveCheckmark");
    }
    else
    {
        savedIndex = 0;
        PlayerPrefs.SetInt("ActiveCheckmark", 0);
        PlayerPrefs.Save();
    }

    if (savedIndex >= 0 && savedIndex < CheckMark.Length && CheckMark[savedIndex] != null)
    {
        CheckMark[savedIndex].SetActive(true);
    }
    }


    public void deleteActPanel(int index)
    {
        if (panelActive[index] != null)
        {
            Destroy(panelActive[index]);
            PlayerPrefs.SetInt("Panel_" + index, 0);
            PlayerPrefs.Save();
        }
    }

    public void CheckMarkk(int index)
    {
        for (int i = 0; i < CheckMark.Length; i++)
        {
            if (CheckMark[i] != null)
            {
                CheckMark[i].SetActive(i == index);
            }
        }

        PlayerPrefs.SetInt("ActiveCheckmark", index);
        PlayerPrefs.Save();
    }
}
