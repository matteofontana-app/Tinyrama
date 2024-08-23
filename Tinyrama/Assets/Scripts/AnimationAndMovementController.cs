using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class AnimationAndMovementController : MonoBehaviour
{

    PlayerInput playerInput;
    CharacterController characterController;
    Animator animator;

    int isWalkingHash;
    int isRunningHash;

    Vector2 currentMovementInput;
    Vector3 currentMovement;
    Vector3 currentRunMovement;

    bool isMovementPressed;
    bool isRunPressed;
    float rotationFactorPerFrame = 15f;
    float runMultiplier = 3.0f;

    float gravity = 0.5f;
    float groundedGravity = 0.005f;

    bool isJumpPressed = false;
    float initialJumpVelocity;
    float maxJumpHeight = 4f;
    float maxJumpTime = 1f;
    bool isJumping = false;
    int isJumpingHash;
    bool isJumpAnimating = false;


    void Awake() {
        
        playerInput = new PlayerInput();
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();

        isWalkingHash = Animator.StringToHash("isWalking");
        isRunningHash = Animator.StringToHash("isRunning");
        isJumpingHash = Animator.StringToHash("isJumping");

        playerInput.CharacterControls.Move.started += onMovementInput;
        playerInput.CharacterControls.Move.canceled += onMovementInput;
        playerInput.CharacterControls.Move.performed += onMovementInput;
        playerInput.CharacterControls.Run.started += onRun;
        playerInput.CharacterControls.Run.canceled += onRun;
        playerInput.CharacterControls.Jump.started += onJump;
        playerInput.CharacterControls.Jump.canceled += onJump;

        void onMovementInput (InputAction.CallbackContext context) {

            currentMovementInput = context.ReadValue<Vector2>();
            currentMovement.x = currentMovementInput.x;
            currentMovement.z = currentMovementInput.y;
            currentRunMovement.x = currentMovementInput.x * runMultiplier;
            currentRunMovement.z = currentMovementInput.y * runMultiplier;
            isMovementPressed = currentMovementInput.x != 0 || currentMovementInput.y != 0;

        };

        setupJumpVariables();
    }

    void setupJumpVariables()
    {
        float timeToApex = maxJumpTime / 2;
        gravity = (-2 * maxJumpHeight) / Mathf.Pow(timeToApex, 2);
        initialJumpVelocity = (2 * maxJumpHeight) / timeToApex;

    }

    void handleJump()
    {
        if (!isJumping && characterController.isGrounded && isJumpPressed) {
            animator.SetBool(isJumpingHash, true);
            isJumpAnimating = true;
            isJumping = true;
            currentMovement.y = -initialJumpVelocity * .5f;
            currentRunMovement.y = -initialJumpVelocity * .5f;
        } else if (!isJumpPressed && isJumping && characterController.isGrounded) {
            isJumping = false;
        }
    }

    void onJump (InputAction.CallbackContext context)
    {
        isJumpPressed = context.ReadValueAsButton();
        Debug.Log(isJumpPressed);
    }


    void onRun (InputAction.CallbackContext context)
    {
        isRunPressed = context.ReadValueAsButton();
    }

    void handleRotation()
    {
        Vector3 positionToLookAt;

        positionToLookAt.x = -currentMovement.x;
        positionToLookAt.y = 0.0f;
        positionToLookAt.z = -currentMovement.z;

        Quaternion currentRotation = transform.rotation;

        if (isMovementPressed) {
            Quaternion targetRotation = Quaternion.LookRotation(positionToLookAt);
            transform.rotation = Quaternion.Slerp(currentRotation, targetRotation, rotationFactorPerFrame * Time.deltaTime);
        }
    }

    void handleAnimation(){

        bool isWalking = animator.GetBool(isWalkingHash);
        bool isRunning = animator.GetBool(isRunningHash);

        if (isMovementPressed && !isWalking) {
            animator.SetBool(isWalkingHash, true);
        }

        else if (!isMovementPressed && isWalking) {
            animator.SetBool(isWalkingHash, false);
        }

        if ((isMovementPressed && isRunPressed) && !isRunning)
        {
            animator.SetBool(isRunningHash, true);
        }

        else if ((!isMovementPressed && !isRunPressed) && isRunning)
        {
            animator.SetBool(isRunningHash, false);
        }

    }

    void handleGravity(){

        bool isFalling = currentMovement.y >= 0.0f || !isJumpPressed;
        float fallMultiplier = 2.5f;


        if (characterController.isGrounded)
        {
            if (isJumpAnimating) {
                animator.SetBool(isJumpingHash, false);
                isJumpAnimating = false;
            }
            currentMovement.y = groundedGravity;
            currentRunMovement.y = groundedGravity;
        } else if (isFalling) {
            float previousYVelocity = currentMovement.y;
            float newYVelocity = currentMovement.y - (gravity * fallMultiplier * Time.deltaTime);
            float nextYVelocity = Mathf.Max((previousYVelocity + newYVelocity) * .5f, 0.4f);
            currentMovement.y = nextYVelocity;
            currentRunMovement.y = nextYVelocity;
        }
        else
        {
            float previousYVelocity = currentMovement.y;
            float newYVelocity = currentMovement.y - (gravity * Time.deltaTime);
            float nextYVelocity = (previousYVelocity + newYVelocity) * .5f;
            currentMovement.y = nextYVelocity;
            currentRunMovement.y = nextYVelocity;
        }
    }



    // Update is called once per frame
    void Update()
    {
        handleRotation();
        handleAnimation();
        if (isRunPressed) {
            characterController.Move(currentRunMovement * Time.deltaTime * -2f);
        } else {
            characterController.Move(currentMovement * Time.deltaTime * -2f);
        }
        handleGravity();
        handleJump();
    }

    void OnEnable() {
        playerInput.CharacterControls.Enable();
    }

    void OnDisable() {
        playerInput.CharacterControls.Disable();
    }
}
