using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    Quiz quiz;
    EndScreen endScreen;
    ScoreKeeper scoreKeeper;
    MainMenu mainMenu;

    private void Awake()
    {
        quiz = FindObjectOfType<Quiz>();
        endScreen = FindObjectOfType<EndScreen>();
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
        mainMenu = FindObjectOfType<MainMenu>();
    }

    private void Start()
    {
        mainMenu.gameObject.SetActive(true);
        quiz.gameObject.SetActive(false);
        endScreen.gameObject.SetActive(false);
    }

    private void Update()
    {
        string division = scoreKeeper.CalculateDivision();

        if (division == "Grandmaster") 
        {
            quiz.gameObject.SetActive(false);
            endScreen.gameObject.SetActive(true);
            endScreen.ShowFianlScore();
        }
    }

    public void OnReplayLevel() 
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void OnPlayGame() 
    {
        mainMenu.gameObject.SetActive(false);
        quiz.gameObject.SetActive(true);
        endScreen.gameObject.SetActive(false);
    }

    public void OnClickExit() 
    {
        Application.Quit();
    }
}
