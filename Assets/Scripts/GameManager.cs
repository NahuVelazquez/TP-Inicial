using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("Configuración de Partida (US22)")]
    [SerializeField] private int targetScore = 5; // puntaje mínimo para ganar por tiempo
    [SerializeField] private bool isMultiplayer = false; // booleano para diferenciar modo un jugaor o dos jugadores

    [Header("UI de Fin de Partida (US21 / US22)")]
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private TMP_Text gameOverResultText;

    [Header("HUD")]
    public TMP_Text scoreP1Text;
    public TMP_Text scoreP2Text; // Queda listo para cuando activemos P2

    private int scoreP1 = 0; //puntaje pj1
    private int scoreP2 = 0; //puntaje pj2
    private int collectedItemsCount = 0; //cuentos estrellas se agarraron
    private int totalCollectiblesCount = 0; //cuantas estrellas hay en total en el mapa al empezar el juego
    private bool isGameOver = false;

    void Start()
    {
        scoreP1 = 0;
        scoreP2 = 0;
        collectedItemsCount = 0;
        isGameOver = false;

        // cuenta cantidad de coleccionables en la escena
        totalCollectiblesCount = FindObjectsByType<Collectible>(FindObjectsSortMode.None).Length;

        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(false);
        }

        UpdateScoreUI();
    }

    // suma los puntos obtenidos de agarrar una estrella al puntaje jugador que corresponda
    public void addCollectible(int playerIndex = 1, int points = 1)
    {
        if (isGameOver) return;

        collectedItemsCount++;

        if (playerIndex == 1) scoreP1 += points;
        else if (playerIndex == 2) scoreP2 += points;

        UpdateScoreUI();

        // victoria por juntar todas las estrellas
        if (totalCollectiblesCount > 0 && collectedItemsCount >= totalCollectiblesCount)
        {
            EndMatch();
        }
    }

    public int GetScoreP1() => scoreP1;
    public int GetScoreP2() => scoreP2;

    private void UpdateScoreUI()
    {
        if (scoreP1Text != null)
        {
            scoreP1Text.text = scoreP1.ToString();
        }
        if (scoreP2Text != null)
        {
            scoreP2Text.text = scoreP2.ToString();
        }
    }

    public void EndMatch()
    {
        if (isGameOver) return;
        isGameOver = true;

        // frenar cronometro
        Timer timer = FindAnyObjectByType<Timer>();
        if (timer != null) timer.enabled = false;

        // desactivar al primer pj al terminar la partida
        MovimientoJugador p1 = FindAnyObjectByType<MovimientoJugador>();
        if (p1 != null)
        {
            p1.enabled = false;
            Animator animP1 = p1.GetComponentInChildren<Animator>();
            if (animP1 != null)
            {
                animP1.SetBool("IsMoving", false);
                animP1.SetBool("IsRunning", false);
                animP1.SetBool("Grounded", true);
                animP1.SetFloat("VerticalVelocity", 0f);
                animP1.ResetTrigger("Jump");
            }
        }

        // desactivar al segundo pj al terminar la partida
        MovimientoJugadorP2 p2 = FindAnyObjectByType<MovimientoJugadorP2>();
        if (p2 != null)
        {
            p2.enabled = false;
            Animator animP2 = p2.GetComponentInChildren<Animator>();
            if (animP2 != null)
            {
                animP2.SetBool("IsMoving", false);
                animP2.SetBool("IsRunning", false);
                animP2.SetBool("Grounded", true);
                animP2.SetFloat("VerticalVelocity", 0f);
                animP2.ResetTrigger("Jump");
            }
        }

        // activa el cartel de game over
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(true);
        }
        //activa el cartel de resulatod
        if (gameOverResultText != null)
        {
            if (isMultiplayer) // este rama es ara el multiplaye, compara puntos de ambos jugadores y decide al ganador
            {
                if (scoreP1 > scoreP2)
                {
                    gameOverResultText.text = $"¡GANA JUGADOR 1!\n{scoreP1} pts vs {scoreP2} pts";
                }
                else if (scoreP2 > scoreP1)
                {
                    gameOverResultText.text = $"¡GANA JUGADOR 2!\n{scoreP2} pts vs {scoreP1} pts";
                }
                else
                {
                    gameOverResultText.text = $"¡EMPATE!\nAmbos consiguieron {scoreP1} pts";
                }
            }
            else // esta rama es para el modo un jugador
            {
                bool cleanedMap = (totalCollectiblesCount > 0 && collectedItemsCount >= totalCollectiblesCount); //junto todas las estrellas
                bool reachedTarget = (scoreP1 >= targetScore); //junto la cantidad minima de puntos
                bool hasWon = cleanedMap || reachedTarget; //cumplio alguna condicion de victoria

                if (cleanedMap) //si junto todas las estrellas
                {
                    gameOverResultText.text = $"¡VICTORIA PERFECTA!\nJuntaste todos los objetos ({collectedItemsCount}/{totalCollectiblesCount})\nPuntos: {scoreP1}";
                }
                else if (hasWon) //si llego a la minima cantidad de puntos
                {
                    gameOverResultText.text = $"¡VICTORIA!\nPuntos: {scoreP1} / {targetScore} requeridos";
                }
                else //si no hizo ninguna, perdio
                {
                    gameOverResultText.text = $"¡DERROTA!\nPuntos: {scoreP1} / {targetScore} requeridos";
                }
            }
        }

        Debug.Log($"Partida terminada. Multijugador: {isMultiplayer} | P1: {scoreP1} | P2: {scoreP2}");
    }

    // metodo para asociar al botón de Reiniciar en la UI (US24)
    public void RestartMatch()
    {
        // recarga la escena desde cero resetea coleccionables, cronómetro y posiciones
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}