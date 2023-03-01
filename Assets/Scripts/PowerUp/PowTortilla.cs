using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowTortilla : MonoBehaviour
{
    private const string PlayerTag = "Player";
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag(PlayerTag))
        {
            Destroy(this.gameObject);
        }
    }
}
