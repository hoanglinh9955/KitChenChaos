using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DeliveryRecipeUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textMeshProUGUI;
    [SerializeField] private TextMeshProUGUI textMeshProUGUINumber;

    private void Start()
    {
        KitchenGameManager.Instance.OnStateChange += GameManager_OnStateChange;

        Hide(textMeshProUGUI);
        Hide(textMeshProUGUINumber);

    }

    private void GameManager_OnStateChange(object sender, System.EventArgs e)
    {

        if (KitchenGameManager.Instance.IsGameOver())
        {
            Show(textMeshProUGUI);
            Show(textMeshProUGUINumber);

        }
        else
        {
            Hide(textMeshProUGUI);
            Hide(textMeshProUGUINumber);

        }
    }

    private void Show(TextMeshProUGUI text)
    {
        text.gameObject.SetActive(true);

    }
    private void Hide(TextMeshProUGUI text)
    {
        text.gameObject.SetActive(false);
    }
    private void Update()
    {
        textMeshProUGUINumber.text = DeliveryManager.Instance.GetDeliverySuccess().ToString();
    }
}
