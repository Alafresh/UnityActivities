using UnityEngine;

public class ZombieSpawner : MonoBehaviour
{
    [SerializeField] private ZombieTarget _zombiePrefab;
    [SerializeField] private int _spawnAmount = 1;

    private void Start()
    {
        InvokeRepeating("Spawn", 2f, 2f);
    }

    private void Spawn()
    {
        for (var i = 0; i < _spawnAmount; i++)
        {
            var zombie = Instantiate(_zombiePrefab, transform.position, Quaternion.identity);
        }
    }

}
