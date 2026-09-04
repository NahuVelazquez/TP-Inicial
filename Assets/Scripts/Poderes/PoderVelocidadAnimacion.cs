using UnityEngine;

public class PoderVelocidadAnimacion : MonoBehaviour
{
    public float rotationSpeed = 100f;
    public float moveDistance = 0.5f;
    public float moveSpeed = 2f;

    private Vector3 startPosition;

    void Start()
    {
        startPosition = transform.position;
    }

    void Update()
    {
        // Rotación
        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);

        // Movimiento de un lado a otro
        float newX = startPosition.x +
        Mathf.Sin(Time.time * moveSpeed) * moveDistance;

        transform.position = new Vector3
        (
            newX,
            startPosition.y,
            startPosition.z
        );
    }
}
