using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class StoveCounter : BaseCounter, IHasProgressBar
{
    public event EventHandler<OnStateChangeEventArgs> OnStateChange;
    public event EventHandler<IHasProgressBar.OnProgressChangedEventArgs> OnProgressChanged;
    public class OnStateChangeEventArgs
    {
        public State state;
    }

    [SerializeField] private FryingRecipeSO[] fryingRecipeSOArray;
    [SerializeField] private BurnRecipeSO[] burnRecipeSOArray;
    private FryingRecipeSO fryingSO;
    private BurnRecipeSO burnSO;
    private float fryingTime;
    private float burnTime;
    private State state;

    public enum State
    {
        Idle,
        Frying,
        Fried,
        Burn
    }
    private void Start()
    {
        state = State.Idle;
      
    }

    private void Update()
    {
        switch (state)
        {

            case State.Idle:
                break;
            case State.Frying:
                if (HasKitchenObject())
                {
                    fryingTime += Time.deltaTime;
                    OnProgressChanged?.Invoke(this, new IHasProgressBar.OnProgressChangedEventArgs
                    {
                        progressNormolize = (float)fryingTime / (float)fryingSO.timeFrying
                    });
                    if (fryingTime > fryingSO.timeFrying)
                    {
                        //fried
                        
                        fryingTime = 0f;
                   
                        GetKitchenObject().DestroySelf();
                        KitchenObject.SpawnKitchenObject(fryingSO.output, this);

                        //set burn state and get burnSO to use in burn fried state
                        state = State.Fried;
                
                        OnStateChange?.Invoke(this, new OnStateChangeEventArgs {
                            state = state 
                        });
                        burnTime = 0f;
                        burnSO = GetBurnRecipeSOWithInput(GetKitchenObject().GetKitchenObjecSO());

                    }
                 
                }
                break;
            case State.Fried:
                if (HasKitchenObject())
                {
                    burnTime += Time.deltaTime;
                    OnProgressChanged?.Invoke(this, new IHasProgressBar.OnProgressChangedEventArgs
                    {
                        progressNormolize = (float)burnTime / (float)burnSO.timeBurn
                    });
                    if (burnTime > burnSO.timeBurn)
                    {
                        // Burn
                        
                       
                        GetKitchenObject().DestroySelf();
                        KitchenObject.SpawnKitchenObject(burnSO.output, this);

                        //set Burn state
                        state = State.Burn;
                        Debug.Log(state);
                        OnStateChange?.Invoke(this, new OnStateChangeEventArgs { 
                            state = state 
                        });
                        OnProgressChanged?.Invoke(this, new IHasProgressBar.OnProgressChangedEventArgs
                        {
                            progressNormolize = 0f
                        });
                    }
                    
               
                }
                break;
            case State.Burn: 
                break;
        }
        
    }
    public override void Interact(PlayerController player)
    {

        if (!HasKitchenObject())
        {
            //check if player carring something
            if (player.HasKitchenObject())
            {
                if (HasRepcipeWithInput(player.GetKitchenObject().GetKitchenObjecSO()))
                {
                    player.GetKitchenObject().SetKitchenParentObject(this);
                    fryingSO = GetFryingRecipeSOWithInput(GetKitchenObject().GetKitchenObjecSO());
                    state = State.Frying;
                    OnStateChange?.Invoke(this, new OnStateChangeEventArgs
                    {
                        state = state
                    });
                    OnProgressChanged?.Invoke(this, new IHasProgressBar.OnProgressChangedEventArgs
                    {
                        progressNormolize = fryingTime / fryingSO.timeFrying
                    });
                }
            }
        }
        else
        {
            // counter has kitchenObject and player carring something
            if (player.HasKitchenObject())
            {
                // counter has kitchenObject and player carring something
                if (player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject))
                {
                    //player hold a plate
                    if (plateKitchenObject.TryAddIngredient(GetKitchenObject().GetKitchenObjecSO()))
                    {
                        GetKitchenObject().DestroySelf();
                        state = State.Idle;

                        OnStateChange?.Invoke(this, new OnStateChangeEventArgs
                        {
                            state = state
                        });
                        OnProgressChanged?.Invoke(this, new IHasProgressBar.OnProgressChangedEventArgs { progressNormolize = 0f });

                    }

                }
            }
            else
            {
                // Player didnt carring any thing we can set kitchenObject for player
                GetKitchenObject().SetKitchenParentObject(player);
                state = State.Idle;

                OnStateChange?.Invoke(this, new OnStateChangeEventArgs
                {
                    state = state
                });
                OnProgressChanged?.Invoke(this, new IHasProgressBar.OnProgressChangedEventArgs { progressNormolize = 0f });

            }
        }
    }
    private KitchenObjectSO GetOutputForInput(KitchenObjectSO inputKitchenObjectSO)
    {
        FryingRecipeSO fryingRecipeSO = GetFryingRecipeSOWithInput(inputKitchenObjectSO);
        if(fryingRecipeSO != null)
        {
            return fryingRecipeSO.output;
        }
        else
        {
            return null;
        }
        
    }
    private FryingRecipeSO GetFryingRecipeSOWithInput(KitchenObjectSO inputKitchenObjectSO)
    {
        foreach(FryingRecipeSO fryingRecipeSO in fryingRecipeSOArray)
        {
            if(fryingRecipeSO.input == inputKitchenObjectSO)
            {
                return fryingRecipeSO;
            }
        }
        return null;
    }
    private BurnRecipeSO GetBurnRecipeSOWithInput(KitchenObjectSO inputKitchenObjectSO)
    {
        foreach (BurnRecipeSO burnRecipeSO in burnRecipeSOArray)
        {
            if (burnRecipeSO.input == inputKitchenObjectSO)
            {
                return burnRecipeSO;
            }
        }
        return null;
    }
    private bool HasRepcipeWithInput(KitchenObjectSO kitchenObjectSO)
    {
        FryingRecipeSO fryingRecipeSO = GetFryingRecipeSOWithInput(kitchenObjectSO);
        return fryingRecipeSO != null;
    }
}
