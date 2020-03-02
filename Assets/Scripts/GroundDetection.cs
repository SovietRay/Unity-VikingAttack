using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundDetection : MonoBehaviour
{
    [SerializeField] private bool _isGrounded;
    public bool IsGrounded => _isGrounded;
    private string _collisionTag = "Platform";

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(_collisionTag))
        {
            _isGrounded = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(_collisionTag))
        {
            _isGrounded = false;
        }
    }
}
