using TMPro;
using UnityEngine;

public class CardIdlePosition : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI orderText;
    int order;

    public Transform PositionRef => transform;

    void Start()
    {
        CardManager.onCardOrderUpdated += OnCardOrderUpdated;
        order = -1;
    }

    private void OnDestroy()
    {
        CardManager.onCardOrderUpdated -= OnCardOrderUpdated;
    }

    void Update()
    {
        bool shouldDisplayOrder = CardManager.Instance.IsInViewMode && order >= 0;
        if (shouldDisplayOrder != orderText.enabled)
            orderText.enabled = shouldDisplayOrder;
    }

    private void OnCardOrderUpdated()
    {

    }

    public void SetOrder(int order)
    {
        orderText.text = order.ToString();
        this.order = order;
    }
}
