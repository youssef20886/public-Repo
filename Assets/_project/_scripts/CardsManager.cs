using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;

public class CardsManager : SingletonMonobehaviour<CardsManager>
{
    private const int _cardsNumbers = 8;

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

        var bounds = cards[0].GetComponentInChildren<SpriteRenderer>().bounds;
        float baseW = bounds.size.x;
        float baseH = bounds.size.y;

        float scale = CardUtils.ComputeCardScale(baseW * 2, baseH * 3, count);

        // Apply scale to all cards
        foreach (var card in cards)
            card.transform.localScale = new Vector3(scale * 2f, scale * 3f, 1f);

        // Generate grid positions with the new scale
        var positions = CardUtils.GenerateGridPositions(count, (baseW * 2) * scale, (baseH * 3) * scale);

        Vector3 verticalOffset = new Vector3(0f, -0.3f, 0f);
        for (int i = 0; i < positions.Count; i++)
        {
            positions[i] += verticalOffset;
        }

        // Animate
        for (int i = 0; i < cards.Count; i++)
        {
            int index = i;
            var seq = DOTween.Sequence();

            seq.AppendCallback(() =>
            {
                cards[index].FlipCardWithSwap(true, () => cards[index].SwapSprite());
            });

            seq.Append(
                cards[index].transform.DOMove(positions[index], 0.6f)
                    .SetEase(Ease.OutBack)
            );

            seq.SetDelay(index * 0.5f);

            if (index == cards.Count - 1)
            {
                seq.OnComplete(() => StartCoroutine(HideCards()));
            }
        }
    }


    private IEnumerator HideCards()
    {
        yield return new WaitForSeconds(3f);
        foreach (var card in _cardsToSpawn)
        {
            card.SetOriginalScale();
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
