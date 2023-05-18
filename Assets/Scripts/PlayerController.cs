using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour,IKitchenParentObject
{
    public static PlayerController Instance { get; private set; }

 
    public event EventHandler OnPlayerPickUp;
    public event EventHandler<OnSelectedCounterChangedEventArgs> OnSelectedCounterChanged;
    public class OnSelectedCounterChangedEventArgs: EventArgs
    {
        public BaseCounter selectedCounter;
    }
    
    [SerializeField] private float Speed;
    [SerializeField] public GameInput gameInput;
    [SerializeField] private LayerMask counterLayMask;
    [SerializeField] private Transform KitchenObjectHoldPoint;

    private bool isWalking;
    private Vector3 lastInteraction;
    private BaseCounter selectedCounter;
    private KitchenObject kitchenObject;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("there are more than 1 player");
        }
        Instance = this;
    }
    private void Start()
    {
        gameInput.OnInteractEvent += GameInput_OnInteractEvent;
        gameInput.OnInteractAlternateEvent += GameInput_OnInteractAlternateEvent;
    }

    private void GameInput_OnInteractAlternateEvent(object sender, EventArgs e)
    {
        if (!KitchenGameManager.Instance.IsGamePlaying()) return;
        
        if(selectedCounter != null)
        {
            selectedCounter.InteractAlternate(this);

        }


    }

    private void GameInput_OnInteractEvent(object sender, System.EventArgs e)
    {
        if (!KitchenGameManager.Instance.IsGamePlaying()) return;

        if(selectedCounter != null) 
        {
            selectedCounter.Interact(this);
        }
    }
    

    // Update is called once per frame
    private void Update()
    {
        HandleMovements();
        HandlerInteractions();
    }
    public bool IsWalking()
    {
        return isWalking;
    }

    private void HandleMovements()
    {
        Vector2 inputVector = gameInput.GetMovementVector2Normalized();

        Vector3 moveDir = new Vector3(inputVector.x, 0f, inputVector.y);
        float playerSize = .7f;
        float playerRadius = .5f;
        float playerHeight = 1;

        bool canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDir, playerSize);
        if (!canMove)
        {
            Vector3 moveDirX = new Vector3(moveDir.x, 0f, 0f);
            canMove = moveDir.x != 0 && !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirX, playerSize);

            if (canMove)
            {
                moveDir = moveDirX;
            }
            else
            {
                Vector3 moveDirZ = new Vector3(0f, 0f, moveDir.z);
                canMove = moveDir.z != 0 && !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirZ, playerSize);
                if (canMove)
                {
                    moveDir = moveDirZ;
                }
            }
        }
        if (canMove)
        {
            transform.position += moveDir * Speed * Time.deltaTime;
        }

        isWalking = moveDir != Vector3.zero;

        float speedRotation = 15f;
        transform.forward = Vector3.Slerp(transform.forward, moveDir, speedRotation * Time.deltaTime);
    }

    private void HandlerInteractions()
    {
        Vector2 inputVector = gameInput.GetMovementVector2Normalized();

        Vector3 moveDir = new Vector3(inputVector.x, 0f, inputVector.y);
        if (moveDir != Vector3.zero)
        {
            lastInteraction = moveDir;
        }
        float interactDistance = 2f;
        if (Physics.Raycast(transform.position, lastInteraction, out RaycastHit raycastHit, interactDistance, counterLayMask))
        {
            if (raycastHit.transform.TryGetComponent(out BaseCounter baseCounter))
            {
                if (baseCounter != selectedCounter)
                {
                    SetSelectedCounter(baseCounter);
                }
            }
            else
            {
                SetSelectedCounter(null);

            }
        }
        else
        {
            SetSelectedCounter(null);
        }
    }
    private void SetSelectedCounter(BaseCounter selectedCounter)
    {
        this.selectedCounter = selectedCounter;

        OnSelectedCounterChanged?.Invoke(this, new OnSelectedCounterChangedEventArgs
        {
            selectedCounter = selectedCounter
        }) ;
    }

    public Transform GetKitchenObjectFollowTranform()
    {
        return KitchenObjectHoldPoint;
    }
    public KitchenObject GetKitchenObject()
    {
        return kitchenObject;
    }
    public void SetKitchenObject(KitchenObject kitchenObject)
    {
        this.kitchenObject = kitchenObject;

        if(kitchenObject != null)
        {
            OnPlayerPickUp?.Invoke(this, EventArgs.Empty);
        }
    }
    public void ClearKitchenObject()
    {
        kitchenObject = null;
    }
    public bool HasKitchenObject()
    {
        return kitchenObject != null;
    }

}

