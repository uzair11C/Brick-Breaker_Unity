using UnityEngine;

public class Ball : MonoBehaviour
{
    private Renderer ballRenderer;

    [SerializeField]
    private ColorScript colorScript;

    [SerializeField]
    private float speed = 400;

    [SerializeField]
    private Transform explosion;

    [SerializeField]
    private GameManager gameManager;

    [SerializeField]
    Transform[] powerUps;

    void Start()
    {
        colorScript = FindObjectOfType<ColorScript>();
        ballRenderer = GetComponent<Renderer>();
    }

    void Update()
    {
        if (!gameManager.ballInPlay)
        {
            gameManager.ResetBallPosiiton();
        }

        if (Input.GetButtonDown("Jump") && !gameManager.ballInPlay)
        {
            gameManager.ballInPlay = true;
            gameManager.ball.AddForce(Vector2.up * speed);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("bottomCollider"))
        {
            gameManager.ball.velocity = Vector2.zero;
            gameManager.ballInPlay = false;
            gameManager.ResetBallPosiiton();
            gameManager.UpdateLives(-1);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Fix horizontal-only bounce on wall collisions
        if (collision.transform.CompareTag("wall"))
        {
            Vector2 velocity = gameManager.ball.velocity;
            float speed = velocity.magnitude;

            // If vertical speed is too low, nudge it
            if (Mathf.Abs(velocity.y) < 0.2f)
            {
                velocity.y = velocity.y < 0 ? -0.5f : 0.5f;
                velocity = velocity.normalized * speed;
                gameManager.ball.velocity = velocity;
            }
        }

        // Handle brick collision
        if (collision.transform.CompareTag("brick"))
        {
            Brick brick = collision.gameObject.GetComponent<Brick>();

            if (brick.hitPoints > 1)
            {
                brick.BreakBrick();
                return;
            }

            int randomChance = Random.Range(1, 101);

            if (randomChance <= 30)
            {
                Instantiate(
                    powerUps[Random.Range(0, powerUps.Length)],
                    collision.transform.position,
                    collision.transform.rotation
                );
            }

            Destroy(collision.gameObject);
            // The explosion is of type Transform, if need to separate to variable in the future
            Destroy(
                Instantiate(
                    explosion,
                    collision.transform.position,
                    collision.transform.rotation
                ).gameObject,
                1.0f
            );
            gameManager.UpdateScore(brick.points);
            gameManager.BrickDestroyed();
            ballRenderer.material.color = colorScript.RandomColor(bright: true);
        }
    }
}
