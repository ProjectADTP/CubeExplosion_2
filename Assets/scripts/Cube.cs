using UnityEngine;

public class Cube : MonoBehaviour
{
    [SerializeField] private float _splitChance = 1f;

    private CubeSpawner _cubeSpawner;

    public float SplitChance => _splitChance; 

    public void Initialize(CubeSpawner cubeSpawner, float spawnChance)
    {
        _cubeSpawner = cubeSpawner;
        _splitChance = spawnChance;
    }

    private void OnMouseUpAsButton()
    {
        _cubeSpawner.Split(this);

        Destroy(gameObject);
    }
}
