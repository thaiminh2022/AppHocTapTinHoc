using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(UserChapters))]
public class CreateUserChapters : MonoBehaviour
{
    // Create a tempChapter
    public tempChapter[] tempChapters;


    public string pathToScriptableFolders = "Assets/QuesitionAssest";
    // Get refrence to userhapter
    private UserChapters userChapters;

    private void Awake()
    {
        // Do this as soon as posible to avoid getting errors
        TryGetComponent<UserChapters>(out userChapters);
        AddQuestionToChapter();
    }
    public void AddQuestionToChapter()
    {
        // ! This is a list add to the chapter List
        List<Chapter> returnChapter = new List<Chapter>();

        // Loop through all the temp chapters
        for (int i = 0; i < tempChapters.Length; i++)
        {
            // Create a list of questions
            List<Question> questionsToAddPerChapter = new List<Question>();

            // Loop through all the questions in tempMultipleChoicequestions
            for (int u = 0; u < tempChapters[i].tempMultipleChoiceQuestions.Count; u++)
            {
                // Create a new question
                Question newQuestion = new Question
                {
                    multipleChoiceObject = tempChapters[i].tempMultipleChoiceQuestions[u],
                    isChecked = false,
                    AnswerIsRight = false,
                };

                // Add that to the question list
                questionsToAddPerChapter.Add(newQuestion);
            }

            // Create a new chapter
            Chapter newChapter = new Chapter
            {
                chapterName = tempChapters[i].chapterName,
                questions = questionsToAddPerChapter.ToArray(),
            };

            // add that to the chapter list
            returnChapter.Add(newChapter);
        }

        // Set the user chapter
        userChapters.ChapterList = returnChapter;
    }

    public void ResetAssetInArray()
    {
        List<tempChapter> temptempChapters = new List<tempChapter>();

        foreach (var item in tempChapters)
        {
            temptempChapters.Add(item);
        }

        temptempChapters.Clear();

        tempChapters = temptempChapters.ToArray();
    }
}

// THIS IS A TEMPCHAPTER, DO NOT TOUCH
[System.Serializable]
public class tempChapter
{
    public string chapterName;
    public List<MultipleChoiceQuestion> tempMultipleChoiceQuestions;
}