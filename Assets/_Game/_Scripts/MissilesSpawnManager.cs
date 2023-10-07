using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Object pool logic can be separated to a class to reuse it everywhere.
public class MissilesSpawnManager : MonoBehaviour
{
    private enum MissileType
    {
        Homing,//1
        Directional,//2
    }

    [SerializeField] private GameObject _homingMissile;
    [SerializeField] private GameObject _directionalMissile;
    [SerializeField] private float _missileSpawnOffset = 1f;

    private Camera _camera;
    private Vector2 _leftMinBound;//for homing missiles
    private Vector2 _leftMaxBound;//to spawn at left side of the screen (backside of the plane)

    private Vector2 _rightMinBound;//for directional missiles
    private Vector2 _rightMaxBound;//to spawn at right side of the screen (in front of the plane)
    private int _poolSize = 5;
    private Dictionary<MissileType, List<GameObject>> _missilesPool = new();
    private float _missileSpawnInterval = 6f;
    //ScreenCroners
    private Vector3 _corner0;
    private Vector3 _corner1;
    private Vector3 _corner2;
    private Vector3 _corner3;
    private void Awake()
    {
        _Init();
        InvokeRepeating(nameof(_SpawnRandomMissile), 0, _missileSpawnInterval);
    }
    private void OnEnable()
    {
        GlobalEventHandler.AddListener(EventID.Event_On_Player_Dead, Callback_On_Player_Dead);
    }
    private void OnDisable()
    {
        GlobalEventHandler.RemoveListener(EventID.Event_On_Player_Dead, Callback_On_Player_Dead);
    }
    private void _Init()
    {
        _camera = Camera.main;
        _corner0 = new Vector3(0, 0, _camera.transform.position.z);
        _corner1 = new Vector3(0, Screen.height, _camera.transform.position.z);
        _corner2 = new Vector3(Screen.width, 0, _camera.transform.position.z);
        _corner3 = new Vector3(Screen.width, Screen.height, _camera.transform.position.z);

        _missilesPool.Add(MissileType.Homing, new List<GameObject>());
        _missilesPool.Add(MissileType.Directional, new List<GameObject>());
        for (int i = 0; i < _poolSize; i++)
        {
            AddToDict(MissileType.Homing, _homingMissile);
            AddToDict(MissileType.Directional, _directionalMissile);
        }
        void AddToDict(MissileType missileType, GameObject powerup)
        {
            if (powerup == null) return;
            GameObject item = Instantiate(powerup, transform);
            item.SetActive(false);
            _missilesPool[missileType].Add(item);
        }
    }


    private void _SpawnRandomMissile()
    {
        GameObject missile = null;
        switch (Random.Range(1, 3))
        {
            case 1://Homing
                missile = _GetMissileFromPool(MissileType.Homing);
                missile.transform.position = _GetRandomPoseForHomingMissile();
                break;
            case 2://Directional
                missile = _GetMissileFromPool(MissileType.Directional);
                missile.transform.position = _GetRandomPoseForDirectionalMissile();
                //missile.transform.rotation = Quaternion.Euler(missile.transform.rotation.eulerAngles.x, missile.transform.rotation.eulerAngles.y, Random.Range(-20, 20));
                break;
        }
        missile.SetActive(true);

    }

    private Vector2 _GetRandomPoseForHomingMissile()
    {
        _leftMinBound = _camera.ScreenToWorldPoint(_corner0);
        _leftMaxBound = _camera.ScreenToWorldPoint(_corner1);
        Vector2 pose = new Vector2(_leftMinBound.x, Random.Range(_leftMinBound.y, _leftMaxBound.y));
        // pose.x -= _missileSpawnOffset;
        return pose;
    }
    private Vector2 _GetRandomPoseForDirectionalMissile()
    {
        _rightMinBound = _camera.ScreenToWorldPoint(_corner2);
        _rightMaxBound = _camera.ScreenToWorldPoint(_corner3);

        Vector2 pose = new Vector2(_rightMinBound.x, Random.Range(_rightMinBound.y, _rightMaxBound.y));
        //pose.x += _missileSpawnOffset;
        return pose;
    }

    private GameObject _GetMissileFromPool(MissileType missileType)
    {
        for (int i = 0, count = _missilesPool[missileType].Count; i < count; i++)
        {
            if (!_missilesPool[missileType][i].activeInHierarchy) return _missilesPool[missileType][i];
        }
        return _missilesPool[missileType][^1];
    }

    private void Callback_On_Player_Dead(object args)
    {
        CancelInvoke();
    }
}
