using UnityEngine;

public class Wather : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        // se revisa que jugador toco el agua
        if (other.CompareTag("Player") || other.CompareTag("Player2"))
        {
            // se revisa el componente padre por seguridads
            PlayerRespawn respawn = other.GetComponentInParent<PlayerRespawn>();

            if (respawn != null)
            {
                respawn.Respawn();
            }
        }
    }
}