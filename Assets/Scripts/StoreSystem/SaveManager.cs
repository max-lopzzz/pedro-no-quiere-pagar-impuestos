// SaveManager.cs
using System.Collections.Generic;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    public static SaveManager Instance;

    private List<ItemData> ownedItems = new List<ItemData>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SaveNewItem(ItemData item)
    {
        ownedItems.Add(item);
        Debug.Log("Guardado: " + item.itemName);
    }

    public List<ItemData> GetOwnedItems()
    {
        return ownedItems;
    }
}
