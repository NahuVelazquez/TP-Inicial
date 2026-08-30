using UnityEngine;

public class Elevator : MonoBehaviour
{
    public float height = 5f;       // Cuánto sube
    public float speed = 2f;        // Velocidad

    private Vector3 startPosition;

    void Start()
    {
        startPosition = transform.position;
    }

    void Update()
    {
        float movement = Mathf.PingPong(Time.time * speed, height);

        transform.position = startPosition + Vector3.up * movement;
    }
}
