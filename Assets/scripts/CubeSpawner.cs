using UnityEngine;
using System.Collections.Generic;

public class CubeSpawner : MonoBehaviour
{
    private const int MinSplitValue = 2;
    private const int MaxSplitValue = 7;

    [SerializeField] private float _splitChance = 1f;
    [SerializeField] private Cube _cubePrefab;
    [SerializeField] private Exploder _exploder;
    [SerializeField] private ColorChanger _colorChanger;

    private void Start()
    {
        for (int i = 0; i < 3; i++)
        {
            SpawnCube(transform.position + Random.insideUnitSphere, _splitChance);
        }
    }

    private Cube SpawnCube(Vector3 position, float spawnChance)
    {
        Cube newCube = Instantiate(_cubePrefab, position, Random.rotation);
        newCube.Initialize(this, spawnChance);

        return _colorChanger.ChangeColor(newCube);
    }

    public void Split(Cube cube)
    {
        List<Cube> _explodableCube = new List<Cube>();

        float explosionMultiplier = 1;

        if (Random.value <= cube.SplitChance)
        {
            float newSpawnChance = cube.SplitChance * 0.5f;
            int splitCount = Random.Range(MinSplitValue, MaxSplitValue);

            for (int i = 0; i < splitCount; i++)
            {
                Vector3 spawnPosition = cube.transform.position + Random.insideUnitSphere * 0.5f;
                Cube newCube = SpawnCube(spawnPosition, newSpawnChance);
                newCube.transform.localScale = cube.transform.localScale * 0.5f;

                _explodableCube.Add(newCube);
            }
        }
        else
        {
            explosionMultiplier /= cube.SplitChance;
        }

        _exploder.Explode(cube.transform, _explodableCube, explosionMultiplier);
    }
}