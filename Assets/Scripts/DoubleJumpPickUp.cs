using UnityEngine;

public class DoubleJumpPickUp : MonoBehaviour
{
    [SerializeField] private float doubleJumpForce = 800f;
    [SerializeField] private float jumpDuration = 5f;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player"))
            return;
        
        PlayerController player = other.GetComponent<PlayerController>();
        
        if (player != null)
        {
            player.StartDoubleJump(doubleJumpForce, jumpDuration);
        }

        Destroy(gameObject);
    }
}
