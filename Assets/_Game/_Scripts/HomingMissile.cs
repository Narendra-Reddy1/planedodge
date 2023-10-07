using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomingMissile : Missile
{

    #region Varibales
    private Transform _transform;

    #endregion Varibales

    #region Unity Methods

    private void Awake()
    {
        _Init();
    }
    private void OnEnable()
    {
        Invoke(nameof(AutoBlast), lifetime);
    }
    private void LateUpdate()
    {
        _ChaseTarget();
    }

    #endregion Unity Methods

    #region Public Methods
   
    #endregion Public Methods

    #region Private Methods

    private void _Init()
    {
        _transform = transform;
        target = GameObject.FindGameObjectWithTag(Konstants.PLAYER_TAG).transform;
    }
    private void _ChaseTarget()
    {
        //Vector3 dir = target.position - _transform.position;
        //_transform.position += dir.normalized * _speed * Time.deltaTime;



        // Calculate the direction vector towards the target
        Vector3 direction = (target.position - _transform.position).normalized;

        // Calculate the rotation angle to face the target
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // Smoothly rotate the missile towards the target
        Quaternion targetRotation = Quaternion.Euler(new Vector3(0, 0, angle));
        _transform.rotation = Quaternion.Slerp(_transform.rotation, targetRotation, speed * Time.deltaTime);

        // Move the missile forward
        _transform.position += direction * speed * Time.deltaTime;
    }
    #endregion Private Methods

    #region Callbacks

    #endregion Callbacks

}
