using System;

[Serializable]
public class Question
{
    public MultipleChoiceQuestion multipleChoiceObject;

    public bool AnswerIsRight = false;
    public bool isChecked = false;
}

[Serializable]
public class Chapter
{
    public string chapterName;
    public Question[] questions;
}