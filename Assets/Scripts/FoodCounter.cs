using TMPro;
using UnityEngine;

public class FoodCounter : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _text;


    private void Start()
    {
        EventManager.FoodChanged += UpdateCount;
    }

    public void UpdateCount(int food)
    {
        _text.text = food.ToString();
    }

    private void OnDestroy()
    {
        EventManager.FoodChanged -= UpdateCount;
    }
}
