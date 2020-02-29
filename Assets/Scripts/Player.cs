using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Vector3 _direction; //Save player move direction
    [SerializeField] private float speed;
    [SerializeField] private ParticleSystem _shotgun;
    [SerializeField] private Animator _animator;
    [SerializeField] private Rigidbody2D _rigidbody;

    private bool _canHit = true;
    [SerializeField] private float _reloadTime = 0.5f;
    void Start()
    {

    }

    void Update()
    {
        #region Movement

        _direction = Vector3.zero;
        if (Input.GetKey(KeyCode.A))
            _direction = Vector3.left;
        if (Input.GetKey(KeyCode.D))
            _direction = Vector3.right;
        _direction *= speed;
        _direction.y = _rigidbody.velocity.y;
        _rigidbody.velocity = _direction;
        
        #endregion

        if (Input.GetMouseButtonDown(0) && _canHit)
        {
            StartCoroutine(Shoot());
        }
    }

    private IEnumerator Shoot()
    {
        _shotgun.Play();
        _animator.Play("Shoot");
        _canHit = false;
        yield return new WaitForSeconds(_reloadTime);
        _canHit = true;
    }
}
