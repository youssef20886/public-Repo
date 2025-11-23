using TMPro;
using UnityEngine;

public class ScoreUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI currentScore;
    [SerializeField] TextMeshProUGUI bestScore;


    private void OnEnable()
    {
        ScoreManager.Instance.onScoreUpdate += OnScoreUpdate;
    }

    private void OnDisable()
    {
        ScoreManager.Instance.onScoreUpdate -= OnScoreUpdate;
    }

    private void OnScoreUpdate(int newScore)
    {
        currentScore.text = $"current score: {newScore}";
    }
}
