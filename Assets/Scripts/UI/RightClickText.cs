using TMPro;
using UnityEngine;

public class RightClickText : MonoBehaviour
{
    TutoGlowText glowText;
    Color baseColor;
    TextMeshProUGUI text;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        glowText = GetComponent<TutoGlowText>();
        text = GetComponent<TextMeshProUGUI>();
        baseColor = text.color;
    }

    // Update is called once per frame
    void Update()
    {
        if(CardManager.Instance.cardOrders == null || CardManager.Instance.cardOrders.Count == 0)
        {
            glowText.Glow();
        }
        else
        {
            glowText.Stop();
            text.color = baseColor;
        }
    }
}
