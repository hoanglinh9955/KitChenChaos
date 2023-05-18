using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitchenGameManager : MonoBehaviour
{
    public static KitchenGameManager Instance { get; private set; }

    public event EventHandler OnStateChange;
    public event EventHandler OnPauseEvent;
    public event EventHandler OnResumeEvent;
   
    private State state;

    private float timeWaiting = 1f;
    private float timeCountDown = 3f;
    private float GameIsPlayTime;
    private float GameIsPlayTimeMax = 30f;
    private bool isGamePause = false;
    private enum State
    {
        WaitingToStart,
        CountDownToStart,
        GameInPlay,
        GameOver,
    }
    private void Start()
    {
        GameInput.instance.OnEscapeEvent += KitchenGameManager_OnEscapeEvent;
    }

    private void KitchenGameManager_OnEscapeEvent(object sender, EventArgs e)
    {
        TogglePause();      
    }

    private void Awake()
    {
        Instance = this;
        state = State.WaitingToStart;
    }
    

    private void Update()
    {
        switch(state)
        {
            case State.WaitingToStart:
                timeWaiting -= Time.deltaTime;
                if(timeWaiting < 0f)
                {
                    state = State.CountDownToStart;
                    KitchenGameManager.Instance.OnStateChange?.Invoke(this, EventArgs.Empty);
                }
                break;
            case State.CountDownToStart:
                timeCountDown -= Time.deltaTime;
                if(timeCountDown < 0f)
                {
                    state = State.GameInPlay;
                    OnStateChange?.Invoke(this, EventArgs.Empty);
                }
                break;
            case State.GameInPlay:
                GameIsPlayTime += Time.deltaTime;
                if(GameIsPlayTime > GameIsPlayTimeMax)
                {
                    state = State.GameOver;
                    OnStateChange?.Invoke(this, EventArgs.Empty);
                }
                break;
            case State.GameOver:
                break;
        }
    }
    public bool IsGamePlaying()
    {
        return state == State.GameInPlay;
    }
    public bool IsCountDownStartActive()
    {
        return state == State.CountDownToStart;
    }
    public float GetCountDownTimerStart()
    {
        return timeCountDown;
    }
    public bool IsGameOver()
    {
        return state == State.GameOver;
    }
    public bool IsGamePlay()
    {
        return state == State.GameInPlay;
    }
    public float GetTimerGamePlay()
    {
        return (GameIsPlayTime / GameIsPlayTimeMax);
    }
    public void TogglePause()
    {
        
        if (!isGamePause)
        {
          
            isGamePause = true;
            OnPauseEvent?.Invoke(this, EventArgs.Empty);
            Time.timeScale = 0f;
        }
        else
        {
            isGamePause = false;
            OnResumeEvent?.Invoke(this, EventArgs.Empty);
            Time.timeScale = 1f;
            
        }
        
    }
}
