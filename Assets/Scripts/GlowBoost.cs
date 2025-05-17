using UnityEngine;

public class GlowBoost : MonoBehaviour
{
    [SerializeField] private Renderer[] renderers;
    [SerializeField] private Material glowMat;
    private Material[][] ogMat;

    void Awake()
    {
        ogMat = new Material[renderers.Length][];
        for (int i = 0; i < renderers.Length; i++)
        {
            ogMat[i] = renderers[i].materials;
        }
    }

    public void StartGlow(float duration)
    {
        foreach (var renderer in renderers)
        {
            var mat = renderer.materials;
            for (int i = 0; i < mat.Length; i++)
            {
                mat[i] = glowMat;
            }
            renderer.materials = mat;
        }

        Invoke(nameof(StopGlow), duration);  // stops the glow
    }

    public void StopGlow()
    {
        for (int i = 0; i < renderers.Length; i++)
        {
            renderers[i].materials = ogMat[i];
        }
    }
}
