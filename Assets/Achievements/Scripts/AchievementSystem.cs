using UnityEngine;
using SimpleJSON;

namespace UnikhGames
{
    public class AchievementSystem : MonoBehaviour
    {
        public static AchievementSystem Singleton { get; private set; }

        public AchievementsData achievementsData;
        public AchievementUI achievementUI;

        public static string COINS = "Coins";

        public static string ACHIEVEMENTSDATA = "AchievementsData";                                           // the main string for player prefs
        public static string ACHIEVEMENT = "Achievement";

        public static string BADGELEVEL = "BadgeLevel";
        public static string COUNTER = "Counter";
        public static string COMPLETED = "Completed";


        #region Saving & Loading System

        private void Awake()
        {
            if (Singleton == null)
            {
                Singleton = this;
            }
            else
            {
                Destroy(gameObject);
            }

            DontDestroyOnLoad(this.gameObject);

            if (PlayerPrefs.HasKey(ACHIEVEMENTSDATA) == false)
            {
                InitializeData();
            }
            else
            {
                LoadData();
            }

        }


        public void InitializeData()
        {

            // create a new JSON
            JSONNode nodeData = JSON.Parse("{}");

            nodeData[COINS].AsInt = 0;

            // setting player skins lock status
            for (int i = 0; i < achievementsData.achievement.Length; i++)
            {

                nodeData[ACHIEVEMENTSDATA][ACHIEVEMENT + i.ToString()][BADGELEVEL].AsInt = 0;
                nodeData[ACHIEVEMENTSDATA][ACHIEVEMENT + i.ToString()][COUNTER].AsInt = 0;
                nodeData[ACHIEVEMENTSDATA][ACHIEVEMENT + i.ToString()][COMPLETED].AsBool = false;
            }

            //Now lets save it
            PlayerPrefs.SetString(ACHIEVEMENTSDATA, nodeData.ToJSON(1));
            PlayerPrefs.Save();
            //Lets debug it to be sure
            //  Debug.Log("Initialized Game data: " + PlayerPrefs.GetString(ACHIEVEMENTSDATA));

            //Now load the data
            LoadData();

        }

        public void LoadData()
        {
            //Now lets get our player prefs saves. We know that it has been initialized so we can get it from the player prefs.
            JSONNode nodeData = JSON.Parse(PlayerPrefs.GetString(ACHIEVEMENTSDATA));

            achievementsData.Coins = nodeData[COINS].AsInt;

            for (int i = 0; i < achievementsData.achievement.Length; i++)
            {
                achievementsData.achievement[i].badgeLevel = nodeData[ACHIEVEMENTSDATA][ACHIEVEMENT + i.ToString()][BADGELEVEL].AsInt;
                achievementsData.achievement[i].counter = nodeData[ACHIEVEMENTSDATA][ACHIEVEMENT + i.ToString()][COUNTER].AsInt;
                achievementsData.achievement[i].completed = nodeData[ACHIEVEMENTSDATA][ACHIEVEMENT + i.ToString()][COMPLETED].AsBool;
            }


            //Lets debug it to be sure
            //  Debug.Log("Loaded Game data: " + PlayerPrefs.GetString(ACHIEVEMENTSDATA));

        }


        public void SaveData()
        {
            // create a new JSON
            JSONNode nodeData = JSON.Parse("{}");

            nodeData[COINS].AsInt = achievementsData.Coins;

            for (int i = 0; i < achievementsData.achievement.Length; i++)
            {
                nodeData[ACHIEVEMENTSDATA][ACHIEVEMENT + i.ToString()][BADGELEVEL].AsInt = achievementsData.achievement[i].badgeLevel;
                nodeData[ACHIEVEMENTSDATA][ACHIEVEMENT + i.ToString()][COUNTER].AsInt = achievementsData.achievement[i].counter;
                nodeData[ACHIEVEMENTSDATA][ACHIEVEMENT + i.ToString()][COMPLETED].AsBool = achievementsData.achievement[i].completed;
            }

            //Now lets save it
            PlayerPrefs.SetString(ACHIEVEMENTSDATA, nodeData.ToJSON(1));
            PlayerPrefs.Save();

            //Lets debug it to be sure
            //   Debug.Log("Saved achievements Game data: " + PlayerPrefs.GetString(ACHIEVEMENTSDATA));
            LoadData();
        }

        #endregion


        public void AddAchievement(int i)
        {
            if (achievementsData.achievement[i].counter < achievementsData.achievement[i].subAchievement[achievementsData.achievement[i].badgeLevel].target)
            {
                achievementsData.achievement[i].counter = achievementsData.achievement[i].counter + 1;
                Debug.Log("counter incremented" + achievementsData.achievement[i].counter);
                SaveData();
                achievementUI.UpdateCells();

            }
            else
            {
                achievementsData.achievement[i].counter = achievementsData.achievement[i].subAchievement[achievementsData.achievement[i].badgeLevel].target;

            }

        }

    }
}


