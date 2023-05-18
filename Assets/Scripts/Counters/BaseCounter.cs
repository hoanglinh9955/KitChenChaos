using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseCounter : MonoBehaviour, IKitchenParentObject
{

    public static event EventHandler OnAnyObjectPlaceHere;

    [SerializeField] private Transform topCounter;

    public static void ResetStaticData()
    {
         OnAnyObjectPlaceHere = null;
    }

    private KitchenObject kitchenObject;

    public virtual void Interact(PlayerController player)
    {
        Debug.Log("Interact.basecounter ??");
    }
    public virtual void InteractAlternate(PlayerController player) {
        Debug.Log("Interactaternate.basecounter ???");
    }
    public Transform GetKitchenObjectFollowTranform()
    {
        return topCounter;
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
            OnAnyObjectPlaceHere?.Invoke(this, EventArgs.Empty);
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
