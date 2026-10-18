using UnityEngine;

public class ComputerScreen : MonoBehaviour
{
    float currentDisplay;
    float targetDisplay;
    float refDisplay;
    const float SMOOTH = 0.1f;

    [SerializeField] bool debugStartOn;

    Material mat;

    static readonly int DISPLAY_PROPERTY = Shader.PropertyToID("_Display");

    void Start()
    {
        mat = GetComponent<Renderer>().material;
#if UNITY_EDITOR
        if(!debugStartOn)
#endif
        mat.SetFloat(DISPLAY_PROPERTY, currentDisplay);
    }

    void Update()
    {
        if (CardProcessor.Instance.isRunning)
            targetDisplay = 1;
        else
            targetDisplay = 0;

        if (!Mathf.Approximately(currentDisplay, targetDisplay))
        {
            currentDisplay = Mathf.SmoothDamp(currentDisplay, targetDisplay, ref refDisplay, SMOOTH);
            mat.SetFloat(DISPLAY_PROPERTY, currentDisplay);
        }
    }
}
