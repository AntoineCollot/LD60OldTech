using TMPro;
using UnityEngine;

public class TutoPunch : MonoBehaviour
{
    TextMeshProUGUI text;
    [SerializeField] float stayVisibleTime;
    CanvasGroup group;
    TutoGlowText glow;

    const float SMOOTH = 0.3f;
    float currentAlpha;
    float targetAlpha;
    float refAlpha;

    void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
        group = GetComponent<CanvasGroup>();
        glow = GetComponent<TutoGlowText>();
        group.alpha = 0;
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
            group.alpha = Mathf.Lerp(0, 1, currentAlpha);
        }

        if (CardManager.Instance.HasSelectedCard && CardManager.Instance.SelectedCardIsEmpty)
            glow.Glow();
        else
            glow.Stop();
    }
}
