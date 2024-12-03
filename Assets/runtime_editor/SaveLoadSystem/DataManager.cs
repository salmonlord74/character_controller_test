using System.IO;
using UnityEngine;
using static PlayerData;

public class DataManager
{
    private static DataManager _instance;
    public static DataManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new DataManager();
            }
            return _instance;
        }
    }

    public delegate void OnDataChanged();
    public event OnDataChanged DataChanged;

    public PlayerData playerData;
 
    public void SavePlayer()
    {
        string filePath = Path.Combine(Application.persistentDataPath, "Save.json");
        SaveLoadImplement.Save(filePath, playerData);
        Debug.Log("Player data saved.");
    }

    public void LoadPlayer()
    {
        string filePath = Path.Combine(Application.persistentDataPath, "Save.json");
        PlayerData data = SaveLoadImplement.Load<PlayerData>(filePath);
        if (data != null)
        {
            playerData.coin = data.coin;
            playerData.card_crazy = data.card_crazy;
            playerData.numbers = data.numbers;
            playerData.achievementStatuses = data.achievementStatuses;

            DataChange();

            Debug.Log("Player data loaded.");
        }
        else
        {
            Debug.LogWarning("加载玩家数据失败。");
        }
    }

    public void IncrementCoin(int c)
    {
        playerData.coin += c;
        DataChange();
    }

    public void SetCardCrazy(bool value)
    {
        playerData.card_crazy = value;
        DataChange();
    }
    public void DataChange()
    {
        DataChanged?.Invoke();
    }


}
