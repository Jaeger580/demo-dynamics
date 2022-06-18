using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class LookControls : MonoBehaviour
{
    // Stores the parent of First Person virtual camera
    [SerializeField]
    GameObject primaryTarget;
    [SerializeField]
    float sensitivityX;
    [SerializeField]
    float sensitivityY;

    private float verticalRotation = 0f;

    private Vector2 mouseMovementInput;
    private Vector2 mouseMovement;

    void Start()
    {
        // Temporary code to disable the mouse cursor on start
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        // Rotates the player horizontally to where the player is trying to look
        transform.Rotate(Vector3.up, mouseMovement.x * sensitivityX * Time.deltaTime);

        // Clamps and handles the vertical rotation of the camera
        verticalRotation -= mouseMovement.y * sensitivityY * Time.deltaTime;
        verticalRotation = Mathf.Clamp(verticalRotation, -60, 60);
        Vector3 targetRotation = transform.eulerAngles;
        targetRotation.x = verticalRotation;
        primaryTarget.transform.eulerAngles = targetRotation;
    }

    public void onMouseInput(InputAction.CallbackContext context)
    {
        mouseMovementInput = context.ReadValue<Vector2>();
        mouseMovement.x = mouseMovementInput.x;
        mouseMovement.y = mouseMovementInput.y;
        //isMoving = mouseMovementInput.x != 0 || mouseMovementInput.y != 0;
    }
}
