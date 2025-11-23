using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject cardsNumberSlider;
    [SerializeField] private Button restartButton;
    [SerializeField] private Button startButton;

    private void Awake()
    {
        startButton.onClick.AddListener(() => StartGame());
        restartButton.onClick.AddListener(() => SceneManager.LoadScene(SceneManager.GetActiveScene().name));
    }

    private void Start()
    {
        ScoreManager.Instance.onGameover += HandleGameOver;
    }

    private void OnDestroy()
    {
        ScoreManager.Instance.onGameover -= HandleGameOver;
    }

    private void StartGame()
    {
        startButton.gameObject.SetActive(false);
        cardsNumberSlider.SetActive(false);
        CardsManager.Instance.SpawnGameCards();
    }
    private void HandleGameOver()
    {
        restartButton.gameObject.SetActive(true);
    }
}
