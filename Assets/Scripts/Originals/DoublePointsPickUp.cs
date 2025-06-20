using System;
using UnityEngine;

public class DoublePointsPickUp : MonoBehaviour
{
    [SerializeField] private float multiplier = 2f;

    [SerializeField] private float multiplierDuration = 5f;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player"))
            return;
        
        ScoreManager.Instance.StartMultiplier(multiplier, multiplierDuration);
        Debug.Log("Double points picked up");
        
        Destroy(gameObject);
    }
}
