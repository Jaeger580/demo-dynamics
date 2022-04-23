using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;

// This script is for handling the controls and animations of the player character

public class PlayerController : MonoBehaviour
{
    private CharacterController controller;
    private PlayerInput playerInput;
    private Animator animator;

    private Vector2 currentMovementInput;
    private Vector3 currentMovement;
    private bool isMovementPressed;

    [SerializeField]
    float walkSpeed;

    [SerializeField]
    float sprintSpeed;

    public 

    void Awake()
    {
        playerInput = new PlayerInput();
        controller = gameObject.GetComponent<CharacterController>();
        animator = gameObject.GetComponent<Animator>();

        // Update movement whenever a movement button is pressed or released
        playerInput.PlayerControls.Move.started += onMovementInput;
        playerInput.PlayerControls.Move.canceled += onMovementInput;
        // Update movement for gamepads with analoge sticks
        playerInput.PlayerControls.Move.performed += onMovementInput;

    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        handleAnimation();

        // Moves the player using the Character controller component
        handleGravity();
        controller.Move(currentMovement * walkSpeed * Time.deltaTime);
    }

    void toggleCamera() 
    {

    }

    void onMovementInput(InputAction.CallbackContext context) 
    {
        currentMovementInput = context.ReadValue<Vector2>();
        currentMovement.x = currentMovementInput.x;
        currentMovement.z = currentMovementInput.y;
        isMovementPressed = currentMovementInput.x != 0 || currentMovementInput.y != 0;
    }

    // Method that adds gravity to the player since there is no rigidbody
    void handleGravity() 
    {
        if (controller.isGrounded)
        {
            float groundGravity = -0.01f;
            currentMovement.y = groundGravity;
        }
        else 
        {
            float gravity = -9.81f;
            currentMovement.y = gravity;
        }
    }

    // Method that tells the animator when to run various animations
    void handleAnimation() 
    {
        bool isWalking = animator.GetBool("isWalking");

        if(isMovementPressed && !isWalking) 
        {
            animator.SetBool("isWalking", true);
        }
        else if (!isMovementPressed && isWalking)
        {
            animator.SetBool("isWalking", false);
        }
    }

    void OnEnable()
    {
        playerInput.PlayerControls.Enable();
    }

    void OnDisable()
    {
        playerInput.PlayerControls.Disable();
    }
}