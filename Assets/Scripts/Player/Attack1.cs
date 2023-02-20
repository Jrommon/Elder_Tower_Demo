using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack1 : MonoBehaviour
{
    public delegate void OnAttack11EnterDelegate(Collider2D col);
    public delegate void OnAttack12EnterDelegate(Collider2D col);

    public event OnAttack11EnterDelegate OnAttack11;
    public event OnAttack11EnterDelegate OnAttack12;

    private void OnTriggerEnter2D(Collider2D col)
    {
        OnAttack11(col);
    }
}
