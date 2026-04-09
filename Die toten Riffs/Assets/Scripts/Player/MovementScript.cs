using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class MovementScript : MonoBehaviour
{
    public Camera playerCamera;
    public float walkSpeed = 6f;
    public float runSpeed = 12f;
    public float jumpPower = 7f;
    public float gravityScale = 20f;
    public float acceleration = 10f;
    public float deceleration = 10f;
    public float defaultHeight = 2f;
    public float crouchHeight = 1f;
    public float crouchSpeed = 3f;

    public Vector3 currentMoveVelocity = Vector3.zero;
    private float verticalVelocity = 0f;
    private CharacterController characterController;
    private bool canMove = true;
    private float currentWalkSpeed;
    private float currentRunSpeed;

    public bool isGrounded => characterController.isGrounded;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        currentWalkSpeed = walkSpeed;
        currentRunSpeed = runSpeed;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        bool wantsToCrouch = Input.GetKey(KeyCode.LeftControl) && canMove;
        bool isCrouching = wantsToCrouch;

        if (!wantsToCrouch)
        {
            Vector3 rayOrigin = transform.position + (Vector3.up * (crouchHeight / 2f));
            float distanceToStand = (defaultHeight - crouchHeight);

            if (Physics.SphereCast(rayOrigin, characterController.radius * 0.8f, Vector3.up, 
                    out RaycastHit hit, distanceToStand))
            {
                isCrouching = true;
            }
        }
        
        bool isRunning = Input.GetKey(KeyCode.LeftShift) && !isCrouching;

        float targetHeight = isCrouching ? crouchHeight : defaultHeight;
        characterController.height = Mathf.Lerp(characterController.height, targetHeight, 10f * Time.deltaTime);

        float speed = isCrouching ? crouchSpeed : (isRunning ? runSpeed : walkSpeed);

        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);
        Vector3 targetVelocity = canMove
            ? (forward * Input.GetAxis("Vertical") + right * Input.GetAxis("Horizontal")) * speed
            : Vector3.zero;

        float lerpFactor = (targetVelocity.magnitude > 0.1f ? acceleration : deceleration) * Time.deltaTime;
        currentMoveVelocity = Vector3.Lerp(currentMoveVelocity, targetVelocity, lerpFactor);

        if (characterController.isGrounded)
        {
            if (verticalVelocity < 0f) verticalVelocity = -2f;
            if (Input.GetButtonDown("Jump") && canMove)
                verticalVelocity = jumpPower;
        }
        verticalVelocity -= gravityScale * Time.deltaTime;

        Vector3 finalMove = currentMoveVelocity;
        finalMove.y = verticalVelocity;
        characterController.Move(finalMove * Time.deltaTime);
    }
}