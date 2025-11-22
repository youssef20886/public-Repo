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

    #region Main Functionality
    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Space))
        {
            FlipCardWithSwap(() => SwapSprite());
        }
    }
    public void FlipCardWithSwap(Action onHalfWay)
    {
        if (flipSequence != null && flipSequence.IsActive() && flipSequence.IsPlaying())
            return;

        float targetY = isFaceUp ? 180 : 0;
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

    private void OnMouseEnter()
    {
        currentTween?.Kill();

        currentTween = transform.DOScale(originalScale * hoverScale, tweenDuration)
                                .SetEase(Ease.OutQuad);
    }

    private void OnMouseExit()
    {
        currentTween?.Kill();

        currentTween = transform.DOScale(originalScale, tweenDuration)
                                .SetEase(Ease.OutQuad);
    }

    #endregion
}
