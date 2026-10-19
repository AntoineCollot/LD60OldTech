using UnityEngine;

public class MachineRumbles : MonoBehaviour
{
    [SerializeField] float freq;
    [SerializeField] float amplitude;
    Vector3 originalPosition;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        originalPosition = transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        if (!CardProcessor.Instance.isRunning || CardProcessor.Instance.isFinishedButRunning || CardProcessor.Instance.isPaused)
            return;
        Vector3 rumble = new Vector3(Mathf.PerlinNoise(0.446f, Time.time * freq), Mathf.PerlinNoise(0.27542f, Time.time * freq), Mathf.PerlinNoise(0.148f, Time.time * freq)) * amplitude;
        transform.localPosition = originalPosition + rumble;
    }
}
