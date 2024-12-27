using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private PlayerInputActionsAsset actionsAsset;

    private Movement movementScript;

    private InputAction accelerate;
    private InputAction Roll;
    private InputAction Pitch;


    private void Awake()
    {
        actionsAsset = new();

        movementScript = GetComponent<Movement>();
    }

    private void OnEnable()
    {
        accelerate = actionsAsset.Player.Acceleration;
        Roll = actionsAsset.Player.Roll;
        Pitch = actionsAsset.Player.Pitch;

        accelerate.Enable();
        accelerate.performed += OnAcceleration;

        Roll.Enable();
        Roll.performed += OnRoll;

        Pitch.Enable();
        Pitch.performed += OnPitch;
    }

    private void OnDisable()
    {
        accelerate.Disable();

        Roll.Disable();

        Pitch.Disable();
    }

    private void OnAcceleration(InputAction.CallbackContext context)
    {
        Debug.Log("Forward");
        movementScript.GetAcceleration(context.ReadValue<float>());
    }

    private void OnRoll(InputAction.CallbackContext context)
    {
        if (context.ReadValue<float>() > 0.1f)
        {
            Debug.Log("Right");
            movementScript.GetRoll(context.ReadValue<float>());
        }
        else if (context.ReadValue<float>() < -0.1f)
        {
            Debug.Log("Left");
            movementScript.GetRoll(context.ReadValue<float>());
        }

    }

    private void OnPitch(InputAction.CallbackContext context)
    {
        if (context.ReadValue<float>() > 0.1f)
        {
            Debug.Log("Up");
            movementScript.GetPitch(context.ReadValue<float>());
        }
        else if (context.ReadValue<float>() < -0.1f)
        {
            Debug.Log("Down");
            movementScript.GetPitch(context.ReadValue<float>());
        }

    }
}
