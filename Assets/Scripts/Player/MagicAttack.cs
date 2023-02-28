using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicAttack : MonoBehaviour
{
    private Vector2 _movement;
    private Vector3 _scale;

    public Vector2 Movement
    {
        get => _movement;
        set => _movement = value;
    }

    private void Awake()
    {
        _scale = transform.localScale;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(_movement);
        transform.localScale = _movement.x < 0 ? _scale : new Vector3(-_scale.x, _scale.y, _scale.z);
    }
}
