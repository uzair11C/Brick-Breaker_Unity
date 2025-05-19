using UnityEngine;

public class Ball : MonoBehaviour
{
    private Rigidbody2D rb;
    private bool inPlay;

    [SerializeField]
    private Transform paddleTransform;

    [SerializeField]
    private float speed = 400;

    [SerializeField]
    private Transform explosion;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
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
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("brick"))
        {
            // Transform newExplosion = Instantiate(
            //     explosion,
            //     collision.transform.position,
            //     collision.transform.rotation
            // );
            Destroy(
                Instantiate(
                    explosion,
                    collision.transform.position,
                    collision.transform.rotation
                ).gameObject,
                1.0f
            );
            Destroy(collision.gameObject);
        }
    }
}
