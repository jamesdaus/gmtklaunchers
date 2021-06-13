using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class ScoreUpdater : MonoBehaviour
{

    public TextMeshProUGUI[] scoreGUI;
    public TextMeshProUGUI timertext;
    private Spawner spawner;

    public int currentScore = 0;
    private int currentTime = 0;

    void Awake()
    {
        spawner = FindObjectOfType<Spawner>();
    }

    void Update()
    {
        scoreGUI[1].text = "Score: " + currentScore;
        scoreGUI[0].text = "Score: " + currentScore;
        timertext.text = "Time Left: " + (int)spawner.game_timer;
    }
}
