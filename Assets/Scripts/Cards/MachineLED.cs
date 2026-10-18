using UnityEngine;

public class MachineLED : MonoBehaviour
{
    [SerializeField] MachineButton button;
    [SerializeField] Material mat;
    Light plight;

    static readonly int EMISSION_PROPERTY = Shader.PropertyToID("_EmissionAmount");

    private void Awake()
    {
        plight = GetComponent<Light>();
    }

    void Start()
    {

        if (button != null)
        {
            button.onValueChanged += OnButtonValueChanged;
            OnButtonValueChanged(button.isOn);
        }
    }

    private void OnDestroy()
    {
        if (button != null)
            button.onValueChanged -= OnButtonValueChanged;
    }

    private void OnButtonValueChanged(bool isOn)
    {
        SetOn(isOn);
    }

    public void SetOn(bool isOn)
    {
        if (isOn)
        {
            mat.SetFloat(EMISSION_PROPERTY, 1);
        }
        else
        {
            mat.SetFloat(EMISSION_PROPERTY, 0);
        }

        if (plight != null)
            plight.enabled = isOn;
    }
}
