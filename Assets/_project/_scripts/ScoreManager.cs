using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : SingletonMonobehaviour<ScoreManager>
{
    public Action<int> onScoreUpdate;
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
}
