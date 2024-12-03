// SaveLoadImplement.cs
using System;
using System.IO;
using UnityEngine;

public static class SaveLoadImplement
{
    public static void Save(string filePath, PlayerData pObject)
    {
        try
        {
            string saveString = JsonUtility.ToJson(pObject, true);
            File.WriteAllText(filePath, saveString);
            Debug.Log("数据保存成功。");
        }
        catch (Exception ex)
        {
            Debug.LogError($"保存数据失败: {ex.Message}");
        }
    }

    public static PlayerData Load<PlayerData>(string filePath) where PlayerData : ScriptableObject
    {
        try
        {
            if (!File.Exists(filePath))
            {
                Debug.LogWarning("保存文件不存在。");
                return null;
            }

            string data = File.ReadAllText(filePath);
            PlayerData deserializedData = ScriptableObject.CreateInstance<PlayerData>();
            JsonUtility.FromJsonOverwrite(data, deserializedData);
            return deserializedData;
        }
        catch (Exception ex)
        {
            Debug.LogError($"加载数据失败: {ex.Message}");
            return null;
        }
    }
}
