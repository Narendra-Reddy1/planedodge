using PlaneDodge.ScreenManagement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomeScreen : MonoBehaviour
{

    public void OnClickSettingsBtn()
    {
        ScreenManager.Instance.ChangeScreen(Window.SettingsScreen, ScreenType.Additive, true);
    }
    public void OnClickPlayBtn()
    {
        ScreenManager.Instance.ChangeScreenWithBlinkEffect(Window.GameplayScreen);
    }
}
