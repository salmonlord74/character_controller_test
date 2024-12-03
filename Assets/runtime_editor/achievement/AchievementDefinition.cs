// AchievementDefinition.cs
using UnityEngine;

[CreateAssetMenu(fileName = "AchievementDefinition", menuName = "Achievements/AchievementDefinition")]
public class AchievementDefinition : ScriptableObject
{
    public string id; // 成就的唯一标识符
    public string title; // 成就标题
    public string description; // 成就描述
    public int targetValue; // 完成成就所需的目标值
                            // 其他成就属性，如图标、奖励等
    public int reward;

}
