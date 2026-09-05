using UnityEngine;
using Unity.Cinemachine;

public class GameModeManager : MonoBehaviour
{
    public static bool multiplayer = true;
    public CameraController cameraControllerP1;

    [Header("Jugador")]
    public GameObject jugador2;

    [Header("Cámaras Cinemachine")]
    public CinemachineCamera camaraSolo;
    public CinemachineCamera camaraMultiplayerCompartida;
    public CinemachineCamera camaraP1;
    public CinemachineCamera camaraP2;

    [Header("Cámaras principales")]
    public GameObject mainCameraSolo;
    public GameObject mainCameraP1;
    public GameObject mainCameraP2;

    private void Start()
    {
        if (multiplayer)
        {
            ActivarMultiplayer();
        }
        else
        {
            ActivarSolo();
        }
    }

    private void ActivarMultiplayer()
    {
        // Activar jugador 2
        jugador2.SetActive(true);

        // =====================================
        // CÁMARAS PRINCIPALES
        // =====================================

        mainCameraSolo.SetActive(false);
        mainCameraP1.SetActive(true);
        mainCameraP2.SetActive(true);

        // =====================================
        // CINEMACHINE
        // =====================================

        camaraSolo.gameObject.SetActive(false);

        // Desactivamos la cámara multiplayer
        // compartida que hicimos anteriormente
        camaraMultiplayerCompartida.gameObject.SetActive(false);

        // Activamos las dos cámaras nuevas
        camaraP1.gameObject.SetActive(true);
        camaraP2.gameObject.SetActive(true);
        cameraControllerP1.enabled = true;
    }

    private void ActivarSolo()
    {
        // Ocultar jugador 2
        jugador2.SetActive(false);

        // =====================================
        // CÁMARAS PRINCIPALES
        // =====================================

        mainCameraSolo.SetActive(true);
        mainCameraP1.SetActive(false);
        mainCameraP2.SetActive(false);

        // =====================================
        // CINEMACHINE
        // =====================================

        camaraSolo.gameObject.SetActive(true);

        camaraMultiplayerCompartida.gameObject.SetActive(false);

        camaraP1.gameObject.SetActive(false);
        camaraP2.gameObject.SetActive(false);
        cameraControllerP1.enabled = true;
    }
}