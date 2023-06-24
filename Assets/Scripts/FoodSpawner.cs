using UnityEngine;

public class FoodSpawner : MonoBehaviour
{
    [SerializeField] private MeshCollider _spawnArea;
    [SerializeField] private Food _foodPrefab;
    [SerializeField] private float _spawnInterval = 1f;


    private float _currentTime = 0f;


    private void Update()
    {
        _currentTime -= Time.deltaTime;
        if (_currentTime < 0) {
            _currentTime = _spawnInterval;
            Spawn();
        }
    }

    private void Spawn()
    {
        float x = Random.Range(_spawnArea.bounds.min.x, _spawnArea.bounds.max.x);
        float y = Random.Range(_spawnArea.bounds.min.y, _spawnArea.bounds.max.y);

        Instantiate(_foodPrefab, new Vector2(x, y), Quaternion.identity);
    }
}
