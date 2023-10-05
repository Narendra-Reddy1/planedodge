using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBounds : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private Vector3 _minScreenBounds;
    [SerializeField] private Vector3 _maxScreenBounds;
    [SerializeField] private float playerHeight;


    private Camera _camera;
    private Transform _transform;
    private void Awake()
    {
        _Init();
    }
    private void LateUpdate()
    {
        _CalculatePlayerBounds();
    }
    private void _Init()
    {
        _camera = Camera.main;
        _minScreenBounds = _camera.ScreenToWorldPoint(new Vector3(0, 0, _camera.transform.position.z));
        _maxScreenBounds = _camera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, _camera.transform.position.z));
        _transform = transform;
        if (_spriteRenderer != null)
            playerHeight = _spriteRenderer.bounds.size.y;

    }
    private void _CalculatePlayerBounds()
    {
        Vector3 viewPos = _transform.position;
        viewPos.y = Mathf.Clamp(viewPos.y, _minScreenBounds.y + playerHeight,
            _maxScreenBounds.y - playerHeight);
        _transform.position = viewPos;
    }
}
