using UnityEngine;
using UnityEngine.UI;

public class ItemDetailPanel : MonoBehaviour
{
    public Image icon;
    public Text nameText;
    public Text costText;
    public Text descriptionText;
    public Button buyButton;

    private ItemData currentItem;
    private StoreManager storeManager;

    public void Show(ItemData data, StoreManager manager)
    {
        currentItem = data;
        storeManager = manager;

        icon.sprite = data.sprite;
        nameText.text = data.itemName;
        costText.text = "$" + data.cost;
        descriptionText.text = data.description;

        buyButton.onClick.RemoveAllListeners();
        buyButton.onClick.AddListener(() => Buy());

        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    private void Buy()
    {
        storeManager.BuyItem(currentItem);
        Hide();
    }
}
