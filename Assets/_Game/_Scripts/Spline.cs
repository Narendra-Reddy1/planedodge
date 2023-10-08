using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace PlaneDodge
{
    public class Spline : MonoBehaviour
    {

        #region Varibales
        [SerializeField] private List<Transform> _points;
        [SerializeField] private GameObject _coinPrefab;
        [SerializeField] private Transform _coinsParent;
        private List<GameObject> _coinsPool = new List<GameObject>();
        #endregion Varibales

        #region Unity Methods

#if UNITY_EDITOR
        [SerializeField] private float _sphereRadius = .2f;
        private void OnDrawGizmos()
        {
            for (int i = 0, count = transform.childCount; i < count - 1; i++)
            {
                Gizmos.DrawSphere(transform.GetChild(i).position, _sphereRadius);
                Gizmos.DrawLine(transform.GetChild(i).position, transform.GetChild(i + 1).position);
            }
            Gizmos.DrawSphere(transform.GetChild(transform.childCount - 1).position, _sphereRadius);
        }
#endif
        private void Awake()
        {
            _points = transform.GetComponentsInChildren<Transform>().ToList();
            _points.Remove(this.transform);
            _coinsParent = _points[0];
            _SpawnCoinsAtPointPose();
        }
        private void OnEnable()
        {
            _ActivateCoins();
        }
        #endregion Unity Methods

        #region Public Methods
        #endregion Public Methods

        #region Private Methods

        private void _SpawnCoinsAtPointPose()
        {
            foreach (Transform point in _points)
            {
                GameObject item = Instantiate(_coinPrefab, point.position, Quaternion.identity, _coinsParent);
                _coinsPool.Add(item);
                item.SetActive(false);
            }
        }
        private void _ActivateCoins()
        {
            for (int i = 0, count = _coinsPool.Count; i < count; i++)
            {
                _coinsPool[i].transform.position = _points[i].position;
                _coinsPool[i].SetActive(true);
            }
        }
        #endregion Private Methods

        #region Callbacks
        #endregion Callbacks


    }
}