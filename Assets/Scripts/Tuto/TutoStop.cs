using TMPro;
using UnityEngine;

public class TutoStop : MonoBehaviour
{
    TextMeshProUGUI text;

    void Start()
    {
        text =GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        text.enabled = CardProcessor.Instance.isFinishedButRunning;

        if (!CardProcessor.Instance.isRunning && text.enabled)
            gameObject.SetActive(false);
    }
}
