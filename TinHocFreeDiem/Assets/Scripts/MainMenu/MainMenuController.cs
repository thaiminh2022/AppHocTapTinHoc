using UnityEngine;
using UnityEngine.UI;
public class MainMenuController : MonoBehaviour
{
    [SerializeField] int numberOfChapters;

    [SerializeField] string chaptersPrefix;
    [SerializeField] string screenToSwitchTo;
    [SerializeField] private Transform choosingChapterContent;
    [SerializeField] private GameObject choosingChapterTemplate;

    [SerializeField] private Color[] randomColors;
    [SerializeField] private Image[] backgroundImages;


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
            GameObject go = Instantiate(choosingChapterTemplate, Vector3.zero, Quaternion.identity);

            go.transform.SetParent(choosingChapterContent, false);

            ChapterChoosingTemplate template = go.GetComponent<ChapterChoosingTemplate>();

            // Choose randdom color for the represent image
            Color choosenColor = ChooseRandomColor();


            while (choosenColor == lastColor)
            {
                choosenColor = ChooseRandomColor();
            }

            template.choossingTemplateButton.GetComponent<Image>().color = choosenColor;
            lastColor = choosenColor;

            template.thisIndex = i;

            template.choosingTemplateText.text = $"{chaptersPrefix} {i + 1}";
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
}
