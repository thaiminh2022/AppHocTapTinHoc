using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SubmitAnswer : MonoBehaviour
{
    private UserChapters userChapter;
    private UserTestting userTestting;

    [Header("Counters")]
    [SerializeField] private int thisSectionCorrectAnswer = 0;
    [SerializeField] private int thisSectionWrongAnswer = 0;
    [SerializeField] private int thisSectionAlreadyAnswered = 0;
    [SerializeField] private int thisSectionNotAnswered = 0;

    [Header("Useful UI")]
    [SerializeField] TextMeshProUGUI alreadyAnsweredText;
    [SerializeField] TextMeshProUGUI hasNotAnsweredText;
    [SerializeField] Image fillImage;
    [SerializeField] TextMeshProUGUI fillText;

    [Header("CalculatePoints Ui")]
    [SerializeField] TextMeshProUGUI numbersOfCorrectAnswerText;
    [SerializeField] TextMeshProUGUI numbersOfFalseAnswerText;


    private void Start()
    {
        userChapter = GetComponent<UserChapters>();
        userTestting = GetComponent<UserTestting>();
    }

    public void CalculateAnswers()
    {
        CalculateCorrectAndInCorrectAnswers();
        ChangeAllDisplayButtonToCorrectColor();
        TextsHandel();


        userTestting.isViewingAnswer = true;
        // Add all correct answer to global varible
        AnswerManager.instance.SetTotalCorrectAnswer(addAmmout: thisSectionCorrectAnswer);
    }
    private void ChangeAllDisplayButtonToCorrectColor()
    {
        Transform target = userTestting.GetScrollContent();

        var scripts = target.GetComponentsInChildren<MultipleChoiceTemplate>();

        foreach (var script in scripts)
        {
            Button[] buttons = { script.buttonA, script.buttonB, script.buttonC, script.buttonD };

            foreach (var button in buttons)
            {
                var correctAnswer = button.GetComponent<ChoiceButton>().answerButton;
                if (script.correctAnswer == correctAnswer)
                {
                    button.GetComponent<Outline>().effectColor = userTestting.correctAnswerColor;
                    button.image.color = userTestting.correctAnswerColor;
                }
                else
                {
                    button.GetComponent<Outline>().effectColor = userTestting.wrongAnswerColor;
                    button.image.color = userTestting.wrongAnswerColor;
                }
            }
        }
    }

    private void TextsHandel()
    {
        // Set the display of the correct / incorrect answer
        numbersOfCorrectAnswerText.text = thisSectionCorrectAnswer.ToString();
        numbersOfFalseAnswerText.text = thisSectionWrongAnswer.ToString();

    }

    private void CalculateCorrectAndInCorrectAnswers()
    {
        int choosenChapter = GameManager.instance.choosenChapterIndex;


        // Special case
        if (choosenChapter < 0)
        {
            foreach (var chapter in userChapter.ChapterList)
            {
                foreach (var question in chapter.questions)
                {
                    if (question.AnswerIsRight == true)
                    {
                        // Check how many correct answer
                        thisSectionCorrectAnswer++;
                    }
                    else
                    {
                        thisSectionWrongAnswer++;
                    }
                }
            }

            return;
        }

        for (int i = 0; i < userChapter.ChapterList.Count; i++)
        {

            if (i == choosenChapter)
            {
                // Loop through all the question
                foreach (Question question in userChapter.ChapterList[i].questions)
                {
                    if (question.AnswerIsRight == true)
                    {
                        // Check how many correct answer
                        thisSectionCorrectAnswer++;
                    }
                    else
                    {
                        thisSectionWrongAnswer++;
                    }
                }
                break;
            }



        }
    }

    public void OnClickSubmitButton()
    {
        // Set the varible back to 0 to not get an offset
        thisSectionAlreadyAnswered = 0;
        thisSectionNotAnswered = 0;

        if (GameManager.instance.choosenChapterIndex < 0)
        {

            foreach (Chapter chapter in userChapter.ChapterList)
            {

                foreach (Question question in chapter.questions)
                {

                    // Check for question is answer ? 
                    switch (question.isChecked)
                    {
                        case true:

                            thisSectionAlreadyAnswered++;

                            break;
                        case false:

                            thisSectionNotAnswered += 1;
                            break;

                    }
                }
            }

        }
        else
        {
            for (int i = 0; i < userChapter.ChapterList.Count; i++)
            {

                if (i == GameManager.instance.choosenChapterIndex)
                {
                    // Loop through all the question
                    foreach (Question question in userChapter.ChapterList[i].questions)
                    {
                        switch (question.isChecked)
                        {
                            case true:
                                thisSectionAlreadyAnswered += 1;

                                break;
                            case false:
                                thisSectionNotAnswered += 1;
                                break;
                        }
                    }
                    break;
                }
            }
        }
        // Set the text

        alreadyAnsweredText.text = "Hoàn thành :> " + thisSectionAlreadyAnswered.ToString();
        hasNotAnsweredText.text = "Chưa hoàn thành :< " + thisSectionNotAnswered.ToString();

        // find the percentage
        int allQuestions = thisSectionAlreadyAnswered + thisSectionNotAnswered;
        float percentageOfAlreadyAnswer = (float)thisSectionAlreadyAnswered / allQuestions;

        // Set the fill image and text
        fillImage.fillAmount = percentageOfAlreadyAnswer;
        fillText.text = Mathf.RoundToInt(percentageOfAlreadyAnswer * 100).ToString() + "%";
    }
}
