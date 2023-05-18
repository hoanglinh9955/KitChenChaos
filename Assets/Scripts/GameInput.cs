using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;

public class GameInput : MonoBehaviour
{
    public static GameInput instance { get; private set; }

    private PlayerInputAction playerInputAction;

    public event EventHandler OnInteractEvent;
    public event EventHandler OnInteractAlternateEvent;
    public event EventHandler OnEscapeEvent;

    // Start is called before the first frame update
    private void Awake()
    {
        instance = this;
        playerInputAction = new PlayerInputAction();
        playerInputAction.Player.Enable();
        playerInputAction.Player.InteractAlternate.performed += InteractAlternate_performed;
        playerInputAction.Player.Interact.performed += Interact_performed;
        playerInputAction.Player.Pause.performed += Pause_performed;
    }

    private void OnDestroy()
    {
        playerInputAction.Player.InteractAlternate.performed -= InteractAlternate_performed;
        playerInputAction.Player.Interact.performed -= Interact_performed;
        playerInputAction.Player.Pause.performed -= Pause_performed;

        playerInputAction.Dispose();
    }

    private void Pause_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnEscapeEvent?.Invoke(this, EventArgs.Empty);
    }

    private void InteractAlternate_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnInteractAlternateEvent?.Invoke(this, EventArgs.Empty);
    }

    private void Interact_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnInteractEvent?.Invoke(this, EventArgs.Empty);
    }

    public Vector2 GetMovementVector2Normalized()
    {
        Vector2 inputVector = playerInputAction.Player.Move.ReadValue<Vector2>();
        return inputVector.normalized;
    }
}
