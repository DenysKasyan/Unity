using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
     public static int money;
    public static int rate = 1;
    public static int rate2 = 1;
    public static int skinBonus = 0;
    public static int energy;
    public static int maxEnergy;
    public static float energyRegenInterval = 3f;
    public static float bestEnergyRegenInterval = 3f;

    public int[] clickUpgradeCosts;
    public int[] energyUpgradeCosts;
    public int energyUpgrades = 0;

    private float energyRegenProgress;
    private DateTime lastExitTime;

    [Header("UI")]
    public TMP_Text moneyText;
    public TMP_Text energyText;
    public TMP_Text moneyUpgradeCostText;
    public TMP_Text energyUpgradeCostText;
    public TMP_Text rateText;
    public TMP_Text rate2Text;
    public Button upgradeButton;
    public Button energyUpgradeButton;

    void Start()
    {
        LoadGame();
        RestoreOfflineProgress();
        StartCoroutine(RegenerateEnergy());
    }

    void Update()
    {
        UpdateUI();
    }

    public void ButtonClick()
    {
        if (energy > 0)
        {
            money += rate + skinBonus;
            energy--;
            SaveGame();
        }
    }

    public void UpgradeClick()
    {
        int cost = CalculateUpgradeCost();
        if (money >= cost)
        {
            money -= cost;
            rate++;
            SaveGame();
        }
    }

    public void EnergyUpgradeClick()
    {
        int cost = CalculateEnergyUpgradeCost();
        if (money >= cost)
        {
            money -= cost;
            rate2++;
            maxEnergy += 500;
            SaveGame();
        }
    }

    private void UpdateUI()
    {
        moneyText.text = money.ToString("N0");
        energyText.text = $"{energy}/{maxEnergy}";
        moneyUpgradeCostText.text = rate >= 10 ? "Макс." : CalculateUpgradeCost().ToString("N0");
        energyUpgradeCostText.text = rate2 >= 10 ? "Макс." : CalculateEnergyUpgradeCost().ToString("N0");
        rateText.text = rate.ToString();
        rate2Text.text = rate2.ToString();

        upgradeButton.interactable = rate < 10 && money >= CalculateUpgradeCost();
        energyUpgradeButton.interactable = rate2 < 10 && money >= CalculateEnergyUpgradeCost();
    }

    private int CalculateUpgradeCost() =>
        clickUpgradeCosts[Mathf.Min(rate - 1, clickUpgradeCosts.Length - 1)];

    private int CalculateEnergyUpgradeCost() =>
        energyUpgradeCosts[Mathf.Min(rate2 - 1, energyUpgradeCosts.Length - 1)];

    private IEnumerator RegenerateEnergy()
    {
        while (true)
        {
            energyRegenProgress += Time.deltaTime;
            if (energyRegenProgress >= energyRegenInterval)
            {
                energy = Mathf.Min(energy + 1, maxEnergy);
                energyRegenProgress = 0;
                SaveGame();
            }
            yield return null;
        }
    }

    private void RestoreOfflineProgress()
    {
        if (lastExitTime == DateTime.MinValue) return;

        TimeSpan timeAway = DateTime.Now - lastExitTime;
        int restoredEnergy = (int)(timeAway.TotalSeconds / energyRegenInterval);
        AddEnergy(restoredEnergy);
    }

    private void AddEnergy(int amount)
    {
        energy = Mathf.Clamp(energy + amount, 0, maxEnergy);
    }

    private void SaveGame()
    {
        GameData data = new GameData
        {
            money = money,
            rate = rate,
            rate2 = rate2,
            energy = energy,
            maxEnergy = maxEnergy,
            energyUpgrades = energyUpgrades,
            energyRegenerationProgress = energyRegenProgress,
            energyRegenInterval = energyRegenInterval,
            bestEnergyRegenInterval = bestEnergyRegenInterval,
            lastExitTime = DateTime.Now.ToString()
        };

        SaveSystem.Save(data);
    }

    private void LoadGame()
    {
        GameData data = SaveSystem.Load();

        if (data == null)
        {
            money = 0;
            rate = 1;
            rate2 = 1;
            energy = 1000;
            maxEnergy = 1000;
            energyUpgrades = 0;
            energyRegenProgress = 0f;
            lastExitTime = DateTime.Now;
            return;
        }

        money = data.money;
        rate = data.rate;
        rate2 = data.rate2;
        energy = data.energy;
        maxEnergy = data.maxEnergy;
        energyUpgrades = data.energyUpgrades;
        energyRegenProgress = data.energyRegenerationProgress;
        energyRegenInterval = data.energyRegenInterval;
        bestEnergyRegenInterval = data.bestEnergyRegenInterval;
        lastExitTime = DateTime.Parse(data.lastExitTime);
    }

    void OnApplicationPause(bool pause) 
    {
        if (pause) SaveGame();
    }

    void OnApplicationQuit() 
    {
        SaveGame();
    }
}
