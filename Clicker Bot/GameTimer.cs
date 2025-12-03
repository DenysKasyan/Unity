using UnityEngine;
using System;
using TMPro;
using System.IO;

public class GameTimer : MonoBehaviour
{
    private DateTime lastExitTime;
    public GameManager sp1;

    private const int MaxCoins = 30000;        // Максимальна кількість монет
    private const float MaxTimeSeconds = 3 * 60 * 60f; // 3 години в секундах

    void Start()
    {
        LoadGame();

        if (!string.IsNullOrEmpty(lastExitTime.ToString()))
        {
            TimeSpan timeSpent = DateTime.Now - lastExitTime;

            // Обмежуємо час до 3 годин
            double clampedSeconds = Math.Min(timeSpent.TotalSeconds, MaxTimeSeconds);

            int coinsReward = (int)(clampedSeconds / MaxTimeSeconds * MaxCoins);

            AddCoins(coinsReward);
        }
    }

    void OnApplicationPause(bool pauseStatus)
    {
        if (pauseStatus && enabled)
        {
            lastExitTime = DateTime.Now;
            SaveGame();
        }
    }

    void OnApplicationQuit()
    {
        if (enabled)
        {
            lastExitTime = DateTime.Now;
            SaveGame();
        }
    }

    void AddCoins(int coinsToAdd)
    {
        if (!enabled) return;

        GameManager.money += coinsToAdd;
        UpdateCoinUI();
        SaveGame();

        Debug.Log($"Бот зібрав {coinsToAdd} монет! Всього: {GameManager.money}");
    }

    void UpdateCoinUI()
    {
        if (sp1.moneyText != null)
        {
            sp1.moneyText.text = $"{GameManager.money}";
        }
    }

    private void SaveGame()
    {
        GameData data = new GameData
        {
            money = GameManager.money,
            lastExitTime = lastExitTime.ToString()
        };

        string json = JsonUtility.ToJson(data);
        string encryptedJson = EncryptionHelper.Encrypt(json);
        File.WriteAllText(Application.persistentDataPath + "/savefile_timer.bat", encryptedJson);
    }

    private void LoadGame()
    {
        string path = Application.persistentDataPath + "/savefile_timer.bat";
        if (File.Exists(path))
        {
            string decryptedJson = EncryptionHelper.Decrypt(File.ReadAllText(path));
            GameData data = JsonUtility.FromJson<GameData>(decryptedJson);

            GameManager.money = data.money;
            lastExitTime = DateTime.Parse(data.lastExitTime);
        }
        else
        {
            GameManager.money = 0;
            lastExitTime = DateTime.Now;
        }
    }

    [System.Serializable]
    public class GameData
    {
        public int money;
        public string lastExitTime;
    }
}
