using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardsManager : SingletonMonobehaviour<CardsManager>
{
    [SerializeField]
    private CardHolder _cardPrefab;
    private const int _cardsNumbers = 4;

    private void Start()
    {
        SpawnGameCards();
    }

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
}
