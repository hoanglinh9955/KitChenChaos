using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitchenObject : MonoBehaviour
{
    [SerializeField] private KitchenObjectSO kitchenObjectSO;

    private IKitchenParentObject kitchenParentObject;
    public KitchenObjectSO GetKitchenObjecSO()
    {
        return kitchenObjectSO;
    }

    public IKitchenParentObject GetKitchenParentObject()
    {
        return kitchenParentObject;
    }
    public void SetKitchenParentObject(IKitchenParentObject kitchenParentObject)
    {
        if (kitchenParentObject.HasKitchenObject())
        {
            Debug.LogError("Clear Counter Has KitChen Object !!!");
        }

        if (this.kitchenParentObject != null)
        {
            this.kitchenParentObject.ClearKitchenObject();
        }
       
        this.kitchenParentObject = kitchenParentObject;
        
        kitchenParentObject.SetKitchenObject(this);

        transform.parent = kitchenParentObject.GetKitchenObjectFollowTranform();
 
        transform.localPosition = Vector3.zero;
    }
    public void DestroySelf()
    {
        GetKitchenParentObject().ClearKitchenObject();
        Destroy(gameObject);
    }
    public bool TryGetPlate(out PlateKitchenObject plateKitchenObject)
    {
        if (this is PlateKitchenObject)
        {
            plateKitchenObject = this as PlateKitchenObject;
            return true;
        }
        else
        {
            plateKitchenObject= null;
            return false;
        }
    }


    public static KitchenObject SpawnKitchenObject(KitchenObjectSO kitchenObject, IKitchenParentObject kitchenParentObject)
    {
        Transform kitchenTransform = Instantiate(kitchenObject.prefabs);
        KitchenObject kitchenOB = kitchenTransform.GetComponent<KitchenObject>();
        kitchenOB.SetKitchenParentObject(kitchenParentObject);
        return kitchenOB;
    }
}
