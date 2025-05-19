using UnityEngine;

public class ColorScript : MonoBehaviour
{
    [SerializeField]
    private Renderer paddleRenderer;

    [SerializeField]
    private Renderer ballRenderer;

    void Start()
    {
        paddleRenderer.material.color = RandomColor(bright: true);
        ballRenderer.material.color = RandomColor(bright: true);
    }

    public Color RandomColor(bool bright)
    {
        float hue = Random.value;
        float saturation = Random.Range(0.4f, 0.7f);

        // Avoid very low value which gives pure black
        float value = bright ? Random.Range(0.8f, 1f) : Random.Range(0.15f, 0.3f);

        return Color.HSVToRGB(hue, saturation, value);
    }
}
