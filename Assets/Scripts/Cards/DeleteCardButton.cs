using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DeleteCardButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    Button button;
    bool isHovered;

    void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(OnClick);
    }

    private void OnClick()
    {
        CardManager.Instance.DeleteSelectedCard();
    }

    void Update()
    {
        button.targetGraphic.enabled = CardManager.Instance.HasSelectedCard || isHovered;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        isHovered = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isHovered = false;
    }
}
