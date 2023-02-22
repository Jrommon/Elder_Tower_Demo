using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(SpriteRenderer))]
public class Kappa_Snappa : MonoBehaviour
{
    [Header("Main settings")]
    
    [SerializeField] private Rigidbody2D _rigidbody;
    [SerializeField] private SpriteRenderer _spriteRenderer;

    [Header("Movement settings")]
    //Direccion: 1= derecha, dirrecion 2= Izquierda
    [SerializeField] [Range(-1,1)] private int _direction = 1;
    //Velocidad de movimiento
    [SerializeField] private int _speed;
    

    

    
    void Update()
    {
        //Mueve a la criatura hacia delante mientras no supere la velocidad
        if(_rigidbody.velocity.magnitude  <= _speed)
        {
            moveFoward();
        }
        //Gira a la criatura cuando  choca con una pared
        
        turn();
        //Comprueba si el sprite debe estar girado
        _spriteRenderer.flipX = _direction == 1 ? true : false;


    }

    private void turn()
    {

        if (Physics2D.Raycast(transform.position, Vector2.right * _direction, 1))
        {
            
                _direction = -_direction;
        }
    }

    private void moveFoward()
    {
        _rigidbody.AddForce(new Vector2(1*_speed*_direction,0));
    }

    
    

}
