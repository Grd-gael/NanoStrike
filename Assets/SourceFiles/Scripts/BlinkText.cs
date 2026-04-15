using UnityEngine;
using TMPro;

public class BlinkText : MonoBehaviour
{
    public TMPro.TextMeshProUGUI uiText;

    public Color color1 = Color.red;
    public Color color2 = Color.white;

    public float speed = 2f;

    void Update()
    {
        float t = Mathf.PingPong(Time.time * speed, 1f);
        uiText.color = Color.Lerp(color1, color2, t);
    }
}