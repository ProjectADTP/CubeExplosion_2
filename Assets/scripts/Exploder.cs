using System.Collections.Generic;
using UnityEngine;
using System;

public class Exploder : MonoBehaviour
{
    const float MinForceValue = 1500f;
    const float MaxForceValue = 35000f;

    const float MinRadiusValue = 100f;
    const float MaxRadiusValue = 5000f;

    const float ExplosionForce = 1500f;
    const float ExplosionRadius = 100f;

    [SerializeField] private ParticleSystem _effect;

    public void Explode(Transform mainCube, List<Cube> cubes, float explosionScale)
    {
        float explosionForce = Math.Clamp(ExplosionForce * explosionScale, MinForceValue, MaxForceValue);
        float explosionRadius = Math.Clamp(ExplosionRadius * explosionScale, MinRadiusValue, MaxRadiusValue);

        List<Rigidbody> affectedObjects = new();

        Instantiate(_effect, mainCube.position, mainCube.rotation);

        if (cubes.Count > 0)
        {
            foreach (Cube cube in cubes)
            {
                if (cube.TryGetComponent(out Rigidbody rigidbody))
                {
                    affectedObjects.Add(rigidbody);
                }
            }
        }
        else
        {
            affectedObjects = GetExplodableObjects(explosionRadius);
        }

        foreach (Rigidbody rigidbody in affectedObjects)
        {
            rigidbody.AddExplosionForce(explosionForce, transform.position, explosionRadius);
        }
    }

    private List<Rigidbody> GetExplodableObjects(float explosionRadius)
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, explosionRadius);
        List<Rigidbody> explodableObjects = new();

        foreach (Collider hit in hits)
        {
            if (hit.attachedRigidbody != null)
            {
                explodableObjects.Add(hit.attachedRigidbody);
            }
        }

        return explodableObjects;
    }
}
