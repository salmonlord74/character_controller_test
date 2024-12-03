using UnityEngine;

public class AchievementInitialize : MonoBehaviour
{

    public AchievementDefinition[] achievementDefinitions;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        AchievementManager.Instance.achievementDefinitions = achievementDefinitions;

        AchievementManager.Instance.InitializeAchievements();
    }
}
