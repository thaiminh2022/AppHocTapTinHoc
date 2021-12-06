using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;

public class UserChapters : MonoBehaviour
{
    [Header("Chapters")]
    [SerializeField] private List<Chapter> chapers;

    [Header("Events")]
    [SerializeField] UnityEvent OnPlayerFinishedAnswer;
    [SerializeField] UnityEvent OnPlayerAnswerWrong;
    [SerializeField] UnityEvent OnPlayerAnswerRight;


    private void Start()
    {
        if (chapers == null)
            chapers = new List<Chapter>();
    }

    public void OnPlayerFinished()
    {
        OnPlayerFinishedAnswer?.Invoke();
    }
    public void OnPlayerWrong()
    {
        OnPlayerAnswerWrong?.Invoke();
    }
    public void OnPlayerRight()
    {
        OnPlayerAnswerRight?.Invoke();
    }

    public List<Chapter> GetChapterList()
    {
        return chapers;
    }

}