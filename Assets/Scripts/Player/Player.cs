using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(GroundDetection))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Rigidbody2D))]
public class Player : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _jumpForce;
    [SerializeField] private float _reloadTime;
    [SerializeField] private ParticleSystem _shotgun;

    private Vector3 _movingDirection;
    private GroundDetection _groundDetection;
    private Animator _animator;
    private Rigidbody2D _rigidbody;
    private bool _canShoot = true;

    private void OnEnable()
    {
        if (_shotgun == null)
        {
            enabled = false;
            throw new InvalidOperationException(_shotgun.name);
        }
    }

    private void Start()
    {
        TryGetComponent(out _groundDetection);
        TryGetComponent(out _animator);
        TryGetComponent(out _rigidbody);
    }

    void Update()
    {
        Movement();

        if (Input.GetMouseButtonDown(0) && _canShoot)
        {
            StartCoroutine(Shoot());
        }
    }

    private IEnumerator Shoot()
    {
        _shotgun.Play();
        _animator.Play("Shoot");
        _canShoot = false;
        yield return new WaitForSeconds(_reloadTime);
        _canShoot = true;
    }

    private void Movement()
    {
        _movingDirection = Vector3.zero;
        if (Input.GetKey(KeyCode.A))
            _movingDirection = Vector3.left;
        if (Input.GetKey(KeyCode.D))
            _movingDirection = Vector3.right;
        _movingDirection *= _speed;
        _movingDirection.y = _rigidbody.velocity.y;
        _rigidbody.velocity = _movingDirection;

        if (Input.GetKey(KeyCode.Space) && _groundDetection.IsGrounded) //Add normal to surface test
        {
            _rigidbody.velocity = Vector3.zero; //We remove the folding of several jump pulses due to the friezes of the game
            _rigidbody.AddForce(Vector3.up * _jumpForce, ForceMode2D.Impulse);
            // Jump animation
        }
    }
}
