using UnityEngine;

public class CanvasToggle : MonoBehaviour
{
    public GameObject canvasToToggle;

    public void ToggleCanvas()
    {
        if (canvasToToggle != null)
        {
            bool isActive = canvasToToggle.activeSelf; // Verifica si el Canvas está activo o no
            canvasToToggle.SetActive(!isActive); // Alterna el estado de activación
        }
    }
}
