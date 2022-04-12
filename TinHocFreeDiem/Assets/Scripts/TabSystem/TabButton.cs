using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;

[RequireComponent(typeof(Image))]
public class TabButton : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler, IPointerExitHandler
{
    public TabGroup tabGroup;

    public Image backgroundImage;

    [Header("Selected Images")]
    public bool useSelectImages = false;
    public Sprite selectedSprite;
    private Sprite defaultSprite;
    public Image representImage;


    [Header("Events")]
    public UnityEvent OnTabSelected;
    public UnityEvent OnTabDeselected;

    private void Start()
    {
        backgroundImage = GetComponent<Image>();

        defaultSprite = representImage.sprite;
        tabGroup.Subscribe(this);

        //subscribe to two events
        OnTabSelected.AddListener(SwapImageWhenSelected);
        OnTabDeselected.AddListener(SwapImageWhenDeselected);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        tabGroup.OnTabSelected(this);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        tabGroup.OnTabEnter(this);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        tabGroup.OnTabExit(this);
    }


    public void Select()
    {
        OnTabSelected?.Invoke();
    }
    public void Deselect()
    {
        OnTabDeselected?.Invoke();
    }

    private void SwapImageWhenSelected()
    {
        //Swap the image when selected
        if (useSelectImages)
        {
            representImage.sprite = selectedSprite;
        }
    }
    private void SwapImageWhenDeselected()
    {
        //Swap the image when deselected
        if (useSelectImages)
        {
            representImage.sprite = defaultSprite;
        }
    }
}
