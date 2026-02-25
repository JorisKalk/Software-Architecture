using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PotionInventoryPanel : MonoBehaviour
{
    [HideInInspector]
    public PotionHandler potionHandler;

    [SerializeField]
    private TextMeshProUGUI potionNameText;
    [SerializeField]
    private TextMeshProUGUI healAmountText;
    [SerializeField]
    private Button useButton;

    private void OnEnable()
    {
        if (potionHandler == null)
        {
            Debug.Log("Panel is missing handler");
            Destroy(this.gameObject);
        }

        potionHandler.RequestValues(this);
    }

    public void ReceiveParent(PotionHandler handler)
    {
        potionHandler = handler;
    }

    public void UpdateValues(string pPotionName, int pAmountOwned, int pHealAmount)
    {
        potionNameText.text = pPotionName + ": " + pAmountOwned;
        healAmountText.text = pHealAmount.ToString();
        if (pAmountOwned == 0)
        {
            useButton.gameObject.SetActive(false);
        }
        else
        {
            useButton.gameObject.SetActive(true);
        }
    }

    public void PotionUsed()
    {
        potionHandler.PotionUsed(this);
    }
}
