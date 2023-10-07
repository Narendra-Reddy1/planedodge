using PlaneDodge.ScreenManagement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomeScreen : MonoBehaviour
{

    public void OnClickPlayBtn()
    {
        ScreenManager.Instance.ChangeScreenWithBlinkEffect(Window.GameplayScreen);
    }
}
