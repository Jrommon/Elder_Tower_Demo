using UnityEngine;

public class PowDoubleJump : MonoBehaviour
{
    [SerializeField] private string PlayerTag = "Player";
    private void OnTriggerEnter2D(Collider2D col)
    {
        Debug.Log($"Tag del jugador: {PlayerTag} \n Tag del colisionador: {col.tag}");
        if (col.CompareTag(PlayerTag))
        {
            Destroy(this.gameObject);
        }
        
        //Habilitar en esta seccion el flag de salto doble.
    }
}