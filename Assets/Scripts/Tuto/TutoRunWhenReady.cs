using TMPro;
using UnityEngine;

public class TutoRunWhenReady : MonoBehaviour
{
    TextMeshProUGUI text;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        text =GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        text.enabled = CardManager.Instance.cardOrders != null && CardManager.Instance.cardOrders.Count > 0;

        if (CardProcessor.Instance.isRunning && text.enabled)
            gameObject.SetActive(false);
    }
}
