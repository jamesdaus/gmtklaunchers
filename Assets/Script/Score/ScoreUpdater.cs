using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class ScoreUpdater : MonoBehaviour
{

    public TextMeshProUGUI scoreGUI;

    public int currentScore = 0;

    void Awake()
    {
        scoreGUI = FindObjectOfType<TextMeshProUGUI>().GetComponent<TextMeshProUGUI>();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        scoreGUI.text = "Score: " + currentScore;
        //Debug.Log(currentScore);
    }
}
