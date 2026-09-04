using UnityEngine;

public class PoderSalto : MonoBehaviour
{
    public float aumentoSalto = 5f;
    public float duracion = 10f;

    private void OnTriggerEnter(Collider other)
    {
        // Jugador 1
        if (other.CompareTag("Player"))
        {
            MovimientoJugador jugador1 = other.GetComponent<MovimientoJugador>();

            if (jugador1 != null)
            {
                jugador1.AumentarSalto(aumentoSalto, duracion);
                Destroy(gameObject);
            }
        }

        // Jugador 2
        else if (other.CompareTag("Player2"))
        {
            MovimientoJugadorP2 jugador2 = other.GetComponent<MovimientoJugadorP2>();

            if (jugador2 != null)
            {
                jugador2.AumentarSalto(aumentoSalto, duracion);
                Destroy(gameObject);
            }
        }
    }
}
