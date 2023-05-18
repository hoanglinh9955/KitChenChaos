using Microsoft.Unity.VisualStudio.Editor;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlateIconUI : MonoBehaviour
{
    [SerializeField] private PlateKitchenObject plateKitchenObject;
    [SerializeField] private Transform iConTemplate;

    private void Start()
    {
        plateKitchenObject.OnIngredientAdded += PlateKitchenObject_OnIngredientAdded;
    }
    private void Awake()
    {
        iConTemplate.gameObject.SetActive(false);
    }
    private void PlateKitchenObject_OnIngredientAdded(object sender, PlateKitchenObject.OnIngredientAddedEventArgs e)
    {
        UpdateVisual();
    }
    private void UpdateVisual()
    {
        foreach(Transform child in transform)
        {
            if (child == iConTemplate) continue;
            Destroy(child.gameObject);
        }
        foreach (KitchenObjectSO kitchenObjectSO in plateKitchenObject.GetKitchenObjectSOList())
        {
            Transform iconTranform = Instantiate(iConTemplate, transform);
            iconTranform.gameObject.SetActive(true);
            iconTranform.GetComponent<SingleIconUi>().SetKitchenObjectSO(kitchenObjectSO);
        }
    }
}
