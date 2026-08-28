using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoreManager : MonoBehaviour
{
    [Header("Prefabs & Containers")]
    public GameObject storeItemPrefab;   // Prefab con StoreItemUI
    public GameObject emptySlotPrefab;   // Prefab de espacio vacío
    public Transform storeGrid;          // Grid Layout Group de la tienda

    public GameObject inventoryItemPrefab;
    public Transform inventoryGrid;

    [Header("Item Sprites")]
    public Sprite soySauceSprite;
    public Sprite ketchupSprite;
    public Sprite wasabiSprite;
    public Sprite Catdrick;
    public Sprite ShaoCat;

    [Header("UI & Managers")]
    public ShopUIManager shopUIManager;

    public int maxStoreSlots = 4;

    // Lista maestra de ítems, cuyos price se van a ir ajustando
    private List<ItemData> items = new List<ItemData>();

    void Awake()
    {
        // Crea tus ítems con sus costos iniciales
        items.Add(new ItemData("Soy Sauce", 45, soySauceSprite, "Salty and traditional."));
        items.Add(new ItemData("Ketchup", 25, ketchupSprite, "Sweet tomato condiment."));
        items.Add(new ItemData("Wasabi", 75, wasabiSprite, "Spicy green paste."));
        items.Add(new ItemData("Catdrick Lamar", 125, Catdrick, "Good at Cooking Foo's"));
        items.Add(new ItemData("Shao Cat", 65, ShaoCat, "He got a beard because it looks rad."));
    }

    void Start()
    {
        PopulateStore();

        // Restaurar inventario guardado
        foreach (var item in SaveManager.Instance.GetOwnedItems())
            AddToInventory(item);
    }

    // Llama para refrescar la tienda (p. ej. si quieres ver nuevos precios inmediatamente)
    private void ClearStore()
    {
        foreach (Transform child in storeGrid)
            Destroy(child.gameObject);
    }

    private void PopulateStore()
    {
        ClearStore();

        // Copia temporal para no alterar el orden original
        var temp = new List<ItemData>(items);
        for (int i = 0; i < maxStoreSlots; i++)
        {
            if (temp.Count > 0)
            {
                int rnd = Random.Range(0, temp.Count);
                var item = temp[rnd];
                temp.RemoveAt(rnd);

                var go = Instantiate(storeItemPrefab, storeGrid);
                var ui = go.GetComponent<StoreItemUI>();
                ui.Setup(item, this);
            }
            else
            {
                Instantiate(emptySlotPrefab, storeGrid);
            }
        }
    }

    public void BuyItem(ItemData item)
    {
        if (GameDataManager.Instance.dinero < item.cost)
        {
            Debug.Log("Fondos insuficientes para comprar " + item.itemName);
            return;
        }

        // 1) Descontar dinero
        GameDataManager.Instance.dinero -= item.cost;
        shopUIManager.UpdateMoneyDisplay();

        // 2) Agregar al inventario
        AddToInventory(item);
        SaveManager.Instance.SaveNewItem(item);

        // 3) Ajustar precio del ítem comprado
        AdjustPrice(item.itemName);

        // 4) Refrescar UI de tienda para ver nuevos precios
        PopulateStore();
    }

    private void AdjustPrice(string itemName)
    {
        foreach (var data in items)
        {
            if (data.itemName == itemName)
            {
                switch (itemName)
                {
                    case "Soy Sauce":
                        data.cost += 15;
                        break;
                    case "Ketchup":
                        data.cost += 50;
                        break;
                    case "Catdrick Lamar":
                        data.cost += 200;
                        break;
                        // otros ítems no cambian de precio
                }
                Debug.Log($"Precio de {itemName} ahora {data.cost}");
                break;
            }
        }
    }

    private void AddToInventory(ItemData item)
    {
        var go = Instantiate(inventoryItemPrefab, inventoryGrid);
        var ui = go.GetComponent<OwnedItemUI>();
        ui.Setup(item);
    }
}
