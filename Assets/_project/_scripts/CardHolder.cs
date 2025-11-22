using System;
using DG.Tweening;
using UnityEngine;

public class CardHolder : MonoBehaviour
{
    #region Variables Decleration
    public Sprite cardImage;
    private bool _isFaceUp;
    private float _flipSpeed = 0.3f;
    private Sequence _flipSequence;
    private Sprite _cardBackground;
    private SpriteRenderer _spriteRenderer;

    [Header("Hover Settings")]
    public float hoverScale = 1.2f;
    public float tweenDuration = 0.15f;
    private Vector3 _originalScale;
    private Tween _currentTween;
    private bool _isEnlarged;

    [Header("Card Data")]
    private CardData _cardData;
    public CardData CardData
    {
        get { return _cardData; }
        private set { _cardData = value; }
    }
    #endregion

    #region Iinitialization
    private void Awake()
    {
        _spriteRenderer = GetComponentInChildren<SpriteRenderer>();

        _cardBackground = _spriteRenderer.sprite;
        _originalScale = transform.localScale;
    }
    public void InitializeData(int id)
    {
        _cardData = new CardData(id);
        Debug.Log($"card id = {_cardData.Id}");
    }
    #endregion

    #region Unity Events
    private void OnMouseDown()
    {
        if (_isFaceUp) return;

        FlipCardWithSwap(true, () => SwapSprite());
        StartCoroutine(CardsManager.Instance.AddCardToComparison(this));
    }
    private void OnMouseEnter()
    {
        if (_isFaceUp) return;

        ToggleCardAnimation();
    }

    private void OnMouseExit()
    {
        if (_isFaceUp) return;

        ToggleCardAnimation();
    }
    #endregion

    #region Main Functionality
    public void FlipCardWithSwap(bool faceUp, Action onHalfWay = null)
    {
        if (_flipSequence != null && _flipSequence.IsActive() && _flipSequence.IsPlaying())
            return;

        float targetY = faceUp ? 180 : 0;
        _flipSequence = DOTween.Sequence();

        _flipSequence.Append(transform.DORotate(new Vector3(0, 90, 0), _flipSpeed / 2))
           .AppendCallback(() => onHalfWay?.Invoke())
           .Append(transform.DORotate(new Vector3(0, targetY, 0), _flipSpeed / 2));

        _isFaceUp = !_isFaceUp;
    }

    public void SwapSprite()
    {
        _spriteRenderer.sprite = _isFaceUp ? cardImage : _cardBackground;
    }

    public void ToggleCardAnimation()
    {
        _currentTween?.Kill();
        Vector3 endValue = _isEnlarged ? _originalScale : _originalScale * hoverScale;

        _currentTween = transform.DOScale(endValue, tweenDuration)
                                .SetEase(Ease.OutQuad);

        _isEnlarged = !_isEnlarged;
    }
    #endregion
}
