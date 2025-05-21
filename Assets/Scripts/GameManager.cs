using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private int lives = 3;
    private int score = 0;

    [SerializeField]
    private TextMeshProUGUI livesText;

    [SerializeField]
    private TextMeshProUGUI scoreText;

    [SerializeField]
    GameObject gameOverUI;

    [SerializeField]
    private int numberOfBricks;

    void Start()
    {
        livesText.text = "Lives: " + lives.ToString();
        scoreText.text = "Score: " + score.ToString();
        numberOfBricks = GameObject.FindGameObjectsWithTag("brick").Length;
    }

    public void UpdateLives(int amount)
    {
        lives += amount;
        livesText.text = "Lives: " + lives.ToString();

        if (lives <= 0)
        {
            lives = 0;
            GameOver();
        }
    }

    public void UpdateScore(int amount)
    {
        score += amount;
        scoreText.text = "Score: " + score.ToString();
    }

    public void BrickDestroyed()
    {
        numberOfBricks--;
        if (numberOfBricks <= 0)
        {
            GameOver();
        }
    }

    void GameOver()
    {
        // PlayerPrefs.SetInt("BrickBreaker_highScore", score);
        gameOverUI.SetActive(true);
        Time.timeScale = 0f;
    }
}
