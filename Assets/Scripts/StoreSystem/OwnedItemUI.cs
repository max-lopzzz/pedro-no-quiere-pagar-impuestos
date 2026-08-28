using UnityEngine;
using UnityEngine.UI;

public class OwnedItemUI : MonoBehaviour
{
    public Image icon;
    public Text nameText;

    /// <summary>
    /// Muestra un objeto comprado en el inventario.
    /// </summary>
    public void Setup(ItemData data)
    {
        icon.sprite = data.sprite;
        nameText.text = data.itemName;
    }
}