using System;

[Serializable]
public class Question
{
    public string questionName = "question";
    public MultipleChoiceQuestion multipleChoiceObject;

    public bool AnswerIsRight = false;
}

[Serializable]
public class Chaper
{
    public string chaperName;
    public Question[] questions;
}