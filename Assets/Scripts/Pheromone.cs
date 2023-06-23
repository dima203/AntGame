using UnityEngine;

public class Pheromone : MonoBehaviour
{
    public float Value { get; private set; } = 10f;
    public float Distance { get; private set; }

    [SerializeField] private float _outSpeed = 1f;


    public void Initialize(float distance)
    {
        Distance = distance;
    }

    private void Update()
    {
        Value -= _outSpeed * Time.deltaTime;

        if (Value <= 0)
            Destroy(gameObject);
    }
}
