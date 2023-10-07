using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;
using PlaneDodge.ScreenManagement;

public class InitScene : MonoBehaviour
{
    [SerializeField] private Image _loadingFillbar;
    void Start()
    {
        _loadingFillbar.DOFillAmount(0.45f, .7f).onComplete += () =>
        {
            _loadingFillbar.DOFillAmount(.75f, .45f).SetDelay(0.125f).onComplete += () =>
            {
                _loadingFillbar.DOFillAmount(1, .3f).SetDelay(0.125f).onComplete += () =>
                {
                    AsyncOperation handle1 = SceneManager.LoadSceneAsync(Konstants.MAIN_SCENE, LoadSceneMode.Additive);

                    handle1.completed += _ =>
                    {
                        ScreenManager.Instance.ChangeScreen(Window.HomeScreen);
                        SceneManager.UnloadSceneAsync(Konstants.INIT_SCENE);
                    };

                };
            };
        };

    }
}
