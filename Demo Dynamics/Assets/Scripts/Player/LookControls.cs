using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class LookControls : MonoBehaviour
{
    [SerializeField]
    float sensitivityX;
    [SerializeField]
    float sensitivityY;

    private Vector2 mouseMovementInput;
    private Vector2 mouseMovement;

    private float xRotation = 0f;
    // Holds a reference to the players Transform component.
    //public Transform playerRef;
    public Transform camRef;

    float mouseX;
    float mouseY;

    void Start()
    {
        // Temporary code to disable the mouse cursor on start
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        mouseX = mouseMovement.x * sensitivityX;
        mouseY = mouseMovement.y * sensitivityY;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        //transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        camRef.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        transform.Rotate(Vector3.up * mouseX);
        
    }

    public void onMouseInput(InputAction.CallbackContext context)
    {
        mouseMovementInput = context.ReadValue<Vector2>();
        mouseMovement.x = mouseMovementInput.x;
        mouseMovement.y = mouseMovementInput.y;

    }
}
