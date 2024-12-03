// AchievementManager.cs
using System;
using System.Collections.Generic;
using UnityEngine;

public class AchievementManager
{
    private static AchievementManager _instance;
    public static AchievementManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new AchievementManager();
            }
            return _instance;
        }
    }

    public delegate void OnAchievementChanged(AchievementDefinition achievement);
    public event OnAchievementChanged AchievementChanged;

    // 所有的成就定义
    public AchievementDefinition[] achievementDefinitions;

    // 玩家成就状态映射，key 为成就 id
    private Dictionary<string, AchievementStatus> achievementStatuses = new Dictionary<string, AchievementStatus>();

    public void InitializeAchievements()
    {
        // 从 playerData 中加载成就状态
        foreach (var definition in achievementDefinitions)
        {
            AchievementStatus status = DataManager.Instance.playerData.achievementStatuses.Find(s => s.id == definition.id);
            if (status == null)
            {
                // 如果玩家数据中没有该成就状态，创建新的并添加到列表中
                status = new AchievementStatus
                {
                    id = definition.id,
                    isUnlocked = false,
                    currentValue = 0,
                    unlockTime = DateTime.MinValue.ToString("o")
                };
                DataManager.Instance.playerData.achievementStatuses.Add(status);
            }

            // 添加到映射中，方便快速查找
            achievementStatuses[definition.id] = status;
        }
    }

    // 更新成就进度的方法
    public void UpdateAchievementProgress(string achievementId, int increment = 1)
    {
        if (achievementStatuses.TryGetValue(achievementId, out var status))
        {
            var definition = GetAchievementDefinitionById(achievementId);
            if (definition != null && !status.isUnlocked)
            {
                status.currentValue += increment;
                if (status.currentValue >= definition.targetValue)
                {
                    UnlockAchievement(status, definition);
                }
            }
        }
    }

    // 解锁成就的方法
    private void UnlockAchievement(AchievementStatus status, AchievementDefinition definition)
    {
        status.isUnlocked = true;
        status.SetUnlockTime();

        // 发放奖励（如果有）
        GrantReward(definition.reward);

        // 显示通知
        ShowAchievementUnlockedUI(definition);

        // 保存状态（可选，根据您的存档逻辑）
        // SaveAchievementStatuses();
    }
    private void GrantReward(int reward)
    {
        DataManager.Instance.IncrementCoin(reward);
    }

    private void ShowAchievementUnlockedUI(AchievementDefinition definition)
    {
        // 实现成就解锁的 UI 展示，例如弹出提示框
        Debug.Log($"成就解锁：{definition.title}");

        AchievementChanged?.Invoke(definition);
    }

    private AchievementDefinition GetAchievementDefinitionById(string id)
    {
        foreach (var definition in achievementDefinitions)
        {
            if (definition.id == id)
                return definition;
        }
        return null;
    }

    // 获取成就状态（用于 UI 显示等）
    public AchievementStatus GetAchievementStatus(string achievementId)
    {
        if (achievementStatuses.TryGetValue(achievementId, out var status))
        {
            return status;
        }
        return null;
    }
}
