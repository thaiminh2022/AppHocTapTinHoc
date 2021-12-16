using UnityEngine.UI;
using TMPro;
using UnityEngine;
using System;

public class LoadingSceneBeutyfied : MonoBehaviour
{
    [Header("Background")]
    public Image backgroundImage;
    public int dayDisplayHours, noonDisplayHour, nightDisplayHour;

    public GameObject dayProp, noonProp, nightProp;
    public Sprite dayBackground, noonBackground, nightBackground;
    public TimeDesination timeDesination;

    [Header("Loading")]
    public Image loadingBar;
    public Color morningColor, noonColor, nightColor;

    [Header("Background props")]
    public Transform[] clouds;

    public float cloudsMoveTimeMin;
    public float cloudsMoveTimeMax;

    public Vector3 cloudOffSetRight;
    public Vector3 cloudOffSetLeft;

    public LeanTweenType cloudLeenType;
    public LeanTweenType cloudLoopType;

    [Space()]
    public Image expocationMark;
    public float markLeanTime;
    public LeanTweenType markLeanType;

    [Header("Loading progress bar")]
    public Slider loadingProgressBar;

    private void Start()
    {
        DisplayProp();
        SuttleTouch();
    }
    private void Update()
    {
        LoadProgessBar();
    }

    void DisplayProp()
    {
        TimeSpan t = DateTime.Now.TimeOfDay;

        float totalHoursNow = (float)t.TotalHours;

        if (totalHoursNow >= dayDisplayHours && totalHoursNow < noonDisplayHour)
        {
            dayProp.SetActive(true);
            noonProp.SetActive(false);
            nightProp.SetActive(false);

            backgroundImage.sprite = dayBackground;

            if (loadingBar != null)
                loadingBar.color = morningColor;
        }
        else if (totalHoursNow >= noonDisplayHour && totalHoursNow < nightDisplayHour)
        {
            dayProp.SetActive(false);
            noonProp.SetActive(true);
            nightProp.SetActive(false);

            backgroundImage.sprite = noonBackground;

            if (loadingBar != null)
                loadingBar.color = noonColor;
        }
        else if (totalHoursNow >= nightDisplayHour || totalHoursNow < 7)
        {
            dayProp.SetActive(false);
            noonProp.SetActive(false);
            nightProp.SetActive(true);

            backgroundImage.sprite = nightBackground;

            if (loadingBar != null)
                loadingBar.color = nightColor;
        }

    }

    void SuttleTouch()
    {
        foreach (var cloud in clouds)
        {
            Vector3 cloudCurrentPosition = cloud.localPosition;

            Vector3 startPostition = cloudCurrentPosition + cloudOffSetLeft;
            Vector3 endPosition = cloudCurrentPosition + cloudOffSetRight;

            cloud.localPosition = startPostition;

            float randMoveTime = UnityEngine.Random.Range(cloudsMoveTimeMin, cloudsMoveTimeMax);

            cloud.LeanMoveLocal(endPosition, randMoveTime).setEase(cloudLeenType).setLoopType(cloudLoopType);


        }

        if (expocationMark != null)
            LeanTween.alphaCanvas(expocationMark.GetComponent<CanvasGroup>(), 0, markLeanTime).setEase(markLeanType).setLoopPingPong();
    }

    void LoadProgessBar()
    {
        if (loadingProgressBar != null)
            loadingProgressBar.value = Loader.GetLoadingProgress();
    }
}
