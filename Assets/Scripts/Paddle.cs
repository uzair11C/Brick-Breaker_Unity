using UnityEngine;

public class Paddle : MonoBehaviour
{
    [SerializeField]
    private float screenEdge = 6.1f;

    [SerializeField]
    private GameManager gameManager;

    void Update()
    {
        transform.Translate(
            Input.GetAxis("Horizontal")
                * gameManager.GetPaddleSpeed()
                * Time.deltaTime
                * Vector2.right
        );

        if (transform.position.x < -screenEdge)
        {
            transform.position = new Vector2(-screenEdge, transform.position.y);
        }
        else if (transform.position.x > screenEdge)
        {
            transform.position = new Vector2(screenEdge, transform.position.y);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("extraLife"))
        {
            Destroy(collision.gameObject);
            gameManager.UpdateLives(1);
        }
        else if (collision.CompareTag("speedBoost"))
        {
            Destroy(collision.gameObject);
            gameManager.BoostPaddleSpeed();
        }
    }
}
