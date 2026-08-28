using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{

    //serializables para aparecer en el inspector de GameManager
    [Header("UI de Fin de Partida (US21)")]
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private TMP_Text gameOverResultText;



    public TMP_Text collectiblesNumbersText;

    private int collectiblesNumber=0;

    void Start()
    {
        collectiblesNumber = 0;

        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(false); // Oculto al iniciar la partida
        }

        UpdateScoreUI();
    }

    void Update()
    {
    }

    public void addCollectible()
    {
        collectiblesNumber++;
        UpdateScoreUI();
    }
    //getter de puntaje
    public int GetScore()
    {
        return collectiblesNumber;
    }

    //actualizador de puntaje
    private void UpdateScoreUI()
    {
        if (collectiblesNumbersText != null)
        {
            collectiblesNumbersText.text = collectiblesNumber.ToString();
        }
    }

    // Finalizador de la partida. Se ejecuta cuando el Timer llega a 00:00
    public void EndMatch()
    {
        // Desactiva al p1
        MovimientoJugador p1 = FindAnyObjectByType<MovimientoJugador>();
        if (p1 != null)
        {
            p1.enabled = false; // Corta el Update del movimiento
            Animator animP1 = p1.GetComponentInChildren<Animator>();
            if (animP1 != null)
            {
                animP1.SetBool("IsMoving", false);
                animP1.SetBool("IsRunning", false);
                animP1.SetBool("Grounded", true);          // Fuerza contacto con el suelo
                animP1.SetFloat("VerticalVelocity", 0f);    // Resetea velocidad vertical
                animP1.ResetTrigger("Jump");
            }
        }

        // Desactiva al p2
        MovimientoJugadorP2 p2 = FindAnyObjectByType<MovimientoJugadorP2>();
        if (p2 != null)
        {
            p2.enabled = false; // Corta el Update del movimiento
            Animator animP2 = p2.GetComponentInChildren<Animator>();
            if (animP2 != null)
            {
                animP2.SetBool("IsMoving", false);
                animP2.SetBool("IsRunning", false);
                animP2.SetBool("Grounded", true);          // Fuerza contacto con el suelo
                animP2.SetFloat("VerticalVelocity", 0f);    // Resetea velocidad vertical
                animP2.ResetTrigger("Jump");
            }
        }

        // Aviso de fin de la partida
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(true);
        }

        if (gameOverResultText != null)
        {
            gameOverResultText.text = "¡Tiempo Agotado!\nPuntos: " + collectiblesNumber;
        }

        Debug.Log("Partida finalizada. Movimiento bloqueado.");
    }
}
