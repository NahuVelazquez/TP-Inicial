using UnityEngine;

public class CameraFollowP2 : MonoBehaviour
{
    public Transform jugador;

    private void LateUpdate()
    {
        if (jugador == null)
            return;

        transform.position = jugador.position + Vector3.up * 0.5f;
        transform.rotation = Quaternion.Euler(0f, 0f, 0f);
    }
}
