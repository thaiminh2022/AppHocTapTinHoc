using System;
using System.Collections.Generic;

using UnityEngine;
using TMPro;
using UnityEngine.UI;

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
    [SerializeField] bool isTimerMoving = true;

    [Header("Colors")]
    [SerializeField] Color defaultTextColor;
    [SerializeField] Color defaultColor;
    [SerializeField] Color chosenAnswerColor;
    public Color correctAnswerColor;
    public Color wrongAnswerColor;

    [Header("Time")]
    [SerializeField] float timeDoingTest = 0;

    [Header("Game")]
    public bool isViewingAnswer = false;
    [SerializeField] GameObject noChapter;

    private void Start()
    {
        choosenIndex = GameManager.instance.choosenChapterIndex;

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
        if (choosenIndex > userChapters.GetChapterList().Count - 1)
        {
            NoChapter();
            return;
        }
        // !If the choosen index is -1. Do a SpecialTest
        if (choosenIndex == -1)
        {
            DoSpecialTest();
            return;
        }



        AddQuestionToMultipleChoiceContent(choosenIndex);


    }

    private void NoChapter()
    {
        noChapter.SetActive(true);
        LeanTweenManagers.instance.ScalingEntryOnCall(noChapter.transform);
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
        List<Question> questionsList = new List<Question>();

        // Add the questions to the question List
        foreach (var question in chapterList.questions)
        {
            questionsList.Add(question);
        }


        for (int io = 0; io < chapterList.questions.Length; io++)
        {
            // Random index
            int i = UnityEngine.Random.Range(0, questionsList.Count);
            Question choosenQuestion = questionsList[i];
            questionsList.RemoveAt(i);


            // Create a multiple choices 
            GameObject go = Instantiate(multipleChoiceTemplate, Vector3.zero, Quaternion.identity);
            go.transform.SetParent(multipleChoiceContent, false);

            // Get the template component atached to the multiple choice template gameobject
            MultipleChoiceTemplate template = go.GetComponent<MultipleChoiceTemplate>();

            // ! IDK ABOUT THIS: template.indexSelf = i;

            // Set the template question box
            template.questionBox.text = choosenQuestion.multipleChoiceObject.question;

            // Set the buttons 
            template.buttonAText.text = "A. " + choosenQuestion.multipleChoiceObject.answerA;
            template.buttonBText.text = "B. " + choosenQuestion.multipleChoiceObject.answerB;
            template.buttonCText.text = "C. " + choosenQuestion.multipleChoiceObject.answerC;
            template.buttonDText.text = "D. " + choosenQuestion.multipleChoiceObject.answerD;

            // Current chapter list quaestion
            Question currentQuestion = choosenQuestion;

            // Create a list of available button inside the template
            Button[] buttons = { template.buttonA, template.buttonB, template.buttonC, template.buttonD };

            // Loop through all the buttons 
            foreach (var button in buttons)
            {
                // Check if the button is the correct answer
                if (button.GetComponent<ChoiceButton>().answerButton == choosenQuestion.multipleChoiceObject.correctAnswer)
                {
                    // Add a special onclick
                    button.onClick.AddListener(() =>
                    {
                        CorrectAnswer(currentQuestion, button);
                        ChooseAnAsnwer(button, buttons, currentQuestion);
                    });

                }
                else
                    // Else add a wrong onclick
                    button.onClick.AddListener(() =>
                    {
                        WrongAnswer(currentQuestion, button);
                        ChooseAnAsnwer(button, buttons, currentQuestion);
                    });
            }

        }
    }
    #endregion
    public void ChooseAnAsnwer(Button button, Button[] buttons, Question question)
    {
        if (isViewingAnswer == true) return;

        // find the index of the curreent button
        int index = Array.FindIndex(buttons, item => item == button);
        button.GetComponentInParent<MultipleChoiceTemplate>().correctAnswer = question.multipleChoiceObject.correctAnswer;

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

        // Onclick behavior


        //  Mark the question is checked
        question.isChecked = true;
    }
    public void CorrectAnswer(Question question, Button button)
    {
        if (isViewingAnswer == true) return;

        question.AnswerIsRight = true;
        // button.GetComponentInParent<MultipleChoiceTemplate>().isTheCorrectButton = true;

        // Call whenever the play answer a question correct
        userChapters.OnPlayerRight();

    }
    public void WrongAnswer(Question question, Button button)
    {
        if (isViewingAnswer == true) return;

        question.AnswerIsRight = false;
        //  button.GetComponentInParent<MultipleChoiceTemplate>().isTheCorrectButton = false;
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
        if (isTimerMoving == false)
            return;

        timeDoingTest += Time.deltaTime * timerSpeedOffset;
        // int timeTextInt = Mathf.RoundToInt(timeText);
        string timeDisplayString = "";

        TimeSpan t = TimeSpan.FromSeconds(timeDoingTest);

        if (timeDoingTest < 3600)
        {
            timeDisplayString = t.ToString(@"mm\:ss");
        }
        else if (timeDoingTest > 3600 && timeDoingTest < 24 * 3600)
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
        if (choosenIndex > userChapters.GetChapterList().Count - 1)
            return;
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


    #region GetSet
    public string GetPlayerDoingTestTimeString()
    {
        TimeSpan t = TimeSpan.FromSeconds(timeDoingTest);

        if (timeDoingTest > 24 * 3600)
        {
            return t.ToString(@"dd\:hh\:mm\:ss");
        }
        else
        {
            return t.ToString(@"hh\:mm\:ss");
        }
    }
    public Transform GetScrollContent()
    {
        return multipleChoiceContent;
    }

    public void SetIsTimerMoving(bool isIt)
    {
        isTimerMoving = isIt;
    }
    #endregion
}