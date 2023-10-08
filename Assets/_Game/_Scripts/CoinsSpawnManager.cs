using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinsSpawnManager : MonoBehaviour
{

    #region Varibales


    [SerializeField] private List<GameObject> _coinsSplinePrefabs;
    private Transform _playerTransform;
    private float _intervalToSpawnCoins = 8f;
    private Vector3 _minBound;
    private Vector3 _maxBound;
    [SerializeField] private float _offsetDistanceToThePlayer = 75f;//Xposition Offset.
    #endregion Varibales

    #region Unity Methods
    private void Start()
    {
        _Init();
    }
    #endregion Unity Methods

    #region Public Methods
    #endregion Public Methods

    #region Private Methods
    private void _Init()
    {
        _playerTransform = GameObject.FindGameObjectWithTag(Konstants.PLAYER_TAG).transform;
        _minBound = GameManager.Instance.MinBound;
        _maxBound = GameManager.Instance.MaxBound;
        InvokeRepeating(nameof(_SpawnCoins), 1, _intervalToSpawnCoins);
    }
    int index = 0;
    private void _SpawnCoins()
    {
        Vector2 currentPlayerPose = _playerTransform.position;
        currentPlayerPose.x += _offsetDistanceToThePlayer;//new offset pose for coins to spawn.
        currentPlayerPose.y = Random.Range(_minBound.y + 2, _maxBound.y - 2);
        GameObject splineObj = _coinsSplinePrefabs[index];
        splineObj.SetActive(false);
        splineObj.transform.position = currentPlayerPose;
        splineObj.SetActive(true);
        index++;
        if (index >= _coinsSplinePrefabs.Count) index = 0;
    }


    private GameObject GetSpline()
    {
        GameObject splineObj = _coinsSplinePrefabs[Random.Range(0, _coinsSplinePrefabs.Count)];
        if (splineObj.activeInHierarchy)
        {

        }
        return null;
    }
    #endregion Private Methods

    #region Callbacks
    #endregion Callbacks



}
