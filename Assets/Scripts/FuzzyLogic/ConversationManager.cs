using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConversationManager : MonoBehaviour
{
    [Header("UI")]
    [SerializeField]
    private GameObject questionPanel;
    [SerializeField]
    private GameObject resultPanel;
    [SerializeField]
    private Text resultText;
    [SerializeField]
    private Text questionText;
    [SerializeField]
    private Button AnswerButton1;
    [SerializeField]
    private Button AnswerButton2;
    [SerializeField]
    private Button AnswerButton3;

    [Header("Morality Gradient")]
    [SerializeField]
    private AnimationCurve good; // At t=0 Good value = 1 & decreases till t=50, v=0
    [SerializeField]
    private AnimationCurve neutral; // At t=50 Neutral value = 1, it rises & falls t=0, v=0; t=100, v=0
    [SerializeField]
    private AnimationCurve evil; // At t=100 Evil value = 1

    [Header("Questions")]
    [SerializeField]
    private Question[] questions;

    private int questionIndex = 0;
    private float answerTotal = 0f;

    private void LoadQuestion(int index)
    {
        if (index < 0 || index >= questions.Length) 
        { 
            EndConversation();
            return;
        }

        questionText.text = questions[index].questionText;
        AnswerButton1.GetComponentInChildren<Text>().text = questions[index].answers[0].answerText;
        AnswerButton2.GetComponentInChildren<Text>().text = questions[index].answers[1].answerText;
        AnswerButton3.GetComponentInChildren<Text>().text = questions[index].answers[2].answerText;
    }

    public void OnAnswerSubmitted(int answerIndex)
    {
        answerTotal += questions[questionIndex].answers[answerIndex].moralityValue;
        questionIndex++;
        LoadQuestion(questionIndex);
    }

    // Start is called before the first frame update
    void Start()
    {
        LoadQuestion(questionIndex);
    }

    private void EndConversation()
    {
        questionPanel.SetActive(false);

        float average = answerTotal / questions.Length;
        float goodRating = good.Evaluate(average);
        float neutralRating = neutral.Evaluate(average);
        float evilRating = evil.Evaluate(average);

        Debug.Log(average);

        string alignmentText = (goodRating > neutralRating) ? ((goodRating > evilRating) ? "Good" : "Evil") : ((neutralRating > evilRating) ? "Neutral" : "Evil");

        resultPanel.SetActive(true);
        resultText.text = "Your morality alignment is: " + alignmentText;
    }
}
