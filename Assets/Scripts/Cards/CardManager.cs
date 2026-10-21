using System;
using System.Collections.Generic;
using UnityEngine;

public class CardManager : MonoBehaviour
{
    CardData[] cardData;
    Card[] cardObjects;
    [SerializeField] Card cardPrefab;

    [Header("Positions")]
    [SerializeField] Transform idleLayout;
    [SerializeField] Transform hidePosition;
    [SerializeField] Transform selectedPosition;
    [SerializeField] Transform processedCardPosition;
    [SerializeField] Transform processingCardPosition;
    CardIdlePosition[] idleCardPosition;
    const int CARD_COUNT = 12;

    //View mode
    public enum ViewMode { Hide, Show }
    public ViewMode currentMode { get; private set; }
    public bool IsInViewMode => currentMode == ViewMode.Show;

    [Header("Selected")]
    int selectedCardID;
    int lastSelectedCardID;//use for calling the delete card as the card is being deselected
    public bool HasSelectedCard => selectedCardID >= 0;
    public bool SelectedCardIsEmpty => cardData[selectedCardID].isEmpty;

    //Hover Hole
    Card hoveredHoleCard;
    int hoveredDigit;
    int hoveredColumn;

    //Card Order
    public List<int> cardOrders = new();
    public static event Action onCardOrderUpdated;

    public static CardManager Instance;

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        cardObjects = new Card[CARD_COUNT];
        cardData = new CardData[CARD_COUNT];
        idleCardPosition = new CardIdlePosition[CARD_COUNT];

        for (int i = 0; i < CARD_COUNT; i++)
        {
            cardObjects[i] = Instantiate(cardPrefab, transform);
            cardData[i] = new CardData(i);
            cardObjects[i].SetData(cardData[i], i);
            idleCardPosition[i] = idleLayout.GetChild(i).GetComponent<CardIdlePosition>();
        }

        SetMode(ViewMode.Hide, true);
    }

    public void SetMode(ViewMode mode, bool force = false)
    {
        if (currentMode == mode && !force)
            return;

        if (CardProcessor.Instance.isRunning && mode == ViewMode.Show)
            return; // Don't allow show mode during runtime

        currentMode = mode;
        switch (mode)
        {
            case ViewMode.Hide:
                SetAllToHideMode();
                break;
            case ViewMode.Show:
                SetAllToViewMode();
                break;
        }
    }

    void SetAllToViewMode()
    {
        for (int i = 0; i < CARD_COUNT; i++)
        {
            cardObjects[i].SetPosition(idleCardPosition[i].PositionRef, false, Vector3.up * 180);
        }
        ClearCurrentHover();
        selectedCardID = -1;
    }

    void SetAllToHideMode()
    {
        for (int i = 0; i < CARD_COUNT; i++)
        {
            cardObjects[i].SetPosition(hidePosition, false);
        }
        ClearCurrentHover();
        selectedCardID = -1;
    }

    public void SetSelected(Card card)
    {
        SetAllToViewMode();

        selectedCardID = card.id;
        lastSelectedCardID = selectedCardID;
        card.SetPosition(selectedPosition, true);
        ClearCurrentHover();

        SFXManager.PlaySound(GlobalSFX.CardSelect);
    }

    public void ToggleMode()
    {
        switch (currentMode)
        {
            case ViewMode.Hide:
                SetMode(ViewMode.Show);
                break;
            case ViewMode.Show:
            default:
                SetMode(ViewMode.Hide);
                break;
        }
    }

    public void PunchCard(Card card, int digit, int column)
    {
        cardData[card.id].TogglePunch(digit, column);
    }

    public void HoverHole(Card card, int digit, int column)
    {
        if (hoveredHoleCard == card && hoveredDigit == digit && hoveredColumn == column)
            return; //No changes

        ClearCurrentHover();

        hoveredColumn = column;
        hoveredDigit = digit;
        hoveredHoleCard = card;
        cardData[hoveredHoleCard.id].HoverHole(digit, column);
    }

    public void ClearCurrentHover()
    {
        if (hoveredHoleCard == null)
            return;
        cardData[hoveredHoleCard.id].ClearHover(hoveredDigit, hoveredColumn);
        hoveredHoleCard = null;
    }

    public void UnselectCardIfInViewMode()
    {
        if (currentMode == ViewMode.Show)
            SetAllToViewMode();
    }

    public void ToggleCardOrder(Card card)
    {
        if (!IsInViewMode)
            return;

        if (cardOrders.Contains(card.id))
            cardOrders.Remove(card.id);
        else
            cardOrders.Add(card.id);

        UpdateCardOrders();
        onCardOrderUpdated?.Invoke();
    }

    void UpdateCardOrders()
    {
        //Clear
        for (int i = 0; i < CARD_COUNT; i++)
        {
            idleCardPosition[i].SetOrder(-1);
        }

        //Assign
        for (int i = 0; i < cardOrders.Count; i++)
        {
            idleCardPosition[cardOrders[i]].SetOrder(i);
        }
    }

    public void DeleteSelectedCard()
    {
        if (!IsInViewMode)
            return;
        cardData[lastSelectedCardID].Clear();
        cardObjects[lastSelectedCardID].UpdateEmptyState();
        SetAllToViewMode();

        SFXManager.PlaySound(GlobalSFX.CardDelete);
    }

    public List<CardData> GetCardsToPlay()
    {
        List<CardData> dataToPlay = new();

        foreach (int order in cardOrders)
        {
            dataToPlay.Add(cardData[order]);
        }

        return dataToPlay;
    }

    public void SetProcessingCardPosition()
    {
        for (int i = 0; i < cardObjects.Length; i++)
        {
            cardObjects[i].SetPosition(processingCardPosition, false);
        }
    }

    public void SetProcessedCardPosition(CardData data)
    {
        int id = Array.IndexOf(cardData, data);
        cardObjects[id].SetPosition(processedCardPosition, false);
    }
}
