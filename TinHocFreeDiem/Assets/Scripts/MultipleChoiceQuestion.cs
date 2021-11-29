using UnityEngine;

[CreateAssetMenu(fileName = "MultipleChoiceQuestion", menuName = "Questions/MultipleChoiceQuestion", order = 0)]
public class MultipleChoiceQuestion : ScriptableObject
{
    public string question;
    public string[] answer;
    public int correctIndex;
}