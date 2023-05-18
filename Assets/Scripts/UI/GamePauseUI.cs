using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GamePauseUI : MonoBehaviour
{
    [SerializeField] private Button resumebutton;
    [SerializeField] private Button mainMenubutton;

    private void Awake()
    {
        resumebutton.onClick.AddListener(() =>
        {
            KitchenGameManager.Instance.TogglePause();
        });
        mainMenubutton.onClick.AddListener(() =>
        {
            Loader.Load(Loader.Scene.MainMenuScene);
        });
    }
    private void Start()
    {
        Hide();
        KitchenGameManager.Instance.OnPauseEvent += KitchenGameManager_OnPauseEvent;
        KitchenGameManager.Instance.OnResumeEvent += KitchenGameManager_OnResumeEvent;
    }

    private void KitchenGameManager_OnResumeEvent(object sender, System.EventArgs e)
    {
        Hide();
    }

    private void KitchenGameManager_OnPauseEvent(object sender, System.EventArgs e)
    {
        Show();
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
