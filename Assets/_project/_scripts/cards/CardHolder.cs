using System;
using DG.Tweening;
using UnityEngine;

public class CardHolder : MonoBehaviour
{
    #region Variables Decleration
    private Sprite _cardImage;
    private bool _isFaceUp;
    private float _flipSpeed = 0.3f;
    private Sequence _flipSequence;
    private Sprite _cardBackground;
    private SpriteRenderer _spriteRenderer;
    private BoxCollider2D _boxCollider;

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
    public void InitializeData(int id, Sprite image)
    {
        _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        _boxCollider = GetComponent<BoxCollider2D>();
        _cardBackground = _spriteRenderer.sprite;

        _cardData = new CardData(id);
        _cardImage = image;
    }

    public void SetOriginalScale()
    {
        _originalScale = transform.localScale;

        Vector2 spriteSize = _spriteRenderer.sprite.bounds.size;

        Vector3 childScale = _spriteRenderer.transform.localScale;

        _boxCollider.size = new Vector2(
            spriteSize.x * childScale.x,
            spriteSize.y * childScale.y
        );
    }

    #endregion

    #region Unity Events
    private void OnMouseDown()
    {
        if (_isFaceUp) return;

        FlipCardWithSwap(true, () => SwapSprite());

        CardsManager.Instance.AddCardToComparison(this);
    }
    private void OnMouseEnter()
    {
        if (_isFaceUp) return;

        ToggleCardAnimation(true);
    }

    private void OnMouseExit()
    {
        if (_isFaceUp) return;

        ToggleCardAnimation(false);
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

    public void SwapSprite(bool skipAudio = false)
    {
        if (!skipAudio)
            AudioManager.Instance.PlayAudio(GlobalVariables.AutioType.CardClick);

        _spriteRenderer.sprite = _isFaceUp ? _cardImage : _cardBackground;
    }

    public void ToggleCardAnimation(bool enalarge)
    {
        _currentTween?.Kill();
        Vector3 endValue = enalarge ? _originalScale * hoverScale : _originalScale;

        _currentTween = transform.DOScale(endValue, tweenDuration)
                                .SetEase(Ease.OutQuad);

        _isEnlarged = !_isEnlarged;
    }
    #endregion
}
