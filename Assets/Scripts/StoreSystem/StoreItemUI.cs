// StoreItemUI.cs
using UnityEngine;
using UnityEngine.UI;

public class StoreItemUI : MonoBehaviour
{
    public Image icon;
    public Text nameText;
    public Text costText;
    public Button buyButton;

    private ItemData item;
    private StoreManager storeManager;

    /// <summary>
    /// Inicializa la UI del objeto en tienda.
    /// </summary>
    public void Setup(ItemData data, StoreManager manager)
    {
        item = data;
        storeManager = manager;

        icon.sprite = data.sprite;
        nameText.text = data.itemName;
        costText.text = "$" + data.cost;

        buyButton.onClick.RemoveAllListeners();
        buyButton.onClick.AddListener(OnBuy);
    }

    private void OnBuy()
    {
        storeManager.BuyItem(item);
    }
}
