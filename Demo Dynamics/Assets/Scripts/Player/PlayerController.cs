using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;

// This script is for handling the controls and animations of the player character

public class PlayerController : MonoBehaviour
{
    private PlayerInput playerInput;
    private PlayerMovement playerMovement;
    private LookControls lookControls;

    void Awake()
    {
        playerMovement = GetComponent<PlayerMovement>();
        lookControls = GetComponent<LookControls>();
        playerInput = new PlayerInput();

        // Update movement whenever a movement button is pressed or released
        playerInput.PlayerControls.Move.started += playerMovement.onMovementInput;
        playerInput.PlayerControls.Move.canceled += playerMovement.onMovementInput;
        // Update movement for gamepads with analoge sticks
        playerInput.PlayerControls.Move.performed += playerMovement.onMovementInput;

        // update look rotation when mouse / analog stick is used
        playerInput.PlayerControls.Look.started += lookControls.onMouseInput;
        playerInput.PlayerControls.Look.canceled += lookControls.onMouseInput;
        playerInput.PlayerControls.Look.performed += lookControls.onMouseInput;

        // Tell the character to jump when player hits the jump button. Underscore used since no context is required
        // is this really the best formatting?????
        playerInput.PlayerControls.Jump.performed += _ => playerMovement.onJump();

    }

    // Update is called once per frame
    void Update()
    {
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