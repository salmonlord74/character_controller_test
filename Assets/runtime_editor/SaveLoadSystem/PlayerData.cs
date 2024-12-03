// PlayerData.cs
using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "Data/PlayerData")]
public class PlayerData : ScriptableObject
{
    public int coin;
    public bool card_crazy;
    public int[] numbers = new int[3];

    // 将 achievementStatuses 更改为可在 Inspector 中显示
    public List<AchievementStatus> achievementStatuses = new List<AchievementStatus>();

}

[System.Serializable]
public class AchievementStatus
{
    public string id; // 与 AchievementDefinition 的 id 对应
    public bool isUnlocked; // 是否已解锁
    public int currentValue; // 当前进度值
    public string unlockTime; // 解锁时间，以字符串形式存储

    // 添加一个方法来设置解锁时间
    public void SetUnlockTime()
    {
        unlockTime = DateTime.Now.ToString("o"); // 使用 ISO 8601 格式
    }

    // 添加一个方法来获取解锁时间的 DateTime 对象
    public DateTime GetUnlockTime()
    {
        if (DateTime.TryParseExact(unlockTime, "o", null, System.Globalization.DateTimeStyles.RoundtripKind, out DateTime result))
        {
            return result;
        }
        return DateTime.MinValue;
    }
}
