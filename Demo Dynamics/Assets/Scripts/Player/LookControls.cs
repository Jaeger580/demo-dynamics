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
    
    public Transform camRef;
    //public Transform playerHead;

    // target where that player's head will look at
    public GameObject lookTarget;

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
        xRotation = Mathf.Clamp(xRotation, -90f, 75f);
        camRef.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        transform.Rotate(Vector3.up * mouseX);

        Vector3 camPos = Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 5f)); // Gets the world cordinates for the position at the center of the game a specified distance from the camera.
        lookTarget.transform.position = camPos; // Positions the look target at the center of the player's view.
    }

    void FixedUpdate()
    {
    }

    public void onMouseInput(InputAction.CallbackContext context)
    {
        mouseMovementInput = context.ReadValue<Vector2>();
        mouseMovement.x = mouseMovementInput.x;
        mouseMovement.y = mouseMovementInput.y;

    }
}
