using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomingMissile : MonoBehaviour
{
    [SerializeField] private Transform _targetToChase;
    [SerializeField] private float _speed;

    private Transform _transform;
    private void Awake()
    {
        _Init();
    }
    private void _Init()
    {
        _transform = transform;
        _targetToChase = GameObject.FindGameObjectWithTag(Konstants.PLAYER_TAG).transform;
    }
    private void LateUpdate()
    {
        _ChaseTarget();
    }
    private void _ChaseTarget()
    {
        //Vector3 dir = _targetToChase.position - _transform.position;
        //_transform.position += dir.normalized * _speed * Time.deltaTime;



        // Calculate the direction vector towards the target
        Vector3 direction = (_targetToChase.position - _transform.position).normalized;

        // Calculate the rotation angle to face the target
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // Smoothly rotate the missile towards the target
        Quaternion targetRotation = Quaternion.Euler(new Vector3(0, 0, angle));
        _transform.rotation = Quaternion.Slerp(_transform.rotation, targetRotation, _speed * Time.deltaTime);

        // Move the missile forward
        _transform.position += direction * _speed * Time.deltaTime;
    }
}
