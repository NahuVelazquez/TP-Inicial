using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class MovimientoJugadorP2 : MonoBehaviour
{
    private CharacterController controller;

    // ENTRADA DE MOVIMIENTO (VECTOR2 PROVENIENTE DEL NEW INPUT SYSTEM)
    private Vector2 moveInput;

    public float gravity = -9.8f;
    private float verticalVelocity;

    // ROTACION DEL JUGADOR JUNTO CON LA CAMARA
    public Transform modelTransform;
    public Transform cameraPivot;

    // ANIMACIONES
    private Animator animator;

    private bool isRunning;
    private bool isMoving;
    public float walkSpeed = 3f;
    public float runSpeed = 6f;

    public float jumpForce = 5f;

    // BAILAR / INTERACCION
    private bool isDancing;

    private void Start()
    {
        controller = GetComponent<CharacterController>();

        // ANIMACIONES
        animator = GetComponentInChildren<Animator>();

        // SI NO SE ASIGNO UN MODELO ESPECIFICO EN EL INSPECTOR, TOMA ESTE TRANSFORM
        if (modelTransform == null)
        {
            modelTransform = transform;
        }
    }

    private void Update()
    {
        // DETERMINA VELOCIDAD SEGUN SI CORRE Y SE MUEVE
        float currentSpeed = (isRunning && isMoving) ? runSpeed : walkSpeed;

        if (controller.isGrounded && verticalVelocity < 0)
        {
            verticalVelocity = -2f;
        }

        // ROTACION Y MOVIMIENTO RELATIVOS A LA CAMARA
        Vector3 desiredMoveDir;

        if (cameraPivot != null)
        {
            Vector3 forward = cameraPivot.forward;
            Vector3 right = cameraPivot.right;

            forward.y = 0;
            right.y = 0;
            forward.Normalize();
            right.Normalize();

            desiredMoveDir = (forward * moveInput.y + right * moveInput.x);
        }
        else
        {
            // FALLBACK EN COORDENADAS GLOBALES SI NO HAY PIVOT ASIGNADO
            desiredMoveDir = new Vector3(moveInput.x, 0f, moveInput.y);
        }

        if (desiredMoveDir.magnitude > 0.1f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(desiredMoveDir);
            modelTransform.rotation = Quaternion.Slerp(modelTransform.rotation, targetRotation, 15f * Time.deltaTime);
        }

        verticalVelocity += gravity * Time.deltaTime;

        Vector3 velocity = desiredMoveDir * currentSpeed;
        velocity.y = verticalVelocity;

        controller.Move(velocity * Time.deltaTime);

        UpdateAnimations();
    }

    // EVENTOS DEL NEW INPUT SYSTEM (INVOCADOS POR PLAYERINPUT)
    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }

    public void OnSprint(InputAction.CallbackContext context)
    {
        isRunning = context.ReadValueAsButton();
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.performed && controller.isGrounded)
        {
            verticalVelocity = jumpForce;
            if (animator != null)
            {
                animator.SetTrigger("Jump");
            }
        }
    }

    public void OnInteract(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            isDancing = !isDancing;
        }
    }

    public void UpdateAnimations()
    {
        if (animator == null) return;

        isMoving = moveInput.magnitude > 0.1f;

        animator.SetBool("IsMoving", isMoving);
        animator.SetBool("IsRunning", isRunning && isMoving);
        animator.SetBool("IsDancing", isDancing);
        animator.SetBool("Grounded", controller.isGrounded);
        animator.SetFloat("VerticalVelocity", verticalVelocity);
    }

    // PODER SALTO
    public void AumentarSalto(float aumento, float duracion)
    {
        StartCoroutine(PowerUpSalto(aumento, duracion));
    }

    private IEnumerator PowerUpSalto(float aumento, float duracion)
    {
        float saltoOriginal = jumpForce;
        jumpForce += aumento;
        yield return new WaitForSeconds(duracion);
        jumpForce = saltoOriginal;
    }

    // PODER VELOCIDAD
    public void AumentarVelocidad(float aumento, float duracion)
    {
        StartCoroutine(PowerUpVelocidad(aumento, duracion));
    }

    private IEnumerator PowerUpVelocidad(float aumento, float duracion)
    {
        float velocidadCaminarOriginal = walkSpeed;
        float velocidadCorrerOriginal = runSpeed;

        walkSpeed += aumento;
        runSpeed += aumento;

        yield return new WaitForSeconds(duracion);

        walkSpeed = velocidadCaminarOriginal;
        runSpeed = velocidadCorrerOriginal;
    }
}