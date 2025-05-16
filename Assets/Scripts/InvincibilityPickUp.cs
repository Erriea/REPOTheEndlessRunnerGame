using System;
using System.Collections;
using UnityEngine;
using Object = UnityEngine.Object;

public class InvincibilityPickUp : MonoBehaviour
{
    [SerializeField] private float invincDuration = 8f;
    
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player"))
            return;

        PlayerController player = other.GetComponent<PlayerController>();

        if (player != null)
        {
            player.StartInvincibility(invincDuration); // 💡 Correctly trigger it here
        }

        Destroy(gameObject);
        
    }
    
    
}
