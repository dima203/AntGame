using UnityEngine;


public class Food : MonoBehaviour
{
    [SerializeField] private int _count = 1;


    public int GetFood()
    {
        _count--;
        if (_count == 0)
            Destroy(gameObject);
        return 1;
    }
}
