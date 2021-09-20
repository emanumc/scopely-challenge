using UnityEngine;

public class Wallet : MonoBehaviour
{
    [SerializeField] private int _startValue;

    private void Awake()
    {
        Value = _startValue;
    }

    public int Value { get; private set; }

    public void Add(int value)
    {
        Value += value;
    }

    public bool CanSubtract(int value)
    {
        return Value >= value;
    }

    public void Subtract(int value)
    {
        Value = Mathf.Max(Value - value, 0);
    }
}
