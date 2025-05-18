using UnityEngine;

public class Ball : MonoBehaviour
{
    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.AddForce(Vector2.up * 500);
    }

    // Update is called once per frame
    void Update() { }
}
