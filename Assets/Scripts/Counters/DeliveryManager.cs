using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DeliveryManager : MonoBehaviour
{

    public static DeliveryManager Instance { get; private set; }

    [SerializeField] private List<RecipeListSO> recipeListSO;

    private List<RecipeSO> waitingRecipeSOList;
    private float spawnTimerMax = 4f;
    private float spawnTimer;
    private int wattingRecipeMax = 4;
    private int deliverySuccess = 0;

    public event EventHandler OnRecipeSpawn;
    public event EventHandler OnRecipeComplete;
    public event EventHandler OnDeliverySuccess;
    public event EventHandler OnDeliveryFalse;
    private void Awake()
    {   
        Instance = this;
        waitingRecipeSOList = new List<RecipeSO>();
    }

    private void Update()
    {
        spawnTimer += Time.deltaTime;
        if(spawnTimer >= spawnTimerMax) {
            spawnTimer = 0f; ;

            if(waitingRecipeSOList.Count < wattingRecipeMax)
            {
                RecipeSO recipeSO = recipeListSO[0].recipeSOList[UnityEngine.Random.Range(0, recipeListSO[0].recipeSOList.Count)];

                Debug.Log(recipeSO);
                waitingRecipeSOList.Add(recipeSO);

                OnRecipeSpawn?.Invoke(this, EventArgs.Empty);

            }
        }
    }

    
    public void DeliverRecipe(PlateKitchenObject plateKitchenObject)
    {
        for( int i = 0; i < waitingRecipeSOList.Count; i++ )
        {
            RecipeSO waitRecipeSO = waitingRecipeSOList[i];

            if(waitRecipeSO.kitchenObjectSOList.Count == plateKitchenObject.GetKitchenObjectSOList().Count )
            {
                bool ingredientMactchPlate = true;
                foreach (KitchenObjectSO waitKitchenObjectSO in waitRecipeSO.kitchenObjectSOList)
                //Check kitchenObject in a Wait List
                //cycling all ingredent in waitRecipe
                {
             
                    bool ingredentFound = false;
                    foreach (KitchenObjectSO plateKitchenObjectSO in plateKitchenObject.GetKitchenObjectSOList())
                    {
                        if (plateKitchenObjectSO == waitKitchenObjectSO)
                        {
                          
                            ingredentFound = true;
                            break;
                        }
                    }
                    if(!ingredentFound)
                    {
                        //this wait Recipe not found on plate
                        ingredientMactchPlate = false;
                    }
                }
                if (ingredientMactchPlate)
                {
                    //delevery right recipe
                    Debug.Log("Player deliver correct Recipe !!!");
                    waitingRecipeSOList.RemoveAt(i);
                    deliverySuccess++;

                    OnRecipeComplete?.Invoke(this, EventArgs.Empty);
                    OnDeliverySuccess?.Invoke(this, EventArgs.Empty);
                    return;
                }
            }
        }
        // no match found in plate and wait list
        Debug.Log("No Match found in plate and wait list");
        OnDeliveryFalse?.Invoke(this, EventArgs.Empty);
    }

    public List<RecipeSO> GetWaitingListRecipeSO()
    {
        return waitingRecipeSOList;
    }
    public int GetDeliverySuccess() { return deliverySuccess; }
}
