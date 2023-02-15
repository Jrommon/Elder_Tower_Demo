using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class plantbullet : MonoBehaviour
{
    private Rigidbody2D _rigidbody;
    private Animator _animator;
    private bool _isDed;
    private SpriteRenderer _spriteR;

    [SerializeField] private float velocity;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _spriteR = GetComponent<SpriteRenderer>();
    }
    void Update()
    {
        if (!_isDed)
        {
            movement();
        }
        if (_spriteR.isVisible == false)
        {
            Die();
        }
        
    }

    private void movement()
    {
        if(_rigidbody.velocity.magnitude <= velocity)
        {
            if(transform.rotation.y == 0)
            {
                _rigidbody.AddForce(Vector2.left * velocity, ForceMode2D.Force);
            }
            else
            {
                _rigidbody.AddForce(Vector2.right * velocity, ForceMode2D.Force);
            }
        }
    }

    public void Die()
    {
        Destroy(this.gameObject);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Floor"))
        {
            _animator.SetTrigger("Die");
            _isDed=true;
        }
    }
}
