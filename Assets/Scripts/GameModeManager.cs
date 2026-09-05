using UnityEngine;
using Unity.Cinemachine;

public class GameModeManager : MonoBehaviour
{
    public static bool multiplayer = true;

    // =====================================
    // CONTROLADOR DE CAMARA SINGLEPLAYER (OPCIONAL)
    // =====================================
    public CameraController cameraControllerP1;

    // =====================================
    // REFERENCIAS DE PIVOTES Y MOVIMIENTO PARA PJ1
    // =====================================
    [Header("Pivotes de Cámara para PJ1")]
    public Transform pivotSingleplayer;  // ARRASTRAR CameraPivot (MOUSE)
    public Transform pivotMultiplayer;   // ARRASTRAR Target_P1 (ROTACION A 90 GRADOS)
    public MovimientoJugador jugador1Movimiento; // ARRASTRAR Hero_Rock

    [Header("Jugador")]
    public GameObject jugador2; // ARRASTRAR Hero_Rock_P2

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
        // =====================================
        // ACTIVAR JUGADOR 2
        // =====================================
        if (jugador2 != null)
            jugador2.SetActive(true);

        // =====================================
        // CÁMARAS PRINCIPALES (CON CHEQUEO DE NULOS)
        // =====================================
        if (mainCameraSolo != null)
            mainCameraSolo.SetActive(false);

        if (mainCameraP1 != null)
            mainCameraP1.SetActive(true);

        if (mainCameraP2 != null)
            mainCameraP2.SetActive(true);

        // =====================================
        // CINEMACHINE (CON CHEQUEO DE NULOS)
        // =====================================
        if (camaraSolo != null)
            camaraSolo.gameObject.SetActive(false);

        if (camaraMultiplayerCompartida != null)
            camaraMultiplayerCompartida.gameObject.SetActive(false);

        if (camaraP1 != null)
            camaraP1.gameObject.SetActive(true);

        if (camaraP2 != null)
            camaraP2.gameObject.SetActive(true);

        if (cameraControllerP1 != null)
            cameraControllerP1.enabled = false;

        // =====================================
        // ASIGNAR PIVOTE DE 90 GRADOS A PJ1 EN MULTIPLAYER
        // =====================================
        if (jugador1Movimiento != null && pivotMultiplayer != null)
        {
            jugador1Movimiento.cameraPivot = pivotMultiplayer;
            Debug.Log("[GameModeManager] Target_P1 asignado exitosamente como cameraPivot en Hero_Rock.");
        }
        else
        {
            Debug.LogWarning("[GameModeManager] Faltan asignar jugador1Movimiento o pivotMultiplayer en el Inspector.");
        }
    }

    private void ActivarSolo()
    {
        // =====================================
        // DESACTIVAR JUGADOR 2
        // =====================================
        if (jugador2 != null)
            jugador2.SetActive(false);

        // =====================================
        // CÁMARAS PRINCIPALES (CON CHEQUEO DE NULOS)
        // =====================================
        if (mainCameraSolo != null)
            mainCameraSolo.SetActive(true);

        if (mainCameraP1 != null)
            mainCameraP1.SetActive(false);

        if (mainCameraP2 != null)
            mainCameraP2.SetActive(false);

        // =====================================
        // CINEMACHINE (CON CHEQUEO DE NULOS)
        // =====================================
        if (camaraSolo != null)
            camaraSolo.gameObject.SetActive(true);

        if (camaraMultiplayerCompartida != null)
            camaraMultiplayerCompartida.gameObject.SetActive(false);

        if (camaraP1 != null)
            camaraP1.gameObject.SetActive(false);

        if (camaraP2 != null)
            camaraP2.gameObject.SetActive(false);

        if (cameraControllerP1 != null)
            cameraControllerP1.enabled = true;

        // =====================================
        // RESTAURAR PIVOTE ORIGINAL A PJ1 EN SINGLEPLAYER
        // =====================================
        if (jugador1Movimiento != null && pivotSingleplayer != null)
        {
            jugador1Movimiento.cameraPivot = pivotSingleplayer;
        }
    }
}