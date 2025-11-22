using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CardUtils
{
    #region Helper Functions

    public static List<Vector3> GenerateGridPositions(int cardCount, float spacingX, float spacingY)
    {
        int cols = Mathf.CeilToInt(Mathf.Sqrt(cardCount));
        int rows = Mathf.CeilToInt((float)cardCount / cols);

        List<Vector3> positions = new List<Vector3>();

        float totalWidth = (cols - 1) * spacingX;
        float totalHeight = (rows - 1) * spacingY;

        float startX = -totalWidth / 2f;
        float startY = totalHeight / 2f;

        int index = 0;

        for (int r = 0; r < rows; r++)
        {
            for (int c = 0; c < cols; c++)
            {
                if (index >= cardCount)
                    break;

                float x = startX + (c * spacingX);
                float y = startY - (r * spacingY);

                positions.Add(new Vector3(x, y, 0));
                index++;
            }
        }

        return positions;
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
