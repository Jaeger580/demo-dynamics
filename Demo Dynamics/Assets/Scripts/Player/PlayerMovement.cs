using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerMovement : MonoBehaviour
{
    private CharacterController controller;
    private Animator animator;

    private Vector2 currentMovementInput;
    private Vector3 currentMovement;
    private Vector3 moveDirection;
    private Vector3 playerVelocity;

    // check if player is moving so we can animate it
    private bool isMovementPressed;
    private bool jump = false;
    private bool playerGrounded;

    [SerializeField]
    float walkSpeed;
    [SerializeField]
    float sprintSpeed;
    [SerializeField]
    float jumpHeight;
    
    // Custome gravity if we choose not to use Physics.gravity
    //[SerializeField]
    //float gravity = -9.81f;

    private float jumpTimer;

    void Awake()
    {
        controller = gameObject.GetComponent<CharacterController>();
        animator = gameObject.GetComponent<Animator>();
    }    

    // Update is called once per frame
    void Update()
    {
        playerGrounded = controller.isGrounded;

        if(playerGrounded && playerVelocity.y < 0) 
        {
            playerVelocity.y = 0f;
        }

        moveDirection = Vector3.zero;

        moveDirection += currentMovement.z * transform.forward;
        moveDirection += currentMovement.x * transform.right;

        handleAnimation();

        // Moves the player using the Character controller component
        controller.Move(moveDirection * Time.deltaTime * walkSpeed);

        // Changes the height position of the player..
        if (jump && playerGrounded)
        {
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * Physics.gravity.y);
            jump = false;
        }

        playerVelocity.y += Physics.gravity.y * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);
    }

    // Add upward velocity to player to make them jump
    public void onJump() 
    {
        //Debug.Log("Jump Called");

        if (playerGrounded)
            jump = true;
        else
            Debug.Log("Not Grounded");
    }

    public void onMovementInput(InputAction.CallbackContext context) 
    {
        currentMovementInput = context.ReadValue<Vector2>();
        currentMovement.x = currentMovementInput.x;
        currentMovement.z = currentMovementInput.y;
        isMovementPressed = currentMovementInput.x != 0 || currentMovementInput.y != 0;
    }

    // Method that tells the animator when to run various animations
    void handleAnimation() 
    {
        // Convert movement directions related to rotations into float values
        float velocityZ = Vector3.Dot(moveDirection, transform.forward);
        float velocityX = Vector3.Dot(moveDirection, transform.right);

        animator.SetFloat("VelocityZ", velocityZ);
        animator.SetFloat("VelocityX", velocityX);

        animator.SetBool("isGrounded", playerGrounded);

    }
}
