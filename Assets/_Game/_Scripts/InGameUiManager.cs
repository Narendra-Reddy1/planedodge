using PlaneDodge.ScreenManagement;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InGameUiManager : MonoBehaviour
{
    #region SINGLETON
    public static InGameUiManager Instance { get; private set; }
    #endregion SINGLETON

    #region Varibales

    [SerializeField] private TextMeshProUGUI _coinsTxt;
    [SerializeField] private TimerHandler _timerHandler;
    private short _collectedCoins;
    #endregion Varibales

    #region Unity Methods
    private void Awake()
    {
        Instance = this;
    }
    private void OnEnable()
    {
        GlobalEventHandler.AddListener(EventID.Event_On_Coin_Collected, Callback_On_Coin_Collected);
        GlobalEventHandler.AddListener(EventID.Event_On_Player_Dead, Callback_On_Player_Dead);
    }
    private void OnDisable()
    {
        GlobalEventHandler.RemoveListener(EventID.Event_On_Coin_Collected, Callback_On_Coin_Collected);
        GlobalEventHandler.RemoveListener(EventID.Event_On_Player_Dead, Callback_On_Player_Dead);
    }
    #endregion Unity Methods

    #region Public Methods
    public int GetTotalTime()
    {
        return _timerHandler._GetTotatlElapsedTime();
    }
    #endregion Public Methods

    #region Private Methods
    private void _ShowGameOverScreen()
    {
        ScreenManager.Instance.ChangeScreen(Window.GameOverScreen, ScreenType.Additive, true);
    }
    #endregion Private Methods

    #region Callbacks
    private void Callback_On_Coin_Collected(object args)
    {
        _collectedCoins++;
        _coinsTxt.SetText($"{_collectedCoins}");
    }
    private void Callback_On_Player_Dead(object args)
    {
        Invoke(nameof(_ShowGameOverScreen), .45f);
    }
    #endregion Callbacks


}
