using UnityEngine;

public class Paddle : MonoBehaviour
{
    [SerializeField]
    private float speed = 7;

    [SerializeField]
    private float screenEdge = 6.1f;

    void Start() { }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Input.GetAxis("Horizontal") * speed * Time.deltaTime * Vector2.right);

        if (transform.position.x < -screenEdge)
        {
            transform.position = new Vector2(-screenEdge, transform.position.y);
        }
        else if (transform.position.x > screenEdge)
        {
            transform.position = new Vector2(screenEdge, transform.position.y);
        }
    }
}
