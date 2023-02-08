using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace UnikhGames
{
    [CreateAssetMenu(fileName = "AchievementsData", menuName = "UnikhGames/AchievementsData", order = 1)]
    public class AchievementsData : ScriptableObject
    {
        [SerializeField] public int Coins;

        [SerializeField] public Achievement[] achievement;  //array of all unlockable car levels


        [System.Serializable]
        public class Achievement
        {
            [SerializeField] public string AchievementName;
            [SerializeField] public int badgeLevel;
            [SerializeField] public int counter;                         //to be saved to file and read back
            [SerializeField] public bool completed;
            [SerializeField] public SubAchievement[] subAchievement;

        }

        [System.Serializable]
        public class SubAchievement
        {
            [SerializeField] public int reward;
            [SerializeField] public int target;

        }

    }
}

