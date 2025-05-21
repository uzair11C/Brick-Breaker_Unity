using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private int score;

    // [SerializeField]
    private int lives = 3;

    [SerializeField]
    private TextMeshProUGUI livesText;

    [SerializeField]
    private TextMeshProUGUI scoreText;

    [SerializeField]
    GameObject gameOverUI;

    [SerializeField]
    GameObject levelCompleteUI;

    [SerializeField]
    private int numberOfBricks;

    [SerializeField]
    private float paddleSpeed;

    [SerializeField]
    private float boostedSpeed = 15f;

    [SerializeField]
    private float boostDuration = 5f;

    private float originalSpeed;
    private bool isBoosted = false;

    [SerializeField]
    private float boostTimer = 0f; // To track remaining time

    [SerializeField]
    private Slider boostSlider;

    void Start()
    {
        livesText.text = "Lives: " + lives.ToString();
        scoreText.text = "Score: " + score.ToString();
        numberOfBricks = GameObject.FindGameObjectsWithTag("brick").Length;
        boostSlider.maxValue = boostDuration;
        boostSlider.value = 0;
    }

    void Update()
    {
        if (isBoosted)
        {
            boostSlider.value = boostTimer;
        }
        else
        {
            boostSlider.value = 0;
        }
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

    public float GetPaddleSpeed()
    {
        return paddleSpeed;
    }

    public void BoostPaddleSpeed()
    {
        if (!isBoosted)
        {
            boostSlider.gameObject.SetActive(true);
            isBoosted = true;
            originalSpeed = paddleSpeed;
            paddleSpeed = boostedSpeed;
            StartCoroutine(ResetPaddleSpeed());
        }
    }

    private IEnumerator ResetPaddleSpeed()
    {
        boostTimer = boostDuration;
        while (boostTimer > 0)
        {
            boostTimer -= Time.deltaTime;
            yield return null;
        }

        paddleSpeed = originalSpeed;
        isBoosted = false;
        boostTimer = 0;
        boostSlider.gameObject.SetActive(false);
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
            LevelComplete();
        }
    }

    void GameOver()
    {
        // PlayerPrefs.SetInt("BrickBreaker_highScore", score);
        gameOverUI.SetActive(true);
        Time.timeScale = 0f;
    }

    void LevelComplete()
    {
        levelCompleteUI.SetActive(true);
        Time.timeScale = 0f;
    }
}
