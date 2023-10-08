using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlaneController : MonoBehaviour
{
    #region Varibales
    [SerializeField] private PlaneStats _planeStats;
    [SerializeField] private Rigidbody2D _planeRb;
    [SerializeField] private GameObject _shield;

    [SerializeField] private Vector2 _inputVector = new Vector2();
    private Transform _transform;

    private bool _isMagnetActivated = false;
    private bool _isSpeedBoostActivated = false;
    private bool _isShieldActivated = false;
    private float _shieldLifeTime = 7f;
    private float _speedBoostTimer = 8f;
    private float _magnetTimer = 9f;
    private bool _isPlayerDead = false;
    #endregion Varibales

    #region Unity Methods
    private void OnEnable()
    {
        _transform = transform;
    }
    private void Update()
    {
        _inputVector = InputManager.Instance.InputVector;
        _MovePlane();
        if (_isMagnetActivated)
            _AttractCoins();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.tag)
        {
            case Konstants.COIN_TAG:
                DOTween.Kill(collision.transform);
                collision.gameObject.SetActive(false);
                GlobalEventHandler.TriggerEvent(EventID.Event_On_Coin_Collected);
                break;
            case Konstants.MISSILE_TAG:
                Debug.Log($"Missile Hit!!!");
                if (_isShieldActivated || _isPlayerDead) break;
                _isPlayerDead = true;
                AudioManager.instance.PlaySFX(AudioID.MissileBlastSFX);
                collision.GetComponent<Missile>().AutoBlast();
                GlobalEventHandler.TriggerEvent(EventID.Event_On_Player_Dead);
                gameObject.SetActive(false);
                break;
            case Konstants.SHIELD_TAG:
                collision.gameObject.SetActive(false);
                GlobalEventHandler.TriggerEvent(EventID.Event_On_Powerup_Collected);
                _isShieldActivated = true;
                _shield.SetActive(true);
                CancelInvoke(nameof(_DisableShield));
                Invoke(nameof(_DisableShield), _shieldLifeTime);
                break;
            case Konstants.SPEED_BOOST_TAG:
                GlobalEventHandler.TriggerEvent(EventID.Event_On_Powerup_Collected);
                collision.gameObject.SetActive(false);
                _isSpeedBoostActivated = true;
                CancelInvoke(nameof(_DisableSpeedBoost));
                Invoke(nameof(_DisableSpeedBoost), _speedBoostTimer);
                break;
            case Konstants.MAGNET_POWERUP_TAG:
                GlobalEventHandler.TriggerEvent(EventID.Event_On_Powerup_Collected);
                collision.gameObject.SetActive(false);
                _isMagnetActivated = true;
                CancelInvoke(nameof(_DisableMagnetPowerup));
                Invoke(nameof(_DisableMagnetPowerup), _magnetTimer);
                break;
        }
    }

    #endregion Unity Methods

    #region Public Methods
    #endregion Public Methods

    #region Private Methods
    private void _MovePlane()
    {
        Vector2 velocity = _transform.right * (_planeStats.DefaultSpeed);
        Vector2 velocity1 = (_transform.right * (_inputVector.x > 0 ? _inputVector.x * _planeStats.Speed : 0)) +
            (_isSpeedBoostActivated ? _transform.right * _planeStats.Speed * 0.25f : Vector2.zero);
        _planeRb.velocity = (velocity + velocity1) * Time.deltaTime;
        float dir = Vector2.Dot(_planeRb.velocity, _planeRb.GetRelativeVector(Vector2.right));

        _planeRb.rotation = _inputVector.y != 0 ? Mathf.Lerp(_planeRb.rotation, _inputVector.y > 0 ? _planeStats.RotationAngle : -_planeStats.RotationAngle, _planeStats.RotationSpeed * Time.deltaTime) : Mathf.Lerp(_planeRb.rotation, 0, _planeStats.RotationSpeed * Time.deltaTime);

        if (_planeRb.velocity.sqrMagnitude / 2 > _planeStats.MaxSpeed)
            _planeRb.velocity = _planeRb.velocity.normalized * _planeStats.MaxSpeed;
    }

    public float _magnetAttractRadius = 5f;
    public LayerMask _coinLayerMask;

    private void _AttractCoins()
    {
        RaycastHit2D[] hits = Physics2D.CircleCastAll(_transform.position, _magnetAttractRadius, _transform.right, 0f, _coinLayerMask);
        foreach (var hit in hits)
        {
            hit.transform.DOMove(_transform.position, .5f);
        }
    }
    private void _DisableMagnetPowerup()
    {
        _isMagnetActivated = false;
    }
    private void _DisableSpeedBoost()
    {
        _isSpeedBoostActivated = false;
    }
    private void _DisableShield()
    {
        _isShieldActivated = false;
        _shield.SetActive(false);
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, _magnetAttractRadius);
    }
    #endregion Private Methods

    #region Callbacks
    #endregion Callbacks
}
