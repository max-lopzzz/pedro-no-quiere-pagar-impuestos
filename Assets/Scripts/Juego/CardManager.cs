using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardManager : MonoBehaviour
{
    public static CardManager Instance;

    // THE NETHER (aqui estan las cosas del inspector que usara)
    [Header("Prefabs & Containers")]
    public GameObject cardPrefab;
    public Transform spawnArea;

    [Header("Sprites por Tipo")]
    public Sprite[] verduraSprites;
    public Sprite[] proteinaSprites;
    public Sprite[] carbohidratoSprites;

    [Header("UI & Scoring")]
    public ScoreManager scoreManager;

    // Base y bonus de reemplazos
    private int baseReplacements = 2;
    private int bonusReplacements = 0;
    private int replacementsLeft;

    private List<CardData> deck = new List<CardData>();

    [Header("Draw Settings")]
    public int drawCount = 5;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    void Start()
    {
        // inicializo el contador con base + bonus (por si EffectsManager ya sumó bonus)
        replacementsLeft = baseReplacements + bonusReplacements;

        CreateDeck();
        DrawCards(drawCount);
    }

    void CreateDeck()
    {
        deck.Clear();
        foreach (var s in verduraSprites) deck.Add(new CardData("verdura", s));
        foreach (var s in proteinaSprites) deck.Add(new CardData("proteína", s));
        foreach (var s in carbohidratoSprites) deck.Add(new CardData("carbohidrato", s));
    }

    public void DrawCards(int count)
    {
        foreach (Transform child in spawnArea)
            Destroy(child.gameObject);

        var shuffled = new List<CardData>(deck);
        Shuffle(shuffled);

        for (int i = 0; i < count && i < shuffled.Count; i++)
        {
            var go = Instantiate(cardPrefab, spawnArea);
            var ui = go.GetComponentInChildren<CardUI>();

            Sprite cardSprite = null;
            switch (shuffled[i].tipo)
            {
                case "verdura": cardSprite = verduraSprites[Random.Range(0, verduraSprites.Length)]; break;
                case "proteína": cardSprite = proteinaSprites[Random.Range(0, proteinaSprites.Length)]; break;
                case "carbohidrato": cardSprite = carbohidratoSprites[Random.Range(0, carbohidratoSprites.Length)]; break;
            }

            if (ui != null)
                ui.Initialize(shuffled[i].tipo, cardSprite);
            else
                Debug.LogError("CardUI no fue encontrado en el prefab instanciado.");
        }
    }

    public void IncreaseDrawCount(int extra)
    {
        drawCount += extra;
    }

    public void ResetAllSelections()
    {
        foreach (Transform child in spawnArea)
        {
            var ui = child.GetComponentInChildren<CardUI>();
            if (ui != null) ui.ResetSelection();
        }
        CardUI.ResetSelectionCount();
    }

    public void UpdateAllSelections()
    {
        var selected = new List<CardUI>();
        foreach (Transform child in spawnArea)
        {
            var ui = child.GetComponentInChildren<CardUI>();
            if (ui != null && ui.IsSelected()) selected.Add(ui);
        }
        scoreManager.UpdateScores(selected);
    }

    public void ReplaceSelectedCards()
    {
        if (replacementsLeft <= 0)
        {
            Debug.Log("Ya no te quedan reemplazos.");
            return;
        }

        var selected = new List<CardUI>();
        foreach (Transform child in spawnArea)
        {
            var ui = child.GetComponentInChildren<CardUI>();
            if (ui != null && ui.IsSelected()) selected.Add(ui);
        }

        if (selected.Count == 0)
        {
            Debug.Log("No hay cartas seleccionadas para reemplazar.");
            return;
        }

        var shuffledDeck = new List<CardData>(deck);
        Shuffle(shuffledDeck);

        int deckIndex = 0;
        bool replacedAny = false;

        foreach (var card in selected)
        {
            while (deckIndex < shuffledDeck.Count)
            {
                var data = shuffledDeck[deckIndex++];
                if (data.tipo != card.cardType)
                {
                    Sprite newSprite = null;
                    switch (data.tipo)
                    {
                        case "verdura": newSprite = verduraSprites[Random.Range(0, verduraSprites.Length)]; break;
                        case "proteína": newSprite = proteinaSprites[Random.Range(0, proteinaSprites.Length)]; break;
                        case "carbohidrato": newSprite = carbohidratoSprites[Random.Range(0, carbohidratoSprites.Length)]; break;
                    }
                    card.Initialize(data.tipo, newSprite);
                    replacedAny = true;
                    break;
                }
            }
        }

        if (replacedAny)
        {
            replacementsLeft--;
            Debug.Log($"Reemplazos restantes: {replacementsLeft}");
            CardUI.ResetSelectionCount();
            UpdateAllSelections();

            // Actualiza UI
            var gm = GameObject.FindFirstObjectByType<GameManager>();
            if (gm != null)
                SetReplacementsText(gm.replacementsText);
        }
    }

    void Shuffle(List<CardData> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            int rnd = Random.Range(i, list.Count);
            var tmp = list[i];
            list[i] = list[rnd];
            list[rnd] = tmp;
        }
    }

    /// <summary>
    /// Resetea los reemplazos para la nueva ronda, conservando bonus de ketchup.
    /// </summary>
    public void ResetReplacements()
    {
        replacementsLeft = baseReplacements + bonusReplacements;
    }

    public int ReplacementsLeft => replacementsLeft;

    /// <summary>
    /// Actualiza el texto UI de reemplazos.
    /// </summary>
    public void SetReplacementsText(Text textUI)
    {
        if (textUI != null)
            textUI.text = "Reemplazos: " + replacementsLeft;
    }

    /// <summary>
    /// Añade usos extra de reemplazo (efecto ketchup).
    /// </summary>
    public void AddReplacementUses(int cantidad)
    {
        bonusReplacements += cantidad;
        replacementsLeft += cantidad;
        Debug.Log($"Se añadieron {cantidad} usos de reemplazo (bonus). Total ahora: {replacementsLeft}");
    }
}
