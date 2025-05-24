using System.Collections;
using TMPro;
using UnityEngine;
// using UnityEngine.TextCore.Text;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    /**************  Game State Variables  **************/
    private int score;
    private int lives = 3;
    private int numberOfBricks;

    [SerializeField]
    private TextMeshProUGUI livesText;

    [SerializeField]
    private TextMeshProUGUI scoreText;
    public bool ballInPlay;

    /****************************************************/

    /**************  GameObjects  **************/
    public Rigidbody2D ball;

    [SerializeField]
    private Transform paddleTransform;

    /****************************************************/

    /**************  Paddle Speed Boost   **************/
    [SerializeField]
    private float paddleSpeed;

    [SerializeField]
    private float boostedSpeed = 15f;

    [SerializeField]
    private float boostDuration = 5f;

    private float originalSpeed;
    private bool isBoosted = false;

    // Boost Timer
    [SerializeField]
    private float boostTimer = 0f; // To track remaining time

    [SerializeField]
    private Slider boostSlider;

    /****************************************************/

    /**************  UIs   **************/
    [SerializeField]
    GameObject gameOverUI;

    [SerializeField]
    GameObject levelCompleteUI;

    [SerializeField]
    TextMeshProUGUI winCurrentScoreText;

    [SerializeField]
    TextMeshProUGUI loseCurrentScoreText;

    /****************************************************/

    /**************  Level Generation   **************/
    [SerializeField]
    TextAsset[] levelJsonFiles;

    [SerializeField]
    GameObject[] oneHitBricks;

    [SerializeField]
    GameObject[] twoHitBricks;

    [SerializeField]
    Vector2 startPosition;

    [SerializeField]
    private float brickWidth;

    [SerializeField]
    private float brickHeight;

    /****************************************************/

    void Start()
    {
        ClearOldBricks();
        LoadAndSpawnRandomLevel();
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

    private void LoadAndSpawnRandomLevel()
    {
        ResetBallAndPaddle();
        TextAsset jsonFile = levelJsonFiles[Random.Range(0, levelJsonFiles.Length)];

        LevelData level = JsonUtility.FromJson<LevelData>(jsonFile.text);
        SpawnLevel(level);
    }

    private void SpawnLevel(LevelData level)
    {
        for (int row = 0; row < level.rows; row++)
        {
            for (int col = 0; col < level.columns; col++)
            {
                int brickType = level.GetBrickType(row, col);

                if (brickType == 0)
                {
                    continue;
                }

                Vector2 position =
                    startPosition + new Vector2(col * brickWidth, -row * brickHeight);

                GameObject brickPrefab =
                    brickType == 1
                        ? oneHitBricks[Random.Range(0, oneHitBricks.Length)]
                        : twoHitBricks[Random.Range(0, twoHitBricks.Length)];
                Instantiate(brickPrefab, position, Quaternion.identity);
            }
        }
        numberOfBricks = GameObject.FindGameObjectsWithTag("brick").Length;
    }

    private void ClearOldBricks()
    {
        foreach (var brick in GameObject.FindGameObjectsWithTag("brick"))
        {
            Destroy(brick);
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
            Invoke(nameof(LevelComplete), 1f);
        }
    }

    void GameOver()
    {
        if (score > PlayerPrefs.GetInt("BrickBreaker_highScore", 0))
        {
            Debug.Log("New High Score!");
            PlayerPrefs.SetInt("BrickBreaker_highScore", score);
        }
        gameOverUI.SetActive(true);
        loseCurrentScoreText.text = "Your Score: " + score.ToString();
        ResetBallAndPaddle();
        Time.timeScale = 0f;
    }

    void LevelComplete()
    {
        winCurrentScoreText.text = "Your Score: " + score.ToString();
        if (score > PlayerPrefs.GetInt("BrickBreaker_highScore", 0))
        {
            Debug.Log("New High Score!");
            PlayerPrefs.SetInt("BrickBreaker_highScore", score);
        }
        Time.timeScale = 0f;
        ResetBallAndPaddle();
        levelCompleteUI.SetActive(true);
    }

    public void ResetBallPosiiton()
    {
        ballInPlay = false;
        Transform paddleAnchor = paddleTransform.Find("anchor");
        ball.transform.position = paddleAnchor.position;
    }

    public void ResetBallAndPaddle()
    {
        ResetBallPosiiton();
        paddleTransform.position = new Vector2(0, -4.3f);
        ball.velocity = Vector2.zero;
    }

    public void ContinuePlaying()
    {
        ResetBallAndPaddle();
        Time.timeScale = 1f;
        levelCompleteUI.SetActive(false);
        ClearOldBricks();
        LoadAndSpawnRandomLevel();
    }
}
