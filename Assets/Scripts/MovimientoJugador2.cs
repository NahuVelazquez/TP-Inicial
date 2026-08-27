using UnityEngine;
using UnityEngine.InputSystem;

public class MovimientoJugadorP2 : MonoBehaviour
{
    private CharacterController controller;
    private Animator animator;

    public float walkSpeed = 3f;
    public float runSpeed = 6f;
    public float gravity = -9.8f;
    public float jumpForce = 5f;
    

    private float verticalVelocity;
    private bool isRunning;
    private bool isMoving;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        float horizontal = 0f;
        float vertical = 0f;

        // Movimiento con flechas
        if (Keyboard.current.leftArrowKey.isPressed)
            horizontal = -1f;

        if (Keyboard.current.rightArrowKey.isPressed)
            horizontal = 1f;

        if (Keyboard.current.upArrowKey.isPressed)
            vertical = 1f;

        if (Keyboard.current.downArrowKey.isPressed)
            vertical = -1f;

        Vector3 move = new Vector3(horizontal, 0f, vertical).normalized;

        // ¿Está corriendo?
        isRunning = Keyboard.current.rightShiftKey.isPressed;

        float currentSpeed = isRunning ? runSpeed : walkSpeed;

        // Gravedad
        if (controller.isGrounded && verticalVelocity < 0)
        {
            verticalVelocity = -2f;
        }

        verticalVelocity += gravity * Time.deltaTime;

        // Movimiento
        Vector3 velocity = move * currentSpeed;
        velocity.y = verticalVelocity;

        controller.Move(velocity * Time.deltaTime);

        // Rotación
        if (move.magnitude > 0.1f)
        {
            transform.rotation = Quaternion.LookRotation(move);
        }

        // Salto
        if (Keyboard.current.rightCtrlKey.wasPressedThisFrame && controller.isGrounded)
        {
            verticalVelocity = jumpForce;
            animator.SetTrigger("Jump");
        }

        // Animaciones
        isMoving = move.magnitude > 0.1f;

        animator.SetBool("IsMoving", isMoving);
        animator.SetBool("IsRunning", isRunning && isMoving);
        animator.SetBool("Grounded", controller.isGrounded);
        animator.SetFloat("VerticalVelocity", verticalVelocity);
    }
}