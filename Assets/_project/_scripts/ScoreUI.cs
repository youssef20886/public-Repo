using TMPro;
using UnityEngine;

public class ScoreUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI currentScore;
    [SerializeField] TextMeshProUGUI bestScore;

    private void Start()
    {
        if (PlayerPrefs.GetInt(GlobalVariables.MAX_SCORE) > 0)
        {
            bestScore.text = $"best score: {PlayerPrefs.GetInt(GlobalVariables.MAX_SCORE)}";
        }
        else
        {
            bestScore.text = $"best score: 0";
        }
        currentScore.text = $"current score: 0";
    }

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
