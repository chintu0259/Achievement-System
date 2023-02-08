using UnityEngine;
using UnityEngine.UI;

namespace UnikhGames
{
    public class AchievementUI : MonoBehaviour
    {
        public AchievementsData achievementsData;

        public AchievementCell[] achievementCell;

        public Text coinsValue;

        private int badgeLevel;
        private int counter;

        // Start is called before the first frame update
        void Start()
        {
            UpdateCells();
        }

        public void UpdateCells()
        {
            for (int i = 0; i < achievementCell.Length; i++)
            {
                badgeLevel = achievementsData.achievement[i].badgeLevel;
                counter = achievementsData.achievement[i].counter;


                if (badgeLevel < 5)
                {

                    achievementCell[i].completeObject.SetActive(false);
                    // achievementCell[i].progresState.SetActive(true);
                    achievementCell[i].claimButton.SetActive(false);
                    achievementCell[i].counterValue.text = counter.ToString();
                    achievementCell[i].targetValue.text = (achievementsData.achievement[i].subAchievement[badgeLevel].target).ToString();
                    achievementCell[i].rewardValue.text = (achievementsData.achievement[i].subAchievement[badgeLevel].reward).ToString();
                    achievementCell[i].badgesBar.fillAmount = (float)(badgeLevel + 1) / 5;
                    Debug.Log("first acchivement badge bar amount is" + achievementCell[i].badgesBar.fillAmount);
                    achievementCell[i].progressBar.fillAmount = (float)counter / achievementsData.achievement[i].subAchievement[badgeLevel].target;
                    Debug.Log("first acchivement progress bar amount is" + achievementCell[i].progressBar.fillAmount);

                    if (achievementsData.achievement[i].counter >= achievementsData.achievement[i].subAchievement[badgeLevel].target)
                    {
                        achievementsData.achievement[i].counter = achievementsData.achievement[i].subAchievement[badgeLevel].target;
                        achievementCell[i].counterValue.text = counter.ToString();
                        achievementCell[i].claimButton.SetActive(true);
                        // achievementCell[i].progresState.SetActive(false);
                    }

                    if (achievementsData.achievement[i].completed)
                    {
                        achievementCell[i].completeObject.SetActive(true);
                        achievementCell[i].claimButton.SetActive(false);
                        achievementCell[i].targetValue.gameObject.SetActive(false);
                        achievementCell[i].rewardValue.gameObject.SetActive(false);
                        achievementCell[i].badgesBar.fillAmount = 1;
                        achievementCell[i].progressBar.gameObject.SetActive(false);
                    }
                }
            }

            coinsValue.text = achievementsData.Coins.ToString();


        }

        public void ClaimRewardButton(int i)
        {
            if (achievementsData.achievement[i].badgeLevel < 4)
            {
                achievementCell[i].claimButton.SetActive(false);
                achievementsData.achievement[i].badgeLevel = achievementsData.achievement[i].badgeLevel + 1;
                achievementsData.achievement[i].counter = 0;
                Debug.Log("first acchivement badge level is" + achievementsData.achievement[i].badgeLevel);
            }
            else                                                     // claim reward for the last time
            {
                achievementsData.achievement[i].badgeLevel = 4;
                achievementsData.achievement[i].counter = 0;
                achievementsData.achievement[i].completed = true;
            }

            achievementsData.Coins += achievementsData.achievement[i].subAchievement[achievementsData.achievement[i].badgeLevel].reward;
            AchievementSystem.Singleton.SaveData();

            UpdateCells();
        }

    }
}

