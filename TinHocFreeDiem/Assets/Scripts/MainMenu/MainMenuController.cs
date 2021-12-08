using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class MainMenuController : MonoBehaviour
{
    [SerializeField] int numberOfChapters;

    [Header("Chapter Objects")]
    [SerializeField] string chaptersPrefix;
    [SerializeField] string screenToSwitchTo;
    [SerializeField] private Transform choosingChapterContent;
    [SerializeField] private GameObject choosingChapterTemplate;

    [Tooltip("The ammout of this should be = to numbers of chapter")]
    [SerializeField] private Sprite[] spritesForRepresentImage;

    [Header("Color")]
    [SerializeField] private Color[] randomColors;
    [SerializeField] private Image[] backgroundImages;

    [Header("Moving")]
    [SerializeField] float moveTime = .5f;
    [SerializeField] float moveDelay = .5f;
    [SerializeField] float moveLength = 3000f;
    [SerializeField] LeanTweenType leanTweenType;
    [SerializeField] MoveDirEntry moveDirEntry;
    [SerializeField] Transform specialTestButton;



    private void Start()
    {
        AddChapters();
        SetRandomColor();
    }


    private void SetRandomColor()
    {
        Color choosenColor = ChooseRandomColor();

        foreach (var backgroundImage in backgroundImages)
        {
            backgroundImage.color = choosenColor;
        }
    }
    private Color ChooseRandomColor()
    {



        int index = Random.Range(0, randomColors.Length);

        Color choosenColor = randomColors[index];
        return choosenColor;
    }

    private void AddChapters()
    {
        Color lastColor = Color.white;

        for (int i = 0; i < numberOfChapters; i++)
        {
            // Instantieate tha gameobject
            GameObject go = Instantiate(choosingChapterTemplate, Vector3.zero, Quaternion.identity);

            // Set the transform to container
            go.transform.SetParent(choosingChapterContent, false);

            // Get the template gameobject
            ChapterChoosingTemplate template = go.GetComponent<ChapterChoosingTemplate>();

            // Choose randdom color for the represent image
            Color choosenColor = ChooseRandomColor();

            // Choose a none repeat color 2 times in a row
            while (choosenColor == lastColor)
            {
                choosenColor = ChooseRandomColor();
            }

            // Set the colors
            template.choossingTemplateButton.GetComponent<Image>().color = choosenColor;
            lastColor = choosenColor;

            // Get the index
            template.thisIndex = i;

            // Set the text
            template.choosingTemplateText.text = $"{chaptersPrefix} {i + 1}";

            // Set the represent image
            if (spritesForRepresentImage[i] != null)
            {
                template.chossingTemplateRepresentImage.sprite = spritesForRepresentImage[i];
            }


            // Set the oclick event
            template.choossingTemplateButton.onClick.AddListener(() =>
            {
                ChossenChapter(index: template.thisIndex);
                UiUtilities.instance.SwitchScene(screenToSwitchTo);
            }
            );

        }
    }

    public void ChossenChapter(int index)
    {
        GameManager.instance.ChoosenChapterIndex = index;
    }
    public void SpecialChapter()
    {
        GameManager.instance.ChoosenChapterIndex = -1;
        UiUtilities.instance.SwitchScene(screenToSwitchTo);
    }


    public void SpecialChapterMove()
    {
        LeanTweenManagers.instance.AddMovingEntryDelayOncall(specialTestButton, moveDirEntry, moveTime, moveLength, moveDelay);
    }
}
