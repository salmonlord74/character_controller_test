using UnityEngine;

public class DataInitialize : MonoBehaviour
{
    public PlayerData playerData;
    void Start()
    {
        var dataManager = DataManager.Instance;
        dataManager.playerData = playerData;
    }

}
