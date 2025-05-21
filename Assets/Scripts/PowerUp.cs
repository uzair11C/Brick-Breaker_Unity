using UnityEngine;

public class PowerUp : MonoBehaviour
{
    [SerializeField]
    private float speed;

    void Update()
    {
        transform.Translate(speed * Time.deltaTime * new Vector2(0f, -1f));

        if (transform.position.y < -6f)
        {
            Destroy(gameObject);
        }
    }
}
