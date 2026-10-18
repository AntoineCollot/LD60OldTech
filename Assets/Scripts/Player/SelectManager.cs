using UnityEngine;
using UnityEngine.InputSystem;

public class SelectManager : MonoBehaviour
{
    [SerializeField] LayerMask layers;

    ISelectable hoveredSelectable;

    void Start()
    {

    }

    void Update()
    {
        Vector2 hitUV = Vector2.zero;
        Ray camRay = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
        if (Physics.Raycast(camRay, out RaycastHit hit, 100, layers))
        {
            hitUV = hit.textureCoord;

            if (hit.collider.TryGetComponent(out ISelectable selectable))
            {
                if (hoveredSelectable != selectable)
                {
                    hoveredSelectable?.OnHoverExit();
                    hoveredSelectable = selectable;
                    hoveredSelectable.OnHoverEnter();
                }
                else
                {
                    hoveredSelectable?.OnHoverStay(hitUV);
                }
            }
            else
            {
                hoveredSelectable?.OnHoverExit();
                hoveredSelectable = null;
            }
        }
        else
        {
            hoveredSelectable?.OnHoverExit();
            hoveredSelectable = null;
        }

        //Left click
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            if (hoveredSelectable != null)
                hoveredSelectable.OnSelect(hitUV);
            else
                CardManager.Instance.UnselectCardIfInViewMode();
        }

        //Right click
        if (Mouse.current.rightButton.wasPressedThisFrame)
        {
            if (hoveredSelectable != null)
                hoveredSelectable.OnRightClick();
        }
    }
}

public interface ISelectable
{
    public void OnHoverEnter();
    public void OnHoverExit();
    public void OnHoverStay(Vector2 uv);
    public void OnSelect(Vector2 uv);
    public void OnRightClick();
}