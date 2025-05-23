using UnityEngine;

namespace Achievements
{
    public class AchievementPanel : MonoBehaviour
    {
        public string achievementDescription;
        public string achievementPrefCode;

        public void Select()
        {
            Achievement achievement = FindAnyObjectByType<AchievementController>().Achievements
                .Find(x => x.achievementUserPrefsCodeName == achievementPrefCode);

            if (achievement.completed)
            {
                achievementDescription = achievement.achievementName + "\n" + achievement.subMessage;
            }
            else
            {
                achievementDescription = achievement.achievementName + "\n???";
            }

            FindAnyObjectByType<AchievementLabel>().AchievementText(achievementDescription);
        }
    }
}