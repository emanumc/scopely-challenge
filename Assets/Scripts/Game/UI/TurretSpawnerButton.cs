using UnityEngine;
using UnityEngine.UI;

public class TurretSpawnerButton : MonoBehaviour
{
    [SerializeField] private TurretSpawnerUI _turretSpawnerUI;
    [SerializeField] private TurretType _turretType;
    [SerializeField] private GameObject _turretPreview;
    [SerializeField] private int _price;
    [SerializeField] private Text _buttonLabel;

    private void Start()
    {
        _buttonLabel.text = $"Buy {_turretType} Turret ({_price})";
    }

    public void ShowPreview()
    {
        _turretSpawnerUI.MovePreview(_turretType, _turretPreview, _price);
    }
}
