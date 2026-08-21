using UnityEngine;
using UnityEngine.InputSystem;

public class MovimientoJugador : MonoBehaviour
{
    private CharacterController controller;

    //public float speed = 5f;

    private Vector2 moveInput;

    public float gravity = -9.8f;
    private float verticalVelocity;

    //rotacion jugador junto con camara
    public Transform modelTransform;
    public Transform cameraPivot;

    //animaciones
    private Animator animator;

    private bool isRunning;
    private bool isMoving;
    public float walkSpeed = 3f;
    public float runSpeed = 6f;

    //bailar
    private bool isDancing;


    private void Start()
    {
        controller = GetComponent<CharacterController>();    

        //animaciones
        animator = GetComponentInChildren<Animator>();  
    }

    private void Update()
    {
        //animaciones
        float currentSpeed = (isRunning&&isMoving) ? runSpeed : walkSpeed;
        //

        if(controller.isGrounded && verticalVelocity<0)
        {
            verticalVelocity = -2f;
        }

        //rotacion jugador junto con camara
        Vector3 forward = cameraPivot.forward;
        Vector3 right = cameraPivot.right;

        forward.y = 0;
        right.y = 0;
        forward.Normalize();
        right.Normalize();

        Vector3 desiredMoveDir = (forward * moveInput.y + right * moveInput.x);

        if(desiredMoveDir.magnitude > 0.1f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(desiredMoveDir);
            modelTransform.rotation = Quaternion.Slerp(modelTransform.rotation, targetRotation, 15f * Time.deltaTime);
        }
        //


        verticalVelocity += gravity * Time.deltaTime;


        Vector3 velocity = desiredMoveDir * currentSpeed;
        velocity.y = verticalVelocity;

        controller.Move(velocity * Time.deltaTime); //evita movimientos bruscos con cambios de fps

        UpdateAnimations();
    }

    //caminar
    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }

    //correr
    public void OnSprint(InputAction.CallbackContext context)
    {
        isRunning = context.ReadValueAsButton();
    }

    //bailar
    public void OnInteract(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Debug.Log("INTERACT FUNCIONA");
            isDancing = !isDancing;
        }
    }

    public void UpdateAnimations()
    {
        isMoving = moveInput.magnitude > 0.1f;

        animator.SetBool("IsMoving", isMoving);
        animator.SetBool("IsRunning", isRunning && isMoving);
        animator.SetBool("IsDancing", isDancing);
    }
}
