using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CardUtils
{
    #region Helper Functions
    public static (int cols, int rows) GetBalancedGrid(int count)
    {
        int bestCols = 1;
        int bestRows = count;
        float bestScore = float.MaxValue;

        for (int cols = 1; cols <= count; cols++)
        {
            int rows = Mathf.CeilToInt(count / (float)cols);
            float ratio = Mathf.Abs((float)cols / rows - 1f);

            if (ratio < bestScore)
            {
                bestScore = ratio;
                bestCols = cols;
                bestRows = rows;
            }
        }

        if (bestCols > bestRows)
            (bestCols, bestRows) = (bestRows, bestCols);

        return (bestCols, bestRows);
    }



    public static List<Vector3> GenerateGridPositions(int count, float cardW, float cardH)
    {
        (int cols, int rows) = GetBalancedGrid(count);

        float spacingX = cardW * 1.3f;
        float spacingY = cardH * 1.25f;

        float totalW = (cols - 1) * spacingX;
        float totalH = (rows - 1) * spacingY;

        float startX = -totalW / 2f;
        float startY = totalH / 2f;

        List<Vector3> pos = new();
        int i = 0;

        for (int r = 0; r < rows; r++)
        {
            for (int c = 0; c < cols; c++)
            {
                if (i >= count) break;

                pos.Add(new Vector3(startX + c * spacingX,
                                    startY - r * spacingY,
                                    0));
                i++;
            }
        }

        return pos;
    }

    public static float ComputeCardScale(float baseW, float baseH, int count)
    {
        (int cols, int rows) = GetBalancedGrid(count);

        float screenH = Camera.main.orthographicSize * 2f;
        float screenW = screenH * Camera.main.aspect;

        float margin = 0.30f;
        float usableH = screenH * (1f - margin);

        float usableW = screenW * 0.97f;

        float scaleH = (usableH / rows) / baseH;
        float scaleW = (usableW / cols) / baseW;

        return Mathf.Min(scaleH, scaleW);
    }

    public static bool IsSameCardId(CardData card1, CardData card2)
    {
        return card1.Id == card2.Id;
    }


    public static void Shuffle<T>(List<T> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            int randIndex = Random.Range(i, list.Count);
            (list[i], list[randIndex]) = (list[randIndex], list[i]);
        }
    }
    #endregion
}
