using System;
using UnityEngine;

public class Home : MonoBehaviour
{
    public int Food { 
        get => _food; 
        private set
        {
            _food = value;
            EventManager.OnFoodChanged(_food);
        } 
    }

    [SerializeField] private AntMovement _antPrefab;
    [SerializeField] private int _foodForAnt = 10;

    private int _food = 0;


    public void AddFood(int count)
    {
        Food += count;
        TrySpawnAnt();
    }

    private void TrySpawnAnt()
    {
        if (Food <_foodForAnt) 
            return;

        Instantiate(_antPrefab, transform.position, Quaternion.identity);
        Food -= _foodForAnt;
    }
}
