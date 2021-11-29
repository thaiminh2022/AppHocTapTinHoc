using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;

public class UserPlayer : MonoBehaviour
{
    [Header("Chapters")]
    [SerializeField] private List<Chaper> chapers;

    [Header("Events")]
    [SerializeField] UnityEvent OnPlayerFinishedAnswer;
    [SerializeField] UnityEvent OnPlayerAnswerWrong;
    [SerializeField] UnityEvent OnPlayerAnswerRight;


    private void Start()
    {
        if (chapers == null)
            chapers = new List<Chaper>();
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
}