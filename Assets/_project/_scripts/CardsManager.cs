using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;

public class CardsManager : SingletonMonobehaviour<CardsManager>
{
    private const int _cardsNumbers = 12;

    [SerializeField] private CardHolder _cardPrefab;
    [SerializeField] private CardSpritesSO _cardSprites;

    private List<CardHolder> _cardsToCompare = new List<CardHolder>();
    private List<CardHolder> _cardsToSpawn = new List<CardHolder>();

    private Vector3 _initialCardsPosition = new Vector3(-10, 0, 0);

    #region Unity Events
    private void Start()
    {
        SpawnGameCards();
    }
    #endregion

    #region Main Functionality
    private void SpawnGameCards()
    {
        for (int i = 0; i < _cardsNumbers / 2; i++)
        {
            CardHolder cardA = Instantiate(_cardPrefab, _initialCardsPosition, Quaternion.identity);
            cardA.InitializeData(i, _cardSprites.cardsSprites[i]);
            _cardsToSpawn.Add(cardA);

            CardHolder cardB = Instantiate(_cardPrefab, _initialCardsPosition, Quaternion.identity);
            cardB.InitializeData(i, _cardSprites.cardsSprites[i]);
            _cardsToSpawn.Add(cardB);
        }

        CardUtils.Shuffle(_cardsToSpawn);
        PositionCards(_cardsToSpawn);
    }

    private void PositionCards(List<CardHolder> cards)
    {
        int count = cards.Count;
        int cols = Mathf.CeilToInt(Mathf.Sqrt(count));
        int rows = Mathf.CeilToInt((float)count / cols);

        List<Vector3> targetPositions = CardUtils.GenerateGridPositions(cards.Count, 2.5f, 3.5f);

        CardUtils.Shuffle(targetPositions);

        DOVirtual.DelayedCall(1f, () =>
        {
            for (int i = 0; i < cards.Count; i++)
            {
                var tween = cards[i].transform.DOMove(targetPositions[i], 0.6f)
                    .SetEase(Ease.OutBack)
                    .SetDelay(i * 0.5f);

                if (i == cards.Count - 1)
                {
                    tween.OnComplete(() => StartCoroutine(HideCards()));
                }
            }
        });
    }

    private IEnumerator HideCards()
    {
        yield return new WaitForSeconds(3f);
        foreach (var card in _cardsToSpawn)
        {
            card.FlipCardWithSwap(false, () => card.SwapSprite());
        }
    }


    public void AddCardToComparison(CardHolder card)
    {
        _cardsToCompare.Add(card);

        if (_cardsToCompare.Count < 2) return;

        var firstTwo = _cardsToCompare.GetRange(0, 2);
        StartCoroutine(CompareCards(firstTwo));
        _cardsToCompare.Clear();
    }

    private IEnumerator CompareCards(List<CardHolder> cardsToCompare)
    {
        if (!CardUtils.IsSameCardId(cardsToCompare[0].CardData, cardsToCompare[1].CardData))
        {
            yield return new WaitForSeconds(1f);

            foreach (var cardHolder in cardsToCompare)
            {
                cardHolder.FlipCardWithSwap(false, () => cardHolder.SwapSprite());
            }
        }

        yield return new WaitForSeconds(0.5f);
        foreach (var cardHolder in cardsToCompare)
        {
            cardHolder.ToggleCardAnimation(false);
        }

    }
    #endregion


}
