using System.Collections.Generic;
using UnityEngine;

public class CardManager : MonoBehaviour
{
    CardData[] cardData;
    Card[] cardObjects;
    [SerializeField] Card cardPrefab;

    [SerializeField] Transform idleLayout;
    Transform[] idleCardPosition;

    const int CardCount = 12;

    void Start()
    {
        cardObjects = GetComponentsInChildren<Card>();
        cardData = new CardData[CardCount];
        idleCardPosition = new Transform[CardCount];

        for (int i = 0; i < CardCount; i++)
        {
            cardObjects[i] = Instantiate(cardPrefab, transform);
            cardObjects[i].SetData(cardData[i]);
            idleCardPosition[i] = idleLayout.GetChild(i);
        }

        Unselect();
    }

    public void Unselect()
    {
        for (int i = 0; i < CardCount; i++)
        {
            cardObjects[i].SetPosition(idleCardPosition[i]);
        }
    }
}
