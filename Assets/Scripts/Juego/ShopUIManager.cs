using UnityEngine;
using UnityEngine.UI;

public class ShopUIManager : MonoBehaviour
{
    public Text moneyText;

    void Start()
    {
        UpdateMoneyDisplay();
    }

    public void UpdateMoneyDisplay()
    {
        if (GameDataManager.Instance != null)
        {
            moneyText.text = "Dinero: $" + GameDataManager.Instance.dinero.ToString();
        }
        else
        {
            moneyText.text = "Dinero: $0";
        }
    }
}
