using TMPro;
using UnityEngine;

public class CreepSpawnerUI : MonoBehaviour
{
    [SerializeField] private CreepSpawner _creepSpawner;
    [SerializeField] private TMP_Text _waveLabel;

    private void Update()
    {
        if (_creepSpawner.CurrentWave != null)
        {
            _waveLabel.text = _creepSpawner.CurrentWave.name;
        }
    }
}
