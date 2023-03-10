using UnityEngine;

public class PowDoubleJump : MonoBehaviour
{
    private const string PlayerTag = "Player";
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag(PlayerTag))
        {
            col.gameObject.GetComponent<PlayerMovement>().JumpPowerUp = true;
            
            Destroy(this.gameObject);
        }
    }
}