using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{
    public Transform pivot;
    public float sensitivity = 0.2f;
    private Vector2 lookInput;
    private float yaw = 0f;
    private float pitch = 0f;

    public void OnLook(InputAction.CallbackContext context)
    {
        lookInput = context.ReadValue<Vector2>();
    }

    private void Update()
    {
         yaw += lookInput.x * sensitivity;  //rotacion horizontal
         pitch -= lookInput.y * sensitivity;    //rotacion vertical

         pitch = Mathf.Clamp(pitch, -45f, 60f); //para que la camara no se meta dentro del escenario

         pivot.localRotation = Quaternion.Euler(pitch, yaw, 0);
    }   
}
