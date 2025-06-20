using UnityEngine;

public class DoublePointsPickUp1 : MonoBehaviour
{
    [SerializeField] private float multiplier = 2f;

    [SerializeField] private float multiplierDuration = 8f;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player"))
            return;

        ScoreManager.Instance.StartMultiplier(multiplier, multiplierDuration);
        Debug.Log("Double points picked up");

        GlowBoost glow = other.GetComponent<GlowBoost>();
        if (glow != null)
        {
            glow.StartGlow(duration: multiplierDuration);
            Debug.Log("Glow started");
        }


        Destroy(gameObject);
    }
}
