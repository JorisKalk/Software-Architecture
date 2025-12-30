using UnityEngine;
using TMPro;

public class NumberPresenter : MonoBehaviour
{
    [SerializeField]
    private IntValue intValue;
    [SerializeField]
    private TextMeshProUGUI numberText;

    private void UpdateNumberText()
    {
        numberText.text = "Gold: " + intValue.value.ToString();
    }

    private void Start()
    {
        UpdateNumberText();
    }

    private void OnEnable()
    {
        intValue.onValueChanged += UpdateNumberText;
    }

    private void OnDisable()
    {
        intValue.onValueChanged -= UpdateNumberText;
    }
}
