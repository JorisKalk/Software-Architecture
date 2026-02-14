using UnityEngine;
using TMPro;

public class ProgressNumberPresenter : MonoBehaviour
{
    [SerializeField]
    private IntValue progressValue;
    [SerializeField]
    private IntValue maxValue;
    [SerializeField]
    private string valueName;
    [SerializeField]
    private TextMeshProUGUI numberText;

    private void UpdateNumberText()
    {
        numberText.text = valueName + ": " + progressValue.value.ToString() + "/" + maxValue.value.ToString();
    }

    private void Start()
    {
        UpdateNumberText();
    }

    private void OnEnable()
    {
        progressValue.onValueChanged += UpdateNumberText;
        maxValue.onValueChanged += UpdateNumberText;
    }

    private void OnDisable()
    {
        progressValue.onValueChanged -= UpdateNumberText;
        maxValue.onValueChanged -= UpdateNumberText;
    }
}
