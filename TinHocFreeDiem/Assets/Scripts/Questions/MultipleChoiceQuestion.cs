using UnityEngine;

[CreateAssetMenu(fileName = "MultipleChoiceQuestion", menuName = "Questions/MultipleChoiceQuestion", order = 0)]
public class MultipleChoiceQuestion : ScriptableObject
{
    [TextArea(0, 4)]
    public string question;

    [TextArea(0, 2)]
    public string answerA;

    [TextArea(0, 2)]
    public string answerB;

    [TextArea(0, 2)]
    public string answerC;

    [TextArea(0, 2)]
    public string answerD;
    public CorrectAnswer correctAnswer;
}