using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnswerManager : MonoBehaviour
{
    public static AnswerManager instance;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);
    }

    [SerializeField] private int totalCorrectAnswer = 0;
    [SerializeField] private int currentStreak;

    public void SetTotalCorrectAnswer(int addAmmout)
    {
        totalCorrectAnswer += addAmmout;
    }
    public void SetTotalCorrectAnswer(float setAmmout)
    {
        totalCorrectAnswer = Mathf.RoundToInt(setAmmout);
    }


    public int GetCurrentStreak()
    {
        return currentStreak;
    }
    public void SetCurrentStreak(int newStreak)
    {
        currentStreak = newStreak;
    }
}
