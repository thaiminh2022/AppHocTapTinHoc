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
    [SerializeField] TextMeshProUGUI totalDoingTimeText;

    [Header("Streak Ui")]
    [SerializeField] TextMeshProUGUI currentStreakText;
    [SerializeField] TextMeshProUGUI newStreakText;
    [SerializeField] GameObject newStreakGameObject;

    private void Start()
    {
        userChapter = GetComponent<UserChapters>();
        userTestting = GetComponent<UserTestting>();
    }

    public void CalculateAnswers()
    {
        CalculateCorrectAndInCorrectAnswers();
        TextsHandel();
        StreakHandel();

        // Add all correct answer to global varible
        AnswerManager.instance.SetTotalCorrectAnswer(addAmmout: thisSectionCorrectAnswer);
    }

    private void StreakHandel()
    {
        // Get the current streak for more easy use
        int currentStreak = AnswerManager.instance.GetCurrentStreak();

        // Check if this secition correct answers > current streak
        if (thisSectionCorrectAnswer > currentStreak)
        {
            // Set the new streak
            AnswerManager.instance.SetCurrentStreak(newStreak: currentStreak);

            // Set the new streak gameobject to true
            newStreakGameObject.SetActive(true);

            // Display the new streak
            newStreakText.text = thisSectionCorrectAnswer.ToString();
        }
        else
        {
            newStreakGameObject.SetActive(false);
        }
    }

    private void TextsHandel()
    {
        // Set the display of the correct / incorrect answer
        numbersOfCorrectAnswerText.text = numbersOfCorrectAnswerText.text + " " + thisSectionCorrectAnswer.ToString();
        numbersOfFalseAnswerText.text = numbersOfFalseAnswerText.text + " " + thisSectionWrongAnswer.ToString();
        // Set the total time text display
        totalDoingTimeText.text = totalDoingTimeText.text + " " + userTestting.GetPlayerDoingTestTimeString();

        // Display the current streak
        currentStreakText.text = AnswerManager.instance.GetCurrentStreak().ToString();
    }

    private void CalculateCorrectAndInCorrectAnswers()
    {
        // Loop through all the chapter
        foreach (Chapter chapter in userChapter.GetChapterList())
        {
            // Loop through all the question
            foreach (Question question in chapter.questions)
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
    }

    public void OnClickSubmitButton()
    {
        // Set the varible back to 0 to not get an offset
        thisSectionAlreadyAnswered = 0;
        thisSectionNotAnswered = 0;

        foreach (Chapter chapter in userChapter.GetChapterList())
        {
            foreach (Question question in chapter.questions)
            {
                // Check for question is answer ? 
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
