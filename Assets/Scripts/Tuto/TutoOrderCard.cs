using TMPro;
using UnityEngine;

public class TutoOrderCard : MonoBehaviour
{
    TextMeshProUGUI text;
    [SerializeField] MachineButton runButton;
    [SerializeField] float stayVisibleTime;
    float lastRunTime;

    void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
        runButton.onValueChanged += OnValueChanged;
        lastRunTime = -10;
    }

    private void OnDestroy()
    {
        if(runButton != null)
            runButton.onValueChanged -= OnValueChanged;
    }

    private void OnValueChanged(bool isOn)
    {
        if (!isOn)
            return;

        lastRunTime = Time.time;
    }

    void Update()
    {
        text.enabled = Time.time < lastRunTime + stayVisibleTime && CardManager.Instance.cardOrders.Count == 0;
    }
}
