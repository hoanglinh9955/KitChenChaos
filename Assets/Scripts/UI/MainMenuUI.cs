using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{
    [SerializeField] private Button playButton;
    [SerializeField] private Button QuitButton;

    private void Awake()
    {
        playButton.onClick.AddListener(() =>
        {
            Loader.Load(Loader.Scene.GameScene);
            
        });

        QuitButton.onClick.AddListener(() =>
        {
            Application.Quit();
        });
        Time.timeScale = 1.0f;
    }
}
