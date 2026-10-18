using UnityEngine;
using UnityEngine.UI;

public class SelectedCardBlack : MonoBehaviour
{
    const float SMOOTH = 0.3f;
    float currentAlpha;
    float targetAlpha;
    float refAlpha;

    RawImage image;
    Color c;
    float maxAlpha;

    void Start()
    {
        image = GetComponent<RawImage>();
        c = image.color;
        maxAlpha = c.a;
    }

    void Update()
    {
        if (CardManager.Instance.HasSelectedCard)
            targetAlpha = 1;
        else
            targetAlpha = 0;

        if (!Mathf.Approximately(currentAlpha, targetAlpha))
        {
            currentAlpha = Mathf.SmoothDamp(currentAlpha, targetAlpha, ref refAlpha, SMOOTH);
            image.enabled = currentAlpha > 0.01f;
            c.a= Mathf.Lerp(0, maxAlpha, currentAlpha);
            image.color = c;
        }
    }
}
