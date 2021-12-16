using UnityEngine;
using System;
using TMPro;

public class Welcome : MonoBehaviour
{
    public TimeMessage[] timeMessages;
    public string currentMessage;


    [SerializeField]
    private TextMeshProUGUI welcomeText;

    TimeSpan GetDateTime()
    {
        TimeSpan t = DateTime.Now.TimeOfDay;

        return t;
    }
    private void Start()
    {
        WelcomeLogic();
    }
    public void WelcomeLogic()
    {
        var t = GetDateTime();

        for (int i = 0; i < timeMessages.Length; i++)
        {
            var FisrtIndexSpan = new TimeSpan(timeMessages[0].hours, timeMessages[0].minutes, timeMessages[0].second);
            var LastIndexSpan = new TimeSpan(timeMessages[timeMessages.Length - 1].hours, timeMessages[timeMessages.Length - 1].minutes, timeMessages[timeMessages.Length - 1].second);

            if (t <= FisrtIndexSpan)
            {
                currentMessage = timeMessages[0].message;
                break;
            }
            else if (t >= LastIndexSpan)
            {
                currentMessage = timeMessages[timeMessages.Length - 1].message;
                break;
            }
            else if (i > 0 && i < timeMessages.Length - 1)
            {
                // Input some logic here please
                var lastTimeSpan = new TimeSpan(timeMessages[i - 1].hours, timeMessages[i - 1].minutes, timeMessages[i - 1].second);
                var nextTimeSpan = new TimeSpan(timeMessages[i + 1].hours, timeMessages[i + 1].minutes, timeMessages[i + 1].second);
                var currentTimeSpan = new TimeSpan(timeMessages[i].hours, timeMessages[i].minutes, timeMessages[i].second);

                // Check all the posibility of a timespan
                if (t == currentTimeSpan)
                {
                    currentMessage = timeMessages[i].message;
                }
                else if (t > lastTimeSpan && t < currentTimeSpan)
                {
                    currentMessage = timeMessages[i - 1].message;
                }
                else if (t > currentTimeSpan && t < nextTimeSpan)
                {
                    currentMessage = timeMessages[i].message;
                }

            }

        }

        welcomeText.text = currentMessage;

    }

}

[Serializable]
public class TimeMessage
{
    public string message;
    public int hours;
    public int minutes;
    public int second;
}
