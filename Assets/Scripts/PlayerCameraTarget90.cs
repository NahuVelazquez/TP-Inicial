using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCameraTarget90 : MonoBehaviour
{
    [Header("Objetivo a Seguir")]
    public Transform jugador;
    public Vector3 offset = new Vector3(0f, 1.2f, 0f);

    [Header("Configuracion de Rotacion")]
    [SerializeField] private float smoothSpeed = 10f;
    private float targetYaw = 0f;

    [Header("Teclas de Giro")]
    public Key teclaIzquierda = Key.Q;
    public Key teclaDerecha = Key.E;

    private void Start()
    {
        targetYaw = transform.eulerAngles.y;
    }

    private void Update()
    {
        // Rotacion manual 90 grados
        if (Keyboard.current[teclaIzquierda].wasPressedThisFrame)
        {
            Rotar(-90f);
        }
        else if (Keyboard.current[teclaDerecha].wasPressedThisFrame)
        {
            Rotar(90f);
        }

        // Suavizado de rotacion
        Quaternion targetRot = Quaternion.Euler(0f, targetYaw, 0f);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, smoothSpeed * Time.deltaTime);
    }

    private void LateUpdate()
    {
        // Seguir posicion del jugador sin heredar sus rotaciones al doblar
        if (jugador != null)
        {
            transform.position = jugador.position + offset;
        }
    }

    public void Rotar(float grados)
    {
        targetYaw += grados;
        targetYaw = (targetYaw % 360f + 360f) % 360f;
    }
}