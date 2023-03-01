using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class MagicAttack : MonoBehaviour
{
    private Vector2 _movement;
    private Vector3 _scale;
    private SpriteRenderer _spriteRenderer;
    private bool _flip;
    
    public bool Flip
    {
        get => _flip;
        set => _flip = value;
    }
    
    public Vector2 Movement
    {
        get => _movement;
        set => _movement = value;
    }

    private void Awake()
    {
        _scale = transform.localScale;
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Start is called before the first frame update
    void Start()
    {
        if (_flip)
        {
            GetComponent<SpriteRenderer>().flipX = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(_movement*Time.deltaTime);
        //transform.localScale = _movement.x < 0 ? _scale : new Vector3(-_scale.x, _scale.y, _scale.z);
        
        if (_spriteRenderer.isVisible == false)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
            Destroy(gameObject);
    }
}
