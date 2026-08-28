// EffectsManager.cs
using System.Linq;
using UnityEngine;

public class EffectsManager : MonoBehaviour
{
    void Start()
    {
        ApplyItemEffects();
    }

    void ApplyItemEffects()
    {
        var owned = SaveManager.Instance.GetOwnedItems();

        // 1) Soy Sauce  cada uno duplica el puntaje (stacking: 2^n)
        int soyCount = owned.Count(item => item.itemName == "Soy Sauce");
        if (soyCount > 0)
        {
            int multiplier = (int)Mathf.Pow(2, soyCount);
            ScoreManager.Instance.SetScoreMultiplier(multiplier);
            Debug.Log($"Efecto Soy Sauce aplicado: ×{multiplier} (por {soyCount} Soy Sauce)");
        }

        // 2) Ketchup  +1 reemplazo por cada ketchup (stacking lineal)
        int ketchupCount = owned.Count(item => item.itemName == "Ketchup");
        if (ketchupCount > 0)
        {
            CardManager.Instance.AddReplacementUses(ketchupCount);
            CardManager.Instance.ResetReplacements();
            Debug.Log($"Efecto Ketchup aplicado: +{ketchupCount} reemplazos (por {ketchupCount} Ketchup)");

            var gm = GameObject.FindFirstObjectByType<GameManager>();
            if (gm != null && gm.replacementsText != null)
                gm.replacementsText.text = "Reemplazos: " + CardManager.Instance.ReplacementsLeft;
        }

        // 3) Catdrick Lamar  +1 carta al drawCount por cada Catdrick (stacking lineal)
        int catCount = owned.Count(item => item.itemName == "Catdrick Lamar");
        if (catCount > 0)
        {
            CardManager.Instance.IncreaseDrawCount(catCount);
            Debug.Log($"Efecto Catdrick Lamar aplicado: +{catCount} cartas al drawCount (ahora {CardManager.Instance.drawCount})");
        }
    }
}
