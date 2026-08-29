using UnityEngine;

public class Collectible : MonoBehaviour
{
    [SerializeField] private int pointsValue = 1;
    private bool isCollected = false; // booleano para evitar multiples colisiones y por lo tanto varias sumas de puntos

    private void OnTriggerEnter(Collider other)
    {
        // comprueba si ya colisiono
        if (isCollected) return;

        GameManager gm = FindAnyObjectByType<GameManager>();
        if (gm == null) return;

        // si es el Jugador 2
        if (other.GetComponentInParent<MovimientoJugadorP2>() != null ||
            other.gameObject.name.Contains("P2") ||
            (other.transform.parent != null && other.transform.parent.name.Contains("P2")))
        {
            isCollected = true; // bloquea futuras colisiones
            if (TryGetComponent<Collider>(out var col)) col.enabled = false; // elimina las colisiones al momento

            gm.addCollectible(2, pointsValue);
            Destroy(gameObject);
        }
        // si es el Jugador 1
        else if (other.GetComponentInParent<MovimientoJugador>() != null ||
                 other.gameObject.name.Contains("Hero_Rock"))
        {
            isCollected = true; // bloquea futuras colisiones
            if (TryGetComponent<Collider>(out var col)) col.enabled = false; // elimina las colisiones al momento

            gm.addCollectible(1, pointsValue);
            Destroy(gameObject);
        }
    }
}