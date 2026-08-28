using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;

    public Text platilloText;
    public Text scoreText;

    public int LastScore { get; private set; }
    private int scoreMultiplier = 1;

    private struct Combo
    {
        public int reqV, reqP, reqC;
        public string name;
        public int value;
        public Combo(int v, int p, int c, string n, int val)
        {
            reqV = v; reqP = p; reqC = c;
            name = n; value = val;
        }
    }

    private readonly List<Combo> combos = new List<Combo>()
    {
        new Combo(2, 0, 0, "Hakusai No Ohitashi",         2500),
        new Combo(0, 2, 0, "Yakitori Basashi",            2500),
        new Combo(0, 0, 2, "Mochi Momo",                  2500),
        new Combo(3, 0, 0, "Kyōya Mori Mori Yasai",        4000),
        new Combo(0, 3, 0, "Kuroge Wagyu Yakiniku",       4000),
        new Combo(0, 0, 3, "Chūka Sōmen",                 4000),
        new Combo(2, 2, 1, "Sukiyaki Osaka-Style",        8000),
        new Combo(0, 5, 0, "Ōgata Niku No Shōga Yaki",    12000),
        new Combo(5, 0, 0, "Asari To Kyūri No Chirashi",  12000),
        new Combo(0, 0, 5, "Gohan No Okoku",              12000),
        new Combo(2, 3, 0, "Nikujaga Kama Age",           9000),
        new Combo(3, 0, 2, "Tōfu No Aemono",              9000),
        new Combo(0, 2, 3, "Teriyaki Tare Donburi",       9000),
    };

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        platilloText.horizontalOverflow = HorizontalWrapMode.Overflow;
        platilloText.verticalOverflow = VerticalWrapMode.Overflow;
    }

    /// <summary>
    /// Permite aplicar buffs como Soy Sauce.
    /// </summary>
    public void SetScoreMultiplier(int multiplier)
    {
        scoreMultiplier = multiplier;
    }

    /// <summary>
    /// Calcula el mejor combo y muestra el score, incorporando explícitamente el multiplicador en la UI.
    /// </summary>
    public void UpdateScores(List<CardUI> selectedCards)
    {
        int v = 0, p = 0, c = 0;
        foreach (var card in selectedCards)
        {
            switch (card.cardType)
            {
                case "verdura": v++; break;
                case "proteína": p++; break;
                case "carbohidrato": c++; break;
            }
        }

        Combo? best = null;
        foreach (var combo in combos)
        {
            if (v >= combo.reqV && p >= combo.reqP && c >= combo.reqC)
            {
                if (best == null || combo.value > best.Value.value)
                    best = combo;
            }
        }

        int baseScore = best.HasValue ? best.Value.value : 0;
        LastScore = baseScore * scoreMultiplier;

        if (best.HasValue)
        {
            // Nombre del platillo
            platilloText.text = best.Value.name.Replace(" ", "\u00A0");

            // Si hay multiplicador, lo mostramos explícito
            if (scoreMultiplier > 1)
            {
                // Ej: "16000 pts (8000×2)"
                scoreText.text = $"{LastScore} pts ({baseScore}×{scoreMultiplier})";
            }
            else
            {
                scoreText.text = LastScore + " pts";
            }
        }
        else
        {
            platilloText.text = "Sin\u00A0Platillo";
            scoreText.text = "0 pts";
        }
    }
}
