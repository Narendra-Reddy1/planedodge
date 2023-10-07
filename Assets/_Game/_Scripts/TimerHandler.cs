using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TimerHandler : MonoBehaviour
{
    #region Varibales
    [SerializeField] private TextMeshProUGUI _timerTxt;

    private int _timerCounter = 0;
    #endregion Varibales

    #region Unity Methods
    private void OnEnable()
    {
        GlobalEventHandler.AddListener(EventID.Event_On_Player_Dead, Callback_On_Player_Dead);
    }
    private void OnDisable()
    {
        GlobalEventHandler.RemoveListener(EventID.Event_On_Player_Dead, Callback_On_Player_Dead);
    }
    private void Start()
    {
        _StartTimer();
    }
    #endregion Unity Methods

    #region Public Methods
    public int _GetTotatlElapsedTime() => _timerCounter;
    #endregion Public Methods

    #region Private Methods

    private void _StartTimer()
    {
        InvokeRepeating(nameof(_Tick), 0, 1);
    }
    private void _StopTimer()
    {
        CancelInvoke(nameof(_Tick));
    }
    private void _ResetTimer()
    {
        if (IsInvoking(nameof(_Tick)))
            CancelInvoke(nameof(_Tick));
        _timerCounter = 0;
    }

    private void _Tick()
    {
        _timerCounter++;
        if (_timerTxt)
            _timerTxt.SetText(_GetFormattedSeconds(_timerCounter));
    }
    private string _GetFormattedSeconds(int seconds)
    {
        System.TimeSpan timeSpan = System.TimeSpan.FromSeconds(seconds);

        string formattedString = "";
        if (timeSpan.Days > 0)
            formattedString = timeSpan.ToString(@"d\d\ h\h");
        else if (timeSpan.Hours > 0)
            formattedString = timeSpan.ToString(@"h\h\ m\m");
        else
            formattedString = timeSpan.ToString(@"mm\:ss");
        return formattedString;
    }
    #endregion Private Methods

    #region Callbacks
    private void Callback_On_Player_Dead(object args)
    {
        _StopTimer();
    }
    #endregion Callbacks
}
