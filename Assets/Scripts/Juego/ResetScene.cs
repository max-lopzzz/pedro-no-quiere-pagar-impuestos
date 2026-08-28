using UnityEngine;
using UnityEngine.SceneManagement;

public class ResetScene : MonoBehaviour
{
    // Este método debe enlazarse al OnClick() de tu Button en el Inspector
    public void RestartScene()
    {
        // Deselecciona todas las cartas y resetea el contador
        CardManager.Instance.ResetAllSelections();

        // Recarga la escena activa
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}