using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    #region SINGLETON
    public static GameManager Instance { get; private set; }
    #endregion SINGLETON

    #region Varibales
    private Camera _camera;
    private Vector2 _minBound;
    private Vector2 _maxBound;
    public Vector2 MaxBound => _maxBound;
    public Vector2 MinBound => _minBound;
    #endregion Varibales

    #region Unity Methods
    private void Awake()
    {
        if (Instance != null && Instance != this) Destroy(gameObject);
        else
            Instance = this;
        _CalculateScreenBounds();
    }
    #endregion Unity Methods

    #region Public Methods

    #endregion Public Methods

    #region Private Methods
    private void _CalculateScreenBounds()
    {
        _camera = Camera.main;
        _minBound = _camera.ScreenToWorldPoint(new Vector3(0, 0, _camera.transform.position.z));
        _maxBound = _camera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, _camera.transform.position.z));
    }
    #endregion Private Methods

    #region Callbacks
    #endregion Callbacks
}
