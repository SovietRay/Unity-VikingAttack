using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(GroundDetection))]
public class Player : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _jumpForce;
    [SerializeField] private ParticleSystem _shotgun;
    [SerializeField] private Animator _animator;
    [SerializeField] private Rigidbody2D _rigidbody;
    [SerializeField] private float _reloadTime;

    private Vector3 _direction; //Save player move direction
    private GroundDetection _groundDetection;
    private bool _canShoot = true;

    private void Start()
    {
        _groundDetection = GetComponent<GroundDetection>();
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
        _direction = Vector3.zero;
        if (Input.GetKey(KeyCode.A))
            _direction = Vector3.left;
        if (Input.GetKey(KeyCode.D))
            _direction = Vector3.right;
        _direction *= _speed;
        _direction.y = _rigidbody.velocity.y;
        _rigidbody.velocity = _direction;

        if (Input.GetKey(KeyCode.Space) && _groundDetection.IsGrounded) //Add normal to surface test
        {
            _rigidbody.velocity = Vector3.zero; //We remove the folding of several jump pulses due to the friezes of the game
            _rigidbody.AddForce(Vector3.up * _jumpForce, ForceMode2D.Impulse);
            // Jump animation
        }
    }
}
