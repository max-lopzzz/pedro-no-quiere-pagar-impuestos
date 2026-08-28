using UnityEngine;

[System.Serializable]
public class ItemData
{
    public string itemName;
    public int cost;
    public Sprite sprite;
    public string description;

    public ItemData(string name, int cost, Sprite sprite, string description)
    {
        this.itemName = name;
        this.cost = cost;
        this.sprite = sprite;
        this.description = description;
    }

}