using System;
using System.IO;
using UnityEngine;

public class SaveSystem : MonoBehaviour
{
    private static string SavePath => Application.persistentDataPath + "/savefile.dat";

    public static void Save(GameData data)
    {
        string json = JsonUtility.ToJson(data);
        string encrypted = EncryptionHelper.Encrypt(json);
        File.WriteAllText(SavePath, encrypted);
    }

    public static GameData Load()
    {
        if (!File.Exists(SavePath))
            return null;

        string encrypted = File.ReadAllText(SavePath);
        string decrypted = EncryptionHelper.Decrypt(encrypted);
        return JsonUtility.FromJson<GameData>(decrypted);
    }

    public static void DeleteSave()
    {
        if (File.Exists(SavePath))
            File.Delete(SavePath);
    }
}
