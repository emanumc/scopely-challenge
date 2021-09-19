using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{
    [SerializeField] private Health _health;
    [SerializeField] private Image _topImage;
    [SerializeField] private Image _bottomImage;

    private void Update()
    {
        if (_health.Value < _health.MaxValue)
        {
            _topImage.enabled = true;
            _bottomImage.enabled = true;

            _topImage.fillAmount = Mathf.InverseLerp(0f, _health.MaxValue, _health.Value);
        }
        else
        {
            _topImage.enabled = false;
            _bottomImage.enabled = false;
        }
    }
}
