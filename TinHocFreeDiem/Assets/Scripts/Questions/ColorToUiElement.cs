using UnityEngine.UI;
using UnityEngine;

public class ColorToUiElement : MonoBehaviour
{
    private UserChapters userChapters;

    [SerializeField] public Image[] randomColorToImage;

    private void Start()
    {
        userChapters = GetComponent<UserChapters>();
        ChangeColorToImge();
    }
    private void ChangeColorToImge()
    {
        foreach (var image in randomColorToImage)
        {
            image.color = userChapters.ChooseRandomColor();
        }
    }
}
