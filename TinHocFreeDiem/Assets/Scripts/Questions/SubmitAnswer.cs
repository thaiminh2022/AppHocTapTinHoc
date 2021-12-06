using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SubmitAnswer : MonoBehaviour
{
    private UserChapters userChapter;

    [Header("Counters")]
    [SerializeField] private int thisSectionCorrectAnswer = 0;
    [SerializeField] private int thisSectionAlreadyAnswered = 0;
    [SerializeField] private int thisSectionNotAnswered = 0;

    [Header("Useful UI")]
    [SerializeField] TextMeshProUGUI alreadyAnsweredText;
    [SerializeField] TextMeshProUGUI hasNotAnsweredText;
    [SerializeField] Image fillImage;
    [SerializeField] TextMeshProUGUI fillText;


    private void Start()
    {
        userChapter = GetComponent<UserChapters>();
    }

    public void OnSubmitAnswer()
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
            }
        }

        // Log the ammout of correct answer
        Debug.Log(thisSectionCorrectAnswer);

        // Add all correct answer to global varible
        AnswerManager.instance.ChangeToTalCorrectAnswer(addAmmout: thisSectionCorrectAnswer);

        //! ONLY HERE FOR DEBUG
        UiUtilities.instance.RestartScreen();
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
