using PlaneDodge.ScreenManagement;
using TMPro;
using UnityEngine;

public class GameOverScreen : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _scoreTxt;
    private void OnEnable()
    {
        _UpdateScore();
    }


    public void OnClickRestartBtn()
    {
        ScreenManager.Instance.CloseAllScreens();
        ScreenManager.Instance.ChangeScreenWithBlinkEffect(Window.GameplayScreen);
    }
    public void OnClickHomeBtn()
    {
        ScreenManager.Instance.ChangeScreenWithBlinkEffect(Window.HomeScreen);
    }

    private void _UpdateScore()
    {
        _scoreTxt.SetText(InGameUiManager.Instance.GetTotalTime().ToString());
    }
}
