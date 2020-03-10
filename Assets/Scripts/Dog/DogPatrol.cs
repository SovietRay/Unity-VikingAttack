using System;
using UnityEngine;

[RequireComponent(typeof(GroundDetection))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
public class DogPatrol : MonoBehaviour
{
    [SerializeField] Transform _rightBorder;
    [SerializeField] Transform _leftBorder;
    [SerializeField] float _speed;

    private Rigidbody2D _rigidbady;
    private Animator _animator;
    private GroundDetection _groundDetection;
    private bool _isRightDirection;
    private Vector3 _direction;

    private void OnValidate()
    {
        if (_speed < 0)
            _speed = 0;
    }

    private void OnEnable()
    {
        if (_rightBorder == null)
        {
            enabled = false;
            throw new InvalidOperationException(_rightBorder.name);
        }
        if (_leftBorder == null)
        {
            enabled = false;
            throw new InvalidOperationException(_leftBorder.name);
        }
    }

    private void Start()
    {
        TryGetComponent(out _groundDetection);
        TryGetComponent(out _animator);
        TryGetComponent(out _rigidbady);
    }

    private void Update()
    {
        Moving();
    }

    private void Moving()
    {
        _direction = Vector3.zero;
        if (_groundDetection.IsGrounded)
        {
            SetIsRightDirectionInBorders();
            SetMovingVector();
            _animator.SetFloat("Speed", Mathf.Abs(_direction.x));
        }
        _direction.y = _rigidbady.velocity.y;
        _rigidbady.velocity = _direction;
        SetSpriteDirection();
    }

    private void SetIsRightDirectionInBorders()
    {
        if (transform.position.x > _rightBorder.position.x)
            _isRightDirection = false;
        if (transform.position.x < _leftBorder.position.x)
            _isRightDirection = true;
    }

    private void SetMovingVector()
    {
        _direction = _isRightDirection ? Vector3.right : Vector3.left;
        _direction *= _speed;
    }

    private void SetSpriteDirection()
    {
        if (_direction.x > 0)
            transform.rotation = Quaternion.Euler(0, 0, 0);
        if (_direction.x < 0)
            transform.rotation = Quaternion.Euler(0, 180, 0);
    }
}
