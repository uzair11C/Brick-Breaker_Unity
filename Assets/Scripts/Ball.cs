using UnityEngine;

public class Ball : MonoBehaviour
{
    private Rigidbody2D rb;
    private Renderer ballRenderer;
    private bool inPlay;

    [SerializeField]
    private ColorScript colorScript;

    [SerializeField]
    private Transform paddleTransform;

    [SerializeField]
    private float speed = 400;

    [SerializeField]
    private Transform explosion;

    [SerializeField]
    private GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        colorScript = FindObjectOfType<ColorScript>();
        ballRenderer = GetComponent<Renderer>();
        // gameManager = FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!inPlay)
        {
            transform.position = paddleTransform.position;
        }

        if (Input.GetButtonDown("Jump") && !inPlay)
        {
            inPlay = true;
            rb.AddForce(Vector2.up * speed);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("bottomCollider"))
        {
            Debug.Log("Game Over");
            rb.velocity = Vector2.zero;
            inPlay = false;
            gameManager.UpdateLives(-1);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("brick"))
        {
            // The explosion is of type Transform, if need to separate to variable in the future
            Destroy(
                Instantiate(
                    explosion,
                    collision.transform.position,
                    collision.transform.rotation
                ).gameObject,
                1.0f
            );
            gameManager.UpdateScore(collision.gameObject.GetComponent<Brick>().points);
            Destroy(collision.gameObject);
            ballRenderer.material.color = colorScript.RandomColor(bright: true);
        }
    }
}
