using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ComboDisplayMenu : MonoBehaviour
{
    [System.Serializable]
    private struct Combo
    {
        public int reqVerd, reqProt, reqCarb;
        public string name;
        public int value;

        public Combo(int v, int p, int c, string n, int val)
        {
            reqVerd = v;
            reqProt = p;
            reqCarb = c;
            name = n;
            value = val;
        }
    }

    [Header("Prefabs y UI")]
    public GameObject cardPrefab; // Prefab de carta dummy
    public Transform cardsContainer; // Grid o contenedor para cartas
    public Text comboNameText;
    public Text pointsText;

    [Header("Sprites por tipo de carta")]
    public Sprite verduraSprite;
    public Sprite proteinaSprite;
    public Sprite carbohidratoSprite;

    private int currentIndex = 0;

    private readonly List<Combo> combos = new List<Combo>()
    {
        new Combo(2, 0, 0, "Hakusai No Ohitashi",         2500), // Ensalada de col (verdura)
        new Combo(0, 2, 0, "Yakitori Basashi",            2500), // Brochetas de pollo crudo (proteína)
        new Combo(0, 0, 2, "Mochi Momo",                  2500), // Bolitas de arroz dulce (carbohidrato)
        
        new Combo(3, 0, 0, "Kyōya Mori Mori Yasai",        4000), // Ensalada abundante de verduras variadas
        new Combo(0, 3, 0, "Kuroge Wagyu Yakiniku",       4000), // Carne de res premium a la parrilla
        new Combo(0, 0, 3, "Chūka Sōmen",                 4000), // Fideos finos estilo ramen
        
        new Combo(2, 2, 1, "Sukiyaki Osaka-Style",        8000), // Plato cocido con carne y vegetales
        new Combo(0, 5, 0, "Ōgata Niku No Shōga Yaki",    12000), // Asado grande con jengibre
        new Combo(5, 0, 0, "Asari To Kyūri No Chirashi",  12000), // Ensalada colorida con almejas y pepino
        new Combo(0, 0, 5, "Gohan No Okoku",              12000), // Arroz imperial con ingredientes selectos
        
        new Combo(2, 3, 0, "Nikujaga Kama Age",           9000), // Plato de carne con patatas fritas
        new Combo(3, 0, 2, "Tōfu No Aemono",              9000), // Ensalada fría de tofu con verduras
        new Combo(0, 2, 3, "Teriyaki Tare Donburi",       9000), // Cuenco de arroz con carne glaseada teriyaki
    };

    void Start()
    {
        ShowCombo(currentIndex);
    }

    public void ShowCombo(int index)
    {
        if (index < 0 || index >= combos.Count)
            return;

        currentIndex = index;

        foreach (Transform child in cardsContainer)
        {
            if (Application.isPlaying) Destroy(child.gameObject);
            else DestroyImmediate(child.gameObject);
        }

        Combo combo = combos[index];

        comboNameText.text = combo.name;
        pointsText.text = combo.value + " pts";

        AddCards("verdura", combo.reqVerd);
        AddCards("prote�na", combo.reqProt);
        AddCards("carbohidrato", combo.reqCarb);
    }

    private void AddCards(string type, int count)
    {
        for (int i = 0; i < count; i++)
        {
            GameObject card = Instantiate(cardPrefab, cardsContainer);

            // Cambiar sprite seg�n tipo
            Image img = card.GetComponent<Image>();
            if (img != null)
            {
                if (type == "verdura") img.sprite = verduraSprite;
                else if (type == "prote�na") img.sprite = proteinaSprite;
                else if (type == "carbohidrato") img.sprite = carbohidratoSprite;
            }

            // Cambiar texto (opcional)
            Text label = card.GetComponentInChildren<Text>();
            if (label != null)
                label.text = type;
        }
    }

    public void NextCombo()
    {
        currentIndex = (currentIndex + 1) % combos.Count;
        ShowCombo(currentIndex);
    }

    public void PreviousCombo()
    {
        currentIndex = (currentIndex - 1 + combos.Count) % combos.Count;
        ShowCombo(currentIndex);
    }
}
