using System;
using UnityEngine;

public class ScoreManager : SingletonMonobehaviour<ScoreManager>
{
    public Action<int> onScoreUpdate;
    public event Action onGameover;

    private int _currentScore;
    private int _comboMultiplyer = 0;

    public void UpdateScore(bool isCombo)
    {
        if (isCombo)
        {
            _currentScore += +1;
            _currentScore += _comboMultiplyer;
            _comboMultiplyer++;
            onScoreUpdate?.Invoke(_currentScore);
        }
        else
        {
            // reset combo and return
            _comboMultiplyer = 0;
            return;
        }
    }

    public void GameOver()
    {
        onGameover?.Invoke();

        if (PlayerPrefs.GetInt(GlobalVariables.MAX_SCORE) < _currentScore)
        {
            PlayerPrefs.SetInt(GlobalVariables.MAX_SCORE, _currentScore);
        }
    }
}
