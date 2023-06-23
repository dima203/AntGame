using UnityEngine;

public class PheromoneSpawner : MonoBehaviour
{
    [SerializeField] private Pheromone _homePheromone;
    [SerializeField] private Pheromone _foodPheromone;


    public void SpawnHomePheromone(Vector3 position, float distance)
    {
        Pheromone pheromone = Instantiate(_homePheromone, position, Quaternion.identity);
        pheromone.Initialize(distance);
    }
    
    public void SpawnFoodPheromone(Vector3 position, float distance)
    {
        Pheromone pheromone = Instantiate(_foodPheromone, position, Quaternion.identity);
        pheromone.Initialize(distance);
    }
}
