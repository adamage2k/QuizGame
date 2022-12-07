using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EndScreen : MonoBehaviour
{

    [SerializeField] TextMeshProUGUI finalScoreText;
    ScoreKeeper scoreKeeper;

    void Awake()
    {
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
    }

    public void ShowFianlScore() 
    {
        finalScoreText.text = "Gratulacje!\n Twoja ranga w tym sezonie to: " + scoreKeeper.CalculateDivision();
    }
}
