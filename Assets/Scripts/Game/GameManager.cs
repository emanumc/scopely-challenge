using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject _winPopup;
    [SerializeField] private GameObject _losePopup;
    [SerializeField] private Health _baseHealth;
    [SerializeField] private CreepSpawner _creepSpawner;

    private void Start()
    {
        StartCoroutine(GameRunning());
    }

    private IEnumerator GameRunning()
    {
        bool isGameRunning = true;
        while (isGameRunning)
        {
            bool enemiesAreAlive = !_creepSpawner.FinishSpawning || 
                _creepSpawner.Enemies.Count > 0;

            isGameRunning = _baseHealth.Value > 0 && enemiesAreAlive;
            yield return null;
        }

        // Game over
        if (_baseHealth.Value <= 0)
        {
            _losePopup.SetActive(true);
            Time.timeScale = 0f;
        }
        else
        {
            _winPopup.SetActive(true);
            Time.timeScale = 0f;
        }
    }
}
