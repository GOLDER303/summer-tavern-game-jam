using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;

    private int score = 0;

    private void OnEnable()
    {
        Drone.OnDroneFreeingDockingStation += HandleDroneFreeingDockingStation;
    }

    private void OnDisable()
    {
        Drone.OnDroneFreeingDockingStation -= HandleDroneFreeingDockingStation;
    }

    private void HandleDroneFreeingDockingStation(Drone _)
    {
        score += 100;
        scoreText.text = "Score: " + score;
    }
}
