using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneController : MonoBehaviour
{
    #region Varibales
    [SerializeField] private PlaneStats _planeStats;
    [SerializeField] private Rigidbody2D _planeRb;

    [SerializeField] private Vector2 _inputVector = new Vector2();
    #endregion Varibales

    #region Unity Methods
    private void Update()
    {
        _inputVector.x = Input.GetAxisRaw("Horizontal");
        _inputVector.y = Input.GetAxisRaw("Vertical");
        _MovePlane();
    }
    #endregion Unity Methods

    #region Public Methods
    #endregion Public Methods

    #region Private Methods
    private void _MovePlane()
    {
        Vector2 velocity = transform.right * (_planeStats.DefaultSpeed);
        Vector2 velocity1 = transform.right * _inputVector.x * _planeStats.Speed;

        _planeRb.velocity = (velocity + velocity1) * Time.deltaTime;
        float dir = Vector2.Dot(_planeRb.velocity, _planeRb.GetRelativeVector(Vector2.right));

        _planeRb.rotation = _inputVector.y != 0 ? Mathf.Lerp(_planeRb.rotation, _inputVector.y > 0 ? _planeStats.RotationAngle : -_planeStats.RotationAngle, _planeStats.RotationSpeed * Time.deltaTime) : Mathf.Lerp(_planeRb.rotation, 0, _planeStats.RotationSpeed * Time.deltaTime);

        if (_planeRb.velocity.sqrMagnitude / 2 > _planeStats.MaxSpeed)
            _planeRb.velocity = _planeRb.velocity.normalized * _planeStats.Speed;
    }
    public float _offset = 2f;
    #endregion Private Methods

    #region Callbacks
    #endregion Callbacks
}
