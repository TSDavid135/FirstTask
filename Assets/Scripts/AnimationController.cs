using UnityEngine;

public class AnimationController : MonoBehaviour
{
    public GameObject playerState;
    private Animator animator;

    // Animation parameters
    private readonly int runningParam = Animator.StringToHash("IsRunning");
    private readonly int leftParam = Animator.StringToHash("Left");
    private readonly int rightParam = Animator.StringToHash("Right");
    private readonly int backwardsParam = Animator.StringToHash("Backwards");
    private readonly int jumpingParam = Animator.StringToHash("Jump");

    // Movement variables
    private Vector3 movement;
    private bool isJumping;

    void Start()
    {
        animator = GetComponent<Animator>();
        if (animator == null)
        {
            Debug.LogError("Animator component not found on the character prefab.");
        }
    }

    void Update()
    {
        HandleMovementInput();
        HandleJumpInput();
        UpdateAnimator();
    }

    void HandleMovementInput()
    {
        // Get input from the player
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        // Calculate movement direction
        movement = new Vector3(horizontal, 0, vertical).normalized;

        // Update animator parameters based on movement
        animator.SetBool(runningParam, movement.magnitude > 0.1f);
        animator.SetBool(leftParam, horizontal < -0.1f);
        animator.SetBool(rightParam, horizontal > 0.1f);
        animator.SetBool(backwardsParam, vertical < -0.1f);
    }

    void HandleJumpInput()
    {
        if (Input.GetButton("Jump") && !isJumping)
        {
            isJumping = true;
            animator.SetBool(jumpingParam, isJumping);
        }
    }

    void UpdateAnimator()
    {
        // Reset jumping state when landing (you can detect landing using raycasts or colliders)
        if (isJumping && playerState.GetComponent<CustomScript>().IsGround)
        {
            isJumping = false;
            animator.SetBool(jumpingParam, isJumping);
        }
    }
}
