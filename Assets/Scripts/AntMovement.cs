using UnityEngine;

public class AntMovement : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _rigidbody;
    [SerializeField] private float _speed = 1f;
    [SerializeField] private PheromoneSpawner _pheromoneSpawner;
    [SerializeField] private float _pheromoneSpawnDelay = 1f;

    private bool _goHome = false;
    private GameObject _target = null;
    private float _bestPheromoneDistance = -1f;
    private float _currentDistance = 0f;
    private float _currentTime = 0f;
    

    private void Update()
    {
        _rigidbody.velocity = transform.up * _speed;
        _currentDistance += _speed * Time.deltaTime;
        if (_target == null)
            transform.Rotate(0, 0, Random.Range(-1f, 1f));

        _currentTime -= Time.deltaTime;
        if (_currentTime < 0) {
            if (_goHome)
                _pheromoneSpawner.SpawnFoodPheromone(transform.position, _currentDistance);
            else
                _pheromoneSpawner.SpawnHomePheromone(transform.position, _currentDistance);
            _currentTime = _pheromoneSpawnDelay;
        }
    }

    public void OnTriggerStay2D(Collider2D collider)
    {
        if (!_goHome) {
            if (_target != null)
                return;
            if (collider.TryGetComponent<Food>(out Food food)) {
                _target = collider.gameObject;
                transform.rotation = Quaternion.FromToRotation(Vector3.up, collider.transform.position - transform.position);
                _bestPheromoneDistance = -1f;
            }
            if (_target != null) 
                return;
            if (collider.gameObject.tag == "FoodPheromone") {
                Pheromone pheromone = collider.GetComponent<Pheromone>();
                if (_bestPheromoneDistance == -1f || _bestPheromoneDistance > pheromone.Distance) {
                    _bestPheromoneDistance = pheromone.Distance;
                    transform.rotation = Quaternion.FromToRotation(Vector3.up, pheromone.transform.position - transform.position);
                }
            }
        }
        else {
            if (collider.TryGetComponent<Home>(out Home home)) {
                _target = collider.gameObject;
                transform.rotation = Quaternion.FromToRotation(Vector3.up, collider.transform.position - transform.position);
                _bestPheromoneDistance = -1f;
            }
            if (_target != null) 
                return;
            if (collider.gameObject.tag == "HomePheromone") {
                Pheromone pheromone = collider.GetComponent<Pheromone>();
                if (_bestPheromoneDistance == -1f || _bestPheromoneDistance > pheromone.Distance) {
                    _bestPheromoneDistance = pheromone.Distance;
                    transform.rotation = Quaternion.FromToRotation(Vector3.up, pheromone.transform.position - transform.position);
                }
            }
        }
    }

    public void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.TryGetComponent<Food>(out Food food)) {
            if (!_goHome) {
                food.GetFood();
                _goHome = true;
                _target = null;
                _currentDistance = 0f;
            }
        }
        if (collision.collider.TryGetComponent<Home>(out Home home)) {
            if (_goHome) {
                home.AddFood(1);
                _goHome = false;
                _target = null;
                _currentDistance = 0f;
            }
        }
        
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Border") {
            transform.Rotate(new Vector3(0, 0, 180));
        }
    }
}
