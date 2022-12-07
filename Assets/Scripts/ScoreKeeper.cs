using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreKeeper : MonoBehaviour
{

    int correctAnswers = 0;

    public int GetCorrectAnswers() 
    {
        return correctAnswers;
    }

    public void IncrementCorrectAnswers(QuestionSO question) 
    {
        int difficulty = question.GetDifficulty();

        if (difficulty == 1)
        {
            correctAnswers += 11;
        }
        else if (difficulty == 2)
        {
            correctAnswers += 15;
        }
        else if (difficulty == 3) 
        {
            correctAnswers += 19;
        }
    }

    public string CalculateDivision() 
    {

        string divison;

        if (correctAnswers == 0 || correctAnswers < 12)
        {
            divison = "Br¹z";
            return divison;
        }
        else if (correctAnswers > 12 && correctAnswers <= 30)
        {
            divison = "Silver";
            return divison;
        }
        else if (correctAnswers > 30 && correctAnswers <= 80)
        {
            divison = "Grandmaster";
            return divison;
        }
        else if (correctAnswers > 80 && correctAnswers <= 150)
        {
            divison = "Platyna";
            return divison;
        }
        else if (correctAnswers > 150 && correctAnswers <= 210)
        {
            divison = "Diament";
            return divison;
        }
        else if (correctAnswers > 210 && correctAnswers <= 280)
        {
            divison = "Master";
            return divison;
        }
        else if (correctAnswers > 280 && correctAnswers <= 360) 
        {
            divison = "xxx";
            return divison;
        }
        else
        {
            return "Error with score calculation";
        }
    }
}
