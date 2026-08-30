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

        // ahora se detecta con tag, para jugador dos su tag es "Player2"
        if (other.CompareTag("Player2") || (other.transform.parent != null && other.transform.parent.CompareTag("Player2")))
        {
            isCollected = true; // bloquea futuras colisiones
            if (TryGetComponent<Collider>(out var col)) col.enabled = false; // elimina las colisiones al momento

            gm.addCollectible(2, pointsValue);
            Destroy(gameObject);
        }
        // ahora se detecta con tag, para jugador dos su tag es "Player"
        else if (other.CompareTag("Player") || (other.transform.parent != null && other.transform.parent.CompareTag("Player")))
        {
            isCollected = true; // bloquea futuras colisiones
            if (TryGetComponent<Collider>(out var col)) col.enabled = false; // elimina las colisiones al momento

            gm.addCollectible(1, pointsValue);
            Destroy(gameObject);
        }
    }
}