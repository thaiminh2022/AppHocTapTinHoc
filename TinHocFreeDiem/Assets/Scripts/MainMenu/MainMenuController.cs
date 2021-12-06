using UnityEngine;

public class MainMenuController : MonoBehaviour
{
    [SerializeField] int numberOfChapters;

    [SerializeField] string chaptersPrefix;
    [SerializeField] string screenToSwitchTo;
    [SerializeField] private Transform choosingChapterContent;
    [SerializeField] private GameObject choosingChapterTemplate;

    private void Start()
    {
        AddChapters();
    }

    private void AddChapters()
    {
        for (int i = 0; i < numberOfChapters; i++)
        {
            GameObject go = Instantiate(choosingChapterTemplate, Vector3.zero, Quaternion.identity);

            go.transform.SetParent(choosingChapterContent, false);

            ChapterChoosingTemplate template = go.GetComponent<ChapterChoosingTemplate>();

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
