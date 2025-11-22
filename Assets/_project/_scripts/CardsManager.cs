using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CardsManager : SingletonMonobehaviour<CardsManager>
{
    private const int _cardsNumbers = 4;

    [SerializeField] private CardHolder _cardPrefab;
    private List<CardHolder> cardsToCompare = new List<CardHolder>();

    #region Unity Events
    private void Start()
    {
        SpawnGameCards();
    }
    #endregion

    #region Main Functionality
    private void SpawnGameCards()
    {
        float xSpace = 2.5f;
        for (int i = 0; i < _cardsNumbers; i++)
        {
            Vector3 spawnPos = new Vector3(i * xSpace, 0, 0);
            CardHolder newCard = Instantiate(_cardPrefab, spawnPos, Quaternion.identity);
            newCard.InitializeData(i);
        }
    }

    public IEnumerator AddCardToComparison(CardHolder card)
    {
        cardsToCompare.Add(card);

        if (cardsToCompare.Count != 2) yield break;

        if (!IsSameCardId(cardsToCompare[0].CardData, cardsToCompare[1].CardData))
        {
            yield return new WaitForSeconds(2f);

            foreach (var cardHolder in cardsToCompare)
            {
                cardHolder.FlipCardWithSwap(false, () => cardHolder.SwapSprite());
            }
        }

        foreach (var cardHolder in cardsToCompare)
        {
            cardHolder.ToggleCardAnimation();
        }

        cardsToCompare.Clear();
    }
    #endregion

    #region Helper Functions
    private bool IsSameCardId(CardData card1, CardData card2)
    {
        return card1.Id == card2.Id;
    }
    #endregion
}
