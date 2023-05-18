using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using static PlateCompleteVisual;

public class PlateKitchenObject : KitchenObject
{
    public event EventHandler<OnIngredientAddedEventArgs> OnIngredientAdded;
    public class OnIngredientAddedEventArgs
    {
        public KitchenObjectSO kitchenObjectSO;
    }

    private List<KitchenObjectSO> kitchenObjectSOList;
    [SerializeField] private List<KitchenObjectSO> ValidKitchenObjectSO;

    private void Awake()
    {
        kitchenObjectSOList = new List<KitchenObjectSO>();
    }

    public bool TryAddIngredient(KitchenObjectSO kitchenObjectSO)
    {
        if(!ValidKitchenObjectSO.Contains(kitchenObjectSO)) return false;   

        if (kitchenObjectSOList.Contains(kitchenObjectSO))
        {
            return false;
        }
        else
        {
            kitchenObjectSOList.Add(kitchenObjectSO);
            OnIngredientAdded?.Invoke(this, new OnIngredientAddedEventArgs
            {
                kitchenObjectSO = kitchenObjectSO
            });
            return true;
        }
        
    }
    public List<KitchenObjectSO> GetKitchenObjectSOList()
    {
        return kitchenObjectSOList;
    }
}
