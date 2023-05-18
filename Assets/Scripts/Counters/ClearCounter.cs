using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ClearCounter : BaseCounter
{
    [SerializeField] private KitchenObjectSO kitchenObjectSO;
    public override void Interact(PlayerController player)
    {
        if (!HasKitchenObject())
        {
            if (player.HasKitchenObject())
            {
                player.GetKitchenObject().SetKitchenParentObject(this);
            }
        }
        else
        //there is a kitchen Object here
        {
            
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
                else
                //player carring something not a plate, carring a kitchenobject
                {
                    if(GetKitchenObject().TryGetPlate(out plateKitchenObject))
                    {
                        //Grab a kitchenobject into plate
                        if (plateKitchenObject.TryAddIngredient(player.GetKitchenObject().GetKitchenObjecSO()))
                        {
                            player.GetKitchenObject().DestroySelf();
                        }
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
}

