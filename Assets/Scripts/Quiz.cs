using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Text;

public class Quiz : MonoBehaviour
{
    [Header("Questions")]
    [SerializeField] TextMeshProUGUI questionText;
    [SerializeField] List<QuestionSO> questions = new List<QuestionSO>();
    QuestionSO currentQuestion;

    [Header("Answers")]
    [SerializeField] GameObject[] answerButtons;
    int correctAnswerIndex;
    bool hasAnsweredEarly = true;

    [Header("Button Colors")]
    [SerializeField] Sprite deafultAnswerSprite;
    [SerializeField] Sprite correctAnswerSprite;

    [Header("Timer")]
    [SerializeField] Image timerImage;
    Timer timer;

    [Header("Scoring")]
    [SerializeField] TextMeshProUGUI scoreText;
    ScoreKeeper scoreKeeper;



    void Awake()
    {
        timer = FindObjectOfType<Timer>();
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
    }

    void Update()
    {
        timerImage.fillAmount = timer.fillFraction;
        if (timer.loadNextQuestion)
        {
            hasAnsweredEarly = false;
            GetNextQuestion();
            timer.loadNextQuestion = false;
        }
        else if (!hasAnsweredEarly && !timer.isAnsweringQuestion) 
        {
            DisplayAnswer(-1);
            SetButtonState(false);
        }
    }

    public void OnAnswerSelected(int index) 
    {
        hasAnsweredEarly = true;
        DisplayAnswer(index);
        HighLightIncorrectChoice(index);
        SetButtonState(false);
        timer.CancelTimer();
        scoreText.text = "Ranga: " + scoreKeeper.CalculateDivision();
    }

    void DisplayAnswer(int index) 
    {
        Image buttonImage;
        

        if (index == currentQuestion.GetCorrectAnswerIndex())
        {
            questionText.text = "Dobrze!";
            buttonImage = answerButtons[index].GetComponent<Image>();
            buttonImage.sprite = correctAnswerSprite;
            scoreKeeper.IncrementCorrectAnswers(currentQuestion);
            Debug.Log(scoreKeeper.GetCorrectAnswers());
            Debug.Log(currentQuestion.GetDifficulty());
        }
        else
        {
            correctAnswerIndex = currentQuestion.GetCorrectAnswerIndex();
            string correctAnswer = currentQuestion.GetAnswer(correctAnswerIndex);
            questionText.text = "Zle xdd Poprawna odpowiedü to:\n" + correctAnswer;
            buttonImage = answerButtons[correctAnswerIndex].GetComponent<Image>();           
            buttonImage.sprite = correctAnswerSprite;        
        }
    }

    void HighLightIncorrectChoice(int index) 
    {

        if (index != currentQuestion.GetCorrectAnswerIndex())
        {
            Image wrongButtonImage;

            wrongButtonImage = answerButtons[index].GetComponent<Image>();
            wrongButtonImage.color = Color.red;
        }
    }

    void GetNextQuestion() 
    {
        if (questions.Count > 0)
        {
            SetButtonState(true);
            SetDeafultButtonSprites();
            GetRandomQuestion();
            DisplayQuestion();
        }
        else 
        {
            throw new KeyNotFoundException();
        }
    }

    void GetRandomQuestion()
    {
        int index = Random.Range(0, questions.Count);
        currentQuestion = questions[index];

        if (questions.Contains(currentQuestion))
        {
            questions.Remove(currentQuestion);
        }
 
    }

    void DisplayQuestion() 
    {
        questionText.text = currentQuestion.GetQuestion();

        for (int i = 0; i < answerButtons.Length; i++)
        {
            TextMeshProUGUI buttonText = answerButtons[i].GetComponentInChildren<TextMeshProUGUI>();
            buttonText.text = currentQuestion.GetAnswer(i);
        }
    }

    void SetButtonState(bool state) 
    {
        for (int i = 0; i < answerButtons.Length; i++) 
        {
            Button button = answerButtons[i].GetComponent<Button>();
            button.interactable = state;
        }
    }

    void SetDeafultButtonSprites()
    {
        for (int i = 0; i < answerButtons.Length; i++)
        {
            Image buttonImage = answerButtons[i].GetComponent<Image>();
            buttonImage.sprite = deafultAnswerSprite;
            buttonImage.color = Color.white;
        }
    }
}
