using PlaneDodge.ScreenManagement;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SettingsPopup : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _highScoreTxt;


    private void OnEnable()
    {
        _highScoreTxt.SetText(PlayerPrefs.GetInt(Konstants.HIGHEST_SCORE, 0).ToString());
    }

    public void OnClickClose()
    {
        ScreenManager.Instance.CloseScreen(Window.SettingsScreen);
    }
    public void OnClickExit()
    {
        Application.Quit();
    }
}
