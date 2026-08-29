using UnityEngine;
using Unity.Cinemachine;

public class CameraTargetMultiplayer : MonoBehaviour
{
    public Transform jugador1;
    public Transform jugador2;

    public CinemachineCamera camara;

    [Header("Objetivo")]
    public float alturaObjetivo = 1.5f;
    public float suavidadMovimiento = 8f;

    [Header("Zoom")]
    public float distanciaMinimaCamara = 5f;
    public float distanciaMaximaCamara = 12f;

    [Header("Separación")]
    public float separacionMinima = 3f;
    public float separacionMaxima = 12f;

    [Header("Suavidad del zoom")]
    public float velocidadZoom = 3f;

    private CinemachineThirdPersonFollow thirdPersonFollow;

    private void Start()
    {
        if (camara != null)
        {
            thirdPersonFollow =
                camara.GetComponent<CinemachineThirdPersonFollow>();
        }
    }

    private void LateUpdate()
    {
        if (jugador1 == null || jugador2 == null)
            return;

        // ==========================================
        // PUNTO MEDIO
        // ==========================================

        Vector3 puntoMedio =
            (jugador1.position + jugador2.position) / 2f;

        puntoMedio.y += alturaObjetivo;

        transform.position = Vector3.Lerp(
            transform.position,
            puntoMedio,
            suavidadMovimiento * Time.deltaTime
        );


        // ==========================================
        // DISTANCIA REAL ENTRE LOS JUGADORES
        // ==========================================

        float distancia = Vector3.Distance(
            jugador1.position,
            jugador2.position
        );


        // ==========================================
        // CALCULAR ZOOM
        // ==========================================

        float porcentaje = Mathf.InverseLerp(
            separacionMinima,
            separacionMaxima,
            distancia
        );

        float distanciaObjetivo = Mathf.Lerp(
            distanciaMinimaCamara,
            distanciaMaximaCamara,
            porcentaje
        );


        // ==========================================
        // ZOOM SUAVE
        // ==========================================

        if (thirdPersonFollow != null)
        {
            thirdPersonFollow.CameraDistance = Mathf.MoveTowards(
                thirdPersonFollow.CameraDistance,
                distanciaObjetivo,
                velocidadZoom * Time.deltaTime
            );
        }
    }
}