using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupsAndBoostersSpawnManager : MonoBehaviour
{
    #region Varibales

    [SerializeField] private GameObject _speedBoost;
    [SerializeField] private GameObject _magnetPowerup;
    [SerializeField] private GameObject _shieldPowerup;
    [SerializeField] private int _poolSize = 5;
    [SerializeField] private float _xOffset = 50f;
    private Vector2 _minBound;
    private Vector2 _maxBound;
    private Camera _camera;
    private Dictionary<PowerupType, List<GameObject>> _powerupsPool = new();
    #endregion Varibales

    #region Unity Methods
    private void OnEnable()
    {
        GlobalEventHandler.AddListener(EventID.Event_On_Player_Dead, Callback_On_Player_Dead);
    }
    private void OnDisable()
    {
        GlobalEventHandler.RemoveListener(EventID.Event_On_Player_Dead, Callback_On_Player_Dead);
    }
    private void Start()
    {
        _Init();
    }
    private void OnDestroy()
    {
        _powerupsPool.Clear();
    }
    #endregion Unity Methods

    #region Public Methods

    #endregion Public Methods

    #region Private Methods
    private void _Init()
    {
        _powerupsPool.Add(PowerupType.Shield, new List<GameObject>());
        _powerupsPool.Add(PowerupType.Speed, new List<GameObject>());
        _powerupsPool.Add(PowerupType.Magnet, new List<GameObject>());
        for (int i = 0; i < _poolSize; i++)
        {
            AddToDict(PowerupType.Shield, _shieldPowerup);
            AddToDict(PowerupType.Magnet, _magnetPowerup);
            AddToDict(PowerupType.Speed, _speedBoost);
        }
        void AddToDict(PowerupType powerupType, GameObject powerup)
        {
            if (powerup == null) return;
            GameObject item = Instantiate(powerup, transform);
            item.SetActive(false);
            _powerupsPool[powerupType].Add(item);
        }
        _minBound = GameManager.Instance.MinBound;
        _maxBound = GameManager.Instance.MaxBound;
        _camera = Camera.main;

        InvokeRepeating(nameof(_SpawnRandomPowerup), 0, 8f);
    }
    private int lastSpawnedPowerup = -1;
    private int currentPowerup = -1;
    private void _SpawnRandomPowerup()
    {

        currentPowerup = Random.Range(0, 3);
        while (currentPowerup == lastSpawnedPowerup)
            currentPowerup = Random.Range(0, 3);
        Vector2 pose = new Vector2(Random.Range(_minBound.x, _maxBound.x), Random.Range(_minBound.y + 1, _maxBound.y - 1));
        pose.x += _camera.transform.position.x + _xOffset;
        GameObject powerup = null;
        switch (currentPowerup)
        {
            case 0://magnet
                powerup = GetPowerup(PowerupType.Magnet);
                break;

            case 1://shield
                powerup = GetPowerup(PowerupType.Shield);
                break;

            case 2://speed
                powerup = GetPowerup(PowerupType.Speed);
                break;

            default:
                powerup = GetPowerup(PowerupType.Magnet);
                break;
        }
        lastSpawnedPowerup = currentPowerup;
        powerup.transform.position = pose;
        powerup.SetActive(true);
    }
    private GameObject GetPowerup(PowerupType powerupType)
    {
        for (int i = 0, count = _powerupsPool[powerupType].Count; i < count; i++)
        {
            if (!_powerupsPool[powerupType][i].activeInHierarchy)
                return _powerupsPool[powerupType][i];
        }

        return _powerupsPool[powerupType][^1];
    }
    #endregion Private Methods

    #region Callbacks
    private void Callback_On_Player_Dead(object args)
    {
        CancelInvoke();
    }
    #endregion Callbacks

    private enum PowerupType
    {
        Magnet,//0
        Shield,//1
        Speed,//2

    }
}
