using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    [Header("Configuración de Tiempo (US20)")]
    [Tooltip("Duración de la partida en segundos")]
    [SerializeField] private float initialTime = 60f;

    [Header("Referencias de UI")]
    [SerializeField] private TMP_Text timerText;

    [Header("Referencias de Gestión")]
    [SerializeField] private GameManager gameManager;

    private float currentTime;
    private bool isRunning = false;

    private void Start()
    {
        InitializeTimer();
    }

    public void InitializeTimer()
    {
        currentTime = initialTime;
        isRunning = true;
        UpdateTimerUI();
        Debug.Log("Temporizador inicializado en: " + initialTime + " segundos.");
    }

    private void Update()
    {
        if (!isRunning) return;

        if (currentTime > 0f)
        {
            currentTime -= Time.deltaTime;

            if (currentTime <= 0f)
            {
                currentTime = 0f;
                isRunning = false;
                UpdateTimerUI();
                OnTimeExpired();
            }
            else
            {
                UpdateTimerUI();
            }
        }
    }

    private void UpdateTimerUI()
    {
        if (timerText != null)
        {
            float clampedTime = Mathf.Max(0f, currentTime);
            int minutes = Mathf.FloorToInt(clampedTime / 60f);
            int seconds = Mathf.FloorToInt(clampedTime % 60f);
            timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        }
    }

    private void OnTimeExpired()
    {
        Debug.Log("Tiempo agotado.");

        // Notifica al GameManager si está asignado (gancho para US21/US22)
        if (gameManager != null)
        {
            // gameManager.OnTimeOut(); // Lo descomentamos cuando sumemos el método en GameManager
        }
    }
}