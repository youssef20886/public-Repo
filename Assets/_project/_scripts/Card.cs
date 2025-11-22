using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card
{
    #region variables
    private CardState cardState = CardState.FaceUp;
    public enum CardState
    {
        FaceUp,
        FaceDown,
    }
    #endregion

    #region Main Functionality
    public void ToggleCard()
    {

    }
    #endregion
}
