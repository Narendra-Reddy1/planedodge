using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InGameUiManager : MonoBehaviour
{

    #region Varibales

    [SerializeField] private TextMeshProUGUI _coinsTxt;

    private short _collectedCoins;
    #endregion Varibales

    #region Unity Methods
    private void OnEnable()
    {
        GlobalEventHandler.AddListener(EventID.Event_On_Coin_Collected, Callback_On_Coin_Collected);
    }
    private void OnDisable()
    {
        GlobalEventHandler.RemoveListener(EventID.Event_On_Coin_Collected, Callback_On_Coin_Collected);
    }
    #endregion Unity Methods

    #region Public Methods
    #endregion Public Methods

    #region Private Methods

    #endregion Private Methods

    #region Callbacks
    private void Callback_On_Coin_Collected(object args)
    {
        _collectedCoins++;
        _coinsTxt.SetText($"{_collectedCoins}");
    }
    #endregion Callbacks


}
