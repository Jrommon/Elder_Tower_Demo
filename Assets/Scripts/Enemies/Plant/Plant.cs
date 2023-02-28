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
    //Objetivo del look at target
    [SerializeField] private Transform target;

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

    //Instancia  una bala, se ejecuta al final de la animación
    public void shoot()
    {
        Instantiate(bullet, transform.position, transform.rotation);
    }

    //Hace una animación de ataque a melee cuando colisiona con el player
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            _animator.SetTrigger("Melee trigger");
        }
    }

    //Hace que mire a un objetivo
    private void lookAtTarget()
    {
        if(target.position.x > transform.position.x)
        {
            transform.rotation = new Quaternion(transform.rotation.x, 180, transform.rotation.z,0);
        }
        else
        {
            transform.rotation = new Quaternion(transform.rotation.x, 0, transform.rotation.z, 0);
        }
    }
}
