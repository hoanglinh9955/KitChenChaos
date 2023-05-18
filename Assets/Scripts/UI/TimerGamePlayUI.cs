
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerGamePlayUI : MonoBehaviour
{
    [SerializeField] private Image image;
    private void Start()
    {
      

    }

    private void GameManager_OnStateChange(object sender, System.EventArgs e)
    {

        if (KitchenGameManager.Instance.IsGamePlay())
        {
            Show();

        }
        else
        {
          

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
        image.fillAmount = KitchenGameManager.Instance.GetTimerGamePlay();
    }
}
