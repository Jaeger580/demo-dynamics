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
    private Vector3 moveDirection;


    private Vector2 mouseMovementInput;
    private Vector2 mouseMovement;

    private bool isMovementPressed;
    // bool to check if player is looking around
    //private bool isMoving;

    private float verticalRotation = 0f;
    // Stores the parent of First Person virtual camera
    [SerializeField]
    GameObject primaryTarget;

    [SerializeField]
    float walkSpeed;
    [SerializeField]
    float sprintSpeed;
    [SerializeField]
    float sensitivityX;
    [SerializeField]
    float sensitivityY;


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

        // update look rotation when mouse / analog stick is used
        playerInput.PlayerControls.Look.started += onMouseInput;
        playerInput.PlayerControls.Look.canceled += onMouseInput;
        playerInput.PlayerControls.Look.performed += onMouseInput;


    }

    // Start is called before the first frame update
    void Start()
    {
        // Temporary code to disable the mouse cursor on start
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        handleAnimation();
        handleGravity();

        // Moves the player using the Character controller component
        moveDirection = transform.right * currentMovement.x + transform.forward * currentMovement.z;
        controller.Move(moveDirection * walkSpeed * Time.deltaTime);

        // Rotates the player horizontally to where the player is trying to look
        transform.Rotate(Vector3.up, mouseMovement.x * sensitivityX * Time.deltaTime);

        // Clamps and handles the vertical rotation of the camera
        verticalRotation -= mouseMovement.y * sensitivityY * Time.deltaTime;
        verticalRotation = Mathf.Clamp(verticalRotation, -60, 60);
        Vector3 targetRotation = transform.eulerAngles;
        targetRotation.x = verticalRotation;
        primaryTarget.transform.eulerAngles = targetRotation;
    }

    void toggleCamera() 
    {

    }

    void onMouseInput(InputAction.CallbackContext context)
    {
        mouseMovementInput = context.ReadValue<Vector2>();
        mouseMovement.x = mouseMovementInput.x;
        mouseMovement.y = mouseMovementInput.y;
        //isMoving = mouseMovementInput.x != 0 || mouseMovementInput.y != 0;
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