using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class CardHolder : MonoBehaviour
{
    #region Variables Decleration
    private bool isFaceUp;
    private float flipSpeed = 0.3f;
    private Sequence flipSequence;

    #endregion

    #region Iinitialization
    public void Initialize()
    {
    }
    #endregion

    #region Main Functionality
    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Space))
        {
            FlipCardWithSwap(()=> Debug.Log("swap sprite"));
        }
    }
    public void FlipCardWithSwap(System.Action onHalfWay)
    {
        if (flipSequence != null && flipSequence.IsActive() && flipSequence.IsPlaying())
            return;
            
        float targetY = isFaceUp ? 0 : 180;
        flipSequence = DOTween.Sequence();

        flipSequence.Append(transform.DORotate(new Vector3(0, 90, 0), flipSpeed / 2))
           .AppendCallback(() => onHalfWay?.Invoke())
           .Append(transform.DORotate(new Vector3(0, targetY, 0), flipSpeed / 2));

        isFaceUp = !isFaceUp;
    }

    #endregion
}
