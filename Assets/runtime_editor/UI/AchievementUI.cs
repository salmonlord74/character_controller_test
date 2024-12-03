using TMPro;
using UnityEngine;

public class AchievementUI : MonoBehaviour
{
    public TextMeshProUGUI achievementText;
    
    void Start()
    {
        if (AchievementManager.Instance != null)
        {
            AchievementManager.Instance.AchievementChanged += UpdateUI;
        }
        else
        {
            Debug.LogError("AchievementManager 或 PlayerData 未正确分配！");
        }
    }

    void UpdateUI(AchievementDefinition achievement)
    {
        achievementText.text = $"{achievement.title} Complete!!";
    }

    void OnDestroy()
    {
        if (AchievementManager.Instance != null)
        {
            AchievementManager.Instance.AchievementChanged -= UpdateUI;
        }
    }
}
