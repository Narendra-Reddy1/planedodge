using AYellowpaper.SerializedCollections;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace PlaneDodge.ScreenManagement
{
    public class ScreenManager : MonoBehaviour
    {

        #region SINGLETON
        public static ScreenManager Instance { get; private set; }
        #endregion SINGLETON

        #region Varibales
        [SerializeField] private SerializedDictionary<Window, GameObject> _screens;

        [SerializeField] private Dictionary<Window, GameObject> _dynamicScreens = new Dictionary<Window, GameObject>();
        [SerializeField] private Transform _uiParent;
        [SerializeField] private GameObject _loadingScreenCanvas;
        [SerializeField] private Image _darkOverlay;
        private Stack<Window> _screenStack = new Stack<Window>();
        private Window _currentScreen = Window.None;
        private Window _previousScreen = Window.None;


        #endregion Varibales

        #region Unity Methods
        private void Awake()
        {
            Instance = this;
        }
        #endregion Unity Methods

        #region Public Methods

        public GameObject ChangeScreen(Window window, ScreenType screenType = ScreenType.Replace, bool isUiObject = false)
        {
            //if (_currentScreen == window) { return null; }
            if (!_screens.ContainsKey(window)) return null;
            if (screenType == ScreenType.Replace)
            {
                CloseAllScreens();
            }
            if (_screenStack.Contains(window))
            {
                Debug.LogError($"Trying to same Active screen!!! {window}");
                return null;
            }
            GameObject screen = _screens[window];

            GameObject spawnedScreen = Instantiate(screen, isUiObject ? _uiParent : transform);
            spawnedScreen.transform.SetAsLastSibling();
            if (!_dynamicScreens.ContainsKey(window))
            {
                _dynamicScreens.Add(window, spawnedScreen);
            }
            else
                _dynamicScreens[window] = spawnedScreen;

            _previousScreen = _currentScreen;
            _currentScreen = window;
            if (screenType == ScreenType.Additive)
                _screenStack.Push(window);
            return spawnedScreen;
        }
        public void CloseScreen(Window window)
        {
            if (_dynamicScreens.ContainsKey(window))
            {
                GameObject go;
                if (_dynamicScreens.TryGetValue(window, out go))
                {
                    Destroy(go);
                    _dynamicScreens.Remove(window);
                    if (_screenStack.Peek() == (window))
                        _screenStack.Pop();

                }
            }


        }
        public void CloseAllScreens()
        {
            foreach (GameObject go in _dynamicScreens.Values)
            {
                Destroy(go);
            }
            _dynamicScreens.Clear();
            _screenStack.Clear();
        }

        public void ChangeScreenWithBlinkEffect(Window window, ScreenType screenType = ScreenType.Replace, bool isUiObj = false)
        {
            _loadingScreenCanvas.SetActive(true);
            _darkOverlay.DOFade(1, .35f).onComplete += () =>
            {
                ChangeScreen(window, screenType, isUiObj);
                _darkOverlay.DOFade(0, .35f).onComplete += () =>
                {
                    _loadingScreenCanvas.SetActive(false);
                };
            };
        }
        #endregion Public Methods

        #region Private Methods
        #endregion Private Methods

        #region Callbacks
        #endregion Callbacks
    }

    public enum Window
    {
        None,
        HomeScreen,
        GameplayScreen,
        GameOverScreen,
        SettingsScreen,
    }
    public enum ScreenType
    {
        Additive,
        Replace,
    }
}