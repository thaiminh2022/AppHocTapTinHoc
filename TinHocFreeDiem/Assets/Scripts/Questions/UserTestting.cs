using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class UserTestting : MonoBehaviour
{

    [Header("Multiple Choice")]
    [SerializeField] GameObject multipleChoiceTemplate;
    [SerializeField] Transform multipleChoiceContent;

    [SerializeField] int choosenIndex;
    private UserChapters userChapters;

    [Header("Colors")]
    [SerializeField] Color defaultTextColor;
    [SerializeField] Color defaultColor;
    [SerializeField] Color chosenAnswerColor;
    [SerializeField] Color correctAnswerColor;
    [SerializeField] Color wrongAnswerColor;

    private void Start()
    {
        choosenIndex = GameManager.instance.ChoosenChapterIndex;

        userChapters = GetComponent<UserChapters>();

        DoTest();
    }

    #region  test with specific chapter.
    private void DoTest()
    {
        // !If the choosen index is -1. Do a SpecialTest
        if (choosenIndex == -1)
        {
            DoSpecialTest();
            return;
        }


        string message = "Today we are doing: " + userChapters.GetChapterList()[choosenIndex].chaperName;
        Debug.Log(message);

        AddQuestionToMultipleChoiceContent(choosenIndex);


    }

    private void AddQuestionToMultipleChoiceContent(int chapterIndex)
    {
        // Get the chapter we want to test
        Chaper chapterList = userChapters.GetChapterList()[chapterIndex];

        // Loop through all the qeustions
        for (int indexer = 0; indexer < chapterList.questions.Length; indexer++)
        {
            // Create a multiple choices 
            GameObject go = Instantiate(multipleChoiceTemplate, Vector3.zero, Quaternion.identity);
            go.transform.SetParent(multipleChoiceContent, false);

            // Get the template component atached to the multiple choice template gameobject
            MultipleChoiceTemplate template = go.GetComponent<MultipleChoiceTemplate>();

            // Set the self index to the current index
            template.indexSelf = indexer;

            // Set the template question box
            template.questionBox.text = chapterList.questions[indexer].multipleChoiceObject.question;

            // Set the buttons 
            template.buttonAText.text = chapterList.questions[indexer].multipleChoiceObject.answerA;
            template.buttonBText.text = chapterList.questions[indexer].multipleChoiceObject.answerB;
            template.buttonCText.text = chapterList.questions[indexer].multipleChoiceObject.answerC;
            template.buttonDText.text = chapterList.questions[indexer].multipleChoiceObject.answerD;

            // Current chapter list quaestion
            Question currentQuestion = chapterList.questions[indexer];

            // Create a list of available button inside the template
            Button[] buttons = { template.buttonA, template.buttonB, template.buttonC, template.buttonD };

            // Loop through all the buttons 
            foreach (var button in buttons)
            {
                // Check if the button is the correct answer
                if (button.GetComponent<ChoiceButton>().answerButton == chapterList.questions[indexer].multipleChoiceObject.correctAnswer)
                {
                    // Add a special onclick
                    button.onClick.AddListener(() =>
                    {
                        CorrectAnswer(currentQuestion);
                        ChooseAnAsnwer(button, buttons);
                    });

                }
                else
                    // Else add a wrong onclick
                    button.onClick.AddListener(() =>
                    {
                        WrongAnswer(currentQuestion);
                        ChooseAnAsnwer(button, buttons);
                    });
            }

        }
    }
    #endregion 
    public void ChooseAnAsnwer(Button button, Button[] buttons)
    {
        // find the index of the curreent button
        int index = Array.FindIndex(buttons, item => item == button);

        // Loop through all the buttons ref
        for (int i = 0; i < buttons.Length; i++)
        {
            // if the current selectbutton is = to index. Set the color
            if (i == index)
            {
                buttons[i].GetComponent<Outline>().effectColor = chosenAnswerColor;
                buttons[i].GetComponentInChildren<TextMeshProUGUI>().color = chosenAnswerColor;
            }
            else
            {
                // Else set others back to default color.
                buttons[i].GetComponent<Outline>().effectColor = defaultColor;
                buttons[i].GetComponentInChildren<TextMeshProUGUI>().color = defaultTextColor;
            }
        }

    }
    public void CorrectAnswer(Question question)
    {
        question.AnswerIsRight = true;

        // Call whenever the play answer a question correct
        userChapters.OnPlayerRight();

    }
    public void WrongAnswer(Question question)
    {
        question.AnswerIsRight = false;

        // Call whenever the play answer a question wrong
        userChapters.OnPlayerWrong();
    }

    private void DoSpecialTest()
    {
        Debug.Log("This is a special test");
    }
}