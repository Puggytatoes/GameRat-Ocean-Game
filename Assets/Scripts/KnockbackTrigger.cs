using UnityEngine;

public class KnockbackTrigger : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D other)
    {
        var player = other.collider.GetComponent<MouseMovement>();
        if (player != null)
        {
            Health.RemoveHeart();
            player.Knockback(transform);
        }
    }
}
