using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Managers & UI")]
    public CardManager cardManager;
    public ScoreManager scoreManager;
    public Text targetText;
    public Text roundText;
    public Button confirmButton;
    public Canvas loseCanvas;
    public Button restartButton;
    public Text replacementsText;

    [Header("Objetos a controlar")]
    public GameObject objectToDeactivate;

    [Header("Ronda & Puntaje")]
    public int startingTarget = 2500;
    private int currentTarget;
    private int currentRound;
    private const float difficultyMultiplier = 1.2f;

    void Start()
    {
        currentRound = GameDataManager.Instance.currentRound;

        currentTarget = Mathf.RoundToInt(startingTarget * Mathf.Pow(difficultyMultiplier, currentRound - 1));
        UpdateUI();

        loseCanvas.gameObject.SetActive(false);

        confirmButton.onClick.AddListener(ConfirmHand);
        restartButton.onClick.AddListener(RestartGame);
    }

    void UpdateUI()
    {
        targetText.text = "Objetivo: " + currentTarget + " pts";
        roundText.text = "Ronda: " + currentRound;
    }

    public void ConfirmHand()
    {
        cardManager.ResetReplacements(); //  reinicia descartes aquí
        cardManager.SetReplacementsText(replacementsText);
        cardManager.UpdateAllSelections();
        int playerScore = scoreManager.LastScore;

        if (playerScore >= currentTarget)
        {
            int dineroAGanar = 10 + (currentRound - 1);
            GameDataManager.Instance.AgregarDinero(dineroAGanar);
            CardUI.ResetSelectionCount();
            GameDataManager.Instance.AvanzarRonda();
            SceneManager.LoadScene("Store");
        }
        else
        {
            loseCanvas.gameObject.SetActive(true);
            confirmButton.interactable = false;
            if (objectToDeactivate != null)
                objectToDeactivate.SetActive(false);
        }
    }


    public void RestartGame()
    {
        GameDataManager.Instance.ReiniciarDatos(); //  reinicia dinero y ronda
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
