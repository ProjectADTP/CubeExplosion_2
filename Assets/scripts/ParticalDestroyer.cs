using UnityEngine;

public class ParticalDestroyer : MonoBehaviour
{
    private void OnParticleSystemStopped()
    {
        Destroy(gameObject);
    }
}