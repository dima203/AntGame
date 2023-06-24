using UnityEngine;

public class Pheromone : MonoBehaviour
{
    public float Value { get; private set; } = 20f;
    public float Distance { get; private set; }

    [SerializeField] private float _outSpeed = 1f;
    [SerializeField] private CircleCollider2D _collider;


    public void Initialize(float distance)
    {
        Distance = distance;
        CheckMerge();
    }

    public void Initialize(Pheromone pheromone1, Pheromone pheromone2)
    {
        Distance = (pheromone1.Distance + pheromone2.Distance) / 2;
        Value = pheromone1.Value + pheromone2.Value;
        Destroy(pheromone1.gameObject);
        Destroy(pheromone2.gameObject);
        CheckMerge();
    }

    private void Update()
    {
        Value -= _outSpeed * Time.deltaTime;

        if (Value <= 0)
            Destroy(gameObject);
    }

    public void CheckMerge()
    {
        Pheromone pheromone = null;
        foreach (Collider2D collider in Physics2D.OverlapCircleAll(transform.position, _collider.radius / 2)) {
            if (collider.TryGetComponent<Pheromone>(out pheromone)) {
                if (pheromone != this && pheromone.tag == tag)
                    break;
                else
                    pheromone = null;
            }
        }
        if (pheromone == null) 
            return;

        Vector3 averagePosition = (transform.position + pheromone.transform.position) / 2;
        float distance = (Distance + pheromone.Distance) / 2;
        float value = Value + pheromone.Value;
        Pheromone newPheromone = SpawnMergedPheromone(averagePosition);
        _collider.enabled = false;
        pheromone._collider.enabled = false;
        newPheromone.Initialize(this, pheromone);
    }

    private Pheromone SpawnMergedPheromone(Vector3 position)
    {
        Pheromone pheromone = Instantiate(gameObject, position, Quaternion.identity).GetComponent<Pheromone>();
        return pheromone;
    }
}
