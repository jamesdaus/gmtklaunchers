using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class ScoreUpdater : MonoBehaviour
{

    public TextMeshProUGUI scoreGUI;

    public Interactable[] interactable;

    void Awake()
    {
        scoreGUI = FindObjectOfType<TextMeshProUGUI>().GetComponent<TextMeshProUGUI>();
        interactable = FindObjectsOfType<Interactable>();
        interactable = GetComponents<Interactable>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i <interactable.Length; i++)
        {
            scoreGUI.text = interactable[i].GetPoints().ToString();
            Debug.Log(scoreGUI);
        }


    }
}
