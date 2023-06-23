using UnityEngine;

public class AntMovement : MonoBehaviour
{
    [SerializeField] private float _speed = 1f;
    [SerializeField] private PheromoneSpawner _pheromoneSpawner;
    [SerializeField] private float _pheromoneSpawnDelay = 1f;

    private bool _goHome = false;
    private bool _findTarget = false;
    private float _currentDistance = 0f;
    private float _currentTime = 0f;
    

    private void Update()
    {
        transform.Translate(new Vector3(0, 1, 0) * _speed * Time.deltaTime);
        _currentDistance += _speed * Time.deltaTime;
        if (!_findTarget)
            transform.Rotate(0, 0, Random.Range(-10f, 10f));

        _currentTime -= Time.deltaTime;
        print(_currentTime);
        if (_currentTime < 0) {
            if (_goHome)
                _pheromoneSpawner.SpawnFoodPheromone(transform.position, _currentDistance);
            else
                _pheromoneSpawner.SpawnHomePheromone(transform.position, _currentDistance);
            _currentTime = _pheromoneSpawnDelay;
        }
    }

    public void OnTriggerEnter2D(Collider2D collider)
    {
        if (!_goHome) {
            if (collider.TryGetComponent<Food>(out Food food)) {
                _findTarget = true;
                transform.rotation = Quaternion.FromToRotation(Vector3.up, collider.transform.position - transform.position);
            }
        }
    }

    public void OnTriggerExit2D(Collider2D collider)
    {
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.TryGetComponent<Food>(out Food food)) {
            if (!_goHome) {
                food.GetFood();
                _goHome = true;
                _findTarget = false;
            }
        }
    }
}
