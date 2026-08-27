using UnityEngine;
using Unity.Cinemachine;

public class CameraTargetMultiplayer : MonoBehaviour
{
    public Transform jugador1;
    public Transform jugador2;

    public CinemachineCamera camara;

    public float distanciaMinima = 2f;
    public float distanciaMaxima = 8f;
    public float separacionMaxima = 15f;

    private CinemachineThirdPersonFollow thirdPersonFollow;

    private void Start()
    {
        if (camara != null)
        {
            thirdPersonFollow = camara.GetComponent<CinemachineThirdPersonFollow>();
        }
    }

    private void LateUpdate()
    {
        if (jugador1 == null || jugador2 == null)
            return;

        // Punto medio entre los jugadores
        Vector3 puntoMedio = (jugador1.position + jugador2.position) / 2f;

        // Mantener la altura del objetivo
        puntoMedio.y = transform.position.y;

        transform.position = puntoMedio;

        // Mantener la orientación de la cámara del jugador 1
        Transform pivotP1 = jugador1.Find("CameraPivot");

        if (pivotP1 != null)
        {
            transform.rotation = pivotP1.rotation;
        }

        // Distancia entre los jugadores
        float distancia = Vector3.Distance(jugador1.position, jugador2.position);

        // Convertir la separación en una distancia de cámara
        float porcentaje = Mathf.Clamp01(distancia / separacionMaxima);

        float nuevaDistancia = Mathf.Lerp(
            distanciaMinima,
            distanciaMaxima,
            porcentaje
        );

        // Aplicar distancia a la cámara
        if (thirdPersonFollow != null)
        {
            thirdPersonFollow.CameraDistance = nuevaDistancia;
        }
    }
}