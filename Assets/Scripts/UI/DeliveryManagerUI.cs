using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryManagerUI : MonoBehaviour
{
    [SerializeField] private Transform container;
    [SerializeField] private Transform recipeTemplete;

    private void Awake()
    {
        recipeTemplete.gameObject.SetActive(false);
    }
    private void Start()
    {
        DeliveryManager.Instance.OnRecipeSpawn += Instance_OnRecipeSpawn;
        DeliveryManager.Instance.OnRecipeComplete += Instance_OnRecipeComplete;
        UpdateVisual();
    }

    private void Instance_OnRecipeComplete(object sender, System.EventArgs e)
    {
        UpdateVisual();
    }

    private void Instance_OnRecipeSpawn(object sender, System.EventArgs e)
    {
        UpdateVisual();
    }

    private void UpdateVisual()
    {
        foreach (Transform child in container)
        {
            if (child == recipeTemplete) continue;
            Destroy(child.gameObject);
        }
        foreach (RecipeSO recipeSO in DeliveryManager.Instance.GetWaitingListRecipeSO())
        {
            Transform recipeTransform = Instantiate(recipeTemplete, container);
            recipeTransform.gameObject.SetActive(true);

            recipeTransform.GetComponent<DeliveryManagerSingleUI>().SetRecipeName(recipeSO);
        }
    }


}
