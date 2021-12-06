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

    [Header("Title")]
    [SerializeField] private TextMeshProUGUI titleText;

    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private float timerSpeedOffset = 1f;

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

        UpdateTestChapterTitle();
        DoTest();
    }

    private void Update()
    {
        UpdateTimer();
    }
    #region Testing
    #region  test with specific chapter.
    private void DoTest()
    {
        // !If the choosen index is -1. Do a SpecialTest
        if (choosenIndex == -1)
        {
            DoSpecialTest();
            return;
        }

        AddQuestionToMultipleChoiceContent(choosenIndex);


    }

    private void AddQuestionToMultipleChoiceContent(int chapterIndex)
    {
        // Get the chapter we want to test
        Chapter chapterList = userChapters.GetChapterList()[chapterIndex];

        // Loop through all the qeustions
        InitButtons(chapterList);
    }

    private void InitButtons(Chapter chapterList)
    {
        for (int i = 0; i < chapterList.questions.Length; i++)
        {
            // Create a multiple choices 
            GameObject go = Instantiate(multipleChoiceTemplate, Vector3.zero, Quaternion.identity);
            go.transform.SetParent(multipleChoiceContent, false);

            // Get the template component atached to the multiple choice template gameobject
            MultipleChoiceTemplate template = go.GetComponent<MultipleChoiceTemplate>();

            // Set the self index to the current index
            template.indexSelf = i;

            // Set the template question box
            template.questionBox.text = chapterList.questions[i].multipleChoiceObject.question;

            // Set the buttons 
            template.buttonAText.text = chapterList.questions[i].multipleChoiceObject.answerA;
            template.buttonBText.text = chapterList.questions[i].multipleChoiceObject.answerB;
            template.buttonCText.text = chapterList.questions[i].multipleChoiceObject.answerC;
            template.buttonDText.text = chapterList.questions[i].multipleChoiceObject.answerD;

            // Current chapter list quaestion
            Question currentQuestion = chapterList.questions[i];

            // Create a list of available button inside the template
            Button[] buttons = { template.buttonA, template.buttonB, template.buttonC, template.buttonD };

            // Loop through all the buttons 
            foreach (var button in buttons)
            {
                // Check if the button is the correct answer
                if (button.GetComponent<ChoiceButton>().answerButton == chapterList.questions[i].multipleChoiceObject.correctAnswer)
                {
                    // Add a special onclick
                    button.onClick.AddListener(() =>
                    {
                        CorrectAnswer(currentQuestion);
                        ChooseAnAsnwer(button, buttons, currentQuestion);
                    });

                }
                else
                    // Else add a wrong onclick
                    button.onClick.AddListener(() =>
                    {
                        WrongAnswer(currentQuestion);
                        ChooseAnAsnwer(button, buttons, currentQuestion);
                    });
            }

        }
    }
    #endregion
    public void ChooseAnAsnwer(Button button, Button[] buttons, Question question)
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

        //  Mark the question is checked
        question.isChecked = true;
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
        // Loop through all the chaptr and init the buttons
        foreach (var chapter in userChapters.GetChapterList())
        {
            InitButtons(chapter);
        }
    }
    #endregion

    private void UpdateTimer()
    {
        float timeText = Time.timeSinceLevelLoad * timerSpeedOffset;
        // int timeTextInt = Mathf.RoundToInt(timeText);
        string timeDisplayString = "";

        TimeSpan t = TimeSpan.FromSeconds(timeText);

        if (timeText < 24 * 3600)
        {
            timeDisplayString = t.ToString(@"hh\:mm\:ss");
        }
        else
        {
            timeDisplayString = t.ToString(@"dd\:hh\:mm\:ss");
        }


        timerText.text = timeDisplayString;

    }
    private void UpdateTestChapterTitle()
    {
        switch (choosenIndex)
        {
            case -1:
                titleText.text = "T Ấ T  C Ả";
                break;
            default:
                titleText.text = userChapters.GetChapterList()[choosenIndex].chapterName;
                break;
        }
    }
}