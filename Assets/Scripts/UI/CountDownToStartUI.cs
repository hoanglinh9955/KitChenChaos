using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CountDownToStartUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;

    
    private void Start()
    {
        KitchenGameManager.Instance.OnStateChange += GameManager_OnStateChange;
  
        Hide();

    }

    private void GameManager_OnStateChange(object sender, System.EventArgs e)
    {
      
        if (KitchenGameManager.Instance.IsCountDownStartActive())
        {
            Show();
          
        }
        else
        {
            Hide();
           
        }
    }

    private void Show()
    {
        gameObject.SetActive(true);

    }
    private void Hide()
    {
        gameObject.SetActive(false);
    }
    private void Update()
    {
        text.text = Mathf.Ceil(KitchenGameManager.Instance.GetCountDownTimerStart()).ToString();
    }
}
