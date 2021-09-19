using System;
using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject _losePopup;
    [SerializeField] private Health _baseHealth;

    private void Start()
    {
        StartCoroutine(GameRunning());
    }

    private IEnumerator GameRunning()
    {
        bool isGameRunning = true;
        while (isGameRunning)
        {
            isGameRunning = _baseHealth.Value > 0;
            yield return null;
        }

        // Game over
        if (_baseHealth.Value <= 0)
        {
            _losePopup.SetActive(true);
        }
    }
}
