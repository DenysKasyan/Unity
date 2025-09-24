using System;
using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.UI;
using System.IO;
using System.Security.Cryptography;
using System.Text;

[System.Serializable]
public class GameData
{
    public int money;
    public int rate;
    public int rate2;
    public int energy;
    public int maxEnergy;
    public int energyUpgrades;
    public float energyRegenerationProgress;
    public float energyRegenInterval;
    public float bestEnergyRegenInterval;
    public string lastExitTime;
}

public class GRNupgrade : MonoBehaviour
{
    public static int money;
    public static int rate = 1;
    public static int rate2 = 1;
    public static int skinBonus = 0; // бонус від скіна, не змінює rate
    public static int energy;
    public static int maxEnergy;

    public int[] clickUpgradeCosts;
    public int[] energyUpgradeCosts;
    public int energyUpgrades = 0;

    private float energyRegenerationProgress = 0f;
    public static float energyRegenInterval = 3f;
    public static float bestEnergyRegenInterval = 3f;


    // UI ��������
    public TextMeshProUGUI moneyup;
    public TMP_Text moneyup2;
    public Button upgradeButton;
    public Button energyUpgradeButton;
    public TMP_Text moneyText;
    public TMP_Text energyText;
    public TMP_Text energyUpgradeText;
    public TMP_Text moneyupdate;
    public TMP_Text energyupdate;

    private DateTime lastExitTime;

    void Start()
    {
        LoadGame();

        if (!string.IsNullOrEmpty(lastExitTime.ToString()))
        {
            TimeSpan timeSpent = DateTime.Now - lastExitTime;

            int energyReward = (int)(timeSpent.TotalSeconds / energyRegenInterval); // / 5.0 - це секунди, 1 секунда 1 монета
            AddEnergy(energyReward);
        }

        StartCoroutine(RegenerateEnergy());
    }

    void Update()
    {
        moneyText.text = "" + money.ToString("N0");
        moneyup.text = "" + rate.ToString();
        moneyup2.text = "" + rate2.ToString();
        energyText.text = $"{energy}/{maxEnergy}";
        moneyupdate.text = "" + CalculateUpgradeCost().ToString();
        energyupdate.text = "" + CalculateEnergyUpgradeCost().ToString();
        energyUpgradeText.text = "" + rate2.ToString();

        int upgradeCost = CalculateUpgradeCost();
        int energyUpgradeCost = CalculateEnergyUpgradeCost();

        if (energy <= 0)
        {
            upgradeButton.interactable = false;
        }

        bool maxRateReached = rate >= 10;
        bool maxRateReached2 = rate2 >= 10;
        upgradeButton.interactable = !maxRateReached && (money >= upgradeCost);
        energyUpgradeButton.interactable = !maxRateReached2 && (money >= energyUpgradeCost);

        moneyupdate.text = maxRateReached ? "Макс.." : CalculateUpgradeCost().ToString("N0");
        energyupdate.text = maxRateReached2 ? "Макс.." : CalculateEnergyUpgradeCost().ToString("N0");


    }


    public void ButtonClick()
    {
        if (energy > 0)
        {
            money += rate + skinBonus;
            energy -= 1;
            SaveGame();
        }
    }

    public void UpgradeClick()
    {
        int upgradeCost = CalculateUpgradeCost();

        if (money >= upgradeCost)
        {
            money -= upgradeCost;
            rate += 1;

            SaveGame();
            UpdateUI();
        }
    }

    public void EnergyUpgradeClick()
    {
        int energyUpgradeCost = CalculateEnergyUpgradeCost();

        if (money >= energyUpgradeCost)
        {
            money -= energyUpgradeCost;
            maxEnergy += 500;
            rate2 += 1;

            SaveGame();
            UpdateUI();
        }
    }

    void UpdateEnergyUI()
    {
        if (energyText != null)
        {
            energyText.text = $"{energy}/{maxEnergy}";
        }
    }

    void UpdateMoneyUI()
    {
        if (moneyText != null)
        {
            moneyText.text = $"{money}";
        }
    }

    /* public void ResetStatistics()
       {
           // ��������� ���� ����������
           string path = Application.persistentDataPath + "/savefile.dat";
           if (System.IO.File.Exists(path))
           {
               System.IO.File.Delete(path);
           }

           // ����������� �������� �� �������������
           money = 0;
           rate = 1;
           rate2 = 1;
           energy = 500;
           maxEnergy = 500;
           energyUpgrades = 0;
           energyRegenerationProgress = 0f;
           lastExitTime = DateTime.Now;

           // ��������� UI
           UpdateEnergyUI();
           UpdateMoneyUI();
       }
       */
    public int CalculateUpgradeCost()
    {
        if (rate - 1 < clickUpgradeCosts.Length)
            return clickUpgradeCosts[rate - 1]; // беремо ціну з масиву
        else
            return clickUpgradeCosts[clickUpgradeCosts.Length - 1]; // якщо перевищено — беремо останню ціну
    }


    public int CalculateEnergyUpgradeCost()
    {
        if (rate2 - 1 < energyUpgradeCosts.Length)
            return energyUpgradeCosts[rate2 - 1];
        else
            return energyUpgradeCosts[energyUpgradeCosts.Length - 1];
    }


    private IEnumerator RegenerateEnergy()
    {
        while (true)
        {
            energyRegenerationProgress += Time.deltaTime;

            if (energyRegenerationProgress >= energyRegenInterval)
            {
                energy = Mathf.Min(energy + 1, maxEnergy);
                SaveGame();
                energyRegenerationProgress = 0f;
            }

            UpdateEnergyUI();

            yield return null;
        }
    }

    void OnApplicationPause(bool pauseStatus)
    {
        if (pauseStatus)
        {
            lastExitTime = DateTime.Now;
            SaveGame();
        }
    }

    void OnApplicationQuit()
    {
        // �������� ���� ��� ������� ���
        lastExitTime = DateTime.Now;
        SaveGame();
    }

    void AddEnergy(int energyToAdd)
    {
        energy += energyToAdd;
        energy = Mathf.Clamp(energy, 0, maxEnergy); 
        SaveGame();
        UpdateEnergyUI();

        Debug.Log($"������� ������� {energyToAdd} ����㳿! ����� � ����� {energy} ����㳿.");
    }

    private void SaveGame()
    {
        GameData data = new GameData();
        data.money = money;
        data.rate = rate;
        data.rate2 = rate2;
        data.energy = energy;
        data.maxEnergy = maxEnergy;
        data.energyUpgrades = energyUpgrades;
        data.energyRegenerationProgress = energyRegenerationProgress;
        data.lastExitTime = lastExitTime.ToString();
        data.energyRegenInterval = energyRegenInterval;
        data.bestEnergyRegenInterval = bestEnergyRegenInterval;

        string json = JsonUtility.ToJson(data);
        string encryptedJson = EncryptionHelper.Encrypt(json);
        System.IO.File.WriteAllText(Application.persistentDataPath + "/savefile.dat", encryptedJson); // �������� ������������ ����
    }

    private void LoadGame()
    {
        string path = Application.persistentDataPath + "/savefile.dat";
        if (System.IO.File.Exists(path))
        {
            string encryptedJson = System.IO.File.ReadAllText(path);
            string decryptedJson = EncryptionHelper.Decrypt(encryptedJson); 
            GameData data = JsonUtility.FromJson<GameData>(decryptedJson);

            money = data.money;
            rate = data.rate;
            rate2 = data.rate2;
            energy = data.energy;
            maxEnergy = data.maxEnergy;
            energyUpgrades = data.energyUpgrades;
            energyRegenerationProgress = data.energyRegenerationProgress;
            energyRegenInterval = data.energyRegenInterval;
            bestEnergyRegenInterval = data.bestEnergyRegenInterval;
            lastExitTime = DateTime.Parse(data.lastExitTime);
        }
        else
        {
            money = 0;
            rate = 1;
            rate2 = 1;
            energy = 1000;
            maxEnergy = 1000;
            energyUpgrades = 0;
            energyRegenerationProgress = 0f;
            lastExitTime = DateTime.Now;
        }

        UpdateUI();
    }

    private void UpdateUI()
    {
        UpdateEnergyUI();
        UpdateMoneyUI();
    }
}

public static class EncryptionHelper
{
    private static readonly string Key = "12345678901234567890123456789012"; // ���������, "12345678901234567890123456789012"
    private static readonly byte[] IV = Encoding.UTF8.GetBytes("1234567890123456"); // 16 ���� ��� AES

    public static string Encrypt(string plainText)
    {
        using (Aes aesAlg = Aes.Create())
        {
            aesAlg.Key = Encoding.UTF8.GetBytes(Key);
            aesAlg.IV = IV;

            ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

            using (MemoryStream msEncrypt = new MemoryStream())
            {
                using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                {
                    using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                    {
                        swEncrypt.Write(plainText);
                    }
                    return Convert.ToBase64String(msEncrypt.ToArray());
                }
            }
        }
    }

    public static string Decrypt(string cipherText)
    {
        using (Aes aesAlg = Aes.Create())
        {
            aesAlg.Key = Encoding.UTF8.GetBytes(Key);
            aesAlg.IV = IV;

            ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

            using (MemoryStream msDecrypt = new MemoryStream(Convert.FromBase64String(cipherText)))
            {
                using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                {
                    using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                    {
                        return srDecrypt.ReadToEnd();
                    }
                }
            }
        }
    }
}