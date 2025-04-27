using Managers;
using UnityEngine;

namespace GameUI
{
    public class ScoreUIGrabber : MonoBehaviour
    {
        public TMPro.TMP_Text scoreText;
        public TMPro.TMP_Text highscoreText;
        public GameObject newHighscore;
        public string levelNumber;

        public void Start()
        {
            var highscore = PlayerPrefs.GetInt("Highscore" + levelNumber, 0);
            var score = ScoreManager.Instance.score;

            if (highscore > score)
            {
                highscoreText.text = highscore.ToString();
            }
            else
            {
                highscoreText.text = score.ToString();
                PlayerPrefs.SetInt("Highscore" + levelNumber, score);
                newHighscore.SetActive(true);
            }

            scoreText.text = score.ToString();
        }
    }
}