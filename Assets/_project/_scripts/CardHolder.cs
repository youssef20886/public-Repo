using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class CardHolder : MonoBehaviour
{
    #region Variables Decleration
    public Sprite cardImage;

    private bool isFaceUp;
    private float flipSpeed = 0.3f;
    private Sequence flipSequence;
    private Sprite cardBackground;
    private SpriteRenderer spriteRenderer;

    [Header("Hover Settings")]
    public float hoverScale = 1.2f;
    public float tweenDuration = 0.15f;
    private Vector3 originalScale;
    private Tween currentTween;
    private bool isEnlarged;
    #endregion

    #region Iinitialization
    private void Awake()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();

        cardBackground = spriteRenderer.sprite;
        originalScale = transform.localScale;
    }
    public void Initialize()
    {
    }
    #endregion

    #region Unity events
    private void OnMouseEnter()
    {
        if (isFaceUp) return;

        ToggleCardAnimation();
    }

    private void OnMouseExit()
    {
        if (isFaceUp) return;

        ToggleCardAnimation();
    }

    private void OnMouseDown()
    {
        if (isFaceUp) return;

        FlipCardWithSwap(true, () => SwapSprite());
    }
    #endregion

    #region Main Functionality
    public void FlipCardWithSwap(bool faceUp, Action onHalfWay = null)
    {
        if (flipSequence != null && flipSequence.IsActive() && flipSequence.IsPlaying())
            return;

        float targetY = faceUp ? 180 : 0;
        flipSequence = DOTween.Sequence();

        flipSequence.Append(transform.DORotate(new Vector3(0, 90, 0), flipSpeed / 2))
           .AppendCallback(() => onHalfWay?.Invoke())
           .Append(transform.DORotate(new Vector3(0, targetY, 0), flipSpeed / 2));

        isFaceUp = !isFaceUp;
    }

    private void SwapSprite()
    {
        spriteRenderer.sprite = isFaceUp ? cardImage : cardBackground;
    }

    private void ToggleCardAnimation()
    {
        currentTween?.Kill();

        if (!isEnlarged)
        {
            currentTween = transform.DOScale(originalScale * hoverScale, tweenDuration)
                                .SetEase(Ease.OutQuad);
        }
        else
        {
            currentTween = transform.DOScale(originalScale, tweenDuration)
                                .SetEase(Ease.OutQuad);
        }
        isEnlarged = !isEnlarged;
    }
    #endregion
}
