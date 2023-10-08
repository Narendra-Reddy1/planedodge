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
        int score = InGameUiManager.Instance.GetTotalTime() * Konstants.SCORE_MULTIPLIER;//per second 10 points
        if (score > PlayerPrefs.GetInt(Konstants.HIGHEST_SCORE, 0))
            PlayerPrefs.SetInt(Konstants.HIGHEST_SCORE, score);
        _scoreTxt.SetText(score.ToString());
    }
}
