using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class movementDebug : MonoBehaviour
{
    //Este script es un movimiento simple en 8 direcciones que se usa con WASD.
    //Se creo con intención de testear colisiones entre objetos y demás interacciones.
    private Rigidbody2D rb;
    private Vector2 moveInput;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        Getmovement();
        Move();
    }

    private void Getmovement()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");
        moveInput = new Vector2(moveX, moveY);
    }

    private void Move()
    {
        rb.MovePosition(rb.position + moveInput * 3 * Time.deltaTime);
    }
}
