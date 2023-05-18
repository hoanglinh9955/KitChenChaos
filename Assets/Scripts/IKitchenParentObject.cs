using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public interface IKitchenParentObject
{
    public Transform GetKitchenObjectFollowTranform();
    public KitchenObject GetKitchenObject();
    public void SetKitchenObject(KitchenObject kitchenObject);
    public void ClearKitchenObject();
    public bool HasKitchenObject();
   
}
