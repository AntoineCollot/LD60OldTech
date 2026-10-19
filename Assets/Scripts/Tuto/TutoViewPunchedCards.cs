using TMPro;
using UnityEngine;

public class TutoViewPunchedCards : MonoBehaviour
{
    TextMeshProUGUI text;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        text.enabled = !MainMenu.Instance.IsActive;

        if (CardManager.Instance.IsInViewMode)
            gameObject.SetActive(false);
    }
}
