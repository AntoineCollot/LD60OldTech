using TMPro;
using UnityEngine;

public class TutoStop : MonoBehaviour
{
    TextMeshProUGUI text;

    void Start()
    {
        text =GetComponent<TextMeshProUGUI>();
        text.enabled = false;
    }

    void Update()
    {
        if (!CardProcessor.Instance.isRunning && text.enabled)
            gameObject.SetActive(false);

        text.enabled = CardProcessor.Instance.isFinishedButRunning;
    }
}
