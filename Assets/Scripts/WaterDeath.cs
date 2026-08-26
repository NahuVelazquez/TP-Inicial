using UnityEngine;

public class WaterDeath : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerRespawn respawn = other.GetComponent<PlayerRespawn>();

            if (respawn != null)
            {
                respawn.Respawn();
            }
        }
    }
}