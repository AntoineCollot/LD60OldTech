using System;
using UnityEngine;

public class MachineButton : MonoBehaviour, ISelectable
{
    [SerializeField] float handleAngle = 42;
    [SerializeField] Transform handle;
    public bool isOn;
    Renderer rend;

    public event Action<bool> onValueChanged;

    static readonly int HOVER_PROPERTY = Shader.PropertyToID("_HoverAmount");

    void Start()
    {
        rend = GetComponent<Renderer>();
        SetOnWithoutNotifying(false);
    }

    public void ToggleOn()
    {
        SetOn(!isOn);
    }

    public void SetOn(bool newIsOn)
    {
        if (isOn == newIsOn) return;

        SetOnWithoutNotifying(newIsOn);
        onValueChanged?.Invoke(isOn);

        if(isOn)
            SFXManager.PlaySound(GlobalSFX.MachineButtonClick);
        else
            SFXManager.PlaySound(GlobalSFX.MachineButtonUnClick);
    }

    public void SetOnWithoutNotifying(bool newIsOn)
    {
        isOn = newIsOn;
        if (isOn)
            handle.localRotation = Quaternion.Euler(-handleAngle, 0, 0);
        else
            handle.localRotation = Quaternion.Euler(handleAngle, 0, 0);
    }

    public void OnHoverEnter()
    {
        rend.material.SetFloat(HOVER_PROPERTY, 1);
    }

    public void OnHoverExit()
    {
        rend.material.SetFloat(HOVER_PROPERTY, 0);
    }

    public void OnHoverStay(Vector2 uv)
    {
    }

    public void OnRightClick()
    {
    }

    public void OnSelect(Vector2 uv)
    {
        ToggleOn();
    }
}
