using System.Collections;
using TMPro;
using UnityEngine;

public class TutoGlowText : MonoBehaviour
{
    [SerializeField] Color glowColor0;
    [SerializeField] Color glowColor1;
    [SerializeField] float freq = 10;
    [SerializeField] bool isOnByDefault;
    TextMeshProUGUI text;
    Color baseColor;

    bool isGlowing;

    void Awake()
    {
        text = GetComponent<TextMeshProUGUI>();
        baseColor = text.color;
    }

    private void OnEnable()
    {
        if (isOnByDefault)
            Glow();
    }

    private void OnDisable()
    {
        isGlowing = false;
    }

    public void Glow()
    {
        if (isGlowing)
            return;
        StartCoroutine(GlowC());
    }

    public void Stop()
    {
        if (!isGlowing)
            return;
        StopAllCoroutines();
        isGlowing = false;
        text.color = baseColor;
    }

    IEnumerator GlowC()
    {
        isGlowing = true;
        while (true)
        {
            text.color = Color.Lerp(glowColor0, glowColor1, Mathf.Sin(Time.time * freq) * 0.5f + 0.5f);
            yield return null;
        }
    }
}
