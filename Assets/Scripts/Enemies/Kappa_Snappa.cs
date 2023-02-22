//using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Kappa_Snappa : EnemiesGeneral
{
    [Header("Kappa settings")]
    //Direccion: 1= derecha, dirrecion 2= Izquierda
    [SerializeField] [Range(-1,1)] private int _direction = 1;
    //Velocidad de movimiento
    [SerializeField] private int _speed;


    //Raycast utilizado para detectar colisiones frontales con paredes
    private RaycastHit2D _ray;

    void Update()
    {
        //Mueve a la criatura hacia delante mientras no supere la velocidad
        if(_rigidbody.velocity.magnitude  <= _speed)
        {
            moveFoward();
        }
        //Manda el raycast y gira a la criatura cuando  choca con una pared
        _ray = Physics2D.Raycast(transform.position, Vector2.right * _direction, 1, 3);
        turn();
        //Comprueba si el sprite debe estar girado
        _spriteRenderer.flipX = _direction == 1 ? true : false;


    }

    private void turn()
    {
        if (_ray)
        {
            if (_ray.collider.CompareTag("Floor"))
            {
                _direction = -_direction;
            }
        }
        
    }

    private void moveFoward()
    {
        _rigidbody.AddForce(new Vector2(1*_speed*_direction,0));
    }

    
    

}
