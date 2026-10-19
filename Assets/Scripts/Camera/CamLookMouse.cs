using UnityEngine;
using UnityEngine.InputSystem;

public class CamLookMouse : MonoBehaviour
{
    [SerializeField] float maxAngle = 10;
    [SerializeField, Range(0,0.5f)] float centerRange = 0.1f;
    const float SMOOTH = 0.7f;
    float currentAngle;
    float targetAngle;
    float refAngle;

    Quaternion cameraRot;

    void Start()
    {
        cameraRot = transform.localRotation;
    }

    void Update()
    {
        float mouseX = Mouse.current.position.ReadValue().x;
        mouseX = mouseX / Screen.width;
        mouseX = mouseX - 0.5f;
        targetAngle = Mathf.InverseLerp(centerRange, 0.5f, Mathf.Abs(mouseX));
        targetAngle = Mathf.Lerp(0, maxAngle, targetAngle) * Mathf.Sign(mouseX);

        if (!Mathf.Approximately(currentAngle, targetAngle))
        {
            currentAngle = Mathf.SmoothDamp(currentAngle, targetAngle, ref refAngle, SMOOTH);
            transform.localRotation = cameraRot * Quaternion.Euler(0, currentAngle, 0);
        }
    }
}
