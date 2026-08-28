using UnityEngine;
using UnityEngine.UI;

public class CardUI : MonoBehaviour
{
    [HideInInspector]
    public string cardType;

    private bool isSelected = false;
    private static int selectedCount = 0;
    private static int maxSelectable = 5;

    private Image cardImage;
    private Color normalColor = Color.white;
    private Color highlightColor = new Color(0.6f, 1f, 0.6f); // Light green


    private float lastToggleTime = -1f;
    private const float toggleCooldown = 0.2f;

    void Awake()
    {
        cardImage = GetComponentInParent<Image>();
        if (cardImage == null)
        {
            Debug.LogError("CardUI no encontró el componente Image en el padre del prefab.");
        }

        if (cardImage != null)
            cardImage.color = normalColor;
    }

    public void Initialize(string tipo, Sprite sprite)
    {
        cardType = tipo;

        if (cardImage != null)
        {
            cardImage.sprite = sprite;
            cardImage.color = normalColor;
        }
        else
        {
            Debug.LogError("cardImage no está asignado en Initialize.");
        }
    }

    public void OnClick()
    {
        if (Time.unscaledTime - lastToggleTime < toggleCooldown) return;

        if (!isSelected && selectedCount >= maxSelectable)
        {
            Debug.Log("No puedes seleccionar más cartas.");
            return;
        }

        if (cardImage == null)
        {
            Debug.LogError("cardImage no está asignado en OnClick.");
            return;
        }

        isSelected = !isSelected;
        cardImage.color = isSelected ? highlightColor : normalColor;
        cardImage.transform.localScale = isSelected ? Vector3.one * 1.15f : Vector3.one;
        selectedCount += isSelected ? 1 : -1;
        lastToggleTime = Time.unscaledTime;

        // Checar puntaje inmediatamente después de cada selección o deselección
        CardManager.Instance.UpdateAllSelections();
    }

    public bool IsSelected() => isSelected;

    public void ResetSelection()
    {
        if (isSelected)
        {
            isSelected = false;
            if (cardImage != null)
                cardImage.color = normalColor;
            selectedCount = Mathf.Max(0, selectedCount - 1);
        }
    }

    public static void ResetSelectionCount()
    {
        selectedCount = 0;
    }
}
