using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Animator))]
public class EnemiesGeneral : MonoBehaviour
{
    

    [Header("Main settings")]
    [SerializeField] private int hp;
    [SerializeField] protected Rigidbody2D _rigidbody;
    [SerializeField] protected SpriteRenderer _spriteRenderer;
    [SerializeField] protected Animator _animator;



    private void Update()
    {
        if (hp == 0)
        {
            _animator.SetTrigger("Die");
        }
    }

    public void die()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("PlayerAttack"))
        {
            hp--;

            if (hp <= 0)
            {
                die();
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.collider.CompareTag("PlayerAttack") || col.collider.CompareTag("Void"))
        {
            hp--;

            if (hp <= 0)
            {
                die();
            }
        }
    }
}
