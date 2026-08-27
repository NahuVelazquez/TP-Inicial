using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    private CharacterController controller;

    public Transform respawnPoint;
    public float deathHeight = -10f;

    private MovimientoJugador movimientoJugador;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        movimientoJugador = GetComponent<MovimientoJugador>();
    }

    private void Update()
    {
        if (transform.position.y < deathHeight)
        {
            Respawn();
        }
    }

    public void Respawn()
    {
        controller.enabled = false;
        transform.position = respawnPoint.position;
        controller.enabled = true;
    }
}

