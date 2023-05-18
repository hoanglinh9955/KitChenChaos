using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    [SerializeField] private GameObject hasProgressGameObject;
    [SerializeField] private Image Bar;

    private IHasProgressBar progressBar;
    private void Start()
    {
        progressBar = hasProgressGameObject.GetComponent<IHasProgressBar>();
        if(progressBar != null )
        {
            Debug.LogError("Game Object " + hasProgressGameObject + "didnt implement IHasProgressBar");
        }
        
        progressBar.OnProgressChanged += ProgressBar_OnProgressChanged;
        Bar.fillAmount = 0f;
        Hide();
    }

    private void ProgressBar_OnProgressChanged(object sender, IHasProgressBar.OnProgressChangedEventArgs e)
    {
        Bar.fillAmount = e.progressNormolize;
        if(e.progressNormolize ==  0f || e.progressNormolize == 1f)
        {
            Hide();
        }
        else
        {
            Show();
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
}
