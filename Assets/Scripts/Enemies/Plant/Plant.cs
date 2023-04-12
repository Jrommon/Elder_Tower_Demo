using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plant : EnemiesGeneral
{

    [Header("Plant Settings")]

    //Tiempo que tiene que transcurrir para que dispare
    [SerializeField] private float secondsToShoot=3;
    //Objeto disparado
    [SerializeField] private GameObject bullet;
    

    //Tiempo transcurrido entre disparos
    private float secondsTillShoot;



    private void Start()
    {
        secondsTillShoot = secondsToShoot;   
    }
    private void Update()
    {
        lookAtTarget();
        if (secondsTillShoot > 0)
        {
            secondsTillShoot -= 1 * Time.deltaTime;
        }
        else
        {
            _animator.SetTrigger("Shoot trigger");
            secondsTillShoot = secondsToShoot;
        }
    }

    //Instancia  una bala, se ejecuta al final de la animaci�n
    public void shoot()
    {
        Instantiate(bullet, transform.position, transform.rotation);
    }

    //Hace una animaci�n de ataque a melee cuando colisiona con el player
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("PlayerAttack"))
        {
            _animator.SetTrigger("Melee trigger");
        }
    }

    //Hace que mire a un objetivo
    
    
}
