using UnityEngine;

public class GameDataManager : MonoBehaviour
{
    public static GameDataManager Instance;

    // Datos persistentes
    public int currentRound = 1;
    public int score = 0;
    public int dinero = 0;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Persiste entre escenas
        }
        else
        {
            Destroy(gameObject); // Evita duplicados
        }
    }

    // Métodos auxiliares
    public void AgregarPuntos(int puntos)
    {
        score += puntos;
    }

    public void AvanzarRonda()
    {
        currentRound++;
    }

    public void AgregarDinero(int cantidad)
    {
        dinero += cantidad;
    }

    public void ReiniciarDatos()
    {
        score = 0;
        currentRound = 1;
        dinero = 0;
    }
}
