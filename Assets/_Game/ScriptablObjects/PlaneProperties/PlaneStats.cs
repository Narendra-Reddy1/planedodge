using UnityEngine;

[CreateAssetMenu(fileName = "newPlaneStats", menuName = "ScriptableObjects/PlaneStats")]
public class PlaneStats : ScriptableObject
{
    [SerializeField] private float _defaultSpeed = 1f;
    [SerializeField] private float _acceleration;

    [SerializeField] private float _rotateSpeed;
    [SerializeField] private float _rotationalAngle;

    [SerializeField] private float _speed;
    [SerializeField] private float _maxSpeed;

    public float Acceleration => _acceleration;
    public float RotationSpeed => _rotateSpeed;
    public float RotationAngle => _rotationalAngle;

    public float DefaultSpeed => _defaultSpeed;
    public float Speed => _speed;
    public float MaxSpeed => _maxSpeed;
}
