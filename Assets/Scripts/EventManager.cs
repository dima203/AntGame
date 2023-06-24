using System;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public static event Action<int> FoodChanged;


    public static void OnFoodChanged(int value)
    {
        FoodChanged?.Invoke(value);
    }
}
