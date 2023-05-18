using System;
using UnityEngine;

public class CuttingCounter : BaseCounter, IHasProgressBar
{
    [SerializeField] private CuttingRecipeSO[] cuttingRecipeSOArrays;
    private int cuttingRecipeCount;
    private float cuttingProgressMax = 5;

    public event EventHandler OnCut;
    public static event EventHandler OnAnyCut;
    public event EventHandler<IHasProgressBar.OnProgressChangedEventArgs> OnProgressChanged;
    

    public static void ResetStaticData()
    {
        OnAnyCut = null;
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
                    cuttingRecipeCount = 0;
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
                    }

                }
            }
            else
            {
                // Player didnt carring any thing we can set kitchenObject for player
                GetKitchenObject().SetKitchenParentObject(player);
            }
        }
    }
    public override void InteractAlternate(PlayerController player)
    {
        if (HasKitchenObject() && HasRepcipeWithInput(GetKitchenObject().GetKitchenObjecSO()))
        {
            
            KitchenObjectSO kitchenObjectSO = GetOutputForInput(GetKitchenObject().GetKitchenObjecSO());
            Debug.Log(kitchenObjectSO.ToString());
            cuttingRecipeCount++;
            OnCut?.Invoke(this, EventArgs.Empty);
            OnAnyCut?.Invoke(this, EventArgs.Empty);
            OnProgressChanged?.Invoke(this, new IHasProgressBar.OnProgressChangedEventArgs
            {
                progressNormolize = (float)cuttingRecipeCount / cuttingProgressMax

            });
            if(cuttingRecipeCount >= 5)
            {
                GetKitchenObject().DestroySelf();
                KitchenObject.SpawnKitchenObject(kitchenObjectSO, this);
           
                OnProgressChanged?.Invoke(this, new IHasProgressBar.OnProgressChangedEventArgs
                {
                    progressNormolize = 0f

                });
            }
            Debug.Log(cuttingRecipeCount.ToString());


        }
    }
    private KitchenObjectSO GetOutputForInput(KitchenObjectSO inputKitchenObjectSO)
    {
        foreach(CuttingRecipeSO cuttingRecipeSO in cuttingRecipeSOArrays)
        {
            if(inputKitchenObjectSO == cuttingRecipeSO.input)
            {
                return cuttingRecipeSO.output;
            }
        }
        return null;
    }
    private bool HasRepcipeWithInput(KitchenObjectSO inputKitchenObjectSO)
    {
        foreach (CuttingRecipeSO cuttingRecipeSO in cuttingRecipeSOArrays)
        {
            if (inputKitchenObjectSO == cuttingRecipeSO.input)
            {
                return true;
            }
        }
        return false;
    }
}
