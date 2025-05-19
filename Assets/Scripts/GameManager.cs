using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int lives = 3;

    // [SerializeField]
    public int score = 0;

    [SerializeField]
    private TextMeshProUGUI livesText;

    [SerializeField]
    private TextMeshProUGUI scoreText;

    void Start()
    {
        livesText.text = "Lives: " + lives.ToString();
        scoreText.text = "Score: " + score.ToString();
    }

    public void UpdateLives(int amount)
    {
        lives += amount;
        // if (lives > 0)
        // {
        livesText.text = "Lives: " + lives.ToString();
        // }
        // else
        // {
        //     Debug.Log("Game Over");
        // }
    }

    public void UpdateScore(int amount)
    {
        score += amount;
        scoreText.text = "Score: " + score.ToString();
    }
}
