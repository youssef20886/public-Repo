using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CardNumberSelector : MonoBehaviour
{
    [SerializeField] private Slider cardSlider;
    [SerializeField] private TMP_Text valueText;

    public int SelectedCardCount { get; private set; }

    private void Start()
    {
        cardSlider.onValueChanged.AddListener(OnSliderChanged);

        OnSliderChanged(cardSlider.value);
    }

    private void OnSliderChanged(float value)
    {
        SelectedCardCount = Mathf.RoundToInt(value);
        valueText.text = $"number of cards: {SelectedCardCount}";
        CardsManager.Instance.SetCardsNumbers(SelectedCardCount);
    }
}
