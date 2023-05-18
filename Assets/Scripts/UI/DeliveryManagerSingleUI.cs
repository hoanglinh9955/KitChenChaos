using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DeliveryManagerSingleUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI recipeNameText;
    [SerializeField] private Transform iconContainer;
    [SerializeField] private Transform iconTemplate;


    private void Awake()
    {
        iconTemplate.gameObject.SetActive(false);
    }
    public void SetRecipeName(RecipeSO recipeSO)
    {
        recipeNameText.text = recipeSO.recipeName;

        foreach(Transform child in iconContainer)
        {
            if (child == iconTemplate) continue;
            Destroy(child.gameObject);
        }

        foreach(KitchenObjectSO kitchenObjectSO in recipeSO.kitchenObjectSOList)
        {
            Transform transformIconTemplate = Instantiate(iconTemplate, iconContainer);
            transformIconTemplate.gameObject.SetActive(true);
            transformIconTemplate.GetComponent<Image>().sprite = kitchenObjectSO.sprite;
        }
    }
}
