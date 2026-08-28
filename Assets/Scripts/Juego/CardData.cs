using UnityEngine;

[System.Serializable]
public class CardData
{
    public string tipo;
    public Sprite sprite;

    public CardData(string tipo, Sprite sprite)
    {
        this.tipo = tipo;
        this.sprite = sprite;
    }
}