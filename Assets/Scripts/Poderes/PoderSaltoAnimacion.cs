using UnityEngine;

public class PoderSaltoAnimacion : MonoBehaviour
{
    public float jumpHeight = 0.4f;
    public float jumpSpeed = 3f;
    public float speed = 10f;

    private float startY;

    void Start()
    {
        startY = transform.position.y;
    }

    void Update()
    {
        // Rotación
        transform.Rotate(Vector3.up, speed * Time.deltaTime);

        // Salto
        float newY = startY + Mathf.Abs(Mathf.Sin(Time.time * jumpSpeed)) * jumpHeight;

        transform.position = new Vector3(
        transform.position.x,
        newY,
        transform.position.z
        );
    }
}
