using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField, Min(0)] private int _maxValue;
    public int Value { get; private set; }
    public int MaxValue => _maxValue;

    private void Awake()
    {
        Value = MaxValue;
    }

    public void Restore(int health)
    {
        if (enabled)
        {
            Value = Mathf.Min(Value + health, MaxValue);
        }
    }

    public void ApplyDamage(int damage)
    {
        if (enabled)
        {
            Value = Mathf.Max(Value - damage, 0);
        }
    }
}
